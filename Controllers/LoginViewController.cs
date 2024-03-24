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

[Route("login")]
public class LoginViewController : Controller
{
    private readonly MvcMovieContext _context;

    public LoginViewController(MvcMovieContext context)
    {
        _context = context;
    }

    public IActionResult Index(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index([Bind("Username,Password")] User user)
    {     
        // var cookie = request.Headers.GetCookies(SessionIdToken).FirstOrDefault();
        var userFromDb = await _context.User.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);
        if (userFromDb != null)
        {   
            Response.Cookies.Append("Username", user.Username);
            return RedirectToAction("All","Home");
        }
        else
        {
            // Invalid username or password
            return RedirectToAction("/login");
        }
    
    }
}

}