using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Addtional Namespace
using eRestaurantSystem.BLL;
using eRestaurantSystem.DAL.Entities;
using EatIn.UI;//delegate 
#endregion


public partial class CommonPages_WaiterAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);

    }
    protected void FetchWaiter_Click(object sender, EventArgs e)
    {
        //to properly interface with ourmessage user control 
        //we will delegate the execution of this Click event
        //under the message user control
        if (WaiterList.SelectedIndex==0)
        {
            //issue our own error message
            MessageUserControl.ShowInfo("Please select a waiter to process.");
        }
        else
        {
            //excute the necessary standard lookup code under the control of 
            //the MessageUserControl
            MessageUserControl.TryRun((ProcessRequest)GetWaiterInfo);

        }

    }


    public void GetWaiterInfo()
    {
        // a standard lookup process
        AdminController sysmgr = new AdminController();
        var waiter = sysmgr.GetWaiterByID(int.Parse(WaiterList.SelectedValue));
        IDLabel.Text = waiter.WaiterID.ToString();
        FirstNameTextBox.Text = waiter.FirstName;
        LastNameTextBox.Text = waiter.LastName;
        PhoneTextBox.Text = waiter.Phone;
        AddressTextBox.Text = waiter.Address;
        HireDateTextBox.Text = waiter.HireDate.ToShortDateString();
        if(waiter.ReleaseDate.HasValue)
        {
            ReleaseDateTextBox.Text= waiter.ReleaseDate.ToString();
        }
        else
        {
            ReleaseDateTextBox.Text = "";
        }

    }
}