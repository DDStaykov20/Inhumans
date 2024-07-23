using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomIdentity.Data;
using CustomIdentity.Models;
using Microsoft.AspNetCore.Identity;

namespace CustomIdentity.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly AppDbContext _context;

        public AssignmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Assignment
        public async Task<IActionResult> Index(bool myAssign = false)
        {
            ViewBag.myAssign = myAssign;
            if (!myAssign)
            {
                var appDbContext = _context.Assignments.Include(p => p.Class);
                return View(await appDbContext.ToListAsync());
            }
            
            return View(await _context.Assignments.ToListAsync());
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Class,AssignmentDescription,Payment")] AssignmentModel assignmentModel)
        {
            if (ModelState.IsValid)
            {
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Class,AssignmentDescription,Payment")] AssignmentModel assignmentModel)
        {
            if (id != assignmentModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentModelExists(int id)
        {
            return _context.Assignments.Any(e => e.Id == id);
        }
    }
}
