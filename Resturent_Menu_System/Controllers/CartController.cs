using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturent_Menu_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturent_Menu_System.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CartController));
        private readonly Context _context;

        public CartController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("cart/add")]
        public JsonResult AddToCart([FromBody]ListOfItems listOfItems)
        {
            log.Info("In the AddToCart");
            string message = "", status = "";

            foreach (var item in listOfItems.Items)
            {
                var options = item.Options.Split(",").Where(x=>!string.IsNullOrEmpty(x));

                if(options.Count() > 2)
                {
                    status = "warning";
                    message = "There are more than 2 options";
                    log.Info("In the AddToCart : There are more than 2 options warning");
                    break;
                }
                else
                {
                    _context.Cart.Add(item);
                }
            }

            if(message == "")
            {
                _context.SaveChanges();
                status = "success";
                message = "Added to cart successfully!";
                log.Info("In the AddToCart : Added to cart successfully");

            }

            return new JsonResult(new { status = status, message = message });
        }
    }
}
