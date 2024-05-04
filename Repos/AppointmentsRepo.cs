using DotNetData_Lb3.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;

namespace DotNetData_Lb3.Repos
{
    public class AppointmentsRepo
    {
        private readonly IMongoCollection<Appointment> collection;
        private readonly IMongoCollection<Doctor> doctorsCollection;
        private readonly IMongoCollection<Patient> patientsCollection;

        public AppointmentsRepo()
        {
            var connString = Environment.GetEnvironmentVariable("Mongo");
            collection = new MongoClient(connString)
                .GetDatabase("hospital")
                .GetCollection<Appointment>("appointments");

            doctorsCollection = new MongoClient(connString)
                .GetDatabase("hospital")
                .GetCollection<Doctor>("doctors");

            patientsCollection = new MongoClient(connString)
                .GetDatabase("hospital")
                .GetCollection<Patient>("patients");
        }

        public async Task<List<AppointmentFull>> GetAppointments(
            List<string>? appointmentTypes = null,
            List<ObjectId>? doctors = null, List<ObjectId>? patients = null)
        {
            var filters = new List<FilterDefinition<Appointment>>();

            if (appointmentTypes is not null && appointmentTypes.Any())
                filters.Add(Builders<Appointment>.Filter.In(a => a.AppointmentType, appointmentTypes));

            if (doctors is not null && doctors.Any())
                filters.Add(Builders<Appointment>.Filter.In(a => a.DoctorId, doctors));

            if (patients is not null && patients.Any())
                filters.Add(Builders<Appointment>.Filter.In(a => a.PatientId, patients));

            filters.Clear();


            BsonDocument pipelineStage = new BsonDocument{
                {
                    "$lookup", new BsonDocument{
                        { "from", "doctors" },
                        { "localField", "doctor_id" },
                        { "foreignField", "_id" },
                        { "as", "doctor" }
                    }
                }
            };
            BsonDocument pipelineStage2 = new BsonDocument{
                { "$unwind", "$doctor" }
            };
            BsonDocument pipelineStage3 = new BsonDocument{
                {
                    "$lookup", new BsonDocument{
                        { "from", "patients" },
                        { "localField", "patient_id" },
                        { "foreignField", "_id" },
                        { "as", "patient" }
                    }
                }
            };
            BsonDocument pipelineStage4 = new BsonDocument{
                { "$unwind", "$patient" }
            };

            var pipeline = new BsonDocument[] { pipelineStage, pipelineStage2, pipelineStage3, pipelineStage4 };

            List<BsonDocument> res = await (await collection.AggregateAsync<BsonDocument>(pipeline)).ToListAsync();
            

            List<AppointmentFull> resultList = res.Select(doc => new AppointmentFull
            {
                AppointmentId = doc["_id"].AsObjectId,
                AppointmentDate = doc["appointment_date"].AsDateTime,
                AppointmentPrice = doc["appointment_price"].AsDecimal,
                AppointmentType = doc["appointment_type"].AsString,
                Doctor = new Doctor()
                {
                    DoctorId = doc["doctor"]["_id"].AsObjectId,
                    FirstName = doc["doctor"]["first_name"].AsString,
                    LastName = doc["doctor"]["last_name"].AsString,
                    PhoneNumber = doc["doctor"]["phone_number"].AsString,
                    Speciality = doc["doctor"]["speciality"].AsString
                },
                Patient = new Patient()
                {
                    PatientId = doc["patient"]["_id"].AsObjectId,
                    FirstName = doc["patient"]["first_name"].AsString,
                    LastName = doc["patient"]["last_name"].AsString,
                    PhoneNumber = doc["patient"]["phone_number"].AsString,
                    Age = doc["patient"]["age"].AsInt32
                }
            }).ToList();


            return resultList;

            //var combinedFilter = filters.Any() ? Builders<Appointment>.Filter.And(filters) : Builders<Appointment>.Filter.Empty;

            //return await (await collection.FindAsync(combinedFilter)).ToListAsync();
        }

        public async Task<List<string>> GetAppointmentsTypes()
        {
            return await (await collection.DistinctAsync(a => a.AppointmentType, new BsonDocument())).ToListAsync();
        }

        public async Task InsertNewAppointment(Appointment a)
        {
            try
            {
                await collection.InsertOneAsync(a);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAppointment(string id)
        {
            await collection.DeleteOneAsync(a => a.AppointmentId == new ObjectId(id));
        }

        public async Task UpdateAppointment(string id, Appointment appointment)
        {
            var update = Builders<Appointment>.Update
                .Set(a => a.AppointmentType, appointment.AppointmentType)
                .Set(a => a.AppointmentPrice, appointment.AppointmentPrice)
                .Set(a => a.DoctorId, appointment.DoctorId)
                .Set(a => a.PatientId, appointment.PatientId);

            await collection.UpdateOneAsync(a => a.AppointmentId == new ObjectId(id), update);
        }
    }
}
