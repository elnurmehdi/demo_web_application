using DemoApplication.Areas.Client.ViewComponents;
using DemoApplication.Areas.Client.ViewModels.Basket;
using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketManagementController : Controller
    {


        private readonly DataContext _dataContext;

        public BasketManagementController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }



        #region Add
        [HttpGet("add/{id}", Name = "client-basket-add")]
        public async Task<IActionResult> AddAsync([FromRoute] int id)
        {
            var product = await _dataContext.Books.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var booksViewModel = new List<ProductCookieVieModel>();


            var cookieValue = HttpContext.Request.Cookies["products"];
            if (cookieValue == null)
            {
                 booksViewModel = new List<ProductCookieVieModel>
               {
                   new ProductCookieVieModel(product.Id, product.Title, string.Empty, 1, product.Price, product.Price)
               };

                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(booksViewModel));


            }
            else
            {
                 booksViewModel = JsonSerializer.Deserialize<List<ProductCookieVieModel>>(cookieValue);
                var targetBookInCookie = booksViewModel.FirstOrDefault(tbic => tbic.Id == id);

                if (targetBookInCookie == null)
                {
                    booksViewModel.Add(new ProductCookieVieModel(product.Id, product.Title, string.Empty, 1, product.Price, product.Price));
                    HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(booksViewModel));
                }
                else
                {
                    targetBookInCookie.Quantity += 1;
                    targetBookInCookie.Total = targetBookInCookie.Quantity * targetBookInCookie.Price;

                    HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(booksViewModel));
                }
            }

            return ViewComponent(nameof(ShopCart), booksViewModel);

        }
        #endregion


        #region Delete
        [HttpGet("delete/{id}", Name = "client-basket-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var product = await _dataContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productCookieViewModel = new List<ProductCookieVieModel>();


            var cookieValue = HttpContext.Request.Cookies["products"];
            if (cookieValue == null)
            {
                return NotFound();
            }

            productCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieVieModel>>(cookieValue);

            foreach(var item in productCookieViewModel)
            {
                if(item.Quantity > 1)
                {
                    item.Quantity--;
                    item.Total= item.Quantity * item.Price;
                }
                else
                {
                   productCookieViewModel.Remove(item);
                    break;
                }
                
            }

           

            HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productCookieViewModel));

            return ViewComponent(nameof(ShopCart), productCookieViewModel);
        }

    }

    #endregion
}

