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
    }
}
