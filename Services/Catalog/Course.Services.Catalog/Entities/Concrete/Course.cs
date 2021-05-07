using Course.Services.Catalog.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Entities.Concrete
{
    public class Course :IEntity
    {
        [BsonId] //Mongo DB trafında Bunun Id PK olduğunu belirtir
        [BsonRepresentation(BsonType.ObjectId)] //Mongo db de bunun ObjectId tipinde olduğunu belirtir
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)] ////Mongo db de bunun ObjectId tipinde olduğunu belirtir.Categori ile FK
        public string CategoryId { get; set; }   
        [BsonIgnore] //Mongo Db tarafında bunu ingore et diyoruz. sadece kodlamada kullanacağız
        public Category Category { get; set; } 

        public string CourseName { get; set; }

        [BsonRepresentation(BsonType.Decimal128)] //MongoDb decimal tipi
        public decimal Price { get; set; }
       
        public string Photo { get; set; }
       
        public string Description { get; set; }
      
        public string ShortDescription { get; set; }

        [BsonRepresentation(BsonType.DateTime)] //MongoDb Datetime tipi
        public DateTime CreateDate { get; set; }
       
        public string UserId { get; set; }

        public Feature Feature { get; set; } //Kursun Özelliklerini Tutuyoruz.
       
        public List<string> Languages { get; set; } //Kurstaki Dilleri Liste olarak tutuyoruz.

    
    }
}
