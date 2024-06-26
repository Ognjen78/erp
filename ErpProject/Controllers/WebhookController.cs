using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace ErpProject.Controllers
{
    [Route("api/sportbasic/webhook")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        [HttpPost]
     /*   public async Task<IActionResult> Index()
        {
           
        } */

        private void FulfillOrder(Session session)
        {
            // Handle the post-payment logic here (e.g., save order details in the database)
        }
    }
}
