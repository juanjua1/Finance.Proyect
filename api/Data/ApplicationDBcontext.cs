using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Data
{
    public class ApplicationDBcontex : DbContext
    {
        public ApplicationDBcontext(DbcontextOptions dbcontextOptions)
        : base(dbcontextOptions)
        {
            
        }

        public DbSet<Stock> Stock { get; set; }
        public  DbSet<Comment> Comments { get; set; }
    }
}