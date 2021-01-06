using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFunModel;

namespace UI.Areas.StudioArea.Controllers
{
    public class StudioAreaController : Controller
    {
        // GET: StudioArea/Studio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            string JsonStr = "{\"code\": 0,\"msg\": \"\",\"count\": 1000,\"data\": ";
            List<Studios> uslist = new List<Studios>();
            using (MyContext context = new MyContext())
            {
                uslist = context.Studios.ToList();
            }
            string strJson = JsonConvert.SerializeObject(uslist);
            JsonStr += strJson + "}";
            return Content(JsonStr);
        }

        public ActionResult DelStudio(Studios info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                Studios uinfo = db.Studios.Where(x => x.RoomID == info.RoomID).FirstOrDefault();
                db.Studios.Remove(uinfo);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddStudio(Studios info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                db.Studios.Add(info);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }

        public ActionResult Edit(int? id)
        {
            Studios info = new Studios();
            using (MyContext db = new MyContext())
            {
                info = db.Studios.Where(x => x.RoomID == id).FirstOrDefault();
            }
            return View(info);
        }

        public ActionResult EditStudios(Studios info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                Studios usinfo = db.Studios.Where(x => x.RoomID == info.RoomID).FirstOrDefault();
                usinfo.RoomName = info.RoomName;
                usinfo.Remake = info.Remake;
                usinfo.State = info.State;
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