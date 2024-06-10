using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace ErpProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], "whsec_...");

            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;

                // Fulfill the purchase...
                FulfillOrder(session);
            }

            return Ok();
        }

        private void FulfillOrder(Session session)
        {
            // Handle the post-payment logic here (e.g., save order details in the database)
        }
    }
}
