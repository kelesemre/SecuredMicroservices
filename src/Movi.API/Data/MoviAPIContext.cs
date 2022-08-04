using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movi.API.Model;

namespace Movi.API.Data
{
    public class MoviAPIContext : DbContext
    {
        public MoviAPIContext (DbContextOptions<MoviAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Movi.API.Model.Movie> Movie { get; set; }
    }
}
