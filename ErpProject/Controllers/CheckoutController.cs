using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace ErpProject.Controllers
{
    [Route("/api/sportbasic")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CheckoutController : ControllerBase
    {
        [HttpPost("create-checkout-session")]
        public ActionResult Create([FromBody] List<SessionLineItemOptions> items)
        {

            if (items == null || items.Any(item => item == null))
            {
                return BadRequest(new { error = "Invalid line items" });
            }

            var domain = "http://localhost:4200"; // Your frontend domain

            var lineItems = items.Select(item =>
            {
                if (item.PriceData == null || item.PriceData.ProductData == null)
                {
                    return null; // Skip or handle invalid item
                }

                return new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = item.PriceData.Currency ?? "rsd", // Default to "rsd" if null
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.PriceData.ProductData.Name ?? "Unknown Product"
                        },
                        UnitAmount = item.PriceData.UnitAmount ?? 0 // Default to 0 if null
                    },
                    Quantity = item.Quantity ?? 1 // Default to 1 if null
                };
            }).Where(item => item != null).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = domain + "/success.html",
                CancelUrl = domain + "/cancel.html",
            };

            var service = new SessionService();
            try
            {
                Console.WriteLine("Creating session with the following options:");
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(options));
                Session session = service.Create(options);
                return new JsonResult(new { id = session.Id });
            }
            catch (StripeException e)
            {
                Console.WriteLine($"Stripe error: {e.StripeError.Message}");
                return BadRequest(new { error = e.StripeError.Message });
            }
        }
    }
}
