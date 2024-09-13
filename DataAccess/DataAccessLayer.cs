using BussinessEntity.Models;
using DataAccess.Models;
using InterfaceLibrary;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        //public async Task<List<ModTeamDetails>> GetMyTeamAsync(string regNo)
        //{
        //    var regnoParam = new SqlParameter("@UserId", regNo);
        //    var flagParam = new SqlParameter("@flag", "getallmember");
        //    var result = await _context.myteam
        //        .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
        //        .ToListAsync();

        //    return result;
        //}
    }
}
