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
    public class Table
    {
        [Key]
        public int TableID { get; set; }
        public byte TableNumber { get; set; }//tinyint in sql
        public bool Smoking { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }

        //Navigation properties
        //The Reservations table (sql ) is a many to many relationship the the Table table (sql)

        //Sql solves this problems by having an associate table 
        // that as a compound primary key created from Reservations and Tables.
        //we will not be creating an entity for this associate table 
        //Instead we will creat on overload map in our Dbcontent class
        //however we can still create the virtual navigation property to
        // accomondate this relationship
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
