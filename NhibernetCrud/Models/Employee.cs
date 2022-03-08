using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NhibernetCrud.Models
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
         [Range(2, 3)]
        public virtual int Age { get; set; }

        public virtual string MaritalStatus { get; set; }

         [DefaultValue("true")]
        public virtual string Gender { get; set; }

        //  public virtual SelectList Department { get; set; }
        public virtual string Department { get; set; }
    }
}
    