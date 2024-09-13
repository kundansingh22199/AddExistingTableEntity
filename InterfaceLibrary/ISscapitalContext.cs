using BussinessEntity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary
{
    public interface ISscapitalContext
    {
        DbSet<TblAllIncome> TblAllIncomes { get; set; }

        DbSet<TblAppmst> TblAppmsts { get; set; }

        DbSet<TblBankDetail> TblBankDetails { get; set; }

        DbSet<TblBinaryIncome> TblBinaryIncomes { get; set; }

        DbSet<TblBtcPaymentRequest> TblBtcPaymentRequests { get; set; }

        DbSet<TblBusinessDetail> TblBusinessDetails { get; set; }

        DbSet<TblControlMst> TblControlMsts { get; set; }

        DbSet<TblCountryMaster> TblCountryMasters { get; set; }

        DbSet<TblDirectIncome> TblDirectIncomes { get; set; }

        DbSet<TblEwallet> TblEwallets { get; set; }

        DbSet<TblKycDetail> TblKycDetails { get; set; }

        DbSet<TblLevelDetail> TblLevelDetails { get; set; }

        DbSet<TblLevelIncome> TblLevelIncomes { get; set; }

        DbSet<TblMail> TblMails { get; set; }

        DbSet<TblManageFund> TblManageFunds { get; set; }

        DbSet<TblNews> TblNews { get; set; }

        DbSet<TblRoi> TblRois { get; set; }

        DbSet<TblTransactionDetail> TblTransactionDetails { get; set; }

        DbSet<TblTreeMaster> TblTreeMasters { get; set; }

        DbSet<ModDashboard> dashboard { get; set; }

        DbSet<ModTeamDetails> myteam { get; set; }
    }
}
