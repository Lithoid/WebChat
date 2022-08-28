using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    [Table("chats")]
    public class Chat:DbEntity
    {

        [Column("chatName")]
        [MaxLength(64)]
        public string ChatName { get; set; }

        [Column("dateCreated")]
        [MaxLength(64)]
        public DateTime DateCreated { get; set; }

        [Column("isPrivate")]
        [MaxLength(64)]
        public bool IsPrivate  { get; set; }

        public List<Message> Messages { get; set; }

        public List<AppUser> Users { get; set; }
        public List<UserChat> UserChatKeys { get; set; }
    }
}
