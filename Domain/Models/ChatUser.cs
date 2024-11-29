using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ChatUser
    {
        [Key]
        public int Id { get; set; }
        public string UserName{ get; set; }
        public string Avatar { get; set; }
    }
}
