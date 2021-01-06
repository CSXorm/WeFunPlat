using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFunModel;
using UI.Filter;
using Newtonsoft.Json;

namespace UI.Areas.SystemArea.Controllers
{
    public class UserManagerController : Controller
    {

        // GET: SystemArea/UserManager
        //[LoginFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            string JsonStr = "{\"code\": 0,\"msg\": \"\",\"count\": 1000,\"data\": ";
            List<Users> uslist = new List<Users>();
            using (MyContext context = new MyContext())
            {
                uslist = context.Users.ToList();
            }
            string strJson = JsonConvert.SerializeObject(uslist);
            JsonStr += strJson + "}";
            return Content(JsonStr);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddUser(Users info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                db.Users.Add(info);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }

        public ActionResult DelUser(Users info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                Users uinfo = db.Users.Where(x => x.ID == info.ID).FirstOrDefault();
                db.Users.Remove(uinfo);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }

        public ActionResult Edit()
        {
            return View();
        }
        //public ActionResult EditUser(Users info)
        //{
        //    string result = "Fail";
        //    using (MyContext db = new MyContext())
        //    {
        //        db.Users.Edit(info);
        //        int r = db.SaveChanges();
        //        if (r > 0)
        //        {
        //            result = "Success";
        //        }
        //    }
        //    return Content(result);
        //}
    }
}