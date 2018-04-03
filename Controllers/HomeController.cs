using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PokeInfo.Models;
using Newtonsoft.Json;

namespace PokeInfo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("pokemon/{pokeid}")]
        public IActionResult QueryPoke(int pokeid)
        {
            var PokeInfo = new Dictionary<string, object>();
            WebRequest.GetPokemonDataAsync(pokeid, ApiResponse =>
                {
                    PokeInfo = ApiResponse;
                }   
            ).Wait();

             string name = (string)PokeInfo["name"];
             object primaryType = PokeInfo["types"];

            //HttpContext.Session.SetString("pokedata", PokeInfo);
            ViewBag.PokeInfo = PokeInfo;
            ViewBag.Name = name;
            ViewBag.PrimaryType = primaryType;

            return View();


        }
    }

        
}
