using Azure;
using KSeF_app_fix.Server.Models;
using Microsoft.AspNetCore.Mvc;



namespace KSeF_app_fix.Server.Controllers
{
    [ApiController]
    [Route("s/[controller]")]
    public class SchemaController : ControllerBase
    {
        public class MyResponse
        {
            public Address Address { get; set; }
            public BankAccount BankAccount { get; set; }
            public new ContactInfo ContactInfo { get; set; }
            public new Invoice Invoice { get; set; }
            public new InvoiceLine InvoiceLine { get; set; }
            public new Party Party { get; set; }
            public new PaymentInfo PaymentInfo { get; set; }
            public new PartialPayment PartialPayment { get; set; }
            public new Settlement Settlement { get; set; }
            public new Charge Charge { get; set; }
            public new Deduction Deduction { get; set; }
            public new Terms Terms { get; set; }
            public new Contract Contract { get; set; }
            public new OrderInfo OrderInfo { get; set; }
            public new TransportInfo TransportInfo { get; set; }
            public new Carrier Carrier { get; set; }


        }
        [HttpGet]
        public ActionResult<MyResponse> GetSchema()
        {
            return Forbid();
        }
    }
}