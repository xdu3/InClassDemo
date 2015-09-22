using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces

using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

#endregion

namespace eRestaurantSystem.DAL.Entities
{
    public class SpecialEvent
    {
        [Key]
        [Required(ErrorMessage = "An Event Code is required (only one character).")]
        [StringLength(1,ErrorMessage="Event Code is only one character in length")]
        public string EventCode { get; set; }
        [Required(ErrorMessage="Description is a required field.")]
        [StringLength(30, ErrorMessage="Description have a macimum length of 30 character")]
        public string Description { get; set; }
        public bool Active { get; set;}//it is always have a value in it... no null value. 

        // navigational virtual properties 
        // this is a parent to the Reservation entity 
        public virtual ICollection<Reservation> Reservations { get; set; }

       //default values can be set in the class constructor
        public SpecialEvent()
        {
            Active = true;
        }

    }
}
