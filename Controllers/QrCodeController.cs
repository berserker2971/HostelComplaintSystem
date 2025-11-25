using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;

namespace HostelComplaintSystem.Controllers
{
    [Authorize]
    public class QrCodeController : Controller
    {
        private readonly ILogger<QrCodeController> _logger;

        public QrCodeController(ILogger<QrCodeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string location)
        {
            _logger.LogWarning("Value received for 'location' parameter: {LocationValue}", location);

            if (string.IsNullOrWhiteSpace(location))
            {
                ViewBag.ErrorMessage = "Location field cannot be left empty";
                return View();
            }

            var url = $"{Request.Scheme}://{Request.Host}";
            var safeLocation = location.Replace(' ', '_');
            string complaintUrl = $"{url}/Complaints/Create?location={safeLocation}";

            QRCodeGenerator gen = new QRCodeGenerator();
            QRCodeData data = gen.CreateQrCode(complaintUrl, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(data);
            byte[] qrImageBytes = qrCode.GetGraphic(20);
            string qrCodeImage64 = Convert.ToBase64String(qrImageBytes);

            ViewBag.QRCodeImage = "data:image/png;base64," + qrCodeImage64;
            ViewBag.GeneratedFor = location;

            return View();
        }

        [HttpGet]
        public IActionResult Download(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return BadRequest();

            var url = $"{Request.Scheme}://{Request.Host}";
            var safeLocation = location.Replace(' ', '_');
            string complaintUrl = $"{url}/Complaints/Create?location={safeLocation}";

            QRCodeGenerator gen = new QRCodeGenerator();
            QRCodeData data = gen.CreateQrCode(complaintUrl, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(data);
            byte[] qrImageBytes = qrCode.GetGraphic(20);

            string fileName = $"QR-{safeLocation}.png";
            return File(qrImageBytes, "image/png", fileName);
        }
    }
}
