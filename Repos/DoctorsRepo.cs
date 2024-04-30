using DotNetData_Lb3.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotNetData_Lb3.Repos
{
    public class DoctorsRepo
    {
        private readonly IMongoCollection<Doctor> collection;

        public DoctorsRepo()
        {
            var connString = Environment.GetEnvironmentVariable("Mongo");
            collection = new MongoClient(connString)
                .GetDatabase("hospital")
                .GetCollection<Doctor>("doctors");
        }

        public async Task<List<Doctor>> GetDoctors(List<string>? specialties = null, List<string>? daysOfWeek = null)
        {
            var filters = new List<FilterDefinition<Doctor>>();

            if (specialties is not null && specialties.Any())
                filters.Add(Builders<Doctor>.Filter.In(d => d.Speciality, specialties));

            if (daysOfWeek is not null && daysOfWeek.Any())
                filters.Add(Builders<Doctor>.Filter.AnyIn(d => d.Schedule.Select(schedule => schedule.DayOfWeek), daysOfWeek));


            var combinedFilter = filters.Any() ? Builders<Doctor>.Filter.And(filters) : Builders<Doctor>.Filter.Empty;

            return await (await collection.FindAsync(combinedFilter)).ToListAsync();
        }

        public async Task<Doctor> GetDoctorById(string id)
        {
            return await (await collection.FindAsync(d => d.DoctorId == new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task<List<string>> GetDoctorsSpecialties()
        {
            return await (await collection.DistinctAsync(d => d.Speciality, new BsonDocument())).ToListAsync();
        }

        public async Task InsertNewDoctor(Doctor d)
        {
            try
            {
                await collection.InsertOneAsync(d);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteDoctor(string phoneNumber)
        {
            await collection.DeleteOneAsync(d => d.PhoneNumber == phoneNumber);
        }

        public async Task UpdateDoctorSchedule(string phoneNumber, Schedule s)
        {
            s.StartTime = s.StartTime.AddHours(3);
            s.EndTime = s.EndTime.AddHours(3);
            var update = Builders<Doctor>.Update.Push(d => d.Schedule, s);
            await collection.UpdateOneAsync(d => d.PhoneNumber == phoneNumber, update);
        }

        public async Task UpdateDoctor(string id, Doctor doctor)
        {
            var update = Builders<Doctor>.Update
                .Set(d => d.FirstName, doctor.FirstName)
                .Set(d => d.LastName, doctor.LastName)
                .Set(d => d.PhoneNumber, doctor.PhoneNumber)
                .Set(d => d.Speciality, doctor.Speciality);

            await collection.UpdateOneAsync(d => d.DoctorId == new ObjectId(id), update);
        }

        public async Task<List<Doctor>> SearchDoctorsByName(string name)
        {
            if(string.IsNullOrEmpty(name))
                return await GetDoctors();

            var filter = Builders<Doctor>.Filter.Or(
            Builders<Doctor>.Filter.Regex(d => d.FirstName, new BsonRegularExpression(name, "i")),
            Builders<Doctor>.Filter.Regex(d => d.LastName, new BsonRegularExpression(name, "i"))
            );

            return await collection.Find(filter).ToListAsync();

            //var filter = Builders<Doctor>.Filter.Regex(d => d.FirstName , new BsonRegularExpression(name, "i"));

            //return await collection.Find(filter).ToListAsync();
        }

        public async Task DeleteDoctorSchedule(string phoneNumber, Schedule scheduleToDelete)
        {
            var update = Builders<Doctor>.Update.PullFilter(d => d.Schedule, s => s.DayOfWeek == scheduleToDelete.DayOfWeek);
            await collection.FindOneAndUpdateAsync(new ExpressionFilterDefinition<Doctor>(d => d.PhoneNumber == phoneNumber), update);

        }

        public async void CreateIndexes()
        {
            var indexOptions = new CreateIndexOptions()
            {
                Unique = true
            };
            var indexKeys = Builders<Doctor>.IndexKeys.Ascending(d => d.PhoneNumber);
            var indexModel = new CreateIndexModel<Doctor>(indexKeys, indexOptions);
            await collection.Indexes.CreateOneAsync(indexModel);
        }
    }
}
