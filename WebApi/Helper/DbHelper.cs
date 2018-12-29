
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Helper
{
    public class DbHelper
    {
        //MongoClient client = new MongoClient("mongodb://127.0.0.1:32769");
        //IMongoDatabase database;
        //IMongoCollection<BsonDocument> collection;

        IDatabase db;

        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:32768");

        public DbHelper()
        {
            //database = client.GetDatabase("foo");
            //collection = database.GetCollection<BsonDocument>("bar");
            db = redis.GetDatabase();
        }

        //public BsonDocument GetData()
        //{
        ////    return collection.Find(new BsonDocument()).FirstOrDefault();
        //}

        public string GetData(string elmentName)
        {
           var data = JsonConvert.DeserializeObject<ElmData>( db.StringGet("elm"));
           switch(elmentName)
            {
                case "seller":
                    return JsonConvert.SerializeObject(data.seller); 
                case "goods":
                    return JsonConvert.SerializeObject(data.goods);
                default:
                    return JsonConvert.SerializeObject(data.ratings);
            }
        }
        
    }
}
