using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class UserMessage
    {
        public int UserMessageId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int MessageId { get; set; }

        public virtual ApplicationUser Sender { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
        public virtual Message Message { get; set; }
    }
}