using DemoApplication.Areas.Client.ViewModels.Subscribe;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("subscribe")]
    public class SubscribeController : Controller
    {
        private readonly DataContext _dbContext;

        public SubscribeController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("add", Name = "client-subscribe-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }


            if(_dbContext.Subscribes.Any(se => se.EmailAdress == model.EmailAdress))
            {
                ModelState.AddModelError(string.Empty, "Email exists");
                return BadRequest();
            }

            _dbContext.Subscribes.Add(new Subscribe
            {
                EmailAdress = model.EmailAdress,
                CreatedAt = DateTime.Now,
            });

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
