using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CordobaCommon
{
   public class ProjectSession
    {
     

       public static StoreDetail StoreSession
        {
            get
            {
                if (HttpContext.Current.Session["StoreDetail"] == null)
                {
                    return null;
                }

                return HttpContext.Current.Session["StoreDetail"] as StoreDetail;
            }

            set
            {
                HttpContext.Current.Session["StoreDetail"] = value;
            }
        }


    }

 
}
