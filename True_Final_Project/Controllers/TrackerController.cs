using Microsoft.AspNetCore.Mvc;
using True_Final_Project.Models;

namespace True_Final_Project.Controllers
{
    public class TrackerController : Controller
    {
        private readonly ICalculations repo;

        public TrackerController(ICalculations repo)
        {
            this.repo = repo;
        }


        public IActionResult Index()
        {
            var cost = repo.GetAllCost();
            return View(cost);
        }

        public IActionResult ViewCost(int id)
        {
            var cost = repo.GetCost(id);
            return View(cost);
        }
        public IActionResult UpdateCost(int id)
        {
            CostVal cost = repo.GetCost(id);
            if (cost == null)
            {
                return View("CostNotFound");
            }
            cost.Months = repo.GetMonths();
            return View(cost);
        }

        public IActionResult UpdateCostValToDatabase(CostVal c)
        {
            repo.UpdateCost(c);

            return RedirectToAction("ViewCost", new { id = c.PurchesID });
        }
        public IActionResult InsertCost()
        {
            var prod = repo.AssignMonths();
            return View(prod);
        }
        public IActionResult InsertCostToDatabase(CostVal CostToInsert)
        {
            repo.InsertCostVal(CostToInsert);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteCost(CostVal cost)
        {
            repo.DeleteCost(cost);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAllMonthCost(CostVal cost)
        {
            repo.DeleteAllMonthCost(cost);
            return RedirectToAction("Index");
        }
    } 
}
