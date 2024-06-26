using ErpProject.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Stripe;
using Stripe.Checkout;

namespace ErpProject.Controllers
{
    [Route("api/sportbasic/create-checkout-session")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CheckoutController : ControllerBase
    {
        [HttpPost]
        public ActionResult Create([FromBody] List<LineItemDto> items )
        {

            StripeConfiguration.ApiKey = "sk_test_51PPm0ERrDFWhTqxPPSvVEPQxKlMCtXlSAAq7Dt5trwWxeMix3moNG1BMgcXkeo0Wm8ZJI6EF94c6AuQb1ZMiejJV008VuSdAMf";

            var domain = "http://localhost:4200";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<String>
                {
                    "card",
                },

                LineItems = items.ConvertAll(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "rsd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.name,
                        },
                        UnitAmount = (long)(item.price * 100), // Convert price to cents
                    },
                    Quantity = item.quantity,


                }),
                Mode = "payment",
                SuccessUrl = $"{domain}/success",
                CancelUrl = $"{domain}/cancel"

            };

            var service = new SessionService();
            Session session = service.Create(options);  

            return Ok(new { url = session.Url });
            

        }
    }
}

