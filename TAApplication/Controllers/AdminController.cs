/**
 * Author:      Trevor Koenig
 * Partner:     Ben Kempers
 * Date:        12/6/2022
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
 *    Contains all logic for the Admin page in the website. Allows for an Admin user to change any user's role.
 *    PS9 - added in the controller endpoints for EnrollmentTrends view as well as the get enrollment data information
 *    
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using TAApplication.Models;
using System;
using Microsoft.AspNetCore.Identity;
using TAApplication.Areas.Data;
using Microsoft.EntityFrameworkCore;
using TAApplication.Data;
using TAApplication.Data.Migrations;

namespace TAApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly TAContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<TAUser> _um;
        private readonly RoleManager<IdentityRole> _rm;

        public AdminController(TAContext context, ILogger<HomeController> logger, UserManager<TAUser> um, RoleManager<IdentityRole> rm)
        {
            _context = context;
            _logger = logger;
            _um = um;
            _rm = rm;
        }

        // GET: Admin Roles
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Admin()
        {
            if (_um == null || _rm == null)
            {
                return NotFound();
            }

            var students = _um.Users;
            var roles = _rm.Roles;

            if (students == null || roles == null)
            {
                return NotFound();
            }

            return View(_um);
        }


        /**
         * 
         * This method was written to update the database from the admin page so that the view is accurate
         * This method is called from Admin.cshtml AJAX script
         * 
         */
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<string> RoleRequest(string role, string id)
        {
            Console.WriteLine(id);
            TAUser user = await _um.FindByIdAsync(id);

            // used this webpage to assist in learning how to manage roles for users:
            // https://www.yogihosting.com/aspnet-core-identity-roles/
            switch (role) {
                case "No_Administrator":
                    await _um.RemoveFromRoleAsync(user, "Administrator");
                    break;
                case "Administrator":
                    await _um.AddToRoleAsync(user, "Administrator");
                    break;
                case "No_Professor":
                    await _um.RemoveFromRoleAsync(user, "Professor");
                    break;
                case "Professor":
                    await _um.AddToRoleAsync(user, "Professor");
                    break;
                case "No_Applicant":
                    await _um.RemoveFromRoleAsync(user, "Applicant");
                    break;
                case "Applicant":
                    await _um.AddToRoleAsync(user, "Applicant");
                    break;
            }

            return $"User: " + user.Name + " had role: " + role + " updated.";
        }


        /*
         * 
         * Return a view with controllers to query the database for custom data ranges for enrollment data
         * 
         */
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EnrollmentTrends()
        {
            return View();
        }

        // GET:Enrollment data based on specified parameters
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<string> GetEnrollmentData(string startDate, string endDate, string courseDeptNum)
        {
            System.Diagnostics.Debug.WriteLine(courseDeptNum.Split(' ')[1]);
            var courseNumber = int.Parse(courseDeptNum.Split(' ')[1]);

            Course? course = null;
            var allCourses = await _context.Course.ToArrayAsync<Course>();
            CourseEnrollment? enrollmentData = null;

            for (int i = 0; i < allCourses.Length; i++)
            {
                if (allCourses[i].courseNumber.Equals(courseNumber))
                {
                    course = allCourses[i];
                    enrollmentData = await _context.Enrollments.FindAsync(course.ID);
                }
            }

            if (enrollmentData == null)
            {
                return new string("");
            }
            else
            {
                var enrollmentDates = (string[])enrollmentData.dateList;
                System.Diagnostics.Debug.WriteLine(enrollmentDates[0]);
                int startIndex = 0;
                int endIndex = 0;
                for (int i = 0; i < enrollmentDates.Length; i++)
                {
                    
                    if (String.Equals(" " + startDate, enrollmentDates[i]))
                    {
                        System.Diagnostics.Debug.WriteLine(enrollmentDates[i] + " ");
                        startIndex = i;
                    }
                    if (String.Equals(" " + endDate, enrollmentDates[i]))
                    {
                        System.Diagnostics.Debug.WriteLine(enrollmentDates[i] + " ");
                        endIndex = i;
                    }
                }
                // if the enddate did not find a match include all dates
                if (endIndex == 0)
                {
                    endIndex = enrollmentDates.Length - 1;
                }

                var dates = String.Join(',', new List<string>(enrollmentDates).GetRange(startIndex, endIndex - startIndex + 1));
                var enrollments = String.Join(',', (new List<int>((int[])enrollmentData.enrollmentsList)).GetRange(startIndex, endIndex - startIndex + 1));

                return $"{dates}\n{enrollments}";
            }
        }
    }
}

