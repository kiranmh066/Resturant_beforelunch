using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMVCUI.Controllers
{
    public class HeadChefController : Controller
    {
        private IConfiguration _configuration;

        public HeadChefController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<Order> orderResult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrders";

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        orderResult = JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
                    }
                }
            }
            return View(orderResult);
        }

        //[HttpGet]
        public async Task<IActionResult> Speciality(int OrderId)
        { //IEnumerable<Order> order;
            Order order = new Order();
            order.OrderId = OrderId;

            Food food = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrderById?orderId=" + OrderId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        order = JsonConvert.DeserializeObject<Order>(result);
                    }
                }
            }
           
            return View(order);
        }
        public async Task<IActionResult> Assign(int OrderId)
        {
         
       
            IEnumerable<Employee> employeeList = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployees";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result1 = await response.Content.ReadAsStringAsync();
                        employeeList = JsonConvert.DeserializeObject<IEnumerable<Employee>>(result1);
                    }
                }
            }
            /*TempData["foodcost1"] = Convert.ToInt32(food.FoodCost);
            TempData.Keep();*/

            return View(employeeList);
        }
        /*[HttpPost]
        public async Task<IActionResult> AddOrder1(Order order)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                var foodcost = Convert.ToInt32(TempData["foodcost1"]);
                TempData.Keep();
                order.OrderTotal = order.Quantity * foodcost;
                order.OrderStatus = false;
                StringContent content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/AddOrder";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Order Added Successfull!!";
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }

                }
            }
            return View();
        }*/
    }
}
