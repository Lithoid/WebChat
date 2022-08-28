using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("UserChat")]
    public class UserChat 
    {

       [Column("userId")]
        public Guid UserId { get; set; }

        public AppUser User { get; set; }

        [Column("chatId")]
        public Guid ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
