/**
* Author:      Trevor Koenig
* Partner:     Ben Kempers
* Date:        12/4/2022
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
*   Model class for course enrollments by course
*    
*/

using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using TAApplication.Areas.Data;

namespace TAApplication.Models
{
    public class CourseEnrollment
    {
        [Required]
        public int ID { get; set; }

        //Navigation property
        public Course Course { get; set; } = null!;

        [Required]
        [Display(Name = "Course Enrollment Date Points",
         ShortName = "Enrollment Dates",
         Prompt = "Nov 9",
         Description = "The list of dates that the data was taken in chronological order.")]
        [DisplayFormat(NullDisplayText = "")]
        public string dates { get; set; }
        // a getter that returns it as a string array
        public ICollection<string> dateList { get { return this.dates.Split(','); } }

        [Required]
        [Display(Name = "Course Enrollment Values",
         ShortName = "Enrollments",
         Prompt = "32;15;300;24",
         Description = "Enrollment values for a specific class relative to date data points.")]
        [DisplayFormat(NullDisplayText = "")]
        // unsigned because negative enrollment is not possible
        public string enrollments { get; set; }
        // a getter that returns it as an int array - must convert from string to int
        public ICollection<int> enrollmentsList { get { return Array.ConvertAll(this.enrollments.Split(','), s => int.Parse(s)); } }

        public static implicit operator string?(CourseEnrollment? v) {
            throw new NotImplementedException();
        }
    }
}
