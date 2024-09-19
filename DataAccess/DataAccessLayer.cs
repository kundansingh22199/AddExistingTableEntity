using BussinessEntity.Models;
using DataAccess.Models;
using InterfaceLibrary;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataAccessLayer:IContract
    {
        private readonly SscapitalContext _context;

        public DataAccessLayer(SscapitalContext context)
        {
            _context = context;
        }
        public async Task<string> GetAppMstRegNo()
        {
            try
            {
                string prefix = "SCG";
                string regNo = "";
                Random rnd = new Random();
                const string charPool = "0123456789";

                while (true)
                {
                    StringBuilder sb = new StringBuilder(6);
                    for (int i = 0; i < 6; i++)
                    {
                        sb.Append(charPool[rnd.Next(charPool.Length)]);
                    }
                    regNo = prefix + sb.ToString();
                    bool exists = await _context.TblAppmsts
                        .AnyAsync(a => a.RegNo == regNo);

                    if (!exists)
                    {
                        break;
                    }
                }

                return regNo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public async Task<string> CheckSponsorIDAsync(string sid)
        {
            string msg = "Please Enter Recommendation Code....!";

            if (string.IsNullOrWhiteSpace(sid))
            {
                return msg;
            }
            var sidParam = new SqlParameter("@SID", sid);
            var unameParam = new SqlParameter("@uname", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC CheckSidAndUname @SID, @uname OUTPUT",
                sidParam, unameParam);
            string uname = (string)unameParam.Value;
            return string.IsNullOrEmpty(uname) ? msg : uname;
        }
        public async Task<int> InsertDataAsync(TblAppmst obj)
        {
            var result = 0;
            obj.RegNo = await GetAppMstRegNo();
            if (!string.IsNullOrEmpty(obj.RegNo))
            {
                result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC Proc_Insert_Data @Name = {0}, @regno = {1}, @MobileNo = {2}, @Email = {3}, @Password = {4}, @LeftRight = {5}, @SID = {6}",
                obj.Name, obj.RegNo, obj.MobileNo, obj.Email, obj.Password, obj.LeftRight, obj.Sid);
            }
            return result;
        }
        public async Task<TblAppmst> GetDataAsync(string regNo)
        {
            var regNoParam = new SqlParameter("@RegNo", regNo);

            // Execute the stored procedure and materialize the result
            var result = await _context.TblAppmsts
                .FromSqlRaw("EXEC Proc_Get_Data @RegNo", regNoParam)
                .ToListAsync();
            return result.FirstOrDefault();
        }
        public async Task<int> SignInAsync(ModLogin obj)
        {
            var regNoParam = new SqlParameter("@RegNo", obj.UserId);
            var passwordParam = new SqlParameter("@Password", obj.Password);
            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC Proc_Login @RegNo, @Password",
                regNoParam,
                passwordParam);
            return result;
        }
        public async Task<ModDashboard> GetDashboardAsync(string regNo)
        {
            var regNoParam = new SqlParameter("@RegNo", regNo);

            // Execute the stored procedure and materialize the result
            var result = await _context.dashboard
                .FromSqlRaw("EXEC Proc_GetBalance @RegNo", regNoParam)
                .ToListAsync();
            return result.FirstOrDefault();
        }
        public async Task<List<ModTeamDetails>> GetMyTeamAsync(string regNo)
        {
            var regnoParam = new SqlParameter("@UserId", regNo);
            var flagParam = new SqlParameter("@flag", "getallmember");
            var result = await _context.myteam
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<string> UtrCheckAsync(string utrno)
        {
            var sidParam = new SqlParameter("@utrno", utrno);
            var unameParam = new SqlParameter("@uname", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC Proc_CheckUTR_status @utrno, @uname OUTPUT",
                sidParam, unameParam);
            string uname = (string)unameParam.Value;
            return uname;
        }
        public async Task<int> fundrequest(modFundrequest obj)
        {
            var result = 0;
            result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC Proc_Fund_request @regno = {0}, @amount = {1}, @usd = {2}, @pmode = {3}, @utrno = {4}, @remarks = {5}, @bankname = {6}, @branchname = {7}, @accountno = {8}, @accounthname = {9}, @upiid = {10}",
                obj.Regno, obj.amount, obj.usd, obj.RequestType,obj.Utrno, obj.Remarks, obj.BankName, obj.BranchName,obj.AccountNo,obj.AccountHName,obj.UpiId);
            return result;
        }
        public async Task<string> TranCheckAsync(string tran)
        {
            var sidParam = new SqlParameter("@utrno", tran);
            var unameParam = new SqlParameter("@uname", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC Proc_Checktran_usdtstatus @utrno, @uname OUTPUT",
                sidParam, unameParam);
            string uname = (string)unameParam.Value;
            return uname;
        }
        public async Task<int> fundrequestusdt(modUsdtRequest obj)
        {
            var result = 0;
            result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC spInsertPaymentRequest @Regno = {0}, @usd = {1}, @address = {2}, @TranNo = {3}, @Remark = {4}",
                obj.Regno, obj.usd, obj.address, obj.TranNo, obj.Remark);
            return result;
        }
        public async Task<List<modUsdtRequest>> UsdtFundRequestReportAsync(string regNo)
        {
            var regnoParam = new SqlParameter("@UserId", regNo);
            var flagParam = new SqlParameter("@flag", "usdtfundrequest");
            var result = await _context.fundusdt
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<List<modFundReceive>> UsdtFundReceiveReportAsync(string regNo)
        {
            var regnoParam = new SqlParameter("@UserId", regNo);
            var flagParam = new SqlParameter("@flag", "fundreceivereport");
            var result = await _context.fundreceive
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<decimal> TopupBalance(string regno)
        {
            var regnoParam = new SqlParameter("@RegNo", regno);
            var balance = await _context.topupbal
                .FromSqlRaw("EXEC proc_topfund_manage @RegNo", regnoParam)
                .ToListAsync();
            return balance.FirstOrDefault()?.bal ?? 0;
        }
        public async Task<string> GetTranPassword(string regno)
        {
            var regnoParam = new SqlParameter("@regno", regno);
            var result = await _context.pass
                .FromSqlRaw("EXEC proc_gettrans_password @regno", regnoParam)
                .ToListAsync();

            var password = result.FirstOrDefault()?.TranPassword;

            return password.ToString();
        }
        public async Task<int> TopUpAsync(ModTopup obj)
        {
            var regnoParam = new SqlParameter("@RegNo", obj.regno);
            var amountParam = new SqlParameter("@Amount", obj.amount);
            var fromidParam = new SqlParameter("@From_ID", obj.fromid);
            var tranParam = new SqlParameter("@TranPassword", obj.password);
            string sqlCommand = "EXEC Proc_Topup_Wallet @RegNo, @Amount, @From_ID, @TranPassword";
            int result = await _context.Database.ExecuteSqlRawAsync(
                sqlCommand,
                regnoParam, amountParam, fromidParam, tranParam);
            return result;
        }
        public async Task<List<topupreport>> TopUpReportAsync(string regNo)
        {
            var regnoParam = new SqlParameter("@UserId", regNo);
            var flagParam = new SqlParameter("@flag", "Topupdetails");
            var result = await _context.topup
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<decimal> GetBalance(string regno)
        {
            var regnoParam = new SqlParameter("@regno", regno);
            var balance = await _context.topupbal
                .FromSqlRaw("EXEC proc_getwith_balance @regno", regnoParam)
                .ToListAsync();
            return balance.FirstOrDefault()?.bal ?? 0;
        }
        public async Task<int> withdrawBalance(ModWithdraw obj)
        {
            var result = 0;
            result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC Proc_withdraw_req @RegNo = {0}, @Amount = {1}, @AccountNo = {2}, @IFSC = {3}, @bank = {4}, @CryptoAddress = {5}, @wallettype = {6}",
                obj.regno, obj.amount, obj.AccountNo, obj.IFSC, obj.bank,obj.usdtaddress,obj.wallettype);
            return result;
        }
        public async Task<List<Withdrawreport>> WithdrawReport(string regNo)
        {
            var regnoParam = new SqlParameter("@UserId", regNo);
            var flagParam = new SqlParameter("@flag", "withdrawreport");
            var result = await _context.withdraw
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<List<ModDirectIncome>> DirectIncomeAsync(string regNo)
        {
            var regnoParam = new SqlParameter("@UserId", regNo);
            var flagParam = new SqlParameter("@flag", "directincome");
            var result = await _context.directincome
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<List<ModLevelIncome>> LevelIncomeAsync(string regNo)
        {
            var regnoParam = new SqlParameter("@UserId", regNo);
            var flagParam = new SqlParameter("@flag", "levelincome");
            var result = await _context.levelincome
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<List<ModRoiIncome>> RoiIncomeAsync(string regNo)
        {
            var regnoParam = new SqlParameter("@UserId", regNo);
            var flagParam = new SqlParameter("@flag", "Roi_Income");
            var result = await _context.roiincome
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<List<modCompose>> InboxAsync(string regNo)
        {
            var regnoParam = new SqlParameter("@UserId", regNo);
            var flagParam = new SqlParameter("@flag", "Inbox");
            var result = await _context.mail
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<List<modCompose>> OutboxAsync(string regNo)
        {
            var regnoParam = new SqlParameter("@UserId", regNo);
            var flagParam = new SqlParameter("@flag", "Outbox");
            var result = await _context.mail
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<int> ComposeAsync(modCompose obj)
        {
            var result = 0;
            result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC usp_ComposeMail @Subject = {0}, @Message = {1}, @Ticket = {2}, @sendto = {3}, @RegNo = {4}, @ToRegNo = {5}, @status = {6}, @Reply = {7}, @QueryType = {8}, @Id = {9}, @Ids = {10}",
                obj.Subject, obj.Message, obj.Ticket, obj.sendto, obj.RegNo, obj.ToRegNo, "","",1,"","");
            return result;
        }
    }
}
