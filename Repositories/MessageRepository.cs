using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
 
    public class MessageRepository : DbRepository<Message>, IMessageRepository
    {
        public MessageRepository(DbContext context) : base(context)
        {

        }
    }
}
