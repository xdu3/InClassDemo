using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces

using eRestaurantSystem.DAL.Entities;
//using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

#endregion

namespace eRestaurantSystem.DAL
{
    //this class should only be accessable from classes inside this component library
    //therefore the access level for this class will be internal 
    //this class will inherit form DbContext (Entity framworks)
    internal class eRestaurantContext : DbContext
    {
        //create a constructor which will test the connention string 
        // name to the DbContext 
        public eRestaurantContext() : base("name-EatIn")
        {

        }

        //set of mapping DbSet<T> property
        //mapping an entity to a database table 
        public DbSet<SpecialEvent> SpecialEvents { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Table> Tables { get; set; }

        //when overriding the OnModelCreating(), it is important
        // to remember to call the base method's implementation 
        //before you exit the method 

        // the ManyToManyNavigationPropertyConfiguration.Map method 
        //lets you configure the tables and columns used for this many to many relationship.

        // it takes a ManyToManyNavigationPropertyConfiguration instance 
        // in which you specify the column names by calling the MapLeftKey, MapRightKey, and ToTalbe methods 

        // the "left" key is the one specified in the HasMany method 
        //the "right" key is the one specified in the WithMany method

        // this navigation replaces the sql associated talbe that breaks up a many to many relationship
        //this technique should ONLY be used if the associated table// in sql has ONLY a compound primary key and NO non-key 
        //arrtibutes 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //sql many to many relationship 
            modelBuilder
                .Entity<Reservation>().HasMany(r => r.Tables)
                .WithMany(t => t.Reservations)
                .Map(mapping =>
                {
                    mapping.ToTable("ReservationTables");
                    mapping.MapLeftKey("ReservationId");
                    mapping.MapRightKey("TalbeID");
                }
                );
            base.OnModelCreating(modelBuilder);// DO NOT REMOVE
        }
    }
}
