﻿using System;
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
    public class ContentController : Controller
    {
        private readonly IContentRepository _contentRepository;
        private readonly ICategoryItemRepository _categoryItemRepository;

        public ContentController(IContentRepository contentRepository, ICategoryItemRepository categoryItemRepository)
        {
            _contentRepository = contentRepository;
            _categoryItemRepository = categoryItemRepository;
        }


        // GET: Admin/Content/Create
        public IActionResult Create(int categoryItemId, int categoryId)
        {
            var content = new Content
            {
                CategoryId = categoryId,
                CatItemId = categoryItemId
            };

            return View(content);
        }

        // POST: Admin/Content/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HTMLContent,VideoLink,CatItemId,CategoryId")] Content content)
        {
            content.CategoryItem = await _categoryItemRepository.GetById(content.CatItemId);
            if (ModelState.IsValid)
            {
                _contentRepository.Add(content);

                return RedirectToAction(nameof(Index), "CategoryItem", new { categoryId = content.CategoryId });
            }
            return View(content);
        }

        // GET: Admin/Content/Edit/5
        public async Task<IActionResult> Edit(int categoryItemId, int categoryId)
        {
            if (categoryItemId == 0)
            {
                return NotFound();
            }

            var content = await _contentRepository.GetByCategoryItemId(categoryItemId);

            content.CategoryId = categoryId;

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HTMLContent,VideoLink,CategoryId")] Content content)
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
                return RedirectToAction(nameof(Index), "CategoryItem", new { categoryId = content.CategoryId });
            }
            return View(content);
        }
    }
}
