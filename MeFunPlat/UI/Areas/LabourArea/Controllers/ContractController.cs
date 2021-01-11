using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFunModel;

namespace UI.Areas.LabourArea.Controllers
{
    public class ContractController : Controller
    {
        // GET: LabourArea/Contract
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            string JsonStr = "{\"code\": 0,\"msg\": \"\",\"count\": 1000,\"data\": ";
            List<Contract> uslist = new List<Contract>();
            using (MyContext context = new MyContext())
            {
                uslist = context.Contract.ToList();
            }
            string strJson = JsonConvert.SerializeObject(uslist);
            JsonStr += strJson + "}";
            return Content(JsonStr);
        }
        public ActionResult Add()
        {
            List<Announcer> usinf = new List<Announcer>();
            using (MyContext context = new MyContext())
            {
                usinf = context.Announcer.ToList();
            }

            ViewBag.contype = usinf;
            return View();
        }
        public ActionResult AddStudio(Contract info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                db.Contract.Add(info);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }
        public ActionResult edit(int? ConID)
        {
            Contract uinfo = new Contract();
            using (MyContext db = new MyContext())
            {
                uinfo = db.Contract.Where(x => x.ConID == ConID).FirstOrDefault();
                ViewBag.sysrole = uinfo;
            }
            return View();
        }
        public ActionResult editUser(Contract info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                Contract uinfo = db.Contract.Where(x => x.ConID == info.ConID).FirstOrDefault();
                uinfo.ID = info.ID;
                uinfo.JoinDate = info.JoinDate;
                uinfo.LeaveDate = info.LeaveDate;
                uinfo.ConRemake = info.ConRemake;
                uinfo.ConEndDate = info.ConEndDate;
                uinfo.ConBeginDate = info.ConBeginDate;
                int i = db.SaveChanges();
                if (i > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }

        public ActionResult DelUser(Contract info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                Contract uinfo = db.Contract.Where(x => x.ConID == info.ConID).FirstOrDefault();
                db.Contract.Remove(uinfo);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }
    }
}