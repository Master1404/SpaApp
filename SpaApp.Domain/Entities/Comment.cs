using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaApp.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("HomePage")]
        public string HomePage { get; set; }
        [BsonElement("Text")]
        public string Text { get; set; }
        [BsonElement("TimeCreatedAt")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("parentId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ParentId { get; set; }
    }
}
