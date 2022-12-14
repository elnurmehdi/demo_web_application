using DemoApplication.Areas.Client.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DemoApplication.Areas.Client.ViewComponents
{
    public class ShopCart : ViewComponent
    {

        public  IViewComponentResult Invoke(List<ProductCookieVieModel>? viewModels = null)
        {

            if(viewModels is not null)
            {
                return View(viewModels);

            }

            var cookieValue = HttpContext.Request.Cookies["products"];
            
            var productCookieViewModel = new List<ProductCookieVieModel>();

            if (cookieValue is not null)
            {


                productCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieVieModel>>(cookieValue);


            }
           
            return View(productCookieViewModel);
        }

    }
}
