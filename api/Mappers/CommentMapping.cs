using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapping
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn= comment.CreatedOn,
                StockId= comment.StockId
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentRequestDto comment,int stockId)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
                StockId=stockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentDto comment)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content
            };
        }
    }
}