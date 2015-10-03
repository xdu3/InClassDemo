using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces 

using eRestaurantSystem.DAL;
using eRestaurantSystem.DAL.Entities;
using System.ComponentModel;
using eRestaurantSystem.DAL.DTOs;//for your object Data Source 
using eRestaurantSystem.DAL.POCOs;

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

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<ReservationByDate> GetReservtaionByDate(string reservationOnDate)
        {
            using (var context = new eRestaurantContext())
            {
                //Linq is not very playful or cooperatice with datetime 
                // extract the year, month and dat ourselves 
                //out of the passed paramater value

                var theYear = (DateTime.Parse(reservationOnDate)).Year;
                var theMonth = (DateTime.Parse(reservationOnDate)).Month;
                var theDay = (DateTime.Parse(reservationOnDate)).Day;

                var result = from eventItem in context.SpecialEvents
                             orderby eventItem.Description
                             select new ReservationByDate()// a new instance for each specialevent row on the table.
                             {
                                 Description = eventItem.Description,
                                 Reservations = from row in eventItem.Reservations
                                                where row.ReservationDate.Year == theYear
                                                && row.ReservationDate.Month == theMonth
                                                && row.ReservationDate.Day == theDay
                                                select new ReservationDetail()//a new for each reservation of a particlar specialEvent code
                                                {
                                                    CustomerName = row.CustomerName,
                                                    ReservationDate = row.ReservationDate,
                                                    NumberInParty = row.NumberInParty,
                                                    ContactPhone = row.ContactPhone,
                                                    ReservationStatus = row.ReservationStatus
                                                }
                             };
                return result.ToList();

            }
        }
    }
}
