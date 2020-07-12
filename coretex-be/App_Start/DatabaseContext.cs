using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace coretex_be
{
    public class DatabaseContext
    {
        private readonly string mongoDbName;
        private MongoClient mongoClient;
        // private MongoServer mongoServer;
        public IMongoDatabase mongoDatabase;

        public DatabaseContext()
        {
            mongoDbName = "Coretex";
            mongoClient = new MongoClient("mongodb+srv://admin:nzl.cjb@devcluster.jsf2p.azure.mongodb.net/" + mongoDbName + "?retryWrites=true&w=majority");
            // mongoServer = mongoClient.GetServer();
            mongoDatabase = mongoClient.GetDatabase(mongoDbName);
        }

        public ModelType DeserializeString<ModelType>(string rawString)
        {
            var dict = HttpUtility.ParseQueryString(rawString);
            string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
            ModelType respObj = JsonConvert.DeserializeObject<ModelType>(json);

            return respObj;
        }

        public IEnumerable<ModelType> Get<ModelType>(string tableName)
        {
            return mongoDatabase.GetCollection<ModelType>(tableName).Find(book => true).ToList();
        }

        public void Post<ModelType>(string tableName, ModelType payload)
        {
            mongoDatabase.GetCollection<ModelType>(tableName).InsertOne(payload);
        }
    }
}