using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Context
{
    public class SuperHeroesContext: DbContext
    {
        public SuperHeroesContext(DbContextOptions<SuperHeroesContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeroes => Set<SuperHero>();
    }
}
