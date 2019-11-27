using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace RestaurantDetails.App_Code
{
    public class UserRole
    {
        private int RoleId { get; set; }
        private string RoleName { get; set; }
        private DatabaseConnection dataConn;

        // Constructor to establish connections with the db
        public UserRole()
        {
            dataConn = new DatabaseConnection();
        }

        // datatable returns all the roles present in the UserRole table
        public DataTable getAllroles()
        {
            string command = "Select * FROM UserRole";
            return dataConn.executeReader(command);
        }
    }
}