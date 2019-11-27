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
    public partial class UpdateChefRestaurant : System.Web.UI.Page
    {
        string UserName;
        int RestaurantID, NewRestaurantID;

        // call constructor to establish connections with database
        RestaurantUser rest_user_class = new RestaurantUser();
        Restaurant rest_class = new Restaurant();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                Restaurant rest_class = new Restaurant();
                UserName = Session["UserName"].ToString();
                RestaurantID = Convert.ToInt32(Session["RestaurantID"].ToString());

                // check if page is not a postback
                if (!Page.IsPostBack)
                {
                    int RestaurantID = Convert.ToInt32(Session["RestaurantID"].ToString());
                    rest_class.RestaurantID = RestaurantID;

                    //retrieve departments from middle layer into a DataTable
                    DataTable dt_restaurant = rest_class.getRestaurant();
                    DataTable dt_all_restaurant = rest_class.getAllRestaurant();

                    //check if query was successful
                    if (dt_restaurant != null && dt_restaurant.Rows.Count > 0 && dt_all_restaurant != null && dt_all_restaurant.Rows.Count > 0)
                    {
                        txtRestaurant.Text = dt_restaurant.Rows[0]["RestaurantName"].ToString();
                        txtRestaurant.Enabled = false;
                        lstRestaurants.DataSource = dt_all_restaurant;

                        //assign WorkerID database field to the value property
                        lstRestaurants.DataValueField = "RestaurantID";

                        //assign WorkerName database field to the text property
                        lstRestaurants.DataTextField = "RestaurantName";

                        //bind data
                        lstRestaurants.DataBind();

                    }
                    else
                    {
                        lblError.Text = "Database connection error - cannot display RestaurantName.";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        private void UpdatedRestaurantList()
        {
            txtRestaurant.Enabled = true;
            rest_user_class.UserName = UserName;

            // Fetch data from the restaurantUser table to load all the data 
            DataTable dt_chef_details = rest_user_class.Userdetails();

            if (dt_chef_details != null && dt_chef_details.Rows.Count>0)
            {
                // assign session with the new updated data 
                Session["UserName"] = dt_chef_details.Rows[0]["UserName"].ToString();
                Session["UserPassword"] = dt_chef_details.Rows[0]["UserPassword"].ToString();
                Session["RealName"] = dt_chef_details.Rows[0]["RealName"].ToString();
                Session["EmailAddress"] = dt_chef_details.Rows[0]["EmailAddress"].ToString();
                Session["RoleID"] = dt_chef_details.Rows[0]["RoleID"].ToString();
                Session["RestaurantID"] = dt_chef_details.Rows[0]["RestaurantID"].ToString();

                int RestaurantID = Convert.ToInt32(Session["RestaurantID"].ToString());
                rest_class.RestaurantID = RestaurantID;

                // Fetch chef restaurant and all the other restaurants present
                DataTable dt_restaurant = rest_class.getRestaurant();
                DataTable dt_all_restaurant = rest_class.getAllRestaurant();

                if (dt_restaurant != null && dt_restaurant.Rows.Count>0 && dt_all_restaurant != null && dt_all_restaurant.Rows.Count>0)
                {
                    txtRestaurant.Text = dt_restaurant.Rows[0]["RestaurantName"].ToString();
                    txtRestaurant.Enabled = false;
                    lstRestaurants.DataSource = dt_all_restaurant;

                    //assign WorkerID database field to the value property
                    lstRestaurants.DataValueField = "RestaurantID";

                    //assign WorkerName database field to the text property
                    lstRestaurants.DataTextField = "RestaurantName";

                    //bind data
                    lstRestaurants.DataBind();
                    System.Threading.Thread.Sleep(1000);
                    lblError.Text = "Restaurants Updated";
                    lblError.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("~/UserAccount.aspx?UpdateSuccessful=" + txtRestaurant.Text);
                }
                else
                {
                    lblError.Text = "Database connection error - cannot display RestaurantNames.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblError.Text = "Database connection error while updating restaurant.";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Procedure to load the data with the updated sessions
        protected void btnUserAccount_Click(object sender, EventArgs e)
        {

            rest_user_class.UserName = UserName;
            DataTable dt_chef_details = rest_user_class.Userdetails();
            if (dt_chef_details != null && dt_chef_details.Rows.Count > 0)
            {
                Session["UserName"] = dt_chef_details.Rows[0]["UserName"].ToString();
                Session["UserPassword"] = dt_chef_details.Rows[0]["UserPassword"].ToString();
                Session["RealName"] = dt_chef_details.Rows[0]["RealName"].ToString();
                Session["EmailAddress"] = dt_chef_details.Rows[0]["EmailAddress"].ToString();
                Session["RoleID"] = dt_chef_details.Rows[0]["RoleID"].ToString();
                Session["RestaurantID"] = dt_chef_details.Rows[0]["RestaurantID"].ToString();
            }
            Response.Redirect("~/UserAccount.aspx");
        }
        protected void btnUpdateRestaurant_Click(object sender, EventArgs e)
        {
            // check whether the item is selected or not
            if (lstRestaurants.SelectedIndex == -1)
            {
                lblError.Text = "You must select a Restaurant to update....!";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            else
            {

                string curItem_val = lstRestaurants.SelectedValue.ToString();
                NewRestaurantID = Convert.ToInt32(curItem_val.ToString());
                rest_user_class.UserName = UserName;
                rest_user_class.RestaurantID = RestaurantID;
                rest_user_class.UpdatedRestaurantID = NewRestaurantID;

                // boolen to check whether restaurant is updated or not
                bool update = rest_user_class.updateRestaurant();

                // if true then the listbox will be loaded with the 
                if (update)
                {
                    lblError.Text = "Restaurant Updated.....!";
                    lblError.ForeColor = System.Drawing.Color.Green;
                    UpdatedRestaurantList();
                }
                else
                {
                    lblError.Text = "Database connection error -cannot Update Restaurant.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}