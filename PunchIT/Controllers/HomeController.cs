using Microsoft.AspNet.Identity;
using PunchIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PunchIT.Controllers
{
    [RoutePrefix("")]
    [Authorize]
    public class HomeController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        [AllowAnonymous]
        [Route("", Name = "About")]

        public ActionResult Index()
        {
            return View();
        }

        [Route("ClockIn", Name = "TimeInput")]
        public ActionResult ClockIn()
        {
            var userId = User.Identity.GetUserId();
            var timeIn = new TimeClock();
            
            var timeclock = _context.TimeClocks.Where(t => t.ApplicationUserId == userId).OrderByDescending(u => u.TimeIn).Take(1).ToList();

            foreach (var entry in timeclock)
            {
                var difference = DateTime.MaxValue - entry.TimeOut;
                double hoursDiff = difference.TotalDays;

                if (hoursDiff == 0)
                {
                    var timediff = DateTime.UtcNow - entry.TimeIn;
                    double diffInHours = timediff.TotalHours;

                    if (diffInHours > 4.5)
                    {
                        TempData["PunchITMessage"] = $"Whoa, You must be Tired by now. You are a dedicated Employee and working for a continuous {diffInHours} hours.";
                    }
                    else
                    {
                        TempData["PunchITMessage"] = $"Keep Up the good Work! You are working for {diffInHours} hours.";
                    }
                    timeIn.Id = entry.Id;
                    timeIn.TimeIn = entry.TimeIn;
                    timeIn.ApplicationUserId = entry.ApplicationUserId;
                    return View(timeIn);
                }
            }

            TempData["PunchITMessage"] = $"Welcome {userId}, it is a beautiful day. Let's enjoy it with work";
            return View(timeIn);
        }


        [HttpPost]
        [Route("ClockIn", Name = "TimeOutput")]
        public ActionResult ClockIn(TimeClock model)
        {
            var userId = User.Identity.GetUserId();

            if (model.Id == 0)
            {
                var timeIn = new TimeClock
                {
                    TimeIn = DateTime.UtcNow,
                    ApplicationUserId = userId,
                    TimeOut = DateTime.MaxValue
                };
                _context.TimeClocks.Add(timeIn);
                _context.SaveChanges();

                TempData["PunchITMessage"] = "Your working hours starts now. Have a wonderful day.";
                return Redirect("TimeInput");
            }

            var timeOut = _context.TimeClocks.FirstOrDefault(t => t.Id == model.Id && t.ApplicationUserId == userId);

            timeOut.TimeOut = DateTime.UtcNow;
            _context.SaveChanges();

            return Redirect("TimeInput");
        }
    }
}