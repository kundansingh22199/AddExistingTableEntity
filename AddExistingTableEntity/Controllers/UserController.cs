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
                var result = await _icontract.UsdtFundRequestReportAsync(regno);

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    //ViewData["lst"] = new List<modFundrequest>();
                    ViewBag.ErrorMessage = "No data found ";
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
                var result = await _icontract.UsdtFundReceiveReportAsync(regno);

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    //ViewData["lst"] = new List<modFundReceive>();
                    ViewBag.ErrorMessage = "No data found ";
                }
                return View();
            }
        }
        public async Task<IActionResult> TopUp()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                ViewBag.topupbal = await _icontract.TopupBalance(regno);
                ViewBag.regno = regno;

                return View();
            }
        }
        public async Task<IActionResult> GetTransactionPassword()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@regno", regno);
                var password = await _icontract.GetTranPassword(regno);
                //var password = result.FirstOrDefault()?.TranPassword;
                return Json(password);
            }
        }
        [HttpPost]
        public async Task<IActionResult> TopUp(ModTopup obj)
        {
            string st = "";
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                obj.fromid = regno;
                if (obj.password == await _icontract.GetTranPassword(regno))
                {
                    decimal totbal = 0;
                    totbal = await _icontract.TopupBalance(regno);
                    if (totbal >= obj.amount)
                    {
                        if (obj.amount > 0 && !string.IsNullOrEmpty(obj.regno) && !string.IsNullOrEmpty(obj.password))
                        {
                            int result = await _icontract.TopUpAsync(obj);
                            if (result > 0)
                            {
                                ViewBag.topupbal = await _icontract.TopupBalance(regno);
                                TempData["pack"] = obj.amount.ToString();
                                ViewBag.Message = "TopUp Successfull";
                                return RedirectToAction("ThanksTopup");
                            }
                            else
                            {
                                ViewBag.error = "Topup failed please try again....!";
                            }
                        }
                        else
                        {
                            st = "All Field Required";
                        }
                    }
                    else
                    {
                        st = "Insufficient Balance";
                    }
                }
                else
                {
                    st = "invalid transaction password";
                }
                ViewBag.topupbal = await _icontract.TopupBalance(regno); ;
                ViewBag.mess = st;
                ViewBag.regno = regno;
                return View();
            }
        }
        [HttpGet]
        public IActionResult ThanksTopup()
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
        public async Task<IActionResult> TopUpReport()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var result = await _icontract.TopUpReportAsync(regno);

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
        [HttpGet]
        public async Task<IActionResult> KycPage()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            var result = await _sscapitalcontext.TblKycDetails.FirstOrDefaultAsync(x => x.AppMstRegNo == regno);
            if (result!=null)
            {
                if (result.Status == 1)
                {
                    ViewBag.StatusMessage = "Approved";
                }
                else if (result.Status == 0)
                {
                    ViewBag.StatusMessage = "Pending";
                }
                else if (result.Status == 2)
                {
                    ViewBag.StatusMessage = "Cancelled";
                }
                return View(result);
            }
            else
            {
                ViewBag.error = "No KYC details found for this user.";
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> KycPage(TblKycDetail obj)
        {
            string regno = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(regno))
            {
                return RedirectToAction("SignIn", "Login");
            }
            var existingKyc = await _sscapitalcontext.TblKycDetails
                .FirstOrDefaultAsync(x => x.AppMstRegNo == regno);

            if (existingKyc != null)
            {
                existingKyc.AccountName = obj.AccountName;
                existingKyc.AccountNo = obj.AccountNo;
                existingKyc.Ifsc = obj.Ifsc;
                existingKyc.Bank = obj.Bank;
                existingKyc.Branch = obj.Branch;
                existingKyc.CryptoAddress = obj.CryptoAddress;
                existingKyc.Status = 0;
                existingKyc.Createdate= DateTime.Now.AddHours(5).AddMinutes(30);
                _sscapitalcontext.TblKycDetails.Update(existingKyc);
            }
            else
            {
                obj.AppMstRegNo = regno;
                obj.Status = 0;
                obj.Createdate = DateTime.Now.AddHours(5).AddMinutes(30);
                await _sscapitalcontext.TblKycDetails.AddAsync(obj);
            }
            await _sscapitalcontext.SaveChangesAsync();
            TempData["Message"] = "KYC details have been submitted successfully!";
            return RedirectToAction("KycPage");
        }

        public async Task<IActionResult> Withdraw()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            decimal totbal = await _icontract.GetBalance(regno);
            var kycDetails = await _sscapitalcontext.TblKycDetails
                .FirstOrDefaultAsync(x => x.AppMstRegNo == regno);
            ViewBag.withamt = totbal;
            return View(kycDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Withdraw(ModWithdraw obj)
        {
            string st = "";
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                obj.regno = regno;
                if (obj.amount >= 10)
                {
                    if (obj.password == await _icontract.GetTranPassword(regno))
                    {
                        decimal totbal = 0;
                        totbal = await _icontract.GetBalance(regno);
                        if (totbal >= obj.amount)
                        {
                            int result = await _icontract.withdrawBalance(obj);
                            if (result > 0)
                            {
                                ViewBag.withamt = await _icontract.GetBalance(regno);
                                TempData["withamt"] = obj.amount.ToString();
                                return RedirectToAction("withdrawThanks");
                            }
                            else
                            {
                                st = "Withdraw failed";
                            }
                        }
                        else
                        {
                            st = "Insufficient Balance";
                        }
                    }
                    else
                    {
                        st = "invalid transaction password";
                    }
                }
                else
                {
                    st = "Minimum Withdraw Amount $10";
                }
                var kycDetails = await _sscapitalcontext.TblKycDetails
                .FirstOrDefaultAsync(x => x.AppMstRegNo == regno);
                ViewBag.withamt = await _icontract.GetBalance(regno);
              //ViewBag.kyc = kycDetails;
                ViewBag.mess = st;
                return View(kycDetails);
            }
        }
        [HttpGet]
        public IActionResult withdrawThanks()
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
        public async Task<IActionResult> WithdrawReport()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var result = await _icontract.WithdrawReport(regno);

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
        public async Task<IActionResult> DirectIncome()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var result = await _icontract.DirectIncomeAsync(regno);
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
        public async Task<IActionResult> LevelIncome()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var result = await _icontract.LevelIncomeAsync(regno);
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
        public async Task<IActionResult> RoiIncome()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var result = await _icontract.RoiIncomeAsync(regno);

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
        public IActionResult RewardIncome()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                //var lst = _contractList.GetDirectIncome(regno);
                //ViewData["lst"] = lst;
                return View();
            }
        }
        public async Task<IActionResult> EditProfile()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var result = await _sscapitalcontext.TblAppmsts
                .FirstOrDefaultAsync(x => x.RegNo == regno);
                return View(result);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(UpdateUser obj)
        {
            string st = "";
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var userdata = await _sscapitalcontext.TblAppmsts
                            .FirstOrDefaultAsync(x => x.RegNo == regno);
                obj.UserId = regno;
                if (obj.NewPassword != obj.ConfPassword)
                {
                    @ViewBag.error = "Your New Password and Conform Password Not Matched";
                    return View(userdata);
                }
                else if (obj.TransPassword != obj.ConfTranPassword)
                {
                    @ViewBag.error = "Your New Transaction Password and Conform Transaction Password Not Matched";
                    return View(userdata);
                }
                else
                {
                    
                    if (userdata != null)
                    {
                        userdata.Name = obj.Name;
                        userdata.MobileNo = obj.Mobile;
                        userdata.Email = obj.Email;
                        if (!string.IsNullOrEmpty(obj.NewPassword))
                        {
                            userdata.Password = obj.NewPassword;
                        }
                        if (!string.IsNullOrEmpty(obj.TransPassword))
                        {
                            userdata.TranPassword = obj.TransPassword;
                        }
                        _sscapitalcontext.TblAppmsts.Update(userdata);
                        await _sscapitalcontext.SaveChangesAsync();
                        @ViewBag.Message = "Profile Update Successfull";
                        return View(userdata);
                    }
                    else
                    {
                        @ViewBag.error = "Profile Update Failed";
                        return View(userdata);
                    }
                }
            }
        }
        public async Task<IActionResult> Compose()
        {
            modCompose objcompose = new modCompose();
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var result1 = await _icontract.InboxAsync(regno);
                var result2 = await _icontract.OutboxAsync(regno);
                ViewData["lst1"] = result1;
                ViewData["lst2"] = result2;
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Compose(modCompose obj)
        {
            try
            {
                string regno = HttpContext.Session.GetString("UserId");
                if (regno == null)
                {
                    return RedirectToAction("SignIn", "Login");
                }
                else
                {
                    obj.RegNo = regno;
                    Random rand = new Random();
                    obj.Ticket = rand.Next().ToString();
                    if (obj != null)
                    {
                        int res = await _icontract.ComposeAsync(obj);
                        if (res > 0)
                        {
                            ModelState.Clear();
                            ViewBag.Message = "Message Sent successfully.";
                        }
                        else
                        {
                            ViewBag.error = "Message failed, please try again....!";
                        }
                    }
                    //else
                    //{
                    //    ViewBag.error = "Message Failed please try again...!";
                    //}
                }
                var result1 = await _icontract.InboxAsync(regno);
                var result2 = await _icontract.OutboxAsync(regno);
                ViewData["lst1"] = result1;
                ViewData["lst2"] = result2;
                return View();

            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
