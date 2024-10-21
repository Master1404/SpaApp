using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;


namespace SpaApp.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        [BsonElement("username")]
        [Required(ErrorMessage = "User name is required.")]
        public string Username { get; set; }
        [BsonElement("email")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [BsonElement("password")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}

