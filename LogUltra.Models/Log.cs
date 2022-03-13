using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LogUltra.Models
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedAt { get; set; }

        public string Level { get; set; }

        [BsonElement]
        public string Source { get; set; }

        [BsonElement]
        public string Description { get; set; }
        [BsonElement]
        public bool IsException { get; set; }
        [BsonElement]
        public string Exception { get; set; }
    }
}
