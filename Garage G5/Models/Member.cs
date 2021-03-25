using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MembershipType MembershipType { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string PersonalIdNumber { get; set; }
        public DateTime DateAdded { get ; set; }
        public DateTime BonusAccountExpires { get; set; }

        public string FullName { get => FirstName + " " + LastName; }

    }
}
