using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using MvcMovie.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace mvcMovie.Controllers{

public class LogoutController : Controller
{

    public IActionResult Index(){
        Response.Cookies.Delete("Username");
        return Redirect("/");
    }
}

}