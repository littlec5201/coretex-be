using System;
using System.Collections.Generic;
using System.Web.Http;
using coretex_be.Models;
using MongoDB.Driver;
using System.Web.Http.Cors;

namespace coretex_be.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VehicleController : ApiController
    {
        DatabaseContext dbContext = new DatabaseContext();
        private readonly string tableName = "Vehicles";

        // GET api/<controller>
        public IEnumerable<VehicleModel> Get()
        {
            return dbContext.Get<VehicleModel>(tableName);
        }

        // GET api/<controller>/5
        public VehicleModel Get(string id)
        {
            foreach(var vehicle in Get())
            {
                if (vehicle._id == id)
                {
                    return vehicle;
                }
            }
            return null;
        }

        // POST api/<controller>
        public void Post([FromBody]string data)
        {
            VehicleModel respObj = dbContext.DeserializeString<VehicleModel>(data);
            respObj._id = Guid.NewGuid().ToString();
            dbContext.Post(tableName, respObj);
        }

        // PUT api/<controller>/5
        public void Put([FromBody]string data)
        {
            VehicleModel respObj = dbContext.DeserializeString<VehicleModel>(data);
            dbContext.mongoDatabase.GetCollection<VehicleModel>(tableName).ReplaceOne(vehicle => vehicle._id == respObj._id, respObj);
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
            dbContext.mongoDatabase.GetCollection<VehicleModel>(tableName).DeleteOne(vehicle => vehicle._id == id);
        }
    }
}