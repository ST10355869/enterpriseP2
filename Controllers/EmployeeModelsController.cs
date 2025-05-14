using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using enterpriseP2.Data;
using enterpriseP2.Models;
using enterpriseP2.Services;
using Microsoft.AspNetCore.Authorization;

namespace enterpriseP2.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeModelsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly AuthenticateService _authService;
        private readonly ProductServices _productServices;
        private readonly EmployeeServices _employeeService;

        public EmployeeModelsController(AppDbContext context, AuthenticateService authService,EmployeeServices employeeService)
        {
            _context = context;
            _authService = authService;
            _employeeService = employeeService;
        }

        
        // GET: EmployeeModels/Create
        [HttpGet]
        public IActionResult CreateFarmer()
        {
            return View(new FarmerModel());
        }

        // POST: EmployeeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFarmer(FarmerModel farmer)
        {
            if (ModelState.IsValid)
            {   // Attempt to create farmer
                var result = await _employeeService.CreateFarmer(farmer);
                if (result)
                {
                    TempData["SuccessMessage"] = "Farmer created successfully!";
                    return RedirectToAction("Index", "Farmer");
                }
                ModelState.AddModelError("", "Username already exists or invalid data");
            }
            return View(farmer);
        }
       
        // GET: EmployeeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.Employees.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            return View(employeeModel);
        }

        // POST: EmployeeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Username,Password,Role")] EmployeeModel employeeModel)
        {
            if (id != employeeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(employeeModel.Id))
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
            return View(employeeModel);
        }

        // GET: EmployeeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // POST: EmployeeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeModel = await _context.Employees.FindAsync(id);
            if (employeeModel != null)
            {
                _context.Employees.Remove(employeeModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeModelExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
