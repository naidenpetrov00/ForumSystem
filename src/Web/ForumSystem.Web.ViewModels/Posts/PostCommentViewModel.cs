﻿namespace ForumSystem.Web.ViewModels.Posts
{
    using System;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;
    using Ganss.Xss;

    public class PostCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? PostId { get; set; }

        public int? ParentCommentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }
    }
}
