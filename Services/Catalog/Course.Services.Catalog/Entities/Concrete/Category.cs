using Course.Services.Catalog.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Entities.Concrete
{
    public class Category : IEntity
    {
        [BsonId] //Mongo DB trafında Bunun Id PK olduğunu belirtir
        [BsonRepresentation(BsonType.ObjectId)] //Mongo db de bunun ObjectId tipinde olduğunu belirtir
        public string Id { get; set; }
        public string CategoryName { get; set; }
    }
}
