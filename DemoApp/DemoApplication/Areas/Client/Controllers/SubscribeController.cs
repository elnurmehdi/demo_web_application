using DemoApplication.Database;
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



        
    }
}
