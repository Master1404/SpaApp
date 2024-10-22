using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaApp.Application.Responses;
using MediatR;

namespace SpaApp.Application.Commands.Comment
{
    public class UpdateCommentCommand : IRequest<Response<Domain.Entities.Comment>>
    {
        public string Id { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }
       // public string HomePage { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
