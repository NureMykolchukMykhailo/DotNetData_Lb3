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

        //public async Task<bool> InsertNewDoctor(Doctor d)
        //{
        //    await context.Doctors.AddAsync(d);
        //    return await context.SaveChangesAsync() > 0;
        //}
    }
}
