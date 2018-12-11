using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.suppliers
{
    public partial class SupplierSchedularTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //var expression = new CronExpression("0 26 13 17 10 ? 2015");
            //DateTimeOffset? time = expression.GetTimeAfter(DateTimeOffset.UtcNow);

        }

        protected void btngetnextscheduler_Click(object sender, EventArgs e)
        {
            //var schedule = CrontabSchedule.TryParse("0 0 12 18 1/4 ? *");
            //DateTime test = schedule.GetNextOccurrence(DateTime.Now);
        }

    }
}