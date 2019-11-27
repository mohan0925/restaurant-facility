using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestaurantDetails.App_Code;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantDetails
{
    public partial class UserAccount : System.Web.UI.Page
    {
        public string UserName, UserPassword, RealName, EmailAddress, RoleID, RestaurantID;

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            lblUpdateSuccess.Text = "";
        }

        // Updating the sessions 
        protected void btnUpdateChefRestaurant_Click(object sender, EventArgs e)
        {
            UserName = Session["UserName"].ToString();
            UserPassword = Session["UserPassword"].ToString();
            RealName = Session["RealName"].ToString();
            EmailAddress = Session["EmailAddress"].ToString();
            RoleID = Session["RoleID"].ToString();
            RestaurantID = Session["RestaurantID"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblUpdateSuccess.Text = "";

            // checking if sessions exists or not
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                int role= Convert.ToInt32(Session["RoleID"].ToString());
                // Hiding buttons based on roles
                if (role == 2)
                {
                    btnUserDetails.Visible = false;
                    btnChefDetails.Visible = true;
                    btnUpdateChefRestaurant.Visible = true;
                }
                else
                {
                    btnUserDetails.Visible = true;
                    btnChefDetails.Visible = false;
                    btnUpdateChefRestaurant.Visible = false;
                }

                // assigning sessions if credentials are correct
                UserName = Session["UserName"].ToString();
                UserPassword = Session["UserPassword"].ToString();
                RealName = Session["RealName"].ToString();
                EmailAddress = Session["EmailAddress"].ToString();
                RoleID = Session["RoleID"].ToString();
                RestaurantID = Session["RestaurantID"].ToString();

                string restaurant_update = Request.QueryString["UpdateSuccessful"];
                string password_update = Request.QueryString["UpdateSuccess"];
                if (restaurant_update != null)
                {
                    lblWelcome.Text = "Welcome  " + RealName + "   to your account";
                    Label1.Text = " Restaurant updated successfully..";
                    lblUpdateSuccess.Text = " ";
                }
                else if (password_update != null)
                {
                    lblWelcome.Text = "Welcome  " + RealName + "   to your account";
                    Label1.Text = " Password updated successfully..";
                    lblUpdateSuccess.Text = " ";
                }
                else
                {
                    // Write welcome note with username 
                    lblWelcome.Text = "Welcome  " + RealName + "   to your account";
                    lblUpdateSuccess.Text = " Login successfull";
                }  
            }
        }

        protected void btnUserDetails_Click(object sender, EventArgs e)
        {
            lblUpdateSuccess.Text = "";
        }

        protected void btnChefDetails_Click(object sender, EventArgs e)
        {
            lblUpdateSuccess.Text = "";
        }
    }
}