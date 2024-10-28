using MongoDB.Bson.Serialization.Attributes;

namespace SpaApp.DTO
{
    public class CommentResponceModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HomePage { get; set; }
        public string Text { get; set; }
        public string ParentID { get; set; }    
    }
}
