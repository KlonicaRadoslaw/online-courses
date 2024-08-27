using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Data;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;
using OnlineCourses.Repositories;

namespace OnlineCourses.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        private readonly IContentRepository _contentRepository;

        public ContentController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        // GET: Admin/Content
        public async Task<IActionResult> Index()
        {
            var content = await _contentRepository.GetAll();
            return content != null ? View(content) : Problem("Entity set 'ApplicationDbContext.Content' is null.");
        }

        // GET: Admin/Content/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var content = await _contentRepository.GetById(id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // GET: Admin/Content/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Content/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HTMLContent,VideoLink")] Content content)
        {
            if (ModelState.IsValid)
            {
                _contentRepository.Add(content);
                return RedirectToAction(nameof(Index));
            }
            return View(content);
        }

        // GET: Admin/Content/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var content = await _contentRepository.GetById(id);
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Admin/Content/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HTMLContent,VideoLink")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contentRepository.Update(content);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_contentRepository.DoesExist(content.Id))
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
            return View(content);
        }

        // GET: Admin/Content/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var content = await _contentRepository.GetById(id);

            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // POST: Admin/Content/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = await _contentRepository.GetById(id);

            if (content != null)
            {
                _contentRepository.Delete(content);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
