
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rad3012223.ClubData.ClassLibrary.Week5.Models
{
    public class Student
    {
        [Key]
        public string StudentID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

    }
}
