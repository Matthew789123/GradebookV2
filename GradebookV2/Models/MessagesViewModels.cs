using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class MessageBoxViewModel
    {
        public List<Message> SendedMessages { get; set; }
        public List<Message> ReceivedMessages { get; set; }
    }

    public class MessageViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }

  
}