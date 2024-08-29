using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class MediaTypeController : Controller
    {
        private readonly IMediaTypeRepository _mediaTypeRepository;

        public MediaTypeController(IMediaTypeRepository mediaTypeRepository)
        {
            _mediaTypeRepository = mediaTypeRepository;
        }

        // GET: Admin/MediaType
        public async Task<IActionResult> Index()
        {
            var mediaTypes = await _mediaTypeRepository.GetAll();
            return mediaTypes != null ? View(mediaTypes) : Problem("Entity set 'ApplicationDbContext.MediaType' is null.");
        }

        // GET: Admin/MediaType/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var mediaType = await _mediaTypeRepository.GetById(id);
            if (mediaType == null)
            {
                return NotFound();
            }

            return View(mediaType);
        }

        // GET: Admin/MediaType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MediaType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ThumbnailImagePath")] MediaType mediaType)
        {
            if (ModelState.IsValid)
            {
                _mediaTypeRepository.Add(mediaType);
                return RedirectToAction(nameof(Index));
            }
            return View(mediaType);
        }

        // GET: Admin/MediaType/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var mediaType = await _mediaTypeRepository.GetById(id);
            if (mediaType == null)
            {
                return NotFound();
            }
            return View(mediaType);
        }

        // POST: Admin/MediaType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ThumbnailImagePath")] MediaType mediaType)
        {
            if (id != mediaType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _mediaTypeRepository.Update(mediaType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_mediaTypeRepository.DoesExist(mediaType.Id))
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
            return View(mediaType);
        }

        // GET: Admin/MediaType/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var mediaType = await _mediaTypeRepository.GetById(id);

            if (mediaType == null)
            {
                return NotFound();
            }

            return View(mediaType);
        }

        // POST: Admin/MediaType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaType = await _mediaTypeRepository.GetById(id);

            if (mediaType != null)
            {
                _mediaTypeRepository.Delete(mediaType);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
