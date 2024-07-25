using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomIdentity.Data;
using CustomIdentity.Models;
using Microsoft.AspNetCore.Hosting;

namespace CustomIdentity.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly string _uploadPath;

        public AssignmentController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _uploadPath = Path.Combine(env.WebRootPath, "UploadedFiles");

            // Ensure the upload directory exists
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        // GET: Assignment
        public async Task<IActionResult> Index()
        {
            return View(await _context.Assignments.ToListAsync());
        }

        public async Task<IActionResult> Mathematics()
        {
            var assignments = await _context.Assignments
                .Where(a => a.Class == "Mathematics")
                .ToListAsync();
            return View(assignments);
        }

        // GET: Assignment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignmentModel = await _context.Assignments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignmentModel == null)
            {
                return NotFound();
            }

            return View(assignmentModel);
        }

        // GET: Assignment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Assignment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignmentModel assignmentModel)
        {
            if (ModelState.IsValid)
            {
                if (assignmentModel.File != null && assignmentModel.File.Length > 0)
                {
                    var fileName = Path.GetFileName(assignmentModel.File.FileName);
                    var filePath = Path.Combine(_uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await assignmentModel.File.CopyToAsync(stream);
                    }

                    assignmentModel.FilePath = fileName; // Save the file name in the database
                }

                _context.Add(assignmentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assignmentModel);
        }

        // GET: Assignment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignmentModel = await _context.Assignments.FindAsync(id);
            if (assignmentModel == null)
            {
                return NotFound();
            }
            return View(assignmentModel);
        }

        // POST: Assignment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssignmentModel assignmentModel)
        {
            if (id != assignmentModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (assignmentModel.File != null && assignmentModel.File.Length > 0)
                    {
                        var fileName = Path.GetFileName(assignmentModel.File.FileName);
                        var filePath = Path.Combine(_uploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await assignmentModel.File.CopyToAsync(stream);
                        }

                        assignmentModel.FilePath = fileName; // Update the file name in the database
                    }

                    _context.Update(assignmentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentModelExists(assignmentModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(assignmentModel);
        }

        // GET: Assignment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignmentModel = await _context.Assignments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignmentModel == null)
            {
                return NotFound();
            }

            return View(assignmentModel);
        }

        // POST: Assignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignmentModel = await _context.Assignments.FindAsync(id);
            if (assignmentModel != null)
            {
                _context.Assignments.Remove(assignmentModel);
                if (!string.IsNullOrEmpty(assignmentModel.FilePath))
                {
                    var filePath = Path.Combine(_uploadPath, assignmentModel.FilePath);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Assignment/Download/5
        public IActionResult Download(int id)
        {
            var assignment = _context.Assignments.Find(id);
            if (assignment == null || string.IsNullOrEmpty(assignment.FilePath))
            {
                return NotFound();
            }

            var filePath = Path.Combine(_uploadPath, assignment.FilePath);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        private bool AssignmentModelExists(int id)
        {
            return _context.Assignments.Any(e => e.Id == id);
        }
    }
}
