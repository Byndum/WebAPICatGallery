using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace WebAPICatGallery.Repositories
{
    interface ICatRepository
    {
        Task Add(Cat cat);
        Task<Cat> Get(string id);
    }

    public class CatRepository : ICatRepository
    {
        private IMongoCollection<Cat> _cats;
        public CatRepository(IOptions<CatDatabase> catDatabase)
        {
            
            var mc = new MongoClient(catDatabase.Value.ConnectionString);
            var mongoDB = mc.GetDatabase(catDatabase.Value.DatabaseName);
            _cats = mongoDB.GetCollection<Cat>(catDatabase.Value.CollectionName);
        }

        public async Task Add(Cat cat)
        {
            await _cats.InsertOneAsync(cat);
        }

        public async Task<Cat> Get(string id)
        {
            return await _cats.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
