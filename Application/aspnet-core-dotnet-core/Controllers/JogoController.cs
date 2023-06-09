﻿using AlimentacaoInfantil.Models;
using aspnet_core_dotnet_core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace AlimentacaoInfantil.Controllers
{
    public class JogoController : Controller
    {
        private readonly IConfiguration _config;

        public JogoController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IActionResult Index()
        {
            return View();
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