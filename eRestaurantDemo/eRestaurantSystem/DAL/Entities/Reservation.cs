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
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReservationData { get; set; }
        public int NameInParty { get; set; }
        public string ContactPhone { get; set; }
        public string ReservationStatus { get; set; }
        public string EventCode { get; set; }

        //Navigation properties
        public virtual SpecialEvent Event { get; set; }
        //The Reservations table (sql ) is a many to many relationship the the Table table (sql)

        //Sql solves this problems by having an associate table 
        // that as a compound primary key created from Reservations and Tables.
        //we will not be creating an entity for this associate table 
        //Instead we will creat on overload map in our Dbcontent class
        //however we can still create the virtual navigation property to
        // accomondate this relationship
        public virtual ICollection<Table> Tables { get; set; }
    }
}
