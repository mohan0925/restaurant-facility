using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using RestaurantDetails.App_Code;
using System.Data;

namespace RestaurantDetails
{
    public partial class WaiterRegistration : System.Web.UI.Page
    {
        // to check regular expression for email
        public bool checkforEmail(String email)
        {
            bool isValid = false;
            Regex rEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (rEmail.IsMatch(email))
                isValid = true;
            return isValid;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load data if page is not postback
            if (!Page.IsPostBack)
            {
                //create instane of middle layer business object
                Restaurant rest_class = new Restaurant();
                UserRole user_role_class = new UserRole();

                //retrieve restaurants from middle layer into a DataTable
                DataTable rest_dt = rest_class.getAllRestaurants();
                DataTable User_role_dt = user_role_class.getAllroles();

                //check if query was successful
                if (rest_dt != null && rest_dt.Rows.Count>0 && User_role_dt!= null && User_role_dt.Rows.Count>0)
                {
                    //set DropDownList's data source to the DataTable
                    ddlRestaurants0.DataSource = rest_dt;
                    ddlRestaurants.DataSource = User_role_dt;

                    //assign DepartmentID database field to the value property
                    ddlRestaurants0.DataValueField = "RestaurantID";
                    ddlRestaurants.DataValueField = "RoleId";

                    //assign DepartmentName database field to the text property
                    ddlRestaurants0.DataTextField = "RestaurantName";
                    ddlRestaurants.DataTextField = "RoleName";

                    //bind data
                    ddlRestaurants0.DataBind();
                    ddlRestaurants.DataBind();
                }
                else
                {
                    lblError.Text = "Database connection error - cannot display Restaurants.";
                }

            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string username, password, real_name, email_add;
           
                Console.WriteLine("email_valid", checkforEmail(txtEmailAddress.Text.ToString()));

                if (txtUsername.Text.Length < 5 || txtUsername.Text.Length > 20)
                {
                    lblError.Text = "User name should be greater than 5 or less than 20";
                }
                else if (txtPassword.Text.Length < 6)
                {
                    lblError.Text = "password should be 6 character";
                }
                else if (!txtPassword.Text.Equals(txtConfirmPassword.Text))
                {
                    lblError.Text = "Password did not match";
                }
                else if (String.IsNullOrWhiteSpace(txtRealName.Text))
                {
                    lblError.Text = "please enter full name";
                }
                else if (String.IsNullOrWhiteSpace(txtEmailAddress.Text) || !checkforEmail(txtEmailAddress.Text.ToString()))
                {
                    lblError.Text = "please enter valid email";
                }
                else
                {              
                    lblError.Text = "";

                    RestaurantUser rest_user = new RestaurantUser();

                    //set property, so it can be used as a parameter for the query
                    rest_user.UserName = txtUsername.Text;


                    //check if username exists
                    if (rest_user.userNameExists())
                    {
                        //already exists so output error
                        lblError.Text = "Username already exists, please select another";
                    }
                    else
                    {
                        //midle layar  = //presentation layar
                        rest_user.UserName = txtUsername.Text;
                        rest_user.UserPassword = txtPassword.Text;
                        rest_user.RealName = txtRealName.Text;
                        rest_user.EmailAddress = txtEmailAddress.Text;
                        rest_user.RoleID = Int32.Parse(ddlRestaurants.SelectedValue);
                        rest_user.RestaurantID = Int32.Parse(ddlRestaurants0.SelectedValue);

                        //attempt to add a worker and test if it is successful
                        if (rest_user.addUser())
                        {
                            System.Threading.Thread.Sleep(2000);
                            //redirect user to login page
                            //redirect user to login page
                            Response.Redirect("~/UserLogin.aspx?Registration=Successfull");
                        }
                        else
                        {
                            //exception thrown so display error
                            lblError.Text = "Database connection error - failed to insert record.";
                        }
                    }

                }
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            UserName_exists.Text = "";

            RestaurantUser rest_user = new RestaurantUser();

            //set property, so it can be used as a parameter for the query
            rest_user.UserName = txtUsername.Text;

            //check if username exists
            if (rest_user.userNameExists())
            {
                //already exists so output error
                UserName_exists.Text = "Username already exists...!";
                UserName_exists.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                UserName_exists.Text = "Username Available...!";
                UserName_exists.ForeColor = System.Drawing.Color.Green;
            }
        }

        protected void txtEmailAddress_TextChanged(object sender, EventArgs e)
        {
            EmailExist.Text = "";

            RestaurantUser rest_user = new RestaurantUser();

            //set property, so it can be used as a parameter for the query
            rest_user.EmailAddress = txtEmailAddress.Text;

            //check if username exists
            if (rest_user.emailExists())
            {
                //already exists so output error
                EmailExist.Text = "Email already exists...!";
                EmailExist.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                EmailExist.Text = "Email Available...!";
                EmailExist.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}