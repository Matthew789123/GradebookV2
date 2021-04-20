using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class Message
    {
        [Required]
        public int MessageId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int ParentId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public virtual ApplicationUser Parent { get; set; }
        public virtual ApplicationUser Teacher { get; set; }
    }
}