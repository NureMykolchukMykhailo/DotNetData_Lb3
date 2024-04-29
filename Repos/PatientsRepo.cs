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

        public async Task<List<Patient>> GetPatients()
        {
            return await (await collection.FindAsync(x => true)).ToListAsync();
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

        public async Task<(int, int)> GetPatientAgeBoundaries()
        {
            return new List<int>()
            {
                await collection.
            };
        }
    }
}
