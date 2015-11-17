using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRestaurantSystem.BLL;
using eRestaurantSystem.DAL.Entities;
using eRestaurantSystem.DAL.DTOs;
using eRestaurantSystem.DAL.POCOs;
#endregion

public partial class UXPage_FrontDesk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void MockLastBillingDateTime_Click(object sender, EventArgs e)
    {
        AdminController sysmgr=new AdminController();
        DateTime info = sysmgr.GetLastBillDateTime();
        SearchDate.Text = info.ToString("yyyy-MM-dd");
        SearchTime.Text = info.ToString("HH:mm");
    }
    protected void SeatingGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //extract the talbe number, number in party and the waiter ID from the gridview we will also create the time from the MockDateTime controls arthe top of this form.(typically you would use DateTime.Todat for current datetime.)
        //once the data is collected thn it will be sent to the BLL for processing the command will be done under the control of the MessageUserControl 
        // so if there is an error, the MUC can handle it.
        // we sill use the in-line MUC TryRun techinal
        MessageUserControl.TryRun(()=>
            {
                GridViewRow agvrow = SeatingGridView.Rows[e.NewSelectedIndex];
                //accessing a web control on the gridview row uses.findControl("xxx")
                // uses .FindControl("xxx") as datatype
                string tablenumber = (agvrow.FindControl("TableNumber") as Label).Text;
                string numberinparty = (agvrow.FindControl("NumberInParty") as TextBox).Text;
                string waiterid = (agvrow.FindControl("WaiterList") as DropDownList).SelectedValue;
                DateTime when = DateTime.Parse(SearchDate.Text).Add(TimeSpan.Parse(SearchTime.Text));
                
                //standard call to insert a record into the database 
                AdminController sysmgr = new AdminController();
                sysmgr.SeatCustomer(when, byte.Parse(tablenumber), int.Parse(numberinparty), int.Parse(waiterid));

                SeatingGridView.DataBind();
            },"Customer Seated","New walk-in customer has been saved");
    }
}