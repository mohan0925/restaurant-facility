using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace RestaurantDetails.App_Code
{
    public class RestaurantCuisine
    {
        public int RestaurantID { get; set; }
        public int CuisineID { get; set; }

        private DatabaseConnection dataConn;

        // Constructor to establish connections with the db
        public RestaurantCuisine()
        {
            dataConn = new DatabaseConnection();
        }

        //  Get all the Cuisines from the restaurant
        public DataTable getCuisinedata()
        {
            string command = "Select * FROM RestaurantCuisine";
            return dataConn.executeReader(command);
        }

        //  Get all the Cuisines from particular restaurant
        public DataTable getCuisines()
        {
            dataConn.addParameter("@RestaurantID", RestaurantID);
            string command = "Select * from RestaurantCuisine WHERE RestaurantID= @RestaurantID";
            return dataConn.executeReader(command);           
        }
    }
}