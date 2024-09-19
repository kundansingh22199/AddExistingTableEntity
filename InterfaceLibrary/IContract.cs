using BussinessEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary
{
    public interface IContract
    {
        Task<string> CheckSponsorIDAsync(string sid);
        Task<int> InsertDataAsync(TblAppmst obj);
        Task<TblAppmst> GetDataAsync(string regNo);
        Task<int> SignInAsync(ModLogin obj);
        Task<ModDashboard> GetDashboardAsync(string regNo);
        Task<List<ModTeamDetails>> GetMyTeamAsync(string regNo);
        Task<string> UtrCheckAsync(string utrno);
        Task<int> fundrequest(modFundrequest obj);
        Task<string> TranCheckAsync(string tran);
        Task<int> fundrequestusdt(modUsdtRequest obj);
        Task<List<modUsdtRequest>> UsdtFundRequestReportAsync(string regNo);
        Task<List<modFundReceive>> UsdtFundReceiveReportAsync(string regNo);
        Task<decimal> TopupBalance(string regno);
        Task<string> GetTranPassword(string regno);
        Task<int> TopUpAsync(ModTopup obj);
        Task<List<topupreport>> TopUpReportAsync(string regNo);
        Task<decimal> GetBalance(string regno);
        Task<int> withdrawBalance(ModWithdraw obj);
        Task<List<Withdrawreport>> WithdrawReport(string regNo);
        Task<List<ModDirectIncome>> DirectIncomeAsync(string regNo);
        Task<List<ModLevelIncome>> LevelIncomeAsync(string regNo);
        Task<List<ModRoiIncome>> RoiIncomeAsync(string regNo);
        Task<List<modCompose>> InboxAsync(string regNo);
        Task<List<modCompose>> OutboxAsync(string regNo);
        Task<int> ComposeAsync(modCompose obj);
    }
}
