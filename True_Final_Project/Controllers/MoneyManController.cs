using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using True_Final_Project.Models;

namespace True_Final_Project.Controllers
{
    public class MoneyManController : Controller
    {

        private readonly ICalculations repo;

        public MoneyManController(ICalculations repo)
        {
            this.repo = repo;
        }


        public IActionResult Calcu()
        {
            var calclat = repo.GetAllCalc();
            return View(calclat);
        }
        public IActionResult ViewCalc(int id)
        {
            var calclat = repo.GetCalc(id);
            return View(calclat);
        }

        public IActionResult UpdateCalc(int id)
        {
            CalcVal val = repo.GetCalc(id);
            if (val == null)
            {
                return View("ProductNotFound");
            }
            return View(val);
        }

        public IActionResult UpdateCalcValToDatabase(CalcVal c)
        {
            repo.UpdateCalc(c);

            return RedirectToAction("ViewCalc", new { id = c.MonthID });
        }
    }
}