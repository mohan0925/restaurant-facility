using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantDetails
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                // Remove all the sessions and clearing the cache from the browser so that if the user clicks back button nothing loads 
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetNoServerCaching();
                HttpContext.Current.Response.Cache.SetNoStore();
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            // Removes all the sessions and clears the cache
            Session.RemoveAll();
            // Redirects to the login page
            Response.Redirect("~/UserLogin.aspx");
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            // Redirects to the user account without clearing the cache
            Response.Redirect("~/UserAccount.aspx");
        }
    }
}