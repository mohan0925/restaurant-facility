using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestaurantDetails.App_Code;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantDetails
{
    public partial class UpdatePassword : System.Web.UI.Page
    {
        string UserName, PassWord;

        // Encrypt password using UTF8Encoding
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
        // decrypt password using UTF8Encoding
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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                // Used to access functions from the RestaurantUser class
                RestaurantUser rest_user_class = new RestaurantUser();
                PassWord = Session["UserPassword"].ToString();
                UserName = Session["UserName"].ToString();
            }
        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string passWord;
            
            RestaurantUser rest_user_class = new RestaurantUser();

            // condition to check whether the fields are empty
            if (txtCurrentPassword.Text == "" || txtNewPassword.Text == "" || txtConfirmPassword.Text == "")
            {
                lblError.Text = "Enter password fields correctly....";
            }
            else if (txtNewPassword.Text.Length < 6)
            {
                lblError.Text = "password should be 6 character";
            }
            else if (!txtNewPassword.Text.Equals(txtConfirmPassword.Text))
            {
                lblError.Text = "Password did not match";
            }
            else 
            {
                string input_pass = txtCurrentPassword.Text;

                // Used to decrypt string
                string decrypt_pass = DecryptMd5Hash(PassWord);

                // Compare strings if equal or not
                bool verify_pass = string.Equals(decrypt_pass, input_pass);

                // Update password if strings are equals
                if (verify_pass)
                {
                    rest_user_class.UserName = UserName;
                    rest_user_class.UserPassword = PassWord; 
                    rest_user_class.UpdatedUserPassword = txtConfirmPassword.Text;
                    System.Threading.Thread.Sleep(1000);
                    // check if both passwords are equal
                    bool update_pass = rest_user_class.updatePassword();

                    //if true then display password is updated
                    if (update_pass)
                    {                        
                        // If password is updated then the page is redirected to the User account with the url containing password
                        Response.Redirect("~/UserAccount.aspx?UpdateSuccess="+ txtConfirmPassword.Text);
                        lblError.Text = "Password Sucessfully updated..";
                        lblError.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblError.Text = "Database connection error - cannot update password...";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblError.Text = "Incorrect current password";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}