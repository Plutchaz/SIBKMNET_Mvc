using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIBKMNET_Mvc.Context;
using SIBKMNET_Mvc.Models;

namespace SIBKMNET_Mvc.Controllers
{
    public class ProvincesController : Controller
    {
        private readonly MyContext myContext;

        public ProvincesController(MyContext context)
        {
            this.myContext = context;
        }

        // GET: Provinces
        public IActionResult Index()
        {
            var data = myContext.Provinces.Include(x => x.Region).ToList();
            return View(data);
        }

        // GET: Provinces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await myContext.Provinces
                .Include(p => p.Region)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // GET: Provinces/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(myContext.Regions, "id", "id");
            return View();
        }

        // POST: Provinces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RegionId")] Province province)
        {
            if (ModelState.IsValid)
            {
                myContext.Add(province);
                await myContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(myContext.Regions, "id", "id", province.RegionId);
            return View(province);
        }

        // GET: Provinces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await myContext.Provinces.FindAsync(id);
            if (province == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(myContext.Regions, "id", "id", province.RegionId);
            return View(province);
        }

        // POST: Provinces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RegionId")] Province province)
        {
            if (id != province.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    myContext.Update(province);
                    await myContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinceExists(province.Id))
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
            ViewData["RegionId"] = new SelectList(myContext.Regions, "id", "id", province.RegionId);
            return View(province);
        }

        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await myContext.Provinces
                .Include(p => p.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var province = await myContext.Provinces.FindAsync(id);
            myContext.Provinces.Remove(province);
            await myContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvinceExists(int id)
        {
            return myContext.Provinces.Any(e => e.Id == id);
        }
    }
}
