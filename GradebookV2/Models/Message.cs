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
        public string Content { get; set; }

        public virtual ICollection<UserMessage> UserMessages { get; set; }
    }
}