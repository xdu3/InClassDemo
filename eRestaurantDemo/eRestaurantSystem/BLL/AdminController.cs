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
        #endregion
    }
}
