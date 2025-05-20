﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mySite.Data;
using mySite.Models;

namespace mySite.Controllers
{
    public class NovelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NovelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Novels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Novel.ToListAsync());
        }
        // GET: Novels/showSearch
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        public async Task<IActionResult> Help()
        {
            return View();
        }
        // GET: Novels/showSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index",await _context.Novel.Where( j => j.Title.Contains(SearchPhrase) ).ToListAsync());
        }

        // GET: Novels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novel = await _context.Novel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (novel == null)
            {
                return NotFound();
            }

            return View(novel);
        }

        // GET: Novels/Create
        [Authorize]
        public IActionResult Create()
        {

            return View();
        }

        // POST: Novels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Author,Title,Chapter,Text")] Novel novel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(novel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(novel);
        }

        // GET: Novels/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novel = await _context.Novel.FindAsync(id);
            if (novel == null)
            {
                return NotFound();
            }
            return View(novel);
        }

        // POST: Novels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Author,Title,Chapter,Text")] Novel novel)
        {
            if (id != novel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(novel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NovelExists(novel.ID))
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
            return View(novel);
        }

        // GET: Novels/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novel = await _context.Novel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (novel == null)
            {
                return NotFound();
            }

            return View(novel);
        }

        // POST: Novels/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var novel = await _context.Novel.FindAsync(id);
            if (novel != null)
            {
                _context.Novel.Remove(novel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NovelExists(int id)
        {
            return _context.Novel.Any(e => e.ID == id);
        }
    }
}
