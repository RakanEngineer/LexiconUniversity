using LexiconUniversity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconUniversity.Persistance.Data
{
    public class LexiconUniversityContext : DbContext
    {        
        public LexiconUniversityContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default!;
    }
}
