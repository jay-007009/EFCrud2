using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCrud.Models
{
    public class Employee
    {
       [Key]
        public virtual int EmployeeId { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = ("Only Alphabets are Allowed."))]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = ("Only Alphabets are Allowed."))]
        public virtual string LastName { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        [RegularExpression("^[0-9 ]*$", ErrorMessage = ("Only Numbers are Allowed."))]
        //[Range(1,3)]
        public virtual int Age { get; set; }

        public virtual string MaritalStatus { get; set; }

        // [DefaultValue("true")]
        public virtual string Gender { get; set; }

         [Required(ErrorMessage = "Department is Required")]
         [Display(Name ="Department")]
        public virtual int DepartmentId { get; set; }

        [NotMapped]
        public virtual string Department { get; set; } //Department ni biji bathi field ne access karva 
        //atle departmentid ni jagyaye department nu name show karva
    }
}
