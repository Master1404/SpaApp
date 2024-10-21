using MediatR;
using SpaApp.Application.Queries;
using SpaApp.Application.Queries.Comment;
using SpaApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommentEntity = SpaApp.Domain.Entities.Comment
    ;

namespace SpaApp.Application.Handlers.Comment
{
    public class GetCommentListQueryHandler : IRequestHandler<GetCommentListQuery, IEnumerable<CommentEntity>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentListQueryHandler(ICommentRepository commentRepository) => _commentRepository = commentRepository;

        public async Task<IEnumerable<CommentEntity>> Handle(GetCommentListQuery request, CancellationToken cancellationToken)
        {
            return await _commentRepository.GetAllCommentAsync();
        }
    }
}
