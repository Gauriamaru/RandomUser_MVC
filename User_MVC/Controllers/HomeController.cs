using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using User_MVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static User_MVC.Models.HomeViewModel;

namespace User_MVC.Controllers
{
	
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{

            using (var client = new HttpClient())
            {
                try
                {

					throw new HttpRequestException();
                    client.BaseAddress = new Uri("https://api.randomuser.me/?results=5");
                    var response = await client.GetAsync("https://api.randomuser.me/?results=5");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    RootObject person = JsonConvert.DeserializeObject<RootObject>(stringResult);

                    return View(person);
                }

                catch (HttpRequestException httpRequestException)
                {

                    ErrorViewModel   err= new ErrorViewModel { RequestId = Activity.Current?.Id ?? httpRequestException.Message };
                 
                    return View("Error", err);
                }
            }
        }



		
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}