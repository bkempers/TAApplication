using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TAApplication.Models
{
    public class ModificationTracking
    {
        /**
         * Date that the data entry was first created
         * Must be formatted: MM/dd/yyyy
         */
        [Required]
        [DisplayName("Created On")]
        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        /**
         * Date that the data entry was last modified
         * Must be formatted: MM/dd/yyyy
         */
        [Required]
        [ScaffoldColumn(false)]
        [DisplayName("Last Modified")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; }

        /**
         * string name of the person who created this application
         * No format requirements - non-required
         */
        [ScaffoldColumn(false)]
        [DisplayName("Created By")]
        public string? CreatedBy { get; set; } = string.Empty;

        /**
         * string name of the person who last modified this application
         * No format requirements - non-required
         */
        [ScaffoldColumn(false)]
        [DisplayName("Modified By")]
        public string? ModifiedBy { get; set; } = string.Empty;
    }
}

