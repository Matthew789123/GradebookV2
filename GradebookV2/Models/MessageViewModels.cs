using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class MessageBoxViewModel
    {
        public List<Message> SendedMessages { get; set; }
        public List<Message> ReceivedMessages { get; set; }
    }
}