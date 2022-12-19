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
using TAApplication.Areas.Data;

namespace TAApplication.Models {
    public enum Semester {
        Fall,
        Spring,
        Summer
    }

    public class Course {

        public int ID { get; set; }

        //public int UserID { get; set; }

        //Navigation property
        public TAUser User { get; set; } = null!;

        [Required]
        [Display(Name = "Semester Offered",
                 ShortName = "Sem",
                 Prompt = "Fall",
                 Description = "This course is offered in this semester.")]
        [DisplayFormat(NullDisplayText = "")]
        public Semester semesterOffered { get; set; }

        [Required]
        [Display(Name = "Year Offered",
                 ShortName = "Year",
                 Prompt = "2022",
                 Description = "This course is offered during this year.")]
        [DisplayFormat(NullDisplayText = "")]
        [Range(2000, 2024,
            ErrorMessage = "Value must be a valid year.")]
        public int yearOffered { get; set; }

        [Required]
        [Display(Name = "Course Title",
                 ShortName = "Title",
                 Prompt = "Web Software",
                 Description = "This courses name.")]
        [DisplayFormat(NullDisplayText = "")]
        public string courseTitle { get; set; }

        [Required]
        [Display(Name = "Course Department",
                 ShortName = "Dep.",
                 Prompt = "CS",
                 Description = "This course is offered through this department.")]
        [DisplayFormat(NullDisplayText = "")]
        public string courseDepartment { get; set; }

        [Required]
        [Display(Name = "Course Number",
                 ShortName = "Num",
                 Prompt = "4540",
                 Description = "This courses number.")]
        [DisplayFormat(NullDisplayText = "")]
        [Range(1000, 6000,
            ErrorMessage = "Value must be a valid course number.")]
        public int courseNumber { get; set; }

        [Required]
        [Display(Name = "Course Section",
                 ShortName = "Sec",
                 Prompt = "001",
                 Description = "This courses section.")]
        [DisplayFormat(NullDisplayText = "")]
        [Range(001, 100,
            ErrorMessage = "Value must be a valid course section.")]
        public int courseSection { get; set; }

        [Required]
        [Display(Name = "Course Description",
                 ShortName = "Desc.",
                 Prompt = "Course catalog description",
                 Description = "This courses description, what is offered, and what to excpect from this course.")]
        [DisplayFormat(NullDisplayText = "")]
        public string courseDescription { get; set; }

        [Required]
        [Display(Name = "Professor's UNID",
                 ShortName = "UNID",
                 Prompt = "u0000001",
                 Description = "This courses professor's UNID.")]
        [DisplayFormat(NullDisplayText = "")]
        [Range(0000000, 9999999,
            ErrorMessage = "Value must be a valid UNID.")]
        public int profUNID { get; set; }

        [Required]
        [Display(Name = "Professor's Name",
                 ShortName = "Name",
                 Prompt = "Prof. H. James de St. Germain",
                 Description = "This courses professor's name.")]
        [DisplayFormat(NullDisplayText = "")]
        public string profName { get; set; }

        [Required]
        [Display(Name = "Courses Days Offered",
                 ShortName = "Days",
                 Prompt = "Days offered for this course",
                 Description = "This courses days offered.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}", NullDisplayText = "")]
        [DataType(DataType.DateTime)]
        public DateTime daysOffered { get; set; }

        [Required]
        [Display(Name = "Courses Time Offered",
                 ShortName = "Time",
                 Prompt = "Times offered for this course",
                 Description = "This courses times offered.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}", NullDisplayText = "")]
        [DataType(DataType.DateTime)]
        public DateTime timeOffered { get; set; }

        [Required]
        [Display(Name = "Course Location",
                 ShortName = "Loc",
                 Prompt = "Location of this course",
                 Description = "This courses location.")]
        [DisplayFormat(NullDisplayText = "")]
        public string courseLocation { get; set; }

        [Required]
        [Display(Name = "Credit Hours",
                 ShortName = "Credits",
                 Prompt = "Credit hours for this course",
                 Description = "This courses credit hours.")]
        [DisplayFormat(NullDisplayText = "")]
        [Range(1, 10,
            ErrorMessage = "Value must be a valid number of credit hours.")]
        public int creditHours { get; set; }

        [Required]
        [Display(Name = "Course Enrollment",
                 ShortName = "Enrollment",
                 Prompt = "Enrollment offered for this course",
                 Description = "This courses number of enrolled students.")]
        [DisplayFormat(NullDisplayText = "")]
        [Range(1, 500,
            ErrorMessage = "Value must be a valid number of students enrolled.")]
        public int courseEnrollment { get; set; }

        [Display(Name = "Course Note",
                 ShortName = "Note",
                 Prompt = "Administrator course note",
                 Description = "A note from the administrator about this course.")]
        [DisplayFormat(NullDisplayText = "")]
        [MaxLength(50000,
            ErrorMessage = "Your personal admin note cannot be longer than 50000 characters.")]
        public string? Note { get; set; }
    }
}
