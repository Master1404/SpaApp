using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommentEntity = SpaApp.Domain.Entities.Comment;

namespace SpaApp.Application.Queries.Comment
{
    public record GetCommentByIdQuery : IRequest<CommentEntity>
    {
        public string Id { get; set; }
    }
}
