using KSeF_app_fix.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KSeF_app_fix.Server.Models;


namespace KSeF_app_fix.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceMapper _mapper;
        private readonly InvoiceService _service;

        public InvoiceController(InvoiceMapper mapper, InvoiceService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<int>> UploadXml(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var tempPath = Path.GetTempFileName();

            using (var stream = new FileStream(tempPath, FileMode.Create))
                await file.CopyToAsync(stream);

            var invoice = _mapper.MapXmlToInvoice(tempPath);
            if (invoice == null)
                return BadRequest("Invalid XML");

            await _service.SaveInvoiceAsync(invoice);

            return Ok(new { invoiceId = invoice.Id });
        }

        [HttpGet("GetInvoiceById={id}")]
        public async Task<ActionResult<Invoice>> GetById(int id)
        {
            var invoice = await _service.GetByIdAsync(id);

            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpGet("GetAllInvoices")]
        public async Task<ActionResult<List<Invoice>>> GetAll()
        {
            var invoices = await _service.GetAllInvoicesAsync();
            return Ok(invoices);
        }
        [HttpGet("GetAllInvoicesDTOs")]
        public async Task<ActionResult<List<InvoiceDTOs>>> GetAllDTOs()
        {
            var invoices = await _service.GetAllInvoiceDTOsAsync();
            return Ok(invoices);
        }

    }
}



