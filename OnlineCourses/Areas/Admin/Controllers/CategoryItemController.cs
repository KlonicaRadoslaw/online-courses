﻿using System;
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
    public class CategoryItemController : Controller
    {
        private readonly ICategoryItemRepository _categoryItemRepository;

        public CategoryItemController(ICategoryItemRepository categoryItemRepository)
        {
            _categoryItemRepository = categoryItemRepository;
        }

        // GET: Admin/CategoryItem
        public async Task<IActionResult> Index()
        {
            var categoryItems = await _categoryItemRepository.GetAll();
            return categoryItems != null ? View(categoryItems) : Problem("Entity set 'ApplicationDbContext.CategoryItem' is null.");
        }

        // GET: Admin/CategoryItem/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var categoryItem = await _categoryItemRepository.GetById(id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CategoryId,MediaTypeId,DateTimeItemReleased")] CategoryItem categoryItem)
        {
            if (ModelState.IsValid)
            {
                _categoryItemRepository.Add(categoryItem);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var categoryItem = await _categoryItemRepository.GetById(id);
            if (categoryItem == null)
            {
                return NotFound();
            }
            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CategoryId,MediaTypeId,DateTimeItemReleased")] CategoryItem categoryItem)
        {
            if (id != categoryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryItemRepository.Update(categoryItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryItemRepository.DoesExist(categoryItem.Id))
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
            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var categoryItem = await _categoryItemRepository.GetById(id);

            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryItem = await _categoryItemRepository.GetById(id);

            if (categoryItem != null)
            {
                _categoryItemRepository.Delete(categoryItem);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
