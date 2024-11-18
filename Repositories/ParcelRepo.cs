using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebAPIParcelTracking.Repositories
{
    interface IParcelRepo
    {
        Task Add(Parcel parcel);
        Task<Parcel> Get(string id);
    }
    public class ParcelRepo : IParcelRepo
    {
        private IMongoCollection<Parcel> _parcels;
        public ParcelRepo(IOptions<ParcelDatabase> parcelDatabase)
        {
            var mc = new MongoClient(parcelDatabase.Value.ConnectionString);
            var mongoDB = mc.GetDatabase(parcelDatabase.Value.DatabaseName);
            _parcels = mongoDB.GetCollection<Parcel>(parcelDatabase.Value.CollectionName);
        }
        public async Task Add(Parcel parcel)
        {
            await _parcels.InsertOneAsync(parcel);
        }

        public async Task<Parcel> Get(string id)
        {
            return await _parcels.Find(f => f.Id == id).FirstOrDefaultAsync();
        }
    }
}
