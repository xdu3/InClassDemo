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
        if (!Page.IsPostBack)
        {
            HireDateTextBox.Text = DateTime.Today.ToShortDateString();
            RefreshWaiterList("0");
        }
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
    protected void WaiterInsert_Click(object sender, EventArgs e)
    {
        //inline version of using MessageUserControl
        MessageUserControl.TryRun
            (
                ()=>
                    //remainder of the code is waht would have gone in the 
                    //external method of (processRequest(MethodNum))
                {
                    Waiter item = new Waiter();
                    item.FirstName = FirstNameTextBox.Text;
                    item.LastName = LastNameTextBox.Text;
                    item.Address = AddressTextBox.Text;
                    item.Phone = PhoneTextBox.Text;
                    item.HireDate = DateTime.Parse(HireDateTextBox.Text);

                    if(string.IsNullOrEmpty(ReleaseDateTextBox.Text))
                    {
                        item.ReleaseDate = null;
                    }
                    else
                    {
                        item.ReleaseDate = DateTime.Parse(ReleaseDateTextBox.Text);
                    }
                    AdminController sysmgr = new AdminController();
                    IDLabel.Text = sysmgr.Waiters_Add(item).ToString();
                    MessageUserControl.ShowInfo("Waiter added.");
                    RefreshWaiterList(IDLabel.Text);
                }
                
            );
    }
    protected void RefreshWaiterList(string selectedValue)
    {
        //force the re-execution of the quary for the drop down list
        WaiterList.DataBind();
        //insert the prompt line into the drop down list data
        WaiterList.Items.Insert(0, "Select a waiter.");
        //position the waiterlist to the desired row representing the waiter
        WaiterList.SelectedValue = selectedValue;
    }
    protected void WaiterUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(IDLabel.Text))
        {
            MessageUserControl.ShowInfo("Please select a waiter first before updating.");
        }
        else
        {
            //standard update process
            MessageUserControl.TryRun
           (
               () =>
               //remainder of the code is waht would have gone in the 
               //external method of (processRequest(MethodNum))
               {
                   Waiter item = new Waiter();
                   item.WaiterID = int.Parse(IDLabel.Text);
                   item.FirstName = FirstNameTextBox.Text;
                   item.LastName = LastNameTextBox.Text;
                   item.Address = AddressTextBox.Text;
                   item.Phone = PhoneTextBox.Text;
                   item.HireDate = DateTime.Parse(HireDateTextBox.Text);

                   if (string.IsNullOrEmpty(ReleaseDateTextBox.Text))
                   {
                       item.ReleaseDate = null;
                   }
                   else
                   {
                       item.ReleaseDate = DateTime.Parse(ReleaseDateTextBox.Text);
                   }
                   AdminController sysmgr = new AdminController();
                   sysmgr.Waiter_Update(item);
                   MessageUserControl.ShowInfo("Waiter updated.");
                   RefreshWaiterList(IDLabel.Text);
               }
           );
        }
    }
}