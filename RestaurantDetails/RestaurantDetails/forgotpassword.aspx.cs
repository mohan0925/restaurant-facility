using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System;
using RestaurantDetails.App_Code;
using System.Text;
using System.Data;
using System.Security.Cryptography;

namespace RestaurantDetails
{
    public partial class forgotpassword : System.Web.UI.Page
    {
        // create object for the mail services
        MailMessage mail;
        string UserName, EmailAddress;

        // Import all records from the middle tier 
        UserRole user_role_class = new UserRole();
        RestaurantUser rest_user_class = new RestaurantUser();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable User_role_dt = user_role_class.getAllroles();
                if (User_role_dt != null && User_role_dt.Rows.Count > 0)
                {
                    //set DropDownList's data source to the DataTable         
                    waiterrole.DataSource = User_role_dt;

                    //assign DepartmentID database field to the value property
                    waiterrole.DataValueField = "RoleId";

                    //assign DepartmentName database field to the text property
                    waiterrole.DataTextField = "RoleName";

                    //bind data
                    waiterrole.DataBind();
                }
            }

        }

        //decrypt the password by using the UTF8Encoding standard
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



        protected void Button1_Click1(object sender, EventArgs e)
        {
            // convert the role id from string to integer to pass parameter as integer to database
            rest_user_class.RoleID = Int32.Parse(waiterrole.SelectedValue);
            rest_user_class.EmailAddress = EmailID.Text.ToString();

            // get record of particular inputs from the table 
            DataTable all_users = rest_user_class.getusers();
            if (all_users != null && all_users.Rows.Count > 0)
            {
                string EmailAddress = all_users.Rows[0]["EmailAddress"].ToString();
                string UserName = all_users.Rows[0]["UserName"].ToString();
                string UserPassword = all_users.Rows[0]["UserPassword"].ToString();

                // Decrypt the password
                UserPassword = DecryptMd5Hash(UserPassword);
                string senderID = "graphicpass01@gmail.com"; // use sender’s email id here..
                string senderPassword = "xxxxxxxxxx"; //sender password here…

                // Call smtp constructor for using all the functions in smtp
                //Simple Mail Transfer Protocol, a protocol for sending e-mail messages between servers.
                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com"; // used to declare prototype of gmail  
                smtp.Port = 587;     // assign the port number for sending the message
                smtp.EnableSsl = true;  // SSL is enabled because to keep sensitive information sent across the Internet encrypted so that only the intended recipient can access it.  
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;  // Initialise because to enable send and receiving options over the internet.
                smtp.Credentials = new System.Net.NetworkCredential(senderID, senderPassword); // Credentials are given so it can send the message from the desired mail to the receipient 
                smtp.Timeout = 30000; // Set the time out to the smtp request

                // Message is Written in required format
                var message = new MailMessage(senderID, EmailAddress, "Password for site", "<h2>username=" + UserName + "  <br>Password =" + UserPassword +  "<br><br><br>Thanks for registration!");
                message.IsBodyHtml = true;   // Enable it to send html tags
                smtp.Send(message);  // Send message data to the recceipent 
                Label_Forgotpwd.Text = "Password sent successfully to registered email......!";
             //   Label_Forgotpwd.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Label_Forgotpwd.Text = "User not registered.........!";
            }
        }
    }
}
