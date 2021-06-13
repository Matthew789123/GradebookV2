using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class MessageBoxViewModel
    {
        public List<MessageModel> SendedMessages { get; set; }
        public List<MessageModel> ReceivedMessages { get; set; }
    }

    public class MessageModel
    {
        public int MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUser Sender { get; set; }
        public List<ApplicationUser> Receivers { get; set; }
    }

    public class MessageViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }

    public class ReplyViewModel
    {
        public string ReceiverId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}