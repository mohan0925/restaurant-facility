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
    public partial class WaiterDetails : System.Web.UI.Page
    {
        RestaurantUser rest_user_class = new RestaurantUser();
        Restaurant rest_class = new Restaurant();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblEmail.Text = "";
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                // Load the data if it's not a postback
                if (!Page.IsPostBack)
                {
                    // Convert restaurant id to integer
                    int RestaurantID = Convert.ToInt32(Session["RestaurantID"].ToString());
                    int RoleID = 2;

                    rest_class.RestaurantID = RestaurantID;
                    rest_user_class.RoleID = RoleID;
                    rest_user_class.RestaurantID = RestaurantID;

                    //retrieve departments from middle layer into a DataTable
                    DataTable dt_restaurant = rest_class.getRestaurant();
                    DataTable dt_rest_chefs = rest_user_class.getChefdetails();

                    ////check if query was successful
                    if (dt_restaurant != null && dt_rest_chefs != null)
                    {
                        txtRestaurant.Text = dt_restaurant.Rows[0]["RestaurantName"].ToString();

                        lstChefs.DataSource = dt_rest_chefs;

                        //assign WorkerID database field to the value property
                        lstChefs.DataValueField = "UserID";

                        //assign WorkerName database field to the text property
                        lstChefs.DataTextField = "RealName";

                        //bind data
                        lstChefs.DataBind();
                    }
                    else
                    {
                        lblError.Text = "Database connection error - cannot display RestaurantName.";
                    }
                    txtRestaurant.Enabled = false;
                }
            }         
        }

        protected void btnShowEmail_Click(object sender, EventArgs e)
        {
            // condition to check if item is selected or not
            if (lstChefs.SelectedIndex == -1)
            {
                lblError.Text = "You must select a chef to view email.";
            }
            else
            {
                string curItem = lstChefs.SelectedItem.ToString();
                string curItem_val = lstChefs.SelectedValue.ToString();

                
                int UserID = Convert.ToInt32(curItem_val.ToString()); 
                rest_user_class.UserID = UserID;

                // data table to get email
                DataTable dt_rest_chefs = rest_user_class.getEmail();

                // check whether the datatable is not empty
                if (dt_rest_chefs != null && dt_rest_chefs.Rows.Count>0)
                {
                    System.Threading.Thread.Sleep(2000);
                    lblEmail.Text = dt_rest_chefs.Rows[0]["EmailAddress"].ToString();
                }
                else
                {
                    lblEmail.Text = "Database connection error - cannot display Email.";
                }
            }
        }
    }
}