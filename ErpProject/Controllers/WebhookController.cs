using ErpProject.DTO;
using ErpProject.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;

namespace ErpProject.Controllers
{
    [Route("api/sportbasic/webhook")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class WebhookController : ControllerBase
    {

        private readonly OrderService orderService;

        public WebhookController(OrderService orderService)
        {
            this.orderService = orderService;
        }



        [HttpPost]
       public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    "whsec_k1fwv86IxSabJl4R86KM5xl5Bc6OBh3K"
                );

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    // Rukujte događajem
                    await HandleCheckoutSessionCompleted(session);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }

       }

        private async Task HandleCheckoutSessionCompleted(Session session)
        {
            // Ovdje dodajte logiku za obradu događaja
            // Npr. kreiranje porudžbine u bazi podataka
            Console.WriteLine($"Payment successful for session: {session.Id}");

            var metadata = session.Metadata;
            
            var id_user = Guid.Parse(metadata["id_user"]);
            var id_shipping = int.Parse(metadata["id_shipping"]);
            var price = decimal.Parse(metadata["price"]);
            var items = JsonConvert.DeserializeObject<List<LineItemDto>>(metadata["items"]);


            var orderRequest = new OrderDto
            {
                
                id_user = id_user,
                id_shipping = id_shipping,
                total_price = price,
                items = items
                // Populate items from session if needed
            };

            Console.WriteLine(orderRequest);

            await orderService.CreateOrder(orderRequest);


        }


    }
}
