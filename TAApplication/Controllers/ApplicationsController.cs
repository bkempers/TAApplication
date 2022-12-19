/**
 * Author:      Trevor Koenig
 * Partner:     Ben Kempers
 * Date:        9/27/2022
 * Course:      CS 4540, University of Utah, School of Computing
 * Copyright:   CS 4540 and [Ben Kempers and Trevor Koenig] - This work may not be copied for use in Academic Coursework.
 *
 * I, Trevor Koenig and Ben Kempers, certify that I wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 *
 *   This Controller is responsible for managing what data is displayed to whom for each application
 *   If a malformed request is made this controller should detect that
 *    
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TAApplication.Areas.Data;
using TAApplication.Data;
using TAApplication.Models;

namespace TAApplication.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly TAContext _context;
        private readonly UserManager<TAUser> _um;

        /**
         * ApplicationsController Constructor
         * Param: context, um
         * 
         * This constructor takes the database and usermanager as dependency injections.
         * Once the dependencies have been set up it's job is done
         */
        public ApplicationsController(TAContext context, UserManager<TAUser> um)
        {
            _context = context;
            _um = um;
        }

        /**
         * Index
         * Param: None
         * 
         * Index shows general information about the applications that have been submitted.
         * This includes the avergage GPA of applicants as well as number of applicants.
         */
        // GET: Applications
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Application.ToListAsync());
        }

        /**
         * List
         * Param: none
         * 
         * List displays a list of applivant information for administrators and professors.
         * This list does not include personal information about the applicants.
         */
        // GET: Applications
        [Authorize(Roles = "Professor, Administrator")]
        public async Task<IActionResult> List()
        {
            return View(await _context
                .Application
                .Include(o => o.User)
                .ToListAsync());
        }

        /**
         * Details
         * Param: Id
         * 
         * This method is responsible for displaying information about a single participants application
         * If an Id is not given(default is null Id) then it attempts to display the details page of the current user
         */
        // GET: Applications/Details/5
        [Authorize(Roles = "Administrator, Professor, Applicant")]
        public async Task<IActionResult> Details(int? Id)
        {
            if (_context.Application == null)
            {
                return NotFound();
            }

            if (Id == null)
            {
                string? userId = _um.GetUserId(User);
                var user = await _um.FindByIdAsync(userId);
                var app = await _context
                .Application
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.User.Id == user.Id);

                if (app == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(app);
                }
            }

            var application = await _context.Application.Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == Id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        /**
         * Create
         * Param: None
         * 
         * This method is simple, it shows the application create view
         */
        // GET: Applications/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        /**
         * Create
         * Param: Bind Application
         * 
         * This method takes the returning create application form and saves it into the database.
         */
        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,currentDegree,currentDepartment,UofU_GPA,desiredHours,availableBefore,numSemesters,personalStatement,transferSchool,linkedInURL,resumeFileName")] Application application)
        {
            ModelState.Remove("User");
            application.User = await _um.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        /**
         * Edit
         * Param: id - Id of the app to modify
         * 
         * Allows an admin to modify any application and an applicant to modify their own
         */
        // GET: Applications/Edit/5
        [Authorize(Roles = "Administrator, Applicant")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        /**
         * Edit
         * Param id, Bind Application
         * 
         * This Edit method takes the returning edits to the application and saves them into the database
         */
        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Applicant")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,currentDegree,currentDepartment,UofU_GPA,desiredHours,availableBefore,numSemesters,personalStatement,transferSchool,linkedInURL,resumeFileName")] Application application)
        {
            if (id != application.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ID))
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
            return View(application);
        }

        /**
         * Delete
         * Param: id
         * 
         * This method allows admins to delete any application and applicants to delete their own app
         */
        // GET: Applications/Delete/5
        [Authorize(Roles = "Administrator, Applicant")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .FirstOrDefaultAsync(m => m.ID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        /**
         * DeleteCOnfirmed
         * Param: id
         * 
         * Confirms with the user that they fully want to delete the application they are deleting
         */
        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Applicant")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Application == null)
            {
                return Problem("Entity set 'TAContext.Application'  is null.");
            }
            var application = await _context.Application.FindAsync(id);
            if (application != null)
            {
                _context.Application.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /**
         * ApplicationExists
         * Param: id
         * 
         * Checks if an application with that id exists
         */
        private bool ApplicationExists(int id)
        {
            return _context.Application.Any(e => e.ID == id);
        }
    }
}
