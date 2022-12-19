using System.ComponentModel.DataAnnotations;
using TAApplication.Areas.Data;

namespace TAApplication.Models
{

    /**
    * enum of the degree type, used for current Degree property.
    * As seen fit more degree types should be added.
    */
    public enum Degree
    {
        AS,         // 0
        AA,         // 1
        BS,         // 2
        BA,         // 3
        Masters,    // 1
        BSMS,       // 2
        PhD         // 3
    }

    public class Application : ModificationTracking
    {
        public int ID { get; set; }

        //public int UserID { get; set; }

        //Navigation property
        public TAUser User { get; set; } = null!;


        /**
         * The users current degree at the U.
         * Input must match the enum type Degree.
         */
        [Required]
        [Display(Name = "Current Degree",
                 ShortName = "Deg",
                 Prompt = "BS",
                 Description = "Your current pursued degree.")]
        [DisplayFormat(NullDisplayText = "")]
        public Degree currentDegree { get; set; }

        /**
         * Current Department Applicant is apart of.
         * This property currently has no checks on if the department exists or not.
         */
        [Required]
        [Display(Name = "Current Department",
                 ShortName = "Dept",
                 Prompt = "CS",
                 Description = "The department you are pursuing your degree in.")]
        [DisplayFormat(NullDisplayText = "")]
        public string currentDepartment { get; set; }

        /**
         * Current GPA of the student
         * This must be between 0.0 and 4.0
         */
        [Required]
        [Display(Name = "U of U GPA",
                 ShortName = "GPA",
                 Prompt = "3.0",
                 Description = "Your current U of U GPA")]
        [Range(0.0, 4.0,
            ErrorMessage = "Value must be a valid GPA.")]
        public float UofU_GPA { get; set; }

        /**
         * This describes the number of hours the student desires as an integer.
         * This value may not correspond to the actual working hours.
         */
        [Required]
        [Display(Name = "Desired Hours",
                 ShortName = "Hours",
                 Prompt = "20",
                 Description = "The amount of hours you would like to work per week.")]
        [DisplayFormat(NullDisplayText = "")]
        [Range(5, 20,
            ErrorMessage = "Desired hours mus be between 5 and 20.")]
        public int desiredHours { get; set; }

        /**
         * This field indicates whether or not the student can work the week before.
         * Use this field with a check box in the view.
         * Extra display information is included incase you make bad decisions.
         */
        [Required]
        [Display(Name = "Availabile the Week Before",
                 ShortName = "Early Availability",
                 Prompt = "Yes",
                 Description = "True if you are available to work the week before the semester starts.")]
        public bool availableBefore { get; set; }

        /**
         * Integer value of the number of semesters that a applicant has attended the U for.
         * Range between 0 and int max. Do not enter int max, that's silly.
         */
        [Required]
        [Display(Name = "Number of Semesters",
                 ShortName = "# of Semesters",
                 Prompt = "4",
                 Description = "The number of semesters you have at U of U.")]
        [Range(0, int.MaxValue,
            ErrorMessage = "The number of semesters must be greater than 0.")]
        public int numSemesters { get; set; }

        /**
         * This non-required field describes the applicant breifly.
         * No more than 50,000 characters may be used.
         */
        [DataType(DataType.MultilineText)]
        [Display(Name = "Personal Statement",
                 ShortName = "Statement",
                 Prompt = "I would make a great TA because...",
                 Description = "Write a paragraph or less about yourself.")]
        [DisplayFormat(NullDisplayText = "")]
        [MaxLength(50000,
            ErrorMessage = "Your personal statement cannot be longer than 50000 characters.")]
        public string? personalStatement { get; set; }

        /**
         * non-required free response of the previous school attended by the applicant before transferring
         */
        [Display(Name = "Transfer School",
                 ShortName = "Transfer",
                 Prompt = "SLCC",
                 Description = "Write any schools you transferred from here.")]
        [DisplayFormat(NullDisplayText = "")]
        public string? transferSchool { get; set; }

        /**
         * This is a url to the applicant's linkedin profile
         * This property checks that it is a valud Url
         */
        [Display(Name = "LinkedIn URL",
                 ShortName = "LinkedIn",
                 Prompt = "https://linkedin.com/in/Jane-Doe-05d748283",
                 Description = "The link to your LinkedIn profile.")]
        [DisplayFormat(NullDisplayText = "")]
        [Url]
        public string? linkedInURL { get; set; }

        /**
         * name of the submitted resume file. Non-required.
         * This property must be of type .pdf
         */
        [Display(Name = "Resume File Name",
                 ShortName = "Resume",
                 Prompt = "Jane_Resume.pdf",
                 Description = "Must be a pdf file name.")]
        [DisplayFormat(NullDisplayText = "")]
        [FileExtensions(Extensions = ".pdf",
            ErrorMessage = "File name must have .pdf extension type.")]
        public string? resumeFileName { get; set; }


    }
}
