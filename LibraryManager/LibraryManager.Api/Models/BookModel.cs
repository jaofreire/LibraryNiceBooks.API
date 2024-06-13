﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models
{
    public class BookModel
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public long? AuthorId { get; set; }
        public AuthorModel Author { get; set; } = null!;

        public string Category { get; set; } = string.Empty;

        public DateTime? PublishedDate { get; set; }
    }
}
