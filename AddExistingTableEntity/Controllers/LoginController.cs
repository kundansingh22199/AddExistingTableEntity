using AddExistingTableEntity.Models;
using BussinessEntity.Models;
using InterfaceLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;

namespace AddExistingTableEntity.Controllers
{
    public class LoginController : Controller
    {
        private readonly IContract _icontract;
        //private readonly SscapitalContext _scapitalContext;

        public LoginController(IContract icontract)
        {
            _icontract = icontract;
        }
        [HttpGet]
        public async Task<IActionResult> CheckSponsor(string sid)
        {
            string result = await _icontract.CheckSponsorIDAsync(sid);
            return Json(result);
        }
        
        public IActionResult SignUp(string sid = "", int l = 1)
        {
            TblAppmst obj = new TblAppmst();
            obj.Sid = sid;
            ViewBag.sid = sid;
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(TblAppmst obj)
        {
            var result = await _icontract.InsertDataAsync(obj);
            if (result > 0)
            {
                HttpContext.Session.SetString("NewReg", obj.RegNo);
                return RedirectToAction("Success", "Login");
            }
            else
            {
                ViewBag.Err = "Registration Failed";
            }
            return View();
        }
        public async Task<IActionResult> Success()
        {
            string regno = HttpContext.Session.GetString("NewReg");

            if (!string.IsNullOrEmpty(regno))
            {
                try
                {
                    var result = await _icontract.GetDataAsync(regno);
                    if (result != null )
                    {
                        ViewBag.Data = result;
                    }
                    else
                    {
                        ViewBag.Data = null;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Data = null;
                }
            }
            else
            {
                ViewBag.Data = null;
            }

            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(ModLogin obj)
        {
            var result = await _icontract.SignInAsync(obj);

            if (result == 1)
            {
                HttpContext.Session.SetString("UserId", obj.UserId);
                return RedirectToAction("Dashboard", "User");
            }
            else
            {
                ViewBag.Err = "Login Failed";
                return View();
            }
        }
    }
}
