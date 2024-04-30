using DotNetData_Lb3.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotNetData_Lb3.Repos
{
    public class PatientsRepo
    {
        private readonly IMongoCollection<Patient> collection;

        public PatientsRepo()
        {
            var connString = Environment.GetEnvironmentVariable("Mongo");
            collection = new MongoClient(connString)
                .GetDatabase("hospital")
                .GetCollection<Patient>("patients");
        }

        public async Task<List<Patient>> GetPatients(int? min = null, int? max = null)
        {
            if(min is null && max is null)
                return await (await collection.FindAsync(x => true)).ToListAsync();

            return await (await collection.FindAsync(
                Builders<Patient>.Filter.Where(p => p.Age <=max && p.Age >= min))
                ).ToListAsync();
        }

        public async Task InsertNewPatient(Patient p)
        {
            try
            {
                await collection.InsertOneAsync(p);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeletePatient(string phoneNumber)
        {
            await collection.DeleteOneAsync(p => p.PhoneNumber == phoneNumber);
        }

        public async Task<Patient> GetPatientById(string id)
        {
            return await (await collection.FindAsync(p => p.PatientId == new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task UpdatePatient(string id, Patient patient)
        {
            var update = Builders<Patient>.Update
                .Set(p => p.FirstName, patient.FirstName)
                .Set(p => p.LastName, patient.LastName)
                .Set(p => p.PhoneNumber, patient.PhoneNumber)
                .Set(p => p.Age, patient.Age);

            await collection.UpdateOneAsync(d => d.PatientId == new ObjectId(id), update);
        }

        public async Task<List<Patient>> SearchPatientsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return await GetPatients();

            var filter = Builders<Patient>.Filter.Or(
            Builders<Patient>.Filter.Regex(p => p.FirstName, new BsonRegularExpression(name, "i")),
            Builders<Patient>.Filter.Regex(p => p.LastName, new BsonRegularExpression(name, "i"))
            );

            return await collection.Find(filter).ToListAsync();
        }

        public async void CreateIndexes()
        {
            var indexOptions = new CreateIndexOptions()
            {
                Unique = true
            };
            var indexKeys = Builders<Patient>.IndexKeys.Ascending(p => p.PhoneNumber);
            var indexModel = new CreateIndexModel<Patient>(indexKeys, indexOptions);
            await collection.Indexes.CreateOneAsync(indexModel);
        }

        public (int, int) GetPatientAgeBoundaries()
        {
            int min = Convert.ToInt32(collection.Aggregate().Group(x => 1, gr => new { MinVal = gr.Min(f => f.Age) }).First().MinVal);
            int max = Convert.ToInt32(collection.Aggregate().Group(x => 1, gr => new { MaxVal = gr.Max(f => f.Age) }).First().MaxVal);

            return (min, max);
        }
    }
}
