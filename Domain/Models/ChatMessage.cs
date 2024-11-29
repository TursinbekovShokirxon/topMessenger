using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id{ get; set; }
        public string UserName{ get; set; }
        public string Message{ get; set; }
        public string TimeStamp { get; set; }
        public string ChatRoom { get; set; }
    }
}
