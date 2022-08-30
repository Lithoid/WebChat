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


    [Table("messages")]
    public class Message:DbEntity
    {
        [Column("text")]
        [MaxLength(300)]
        public string Text { get; set; }

        [Column("dateCreated")]
        [MaxLength(64)]
        public DateTime DateCreated { get; set; }


        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }

    }
}
