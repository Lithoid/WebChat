using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ChatRepository : DbRepository<Chat>, IChatRepository
    {
        public ChatRepository(DbContext context) : base(context)
        {

        }
    }
}
