using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Context;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroesContext _context;

        public SuperHeroController(SuperHeroesContext context)
        {
            _context = context;
           
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero)
        {
            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.SuperHeroes ON");
            _context.SuperHeroes.Add(hero);
            
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero)
        {
            var heroInDb = await _context.SuperHeroes.FindAsync(hero.Id);
            if (heroInDb is null)
            {
                return BadRequest("Hero not found");
            }

            heroInDb.Name = hero.Name;
            heroInDb.FirstName = hero.FirstName;
            heroInDb.LastName = hero.LastName;
            heroInDb.Place = hero.Place;

            _context.Entry(heroInDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            var heroInDb = await _context.SuperHeroes.FindAsync(id);
            if (heroInDb is null)
            {
                return BadRequest("Hero not found");
            }

            _context.SuperHeroes.Remove(heroInDb);
            _context.Entry(heroInDb).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
