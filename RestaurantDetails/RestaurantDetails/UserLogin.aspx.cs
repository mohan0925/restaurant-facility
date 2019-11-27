using System;
using RestaurantDetails.App_Code;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantDetails
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string registration_success = Request.QueryString["Registration"];

            if (registration_success != null)
            {
                lblError.Text = " Registration successful..";
                lblError.ForeColor = System.Drawing.Color.Green;
            }
        }

        // Used to encrypt password using UTF8encoding
        static string getMd5Hash(string input)
        {
            string encrypt;
            byte[] data = UTF8Encoding.UTF8.GetBytes(input);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("f0xle@rn"));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    encrypt = Convert.ToBase64String(results, 0, results.Length);
                }
            }

            // Return the hexadecimal string.
            return encrypt.ToString();
        }



        // Used to decrypt password using UTF8encoding
        static string DecryptMd5Hash(string input)
        {
            string decrypt;
            byte[] data = Convert.FromBase64String(input);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("f0xle@rn"));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    decrypt = UTF8Encoding.UTF8.GetString(results);
                }
            }

            return decrypt.ToString();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                if (txtUsername.Text.Length < 5)
                {
                    lblError.Text = "invalid username";
                }
                else if (txtPassword.Text.Length < 6)
                {
                    lblError.Text = "invalid password";
                }
                else
                {
                    // Used to access functions and fetch data from RestaurantUser class
                    RestaurantUser Rest_user_login = new RestaurantUser();

                    // Passing parameters to RestaurantUser class
                    Rest_user_login.UserName = txtUsername.Text;
                    Rest_user_login.UserPassword = txtPassword.Text;

                    // calling function to check username exists or not
                    DataTable check_user = Rest_user_login.CheckUser();

                  
                    // condition to check if data table is not empty 
                    if (check_user.Rows.Count > 0)
                    {

                        string user = check_user.Rows[0]["UserName"].ToString();

                        Rest_user_login.UserName = txtUsername.Text;
                        // calling function to check user credentials
                        DataTable dt_user_login = Rest_user_login.LoginCredentials();

                        // condition to check if strings are equal or not 
                        bool check_user_name = string.Equals(user, txtUsername.Text);

                        // condition to check if data table is not empty 
                        if (dt_user_login.Rows.Count > 0)
                        {
                            string pass = dt_user_login.Rows[0]["UserPassword"].ToString();
                            string input_pass = txtPassword.Text;

                            // Decrypt password based on the input 
                            string decrypt_pass = DecryptMd5Hash(pass);

                            // condition to check if strings are equal or not
                            bool verify_pass = string.Equals(decrypt_pass, input_pass);

                            // condition  if strings are equal proceed to login and create session for user
                            if (verify_pass)
                            {
                                Session["UserName"] = dt_user_login.Rows[0]["UserName"].ToString();
                                Session["UserPassword"] = dt_user_login.Rows[0]["UserPassword"].ToString();
                                Session["RealName"] = dt_user_login.Rows[0]["RealName"].ToString();
                                Session["EmailAddress"] = dt_user_login.Rows[0]["EmailAddress"].ToString();
                                Session["RoleID"] = dt_user_login.Rows[0]["RoleID"].ToString();
                                Session["RestaurantID"] = dt_user_login.Rows[0]["RestaurantID"].ToString();

                                // Redirect to the User login 
                                Response.Redirect("~/UserAccount.aspx");
                            }
                            else
                            {
                                lblError.Text = "Credentials are not correct";
                            }

                        }
                        else
                        {
                            lblError.Text = "Credentials are not correct";
                        }
                    }
                    else
                    {
                        lblError.Text = "User not found";
                    }
                }
            }
        }
    }
}