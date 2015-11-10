<Query Kind="Statements">
  <Connection>
    <ID>a4ec4e60-38cc-4562-b7f9-f8c00192e61d</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

var date = Bills.Max( thebill=> thebill.BillDate);
var justdate = Bills.Max(thebill=>thebill.BillDate).Date;
var newtime = Bills.Max(thebill=>thebill.BillDate).TimeOfDay.Add(new TimeSpan(0,30,0));


var step1 = from data in Tables
             select new
             {
                Table = data.TableNumber,
                Seating = data.Capacity,
                // This sub-query gets the bills for walk-in customers
                WalkIns = from walkIn in data.Bills
                        where 
                               walkIn.BillDate.Year == date.Year
                            && walkIn.BillDate.Month == date.Month
                            && walkIn.BillDate.Day == date.Day
                            && walkIn.BillDate.TimeOfDay <= newtime
                            && (!walkIn.OrderPaid.HasValue || walkIn.OrderPaid.Value >= newtime)
//                          && (!walkIn.PaidStatus || walkIn.OrderPaid >= time)
                        select walkIn,
                 // This sub-query gets the bills for reservations
                 Reservations = from booking in data.ReservationTables
                        from reservationParty in booking.Reservation.Bills
                        where 
                               reservationParty.BillDate.Year == date.Year
                            && reservationParty.BillDate.Month == date.Month
                            && reservationParty.BillDate.Day == date.Day
                            && reservationParty.BillDate.TimeOfDay <= newtime
                            && (!reservationParty.OrderPaid.HasValue || reservationParty.OrderPaid.Value >= newtime)
//                          && (!reservationParty.PaidStatus || reservationParty.OrderPaid >= time)
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
                                    BillTotal = info.BillItems.Sum(bi => bi.Quantity * bi.SalePrice),
                                    Waiter = info.Waiter.FirstName,
                                    Reservation = info.Reservation
                                }
            };
step2.Dump();

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
step3.Dump();


var step4 = from data in step3
            select new // SeatingSummary() // the DTO class to use in my BLL
            {
                Table = data.Table,
                Seating = data.Seating,
                Taken = data.Taken,
                // use a ternary expression to conditionally get the bill id (if it exists)
                BillID = data.Taken ?               // if(data.Taken)
                         data.CommonBilling.BillID  // value to use if true
                       : (int?) null,               // value to use if false
                BillTotal = data.Taken ? 
                            data.CommonBilling.BillTotal : (decimal?) null,
                Waiter = data.Taken ? data.CommonBilling.Waiter : (string) null,
                ReservationName = data.Taken ?
                                  (data.CommonBilling.Reservation != null ?
                                   data.CommonBilling.Reservation.CustomerName : (string) null)
                                : (string) null
            };
step4.Dump();