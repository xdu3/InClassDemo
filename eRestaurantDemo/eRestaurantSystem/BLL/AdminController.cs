using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces 

using eRestaurantSystem.DAL;
using eRestaurantSystem.DAL.Entities;
using System.ComponentModel;//for your object Data Source 

#endregion 

namespace eRestaurantSystem.BLL
{
    [DataObject]
    public class AdminController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]//let user have to pick the method
        public List<SpecialEvent> SpecialEvents_List()
        {
            //connect to our DbContext class in the DAL 
            // create a instance of the class
            //we will use the transaction to hold our query
            using (var context = new eRestaurantContext())//using is transaction 
            {
                //method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList();
                var results = from item in context.SpecialEvents
                              orderby item.Description
                              select item;
                return results.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]//let user have to pick the method
        public List<Reservation> GetReservationsByEventCode(string eventcode)//always lower case.
        {            
            using (var context = new eRestaurantContext())//using is transaction 
            {
                //query syntax
                var results = from item in context.Reservations
                              where item.EventCode.Equals(eventcode)
                              orderby item.CustomerName, item.ReservationDate
                              select item;
                return results.ToList();
            }
        }
    }
}
