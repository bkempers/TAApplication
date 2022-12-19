using System;
using System.Collections.Generic;
using System.Data;
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
using static System.Net.Mime.MediaTypeNames;

namespace TAApplication.Controllers
{
    public class AvailabilitiesController : Controller {
        private readonly TAContext _context;
        private readonly UserManager<TAUser> _um;

        public AvailabilitiesController(TAContext context, UserManager<TAUser> um) {
            _context = context;
            _um = um;
        }

        // GET: Availabilities
        [Authorize(Roles = "Applicant, Professor, Administrator")]
        public async Task<IActionResult> Availability(string? Id) {
            if (_context.Users == null)
            {
                return NotFound();
            }

            if (Id == null)
            {
                string? userId = _um.GetUserId(User);
                var currUser = await _um.FindByIdAsync(userId);
                return View(currUser);
            }

            var user = await _um.FindByIdAsync(Id);
            return View(user);


            //return View(await _context.Availability.ToListAsync());
        }

        /**
         * 
         * This method was written to update the database from the Availability page so that the view is accurate
         * This method is called from Availability.cshtml AJAX script
         * 
         */
        //Given array of slots, update DB to reflect the current users availability
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> SetSchedule(string availabilitySlots, string id)
        {
            //uncomment this code to force an error to see the error message on screen
            // throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.BadRequest);

            Availability availability = new Availability();
            availability.WeekAvailability = availabilitySlots;

            ModelState.Remove("User");
            availability.User = await _um.GetUserAsync(User);

            availability.ID = availability.User.Id;
            if (ModelState.IsValid)
            {
                try {
                   _context.Add(availability);
                    await _context.SaveChangesAsync();
                }
                catch {
                    _context.Update(availability);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            return $"User had availabilities updated to: " + availabilitySlots;
        }

        //Return an array of slots representing the current users availability
        [HttpGet]
        [AllowAnonymous]
        public async Task<string> GetSchedule(string id) {
            var availabilitySlots = await _context.Availability.FindAsync(id);

            if(availabilitySlots == null)
            {
                return new string('0', 240);
            }
            else
            {
                return $"{availabilitySlots.WeekAvailability}";
            }
        }

    /*        // GET: Availabilities/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Availability == null)
                {
                    return NotFound();
                }

                var availability = await _context.Availability
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (availability == null)
                {
                    return NotFound();
                }

                return View(availability);
            }

            // GET: Availabilities/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Availabilities/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("ID,mondayAvailability,tuesdayAvailability,wednesdayAvailability,thursdayAvailability,fridayAvailability")] Availability availability)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(availability);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(availability);
            }*/
}

}
