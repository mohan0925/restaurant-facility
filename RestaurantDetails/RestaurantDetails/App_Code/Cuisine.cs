using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace RestaurantDetails.App_Code
{
    public class Cuisine
    {
        public string CuisineName { get; set; }
        public int CuisineID { get; set; }
        public string CuisineRegion { get; set; }

        private DatabaseConnection dataConn;

        // constructor to establish connection with database
        public Cuisine()
        {
            dataConn = new DatabaseConnection();
        }

        // function to get cuisineRegion and cuisineName present at given CuisineId to display in repeater 
        public DataTable getAllCuisine()
        {
            dataConn.addParameter("@CuisineID", CuisineID);
            string command = "Select * from Cuisine WHERE CuisineID = @CuisineID";
            return dataConn.executeReader(command);
        }
    }
}