﻿using LibraryManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Core.DTOs.Book.ViewModel
{
    public class ViewBooksInCategoryDTO
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public long CategoryId { get; set; }
        public long AuthorId { get; set; }
        public DateTime PublishedTime { get; set; }
    }
}
