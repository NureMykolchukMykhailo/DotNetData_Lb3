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
            var update = Builders<Doctor>.Update.Push(d => d.Schedule, s);
            await collection.UpdateOneAsync(d => d.PhoneNumber == phoneNumber, update);
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
