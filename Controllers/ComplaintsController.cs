using HostelComplaintSystem.Data;
using HostelComplaintSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;


namespace HostelComplaintSystem.Controllers 
{
    
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ComplaintsController> _logger;

        public ComplaintsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<ComplaintsController> logger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }


        [AllowAnonymous]
        public IActionResult Create(string location)
        {
            var complaint = new Complaint
            {
                Location = !string.IsNullOrEmpty(location) ? location : "Unknown"
            };

            return View(complaint);
        }

        [Authorize]
        public async Task<IActionResult> Index(string statusFilter)
        {

            var query = _context.Complaints.AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                query = query.Where(c => c.Status == statusFilter);
            }
            ViewBag.CurrentFilter = statusFilter;

            var allComplaints = await query.OrderByDescending(c=>c.RegisteredTime).ToListAsync();

            return View(allComplaints);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int ?id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FindAsync(id);

            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint); 
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FirstOrDefaultAsync(m=>m.Id == id);

            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create(Complaint complaint, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                complaint.RegisteredTime = DateTime.Now;
                complaint.Status = "New";
                _context.Add(complaint);
                if (imageFile != null && imageFile.Length>0)
                {
                    string rootPath = _webHostEnvironment.WebRootPath;
                    string fileName = (complaint.Id).ToString() + Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);  
                    string uploadFolderPath = Path.Combine(rootPath, "uploads");
                    
                    if(!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    string filePath = Path.Combine(uploadFolderPath, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    complaint.ImagePath = Path.Combine("/uploads/", fileName);
                }


                await _context.SaveChangesAsync();

                return RedirectToAction("Index","Home");
            }
            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, Complaint complaint)
        {
            if (id != complaint.Id)
                return NotFound();

            var dbComplaint = await _context.Complaints.FindAsync(id);
            if (dbComplaint == null)
                return NotFound();
            dbComplaint.Status = complaint.Status;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);

            if (complaint != null)
            {
                string? imagePath = complaint.ImagePath;

                _context.Complaints.Remove(complaint);

                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(imagePath))
                {
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));

                    try
                    {
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to delete the image {imagePath} for Complaint Id {id}", imagePath, id);
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}