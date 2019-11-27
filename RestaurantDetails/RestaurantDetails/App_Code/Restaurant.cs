using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace RestaurantDetails.App_Code
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        private DatabaseConnection dataConn;

        // Constructor to establish connections with the db
        public Restaurant()
        {
            dataConn = new DatabaseConnection();
        }

        // To get all the restaurants from the Restaurant table
        public DataTable getAllRestaurants()
        {
            string command = "Select * FROM Restaurant";
            return dataConn.executeReader(command);
        }

        // To get the restaurant which has the particular id from the Restaurant table
        public DataTable getRestaurant()
        {
            dataConn.addParameter("@RestaurantID", RestaurantID);
            string command = "Select * from Restaurant WHERE RestaurantID = @RestaurantID";
            return dataConn.executeReader(command);
        }

        // boolean function to check if the restaurant is updated or not 
        public bool updateRestaurant()
        {
            dataConn.addParameter("@RestaurantID", RestaurantID);
            string command = "Select * from Restaurant WHERE RestaurantID = @RestaurantID";
            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }

        // Get all the restaurants except the given restaurant id from Restaurant table 
        public DataTable getAllRestaurant()
        {
            dataConn.addParameter("@RestaurantID", RestaurantID);
            string command = "Select * from Restaurant WHERE NOT RestaurantID = @RestaurantID";
            return dataConn.executeReader(command);
        }

    }
}