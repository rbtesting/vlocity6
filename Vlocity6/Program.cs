using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Vlocity6
{
    public class Program
    {
        static void Main(string[] args)
        {
            Vlocity6.Control.NewDashboard obj_Dashboard = new Control.NewDashboard();
            obj_Dashboard.GetBenefitsSummaryData();
            // Dashboard obj_Dashboard = new Dashboard();
           // obj_Dashboard.GetBenefitsSummaryData();

          
            
            
        }
    }
}
