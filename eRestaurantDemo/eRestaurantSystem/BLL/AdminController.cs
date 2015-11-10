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
using System.Data.Entity;
#endregion 

namespace eRestaurantSystem.BLL
{
    [DataObject]
    public class AdminController
    {
        #region Queries


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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ReservationByDate> GetReservtaionByDate(string reservationDate)
        {
            using (var context = new eRestaurantContext())
            {
                //Linq is not very playful or cooperatice with datetime 
                // extract the year, month and dat ourselves 
                //out of the passed paramater value

                int theYear = (DateTime.Parse(reservationDate).Year);
                int theMonth = (DateTime.Parse(reservationDate).Month);
                int theDay = (DateTime.Parse(reservationDate).Day);

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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategoryItems> MenuCategoryItems_List()
        {
            using (var context = new eRestaurantContext())
            {

                var results = from menuItem in context.MenuCategories
                              orderby menuItem.Description
                              select new MenuCategoryItems()// a new instance for each specialevent row on the table.
                              {
                                  Description = menuItem.Description,
                                  MenuItems = from row in menuItem.MenuItems
                                              select new MenuItem()//a new for each reservation of a particlar specialEvent code
                                              {
                                                  Description = row.Description,
                                                  Price = row.CurrentPrice,
                                                  Calories = row.Calories,
                                                  Comment = row.Comment
                                              }
                              };
                return results.ToList();

            }
        }
        #endregion
        #region Add, Update, Delete of CRUD for CQRS

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void SpecialEvent_Add(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                SpecialEvent added = null;
                added = context.SpecialEvents.Add(item);
                //comment is not used until it is actully save
                context.SaveChanges();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void SpecialEvent_Update(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                context.Entry<SpecialEvent>(context.SpecialEvents.Attach(item)).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void SpecialEvent_Delete(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                SpecialEvent existing = context.SpecialEvents.Find(item.EventCode);
                context.SpecialEvents.Remove(existing);

                context.SaveChanges();
            }

        }

        #endregion

        #region
        [DataObjectMethod(DataObjectMethodType.Select, false)]//let user have to pick the method
        public List<Waiter> Waiters_List()
        {
            //connect to our DbContext class in the DAL 
            // create a instance of the class
            //we will use the transaction to hold our query
            using (var context = new eRestaurantContext())//using is transaction 
            {
                //method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList();
                var results = from item in context.Waiters
                              orderby item.LastName, item.FirstName
                              select item;
                return results.ToList();//none, 1 or more
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]//let user have to pick the method
        public Waiter GetWaiterByID(int waiterID)
        {
            //connect to our DbContext class in the DAL 
            // create a instance of the class
            //we will use the transaction to hold our query
            using (var context = new eRestaurantContext())//using is transaction 
            {
                //method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList();
                var results = from item in context.Waiters
                              where item.WaiterID == waiterID
                              select item;
                return results.FirstOrDefault();//one row at most
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Waiters_Add(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                Waiter added = null;
                added = context.Waiters.Add(item);
                //comment is not used until it is actully save
                context.SaveChanges();

                //the waiter instence added contains the newly inserted 
                //record to sql including the genrated pkey value
                return added.WaiterID;
            }

        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Waiter_Update(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                context.Entry<Waiter>(context.Waiters.Attach(item)).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Waiter_Delete(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                Waiter existing = context.Waiters.Find(item.WaiterID);
                context.Waiters.Remove(existing);

                context.SaveChanges();
            }

        }

        #endregion


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CategoryMenuItems> GetReportCategoryMenuItems()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var results = from cat in context.Items
                              orderby cat.Category.Description, cat.Description
                              select new CategoryMenuItems
                              {
                                  CategoryDescription = cat.Category.Description,
                                  ItemDescription = cat.Description,
                                  Price = cat.CurrentPrice,
                                  Calories = cat.Calories,
                                  Comment = cat.Comment
                              };

                return results.ToList(); // this was .Dump() in Linqpad
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<WaiterBilling> GetWaiterBillingReport()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var results =from abillrow in context.Bills
                                    where abillrow.BillDate.Month == 5
                                    orderby abillrow.BillDate,
                                            abillrow.Waiter.LastName,
                                            abillrow.Waiter.FirstName
                             select new WaiterBilling()
                                        {
                                            BillDate = abillrow.BillDate.Year
                                                        +"/"+
                                                        abillrow.BillDate.Month
                                                        + "/" +
                                                        abillrow.BillDate.Day,
                                            WaiterName = abillrow.Waiter.LastName + ", " + abillrow.Waiter.FirstName,
                                            BillID = abillrow.BillID,
                                            BillTotal = abillrow.Items.Sum(eachbillitemrow =>
                                                eachbillitemrow.Quantity * eachbillitemrow.SalePrice),
                                            PartySize = abillrow.NumberInParty,
                                            Contact = abillrow.Reservation.CustomerName
                                        };

                return results.ToList();
            }
        }

        #region FrountDesk
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DateTime GetLastBillDateTime()
        {
            using (var context = new eRestaurantContext())
            {
                var result = 
                    context.Bills.Max(eachBillrow => eachBillrow.BillDate);
                return result;
            }
        }
        //=======================================================================
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ReservationCollection> ReservationsByTime(DateTime date)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var result = (from data in context.Reservations
                              where data.ReservationDate.Year == date.Year
                              && data.ReservationDate.Month == date.Month
                              && data.ReservationDate.Day == date.Day
                                  // && data.ReservationDate.Hour == timeSlot.Hours
                              && data.ReservationStatus == Reservation.Booked
                              select new ReservationSummary()
                              {
                                  ID = data.ReservationID,
                                  Name = data.CustomerName,
                                  Date = data.ReservationDate,
                                  NumberInParty = data.NumberInParty,
                                  Status = data.ReservationStatus,
                                  Event = data.Event.Description,
                                  Contact = data.ContactPhone
                              }).ToList();
                //causes excution of the query so that the retrieced data is in memory 
                var finalResult = from item in result
                                  orderby item.NumberInParty
                                  group item by item.Date.Hour into itemGroup
                                  select new ReservationCollection()
                                  {
                                      Hour = itemGroup.Key,
                                      Reservations = itemGroup.ToList()
                                  };
                return finalResult.OrderBy(x => x.Hour).ToList();//methods syntax 
            }
        }

        [DataObjectMethod (DataObjectMethodType.Select,false)]
        public List<SeatingSummary> SeatingByDateTime (DateTime date, TimeSpan newtime)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var step1 = from eachTableRow in context.Tables
                            select new
                            {
                                Table = eachTableRow.TableNumber,
                                Seating = eachTableRow.Capacity,
                                // This sub-query gets the bills for walk-in customers
                                WalkIns = from walkIn in eachTableRow.Bills
                                          where
                                                 walkIn.BillDate.Year == date.Year
                                              && walkIn.BillDate.Month == date.Month
                                              && walkIn.BillDate.Day == date.Day
                                              //&& walkIn.BillDate.TimeOfDay <= newtime
                                               && DbFunctions.CreateTime(walkIn.BillDate.Hour,
                                               walkIn.BillDate.Minute, walkIn.BillDate.Second) <= newtime
                                              && (!walkIn.OrderPaid.HasValue || walkIn.OrderPaid.Value >= newtime)
                                          // && (!walkIn.PaidStatus || walkIn.OrderPaid >= time)
                                          select walkIn,
                                // This sub-query gets the bills for reservations
                                Reservations = from booking in eachTableRow.Reservations
                                               from reservationParty in booking.Bills
                                               where
                                                      reservationParty.BillDate.Year == date.Year
                                                   && reservationParty.BillDate.Month == date.Month
                                                   && reservationParty.BillDate.Day == date.Day
                                                   // linq don't like date time very good 
                                                  //&& reservationParty.BillDate.TimeOfDay <= newtime
                                                  //inside system.data.entity is a class of function that 
                                                  //will help with DateTime/TimeSpan concerns 
                                                  && DbFunctions.CreateTime(reservationParty.BillDate.Hour,
                                                  reservationParty.BillDate.Minute,
                                                  reservationParty.BillDate.Second) <= newtime
                                                   && (!reservationParty.OrderPaid.HasValue || reservationParty.OrderPaid.Value >= newtime)
                                               // && (!reservationParty.PaidStatus || reservationParty.OrderPaid >= time)
                                               select reservationParty
                            };


                var step2 = from dataRow in step1.ToList() // .ToList() forces the first result set to be in memory
                            select new
                            {
                                Table = dataRow.Table,
                                Seating = dataRow.Seating,
                                CommonBilling = from info in dataRow.WalkIns.Union(dataRow.Reservations)
                                                select new // info
                                                {
                                                    BillID = info.BillID,
                                                    BillTotal = info.Items.Sum(bi => bi.Quantity * bi.SalePrice),
                                                    Waiter = info.Waiter.FirstName,
                                                    Reservation = info.Reservation
                                                }
                            };

                // Step 3 - Get just the first CommonBilling item
                //         (presumes no overlaps can occur - i.e., two groups at the same table at the same time)
                var step3 = from data in step2.ToList()
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.CommonBilling.Count() > 0,
                                // .FirstOrDefault() is effectively "flattening" my collection of 1 item into a 
                                // single object whose properties I can get in step 4 using the dot (.) operator
                                CommonBilling = data.CommonBilling.FirstOrDefault()
                            };


                var step4 = from data in step3
                            select new SeatingSummary() // the DTO class to use in my BLL
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.Taken,
                                // use a ternary expression to conditionally get the bill id (if it exists)
                                BillID = data.Taken ?               // if(data.Taken)
                                         data.CommonBilling.BillID  // value to use if true
                                       : (int?)null,               // value to use if false
                                BillTotal = data.Taken ?
                                            data.CommonBilling.BillTotal : (decimal?)null,
                                Waiter = data.Taken ? data.CommonBilling.Waiter : (string)null,
                                ReservationName = data.Taken ?
                                                  (data.CommonBilling.Reservation != null ?
                                                   data.CommonBilling.Reservation.CustomerName : (string)null)
                                                : (string)null
                            };
                return step4.ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<WaiterOnDuty> ListWaiters()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var result = from person in context.Waiters
                             where person.ReleaseDate == null
                             select new WaiterOnDuty()
                             {
                                 WaiterId = person.WaiterID,
                                 FullName = person.FirstName + " " + person.LastName
                             };
                return result.ToList();
            }
        }
        #endregion
    }
}
