using Microsoft.AspNetCore.Mvc;
using RestaurantEntity;

namespace RestaurantMVCUI.Controllers
{
    public class ChefController : Controller
    {
        public IActionResult Index(int EmpId)
        {
            Employee employee = new Employee();
            employee.EmpId = EmpId;
            return View(employee.EmpId);
        }
    }
}
