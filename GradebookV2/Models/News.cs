using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class News
    {
        [Required]
        public int NewsId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string TeacherId { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}