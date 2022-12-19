/**
* Author:      Trevor Koenig
* Partner:     Ben Kempers
* Date:        10/23/2022
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
*   Course model page to create a new course with all of the listed aspects of the course requirements
*    
*/

using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using TAApplication.Areas.Data;

namespace TAApplication.Models {
    public class Availability {
        [Required]
        public string ID { get; set; }

        //Navigation property
        public TAUser User { get; set; } = null!;

/*
        [Required]
        [Display(Name = "Monday Availability Status",
            ShortName = "Mon Avail Stat",
            Prompt = "Monday 9:00am - 12:30pm",
            Description = "The availability of TA to teach on a Monday.")]
        [DisplayFormat(NullDisplayText = "")]
        [MaxLength(48,
            ErrorMessage = "Your availability status can't reach outside of 8am - 8pm.")]
        public string mondayAvailability { get; set; }

        [Required]
        [Display(Name = "Tuesday Availability Status",
         ShortName = "Tues Avail Stat",
         Prompt = "Tuesday 9:00am - 12:30pm",
         Description = "The availability of TA to teach on a Tuesday.")]
        [DisplayFormat(NullDisplayText = "")]
        [MaxLength(48,
            ErrorMessage = "Your availability status can't reach outside of 8am - 8pm.")]
        public string tuesdayAvailability { get; set; }

        [Required]
        [Display(Name = "Wednesday Availability Status",
         ShortName = "Wed Avail Stat",
         Prompt = "Wednesday 9:00am - 12:30pm",
         Description = "The availability of TA to teach on a Wednesday.")]
        [DisplayFormat(NullDisplayText = "")]
        [MaxLength(48,
            ErrorMessage = "Your availability status can't reach outside of 8am - 8pm.")]
        public string wednesdayAvailability { get; set; }

        [Required]
        [Display(Name = "Thursday Availability Status",
         ShortName = "Thurs Avail Stat",
         Prompt = "Thursday 9:00am - 12:30pm",
         Description = "The availability of TA to teach on a Thursday.")]
        [DisplayFormat(NullDisplayText = "")]
        [MaxLength(48,
            ErrorMessage = "Your availability status can't reach outside of 8am - 8pm.")]
        public string thursdayAvailability { get; set; }

        [Required]
        [Display(Name = "Friday Availability Status",
         ShortName = "Fri Avail Stat",
         Prompt = "Friday 9:00am - 12:30pm",
         Description = "The availability of TA to teach on a Friday.")]
        [DisplayFormat(NullDisplayText = "")]
        [MaxLength(48,
            ErrorMessage = "Your availability status can't reach outside of 8am - 8pm.")]
        public string fridayAvailability { get; set; }
*/

        [Required]
        [Display(Name = "Weekly Availability Status",
         ShortName = "Wkly Avail Stat",
         Prompt = "Mon-Fri 9:00am - 12:30pm",
         Description = "The availability of TA to work during the week.")]
        [DisplayFormat(NullDisplayText = "")]
        [MaxLength(240,
            ErrorMessage = "Your availability status can't reach outside of 8am - 8pm or Monday - Friday.")]
        public string WeekAvailability { get; set; }
    }
}
