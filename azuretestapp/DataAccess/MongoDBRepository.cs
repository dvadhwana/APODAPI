using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using azuretestapp.Model;

namespace azuretestapp.DataAccess
{
    public class MongoDBRepository
    {
        private readonly ILogger<MongoDBRepository> _logger;
        private IMongoDatabase _database;
        public MongoDBRepository(ILogger<MongoDBRepository> logger)
        {
            _logger = logger;
            GetDatabase();
        }
        public virtual void GetDatabase()
        {
            var client = new MongoClient("mongodb://" + AppSetting.Database_Server + ":" + AppSetting.Database_Port);
            _database = client.GetDatabase(AppSetting.Database_Name);
        }
        public virtual List<APOD> GetAPODs(string collectionName)
        {
            var collections = _database.GetCollection<APOD>(collectionName).AsQueryable().ToList();
            return collections;
        }
        public virtual APOD FindAPODFromDate(string collectionName, DateTime APODDate)
        {
            var collections = _database.GetCollection<APOD>(collectionName).FindAsync<APOD>(filter => filter.date == APODDate.ToString("yyyy-MM-dd")).Result.FirstOrDefault();
            return collections;
        }
        public virtual void InsertAPOD(string collectionaName, APOD APODToSave)
        {
            var _collection = _database.GetCollection<APOD>(collectionaName);
            _collection.InsertOne(APODToSave);
        }

    }
}
