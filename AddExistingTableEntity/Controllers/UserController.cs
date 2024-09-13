using AddExistingTableEntity.Models;
using BussinessEntity.Models;
using InterfaceLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AddExistingTableEntity.Controllers
{
    public class UserController : Controller
    {
        private readonly IContract _icontract;
        private readonly ISscapitalContext _sscapitalcontext;

        public UserController(IContract icontract, ISscapitalContext sscapitalcontext)
        {
            _icontract = icontract;
            _sscapitalcontext = sscapitalcontext;
        }
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            string regno = HttpContext.Session.GetString("UserId");

            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                try
                {
                    var result = await _icontract.GetDashboardAsync(regno);
                    if (result != null)
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

                return View();
            }
        }
        public async Task<IActionResult> MyTeam()
        {
            string regno = HttpContext.Session.GetString("UserId");

            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            try
            {
                List<ModTeamDetails> obj = new List<ModTeamDetails>();
                var result = await _icontract.GetMyTeamAsync(regno);

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = new List<ModTeamDetails>();
                    ViewBag.ErrorMessage = "No data found for your team.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching data. Please try again later.";
            }

            return View();
        }
        public async Task<IActionResult> DirectTeam()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                //var myteam = await _scapitalContext.TblAppmsts.ToListAsync();
                var directteam = await _sscapitalcontext.TblAppmsts
                    .Where(x => x.Sid == regno)
                    .ToListAsync();
                if (directteam.Any())
                {
                    ViewData["lst"] = directteam;
                }
                else
                {
                    ViewData["lst"] = null;
                }
            }
            return View();
        }
        public async Task<IActionResult> UtrCheck(string utrno)
        {
            string msg = "Please Enter utrno no....!";
            if (!string.IsNullOrEmpty(utrno))
            {
                string uname = await _icontract.UtrCheckAsync(utrno);
                return Json(uname);
            }
            else
            {
                return Json(msg);
            }
        }
        public IActionResult FundRequest()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> FundRequest(modFundrequest obj)
        {
            string regno = HttpContext.Session.GetString("UserId");

            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            obj.Regno = regno;
            if (obj.RequestType == "BANK")
            {
                if (obj.usd > 0 && obj.amount > 0 && !string.IsNullOrEmpty(obj.Utrno) &&
                    !string.IsNullOrEmpty(obj.Remarks) && !string.IsNullOrEmpty(obj.BankName) &&
                    !string.IsNullOrEmpty(obj.AccountNo) && !string.IsNullOrEmpty(obj.BranchName) &&
                    !string.IsNullOrEmpty(obj.AccountHName))
                {
                    int result = await _icontract.fundrequest(obj);
                    if (result > 0)
                    {
                        ViewBag.Message = "Fund Request Sent Successfully";
                        TempData["fundreq"] = obj.amount.ToString();
                        ModelState.Clear();
                        return RedirectToAction("ThanksRequest");
                    }
                    else
                    {
                        ViewBag.error = "Request failed, please try again....!";
                        ModelState.Clear();
                    }
                }
                else
                {
                    ViewBag.error = "All Fields are Required for BANK Payment Mode";
                }
            }
            else if (obj.RequestType == "UPI")
            {
                if (obj.usd > 0 && obj.amount > 0 && !string.IsNullOrEmpty(obj.Utrno) &&
                    !string.IsNullOrEmpty(obj.Remarks) && !string.IsNullOrEmpty(obj.UpiId))
                {
                    int result = await _icontract.fundrequest(obj);
                    if (result > 0)
                    {
                        ViewBag.Message = "Fund Request Sent Successfully";
                        TempData["fundreq"] = obj.amount.ToString();
                        ModelState.Clear();
                        return RedirectToAction("ThanksRequest");
                    }
                    else
                    {
                        ViewBag.error = "Request failed, please try again....!";
                        ModelState.Clear();
                    }
                }
                else
                {
                    ViewBag.error = "All Fields are Required for UPI Payment Mode";
                }
            }
            else
            {
                ViewBag.error = "Please Select a Payment Mode";
            }
            return View();
        }
        [HttpGet]
        public IActionResult ThanksRequest()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
        public IActionResult FundRequestUsdt()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Fundrequestusdt(modUsdtRequest obj)
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                decimal amt = 0;
                amt = obj.usd;
                TempData["usd"] = obj.usd.ToString();
                if (amt > 0)
                {
                    return RedirectToAction("Confirmusdt");
                }
                else
                {
                    ViewBag.error = "Please enter amount....!";
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Confirmusdt()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                modUsdtRequest objBTC = new modUsdtRequest();
                decimal totalusd = Convert.ToDecimal(TempData["usd"]);
                TempData["totalusd"] = (objBTC.usd = totalusd).ToString();
                //TempData["totalbtc"] = (objBTC.btc = totalusd).ToString();
                objBTC.address = "TXQ5qBjUVDqzX3Btoh2YyXVWnsymhxJa8P";
                return View(objBTC);
            }
        }
        public async Task<IActionResult> TranCheck(string tranno)
        {
            string msg = "Please Enter Tran no....!";
            if (!string.IsNullOrEmpty(tranno))
            {
                string uname = await _icontract.TranCheckAsync(tranno);
                return Json(uname);
            }
            else
            {
                return Json(msg);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Confirmusdt(modUsdtRequest obj)
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                obj.usd = Convert.ToDecimal(TempData["totalusd"]);
                obj.address = "TNTpW2VGTJzhNizKg9iMK4PVBgnZJw2dhA";
                obj.Regno = regno;
                int result = await _icontract.fundrequestusdt(obj);
                if (result > 0)
                {
                    TempData["fundrequsdt"] = obj.usd.ToString();
                    ViewBag.Message = "Usdt Fund Request Successfull";
                    return RedirectToAction("ThanksUsdtRequest");
                }
                else
                {
                    ViewBag.error = "Request failed please try again....!";
                }
            }
            return View(obj);
        }
        [HttpGet]
        public IActionResult ThanksUsdtRequest()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> FundRequestReport()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var query = from d in _sscapitalcontext.TblBankDetails
                            join a in _sscapitalcontext.TblAppmsts on d.Regno equals a.RegNo into appDetails
                            from a in appDetails.DefaultIfEmpty()
                            select new modFundrequestRpt
                            {
                                Name = a.Name,
                                Regno = d.Regno,
                                amount = d.Amount,
                                pmode = d.Pmode,
                                Remarks = d.Remark,
                                Utrno = d.Utrno,
                                CreateDate = d.CreateDate.ToString(),
                                BankName = d.BankName,
                                BranchName = d.BranchName,
                                AccountNo = d.AccountNo,
                                UpiId = d.Upiid,
                                usd = d.Usd,
                                Status = d.Status == 0 ? "Pending"
                                         : d.Status == 1 ? "Approved"
                                         : d.Status == 2 ? "Cancelled"
                                         : "Unknown"
                            };

                var result = await query.ToListAsync();
                if (result != null)
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }

                return View();
            }
        }

        public async Task<IActionResult> UsdtFundRequestReport()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "usdtfundrequest");
                var result = await dbContext.fundusdt
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
                return View();
            }
        }
        public async Task<IActionResult> UsdtFundReceiveReport()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "fundreceivereport");
                var result = await dbContext.fundreceive
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
                return View();
            }
        }
    }
}
