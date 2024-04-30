using DotNetData_Lb3.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotNetData_Lb3.Repos
{
    public class AppointmentsRepo
    {
        private readonly IMongoCollection<Appointment> collection;

        public AppointmentsRepo()
        {
            var connString = Environment.GetEnvironmentVariable("Mongo");
            collection = new MongoClient(connString)
                .GetDatabase("hospital")
                .GetCollection<Appointment>("appointments");
        }

        public async Task<List<Appointment>> GetAppointments(
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

            // это надо будет потом убрать, это для теста
            filters.Clear();

            PipelineDefinition<Appointment, BsonDocument> pipeline = new[]
{
    // Сопоставление доктора
    PipelineStageDefinitionBuilder.Lookup<Appointment, Doctor, BsonDocument>(
        foreignCollectionName: "doctors",
        localField: a => a.DoctorId,
        foreignField: d => d.DoctorId,
        @as: "doctor"
    ),
    PipelineStageDefinitionBuilder.Unwind<BsonDocument>("doctor"), // Развертывание доктора, если доктор может быть в нескольких посещениях

    // Сопоставление пациента
    PipelineStageDefinitionBuilder.Lookup<BsonDocument, Patient, BsonDocument>(
        foreignCollectionName: "patients",
        localField: "doctor.PatientId",
        foreignField: p => p.PatientId,
        @as: "patient"
    ),
    PipelineStageDefinitionBuilder.Unwind<BsonDocument>("patient"), // Развертывание пациента, если пациент может быть в нескольких посещениях

    // Опционально: Операции проекции для форматирования результатов
    PipelineStageDefinitionBuilder.Project<BsonDocument, BsonDocument>(
        Builders<BsonDocument>.Projection.Exclude("doctor_id", "patient_id")
    )
};

            var result = await collection.AggregateAsync<BsonDocument>(pipeline).ToListAsync();

            var combinedFilter = filters.Any() ? Builders<Appointment>.Filter.And(filters) : Builders<Appointment>.Filter.Empty;

            return await (await collection.FindAsync(combinedFilter)).ToListAsync();
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
