using CRUDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication13.Controllers
{
    public class HomeController : Controller
    {
        DB candidateDB = new DB();
        
        public ActionResult Index()
        {
            List<CandidateInfo> canList = new List<CandidateInfo>();
            canList = candidateDB.GetAllCandidates().ToList();
            return View(canList);
        }

        [HttpPost]
        public ActionResult Index(List<CandidateInfo> candidates)
        {

            DB db = new DB();

            foreach (CandidateInfo can in candidates)
            {

                CandidateInfo Existed_Can = candidateDB.GetCandidateById(can.ID);
                Existed_Can.State = can.State;
                Existed_Can.Republicans = can.Republicans;
                Existed_Can.Democrats = can.Democrats;
                db.UpdateCandidate(can);

            }
            
            return View(candidates);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}