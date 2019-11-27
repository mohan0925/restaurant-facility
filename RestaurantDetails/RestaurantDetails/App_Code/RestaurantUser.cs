using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Text;


namespace RestaurantDetails.App_Code
{
    public class RestaurantUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UpdatedUserPassword { get; set; }

        public string RealName { get; set; }
        public string EmailAddress { get; set; }
        public int RoleID { get; set; }
        public int RestaurantID { get; set; }

        public int UpdatedRestaurantID { get; set; }

        public static string hash  = "f0xle@rn";



        private DatabaseConnection dataConn;
        // Constructor to establish connections with the db
        public RestaurantUser()
        {
            dataConn = new DatabaseConnection();
        }

        // Boolean function returns whether username exists or not in db
        public bool userNameExists() //boolean means true or false
        {
            dataConn.addParameter("@UserName", UserName);

            string command = "Select COUNT(UserName) FROM RestaurantUser WHERE UserName=@UserName";

            int result = dataConn.executeScalar(command); //result of count

            return result > 0 || result == -1; //if record found or exception caught
        }

        // boolean functions returns whether email exist or not in db
        public bool emailExists() //boolean means true or false
        {
            dataConn.addParameter("@EmailAddress", EmailAddress);

            string command = "Select COUNT(EmailAddress) FROM RestaurantUser WHERE EmailAddress=@EmailAddress";

            int result = dataConn.executeScalar(command); //result of count

            return result > 0 || result == -1; //if record found or exception caught
        }


        // Fuction to encrypt the password
        static string getMd5Hash(string input)
        {
            string encrypt;
            byte[] data = UTF8Encoding.UTF8.GetBytes(input);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
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

        // Fuction to decrypt the password
        static string DecryptMd5Hash(string input)
        {
            string decrypt;
            byte[] data = Convert.FromBase64String(input);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    decrypt = UTF8Encoding.UTF8.GetString(results);
                }
            }

            return decrypt.ToString();
        }

        // boolean function on adding user it will return 1, if not then 0
        public bool addUser()
        {
            string Userpassword = getMd5Hash(UserPassword);
            dataConn.addParameter("@UserName", UserName);
            dataConn.addParameter("@UserPassword", Userpassword);
            dataConn.addParameter("@RealName", RealName);
            dataConn.addParameter("@EmailAddress", EmailAddress);
            dataConn.addParameter("@RoleID", RoleID);
            dataConn.addParameter("@RestaurantID", RestaurantID);

            string command = "INSERT INTO RestaurantUser (UserName, UserPassword, RealName, EmailAddress, RoleID, RestaurantID) " +
                            "VALUES (@UserName, @Userpassword, @RealName, @EmailAddress, @RoleID, @RestaurantID)";
            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }

        // returns datatable with the row based on the input from the RestaurantUser
        public DataTable CheckUser()
        {
            dataConn.addParameter("@UserName", UserName);
            string command = "Select UserName from RestaurantUser WHERE UserName = @UserName";
            //         int res = dataConn.executeNonQuery(command);
            return dataConn.executeReader(command);  //i.e. 1 or more rows affected
        }

        // returns datatable of user row with given credentials
        public DataTable LoginCredentials()
        {
            string Userpassword = getMd5Hash(UserPassword);
            dataConn.addParameter("@UserName", UserName);
            dataConn.addParameter("@UserPassword", Userpassword);
            string command = "Select * from RestaurantUser WHERE UserName = @UserName and UserPassword= @UserPassword";
            return dataConn.executeReader(command);
        }

        // It returns all the chefs working at the input of restaurant id
        public DataTable getChefdetails()
        {
            dataConn.addParameter("@RestaurantID", RestaurantID);
            dataConn.addParameter("@RoleID", RoleID);
            string command = "Select UserID,RealName,EmailAddress from RestaurantUser WHERE RestaurantID = @RestaurantID and RoleID = @RoleID";
            return dataConn.executeReader(command);
        }

        // It returns the email of particular user given
        public DataTable getEmail()
        {
            dataConn.addParameter("@UserID", UserID);
            string command = "Select EmailAddress from RestaurantUser WHERE UserID = @UserID";
            return dataConn.executeReader(command);
        }

        // Boolean function returns true if command executed successfully i.e; waiter is deleted
        public bool deleteWaiter()
        {
            dataConn.addParameter("@UserID", UserID);
            string command = "DELETE FROM RestaurantUser WHERE UserID = @UserID";
            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }

        // Boolean function returns true if password is updated 
        public bool updatePassword()
        {
            string UpdateUserPassword = getMd5Hash(UpdatedUserPassword);
            dataConn.addParameter("@UserName", UserName);
            dataConn.addParameter("@UserPassword", UserPassword);
            dataConn.addParameter("@UpdatedUserPassword", UpdateUserPassword);
            string command = "UPDATE RestaurantUser SET UserPassword = @UpdatedUserPassword WHERE UserName = @UserName and UserPassword=@UserPassword";
            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }

        // Returns the user details working at the particular restaurant id
        public DataTable getWaitersdetails()
        {
            dataConn.addParameter("@RestaurantID", RestaurantID);
            dataConn.addParameter("@RoleID", RoleID);
            string command = "Select UserID,RealName from RestaurantUser WHERE RestaurantID = @RestaurantID and RoleID = @RoleID";
            return dataConn.executeReader(command);
        }

        // Returns the datatable with the complete row of given username
        public DataTable Userdetails()
        {
            dataConn.addParameter("@UserName", UserName);
            string command = "Select * from RestaurantUser WHERE UserName = @UserName";
            return dataConn.executeReader(command);
        }

        // boolean function returns the true or false on updating restaurantid
        public bool updateRestaurant()
        {
            dataConn.addParameter("@UserName", UserName);
            dataConn.addParameter("@RestaurantID", RestaurantID);
            dataConn.addParameter("@UpdatedRestaurantID", UpdatedRestaurantID);

            string command = "UPDATE RestaurantUser SET RestaurantID = @UpdatedRestaurantID WHERE UserName = @UserName and RestaurantID=@RestaurantID";
            //      int res = dataConn.executeNonQuery(command);
            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }

        // boolean function it checks whether email exist in table or not and returns true or false
        public bool ForgotPwd()
        {
            dataConn.addParameter("@EmailAddress", EmailAddress);
            string command = "Select * from RestaurantUser WHERE EmailAddress = @EmailAddress";
            int result = dataConn.executeNonQuery(command);
            return dataConn.executeNonQuery(command) > 0;  //i.e. 1 or more rows affected
        }

        // datatable returns email address from the restaurantuser table
        public DataTable sendDetails()
        {
            dataConn.addParameter("@EmailAddress", EmailAddress);
            string command = "Select * from RestaurantUser WHERE EmailAddress = @EmailAddress";
            return dataConn.executeReader(command);
        }

        // datatable returns the user details on giving email of the user as input
        public DataTable getusers()
        {
            dataConn.addParameter("@RoleID", RoleID);
            dataConn.addParameter("@EmailAddress", EmailAddress);
            string command = "Select EmailAddress,UserName,UserPassword from RestaurantUser WHERE RoleID = @RoleID and EmailAddress='" + EmailAddress + "'";
            //           DataTable User_role_dt = dataConn.executeReader(command);
            return dataConn.executeReader(command);
        }

    }
}