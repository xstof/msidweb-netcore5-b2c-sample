using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using net5_b2c.Models;

namespace net5_b2c.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IDownstreamWebApi _downstreamWebApi;

        public HomeController(ILogger<HomeController> logger,
                              IDownstreamWebApi downstreamWebApi)
        {
             _logger = logger;
            _downstreamWebApi = downstreamWebApi;
       }

        // [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        //[AuthorizeForScopes()]
        public async Task<IActionResult> Index()
        {
            // try{
                // using var response = await _downstreamWebApi.CallWebApiForUserAsync("DownstreamApi").ConfigureAwait(false);
                // if (response.StatusCode == System.Net.HttpStatusCode.OK)
                // {
                //     var apiResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                //     ViewData["ApiResult"] = apiResult;
                // }
                // else
                // {
                //     var error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                //     throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}: {error}");
                // }
            // }
            // catch {

            // }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> Secured(){
            using var response = await _downstreamWebApi.CallWebApiForUserAsync("DownstreamApi").ConfigureAwait(false);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                ViewData["ApiResult"] = apiResult;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}: {error}");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
