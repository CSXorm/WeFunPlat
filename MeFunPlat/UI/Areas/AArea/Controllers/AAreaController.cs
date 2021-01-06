using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFunModel;
using UI.Filter;
using Newtonsoft.Json;

namespace UI.Areas.AArea.Controllers
{
    public class AAreaController : Controller
    {
        // GET: AArea/AArea
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            string JsonStr = "{\"code\": 0,\"msg\": \"\",\"count\": 1000,\"data\": ";
            List<WeFunModel._Areas> uslist = new List<WeFunModel._Areas>();
            using (MyContext context = new MyContext())
            {
                uslist = context._Areas.ToList();
            }
            string strJson = JsonConvert.SerializeObject(uslist);
            JsonStr += strJson + "}";
            return Content(JsonStr);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddArea(WeFunModel._Areas info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                db._Areas.Add(info);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }

        public ActionResult DelArea(WeFunModel._Areas info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                _Areas uinfo = db._Areas.Where(x => x.ARID == info.ARID).FirstOrDefault();
                db._Areas.Remove(uinfo);
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
            _Areas info = new _Areas();
            using (MyContext db = new MyContext())
            {
                info = db._Areas.Where(x => x.ARID == id).FirstOrDefault();
            }
            return View(info);
        }

        public ActionResult EditArea(_Areas info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                _Areas usinfo = db._Areas.Where(x => x.ARID == info.ARID).FirstOrDefault();
                usinfo.ARName = info.ARName;
                usinfo.ARParent = info.ARParent;
                usinfo.ARIndex = info.ARIndex;
                usinfo.ARState = info.ARState;
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