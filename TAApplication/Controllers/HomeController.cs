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
 *    Contains all logic for the main pages of the website.
 *    Mainly manages which views to display and who is authorized
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
using System.Web;
using TAApplication.Data;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using System.Net;

namespace TAApplication.Controllers
{
    [Authorize(Roles = "Administrator, Professor, Applicant")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<TAUser> _um;

        public HomeController(ILogger<HomeController> logger, UserManager<TAUser> um)
        {
            _logger = logger;
            _um = um;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Applicant")]
        public IActionResult ApplicationCreate()
        {
            return View();
        }

        [Authorize(Roles = "Professor, Administrator, Applicant")]
        public async Task<IActionResult> ApplicationDetailsAsync()
        {
            var userId = _um.GetUserId(User);
            TAUser user = await _um.FindByIdAsync(userId);
            if (!(await _um.IsInRoleAsync(user, "Applicant") && user.Uid == "u0000000"))
            {
                Response.Redirect("/Identity/Account/AccessDenied");
            }

            return View();
        }

        [Authorize(Roles = "Professor, Administrator")]
        public IActionResult ApplicationList()
        {
            return View();
        }

        [Authorize(Roles = "Applicant, Professor, Administrator")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "Applicant, Professor, Administrator")]
        public IActionResult Courses()
        {
            return View();
        }

        [Authorize(Roles = "Applicant, Professor, Administrator")]
        public IActionResult Availabilities()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}