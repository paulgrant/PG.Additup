using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.Data
{

    public class ExerciseContext : DbContext, IDataContext
    {
        public DbSet<Exercise> Exercise { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=exercise.db");
        }
    }

}
