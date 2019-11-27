using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestaurantDetails.App_Code;
using System.Data;

namespace RestaurantDetails
{
    public partial class ChefDetails : System.Web.UI.Page
    {
        // Calling constructor's of different classes to establish connections
        RestaurantUser rest_user_class = new RestaurantUser();
        Restaurant rest_class = new Restaurant();
        RestaurantCuisine rest_cuisine = new RestaurantCuisine();
        Cuisine cuisine_details = new Cuisine();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                // Create a datatable to store each record from the fetched datatable
                DataTable dt = new DataTable();

                //assigning column names for data table
                dt.Columns.Add("CuisineID", typeof(Int32));
                dt.Columns.Add("CuisineRegion", typeof(string));
                dt.Columns.Add("CuisineName", typeof(string));

                if (!Page.IsPostBack)
                {
                    // converting string to integer type.
                    int RestaurantID = Convert.ToInt32(Session["RestaurantID"].ToString());
                    int RoleID = 1;
                    rest_class.RestaurantID = RestaurantID;
                    rest_user_class.RoleID = RoleID;
                    rest_user_class.RestaurantID = RestaurantID;


                    //retrieve departments from middle layer into a DataTable
                    DataTable dt_restaurant = rest_class.getRestaurant();
                    DataTable dt_rest_waiters = rest_user_class.getWaitersdetails();

                    //check if query was successful
                    if (dt_restaurant != null && dt_rest_waiters != null)
                    {
                        txtRestaurant.Text = dt_restaurant.Rows[0]["RestaurantName"].ToString();
                        int rest_id = Convert.ToInt32(dt_restaurant.Rows[0]["RestaurantID"].ToString());
                        rest_cuisine.RestaurantID = rest_id;

                        // Fetch data table for getting all the cuisines 
                        DataTable cusines_ids = rest_cuisine.getCuisines();

                        // Check whether the datatable is empty or not 
                        if (cusines_ids != null && cusines_ids.Rows.Count > 0)
                        {

                            // Loop datatable to display each record in the repeater
                            foreach (DataRow row in cusines_ids.Rows)
                            {
                                int cuisine_id = Convert.ToInt32(row["CuisineID"].ToString());
                                cuisine_details.CuisineID = cuisine_id;

                                // Fetch all the cuisines from the cuisine table to display it on the repeater
                                DataTable cusines_records = cuisine_details.getAllCuisine();
                                if (cusines_records != null && cusines_records.Rows.Count > 0)
                                {
                                    // assign the data row to insert values into the datatable
                                    DataRow dr1 = dt.NewRow();
                                    dr1["CuisineRegion"] = cusines_records.Rows[0]["CuisineRegion"].ToString();
                                    dr1["CuisineName"] = cusines_records.Rows[0]["CuisineName"].ToString();

                                    // add item array to the datatable
                                    dt.Rows.Add(dr1.ItemArray);
                                }
                                else
                                {
                                    Repeater_details.Text = "Could not fetch details.... Error with database";
                                }
                            }

                            // assign the datatable to the repeater datasource 
                            rptCuisines.DataSource = dt;
                            // bind the data to the repeater
                            rptCuisines.DataBind();
                        }
                        else
                        {
                            lblError.Text = "Database connection error - cannot Fetch Cuisine Id's.";
                            lblError.ForeColor = System.Drawing.Color.Red;
                        }

                        // Disavling the textbox from editing
                        txtRestaurant.Enabled = false;

                        lstWaiters.DataSource = dt_rest_waiters;

                        //assign WorkerID database field to the value property
                        lstWaiters.DataValueField = "UserID";

                        //assign WorkerName database field to the text property
                        lstWaiters.DataTextField = "RealName";

                        //bind data
                        lstWaiters.DataBind();
                    }
                    else
                    {
                        lblError.Text = "Database connection error - cannot display RestaurantName.";
                        lblError.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }
           
        }

        // procedure for loading the sessions and the data after the table updation.
        private void LoadWorkersList()
        {
            int RestaurantID = Convert.ToInt32(Session["RestaurantID"].ToString());
            int RoleID = 1;
            rest_user_class.RoleID = RoleID;
            rest_user_class.RestaurantID = RestaurantID;

            // Getting all the records of the waiters from the table 
            DataTable dt_rest_waiters = rest_user_class.getWaitersdetails();
            
            if (dt_rest_waiters != null)
            {
                lstWaiters.DataSource = dt_rest_waiters;

                //assign WorkerID database field to the value property
                lstWaiters.DataValueField = "UserID";

                //assign WorkerName database field to the text property
                lstWaiters.DataTextField = "RealName";

                //bind data
                lstWaiters.DataBind();
            }
            else
            {
                lblError.Text = "Database connection error - cannot delete waiter.";
                lblError.ForeColor = System.Drawing.Color.Red;
            }

        }

        // Procedure for removing the waiters from the table.
        protected void btnRemoveWaiter_Click(object sender, EventArgs e)
        {
            lblSuccess.Text = " ";
            // condition to check whether the item is selected in the listbox or not
            if (lstWaiters.SelectedIndex == -1)
            {
                lblSuccess.Text = "You must select a waiter.";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                string curItem_val = lstWaiters.SelectedValue.ToString();

                // parsing the string to integer
                int UserID = Convert.ToInt32(curItem_val.ToString());

                //passing the parameters to the data tier class for quering
                rest_user_class.UserID = UserID;

                //if this condition is true then the selected waiter will be deleted from the Restaurantuser table
                if (rest_user_class.deleteWaiter())
                {
                    System.Threading.Thread.Sleep(1000);
                    lblSuccess.Text = "Delete sucessfull";
                    lblSuccess.ForeColor = System.Drawing.Color.Green;
                    LoadWorkersList();
                }
                else
                {
                    lblSuccess.Text = "Database connection error -cannot display waiter.";
                    lblSuccess.ForeColor = System.Drawing.Color.Red;
                }
                
            }
        }

        protected void rptCuisines_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}