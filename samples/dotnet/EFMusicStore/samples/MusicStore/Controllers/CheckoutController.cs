using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private const string PromoCode = "FREE";

        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(ILogger<CheckoutController> logger)
        {
            _logger = logger;
        }

        //
        // GET: /Checkout/
        public IActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressAndPayment(
            [FromServices] MusicStoreContext dbContext,
            [FromForm] Order order,
            CancellationToken requestAborted)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            var formCollection = await HttpContext.Request.ReadFormAsync();

            try
            {
                if (string.Equals(formCollection["PromoCode"].FirstOrDefault(), PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = HttpContext.User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Add the Order
                    dbContext.Orders.Add(order);

                    //Process the order
                    var cart = ShoppingCart.GetCart(dbContext, HttpContext);
                    await cart.CreateOrder(order);

                    _logger.LogInformation("User {userName} started checkout of {orderId}.", order.Username, order.OrderId);

                    // Save all changes
                    await dbContext.SaveChangesAsync(requestAborted);

                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }

            catch (Exception ex)
            {
                _logger.LogError("Error occured while checkout.\r\n"+ex, order.Username, order.OrderId);
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete

        public async Task<IActionResult> Complete(
            [FromServices] MusicStoreContext dbContext,
            int id)
        {
            var userName = HttpContext.User.Identity.Name;

            // Validate customer owns this order
            var order = await dbContext.Orders.Select(o => o).Where(o => o.OrderId == id).Where(o => o.Username == userName).FirstOrDefaultAsync();
            bool isValid = order != null;

            if (isValid)
            {
                _logger.LogInformation("User {userName} completed checkout on order {orderId}.", userName, id);
                return View(id);
            }
            else
            {
                _logger.LogError(
                    "User {userName} tried to checkout with an order ({orderId}) that doesn't belong to them.",
                    userName,
                    id);
                return View("Error");
            }
        }
    }
}