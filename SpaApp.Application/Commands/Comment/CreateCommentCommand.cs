using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaApp.Application.Responses;

namespace SpaApp.Application.Commands.Comment
{
    public class CreateCommentCommand : IRequest<Response<Domain.Entities.Comment>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HomePage { get; set; }
        public string Text { get; set; }
        public string CreatedAt { get; set; }
    }
}
