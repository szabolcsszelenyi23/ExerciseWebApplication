using ExerciseWebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExerciseWebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpensesDbContext _context;

        public HomeController(ILogger<HomeController> logger, ExpensesDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _context.Expenses.ToList();

            var totalExpenses = allExpenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? ID)
        {
            if (ID == null)
            {
                var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.ID == ID);
                return View(expenseInDb);
            }

            return View();
        }

        public IActionResult Delete(int ID)
        {
            var expenseInDb =  _context.Expenses.SingleOrDefault(expense => expense.ID == ID);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if(model.ID == 0)
            {
                _context.Expenses.Add(model);
            }

            else
            {
                _context.Expenses.Update(model);
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
