using System;
using System.Collections.Generic;
using BussinessEntity.Models;
using InterfaceLibrary;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class SscapitalContext : DbContext, ISscapitalContext
{
    public SscapitalContext()
    {
    }

    public SscapitalContext(DbContextOptions<SscapitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAllIncome> TblAllIncomes { get; set; }

    public virtual DbSet<TblAppmst> TblAppmsts { get; set; }

    public virtual DbSet<TblBankDetail> TblBankDetails { get; set; }

    public virtual DbSet<TblBinaryIncome> TblBinaryIncomes { get; set; }

    public virtual DbSet<TblBtcPaymentRequest> TblBtcPaymentRequests { get; set; }

    public virtual DbSet<TblBusinessDetail> TblBusinessDetails { get; set; }

    public virtual DbSet<TblControlMst> TblControlMsts { get; set; }

    public virtual DbSet<TblCountryMaster> TblCountryMasters { get; set; }

    public virtual DbSet<TblDirectIncome> TblDirectIncomes { get; set; }

    public virtual DbSet<TblEwallet> TblEwallets { get; set; }

    public virtual DbSet<TblKycDetail> TblKycDetails { get; set; }

    public virtual DbSet<TblLevelDetail> TblLevelDetails { get; set; }

    public virtual DbSet<TblLevelIncome> TblLevelIncomes { get; set; }

    public virtual DbSet<TblMail> TblMails { get; set; }

    public virtual DbSet<TblManageFund> TblManageFunds { get; set; }

    public virtual DbSet<TblNews> TblNews { get; set; }

    public virtual DbSet<TblRoi> TblRois { get; set; }

    public virtual DbSet<TblTransactionDetail> TblTransactionDetails { get; set; }

    public virtual DbSet<TblTreeMaster> TblTreeMasters { get; set; }

    public virtual DbSet<ModDashboard> dashboard { get; set; }
    public virtual DbSet<ModTeamDetails> myteam { get; set; }
    public DbSet<modUsdtRequest> fundusdt { get; set; }
    public DbSet<modFundReceive> fundreceive { get; set; }
    public DbSet<topupbal> topupbal { get; set; }
    public DbSet<ModPassword> pass { get; set; }
    public DbSet<topupreport> topup { get; set; }
    public DbSet<Withdrawreport> withdraw { get; set; }
    public DbSet<ModDirectIncome> directincome { get; set; }
    public DbSet<ModLevelIncome> levelincome { get; set; }
    public DbSet<ModRoiIncome> roiincome { get; set; }
    public DbSet<modCompose> mail { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=SqlCon");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAllIncome>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_All_Income");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Binary).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Direct).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.FundBalance)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("Fund_Balance");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.LevelRoi)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("Level_Roi");
            entity.Property(e => e.ReceiveFund)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("Receive_Fund");
            entity.Property(e => e.RegNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Roi).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.TransFund)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("Trans_Fund");
            entity.Property(e => e.Withdraw).HasColumnType("decimal(18, 4)");
        });

        modelBuilder.Entity<TblAppmst>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Tbl_Appmst");
            entity.Property(e => e.ActiveDate).HasColumnType("datetime");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Doj)
                .HasColumnType("datetime")
                .HasColumnName("DOJ");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.JoinType).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Lastlogin)
                .HasColumnType("datetime")
                .HasColumnName("lastlogin");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Oprank).HasColumnName("oprank");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Pid)
                .HasMaxLength(100)
                .HasColumnName("PID");
            entity.Property(e => e.RegNo).HasMaxLength(100);
            entity.Property(e => e.Sid)
                .HasMaxLength(100)
                .HasColumnName("SID");
            entity.Property(e => e.TranPassword).HasMaxLength(100);
            entity.Property(e => e.Zipcode)
                .HasMaxLength(100)
                .HasColumnName("zipcode");
        });

        modelBuilder.Entity<TblBankDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_bank_details");

            entity.Property(e => e.AccountHname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AccountHName");
            entity.Property(e => e.AccountNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.Approvedate).HasColumnType("datetime");
            entity.Property(e => e.BankName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("createDate");
            entity.Property(e => e.DepositeDate).HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Mobileno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mobileno");
            entity.Property(e => e.PaidAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Pmode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PMode");
            entity.Property(e => e.Receipt)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Regno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remark)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Upiid)
                .HasMaxLength(100)
                .HasColumnName("UPIID");
            entity.Property(e => e.Usd)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("usd");
            entity.Property(e => e.Utrno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UTRNO");
        });

        modelBuilder.Entity<TblBinaryIncome>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Binary_Income");

            entity.Property(e => e.Admin).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Carryleft).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Carryright).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Income).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LeftTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RegNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RightTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tds)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TDS");
        });

        modelBuilder.Entity<TblBtcPaymentRequest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_BTC_Payment_Request");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.AdminRemark)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Approvedate).HasColumnType("datetime");
            entity.Property(e => e.Btc)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("btc");
            entity.Property(e => e.Createdate).HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Regno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remark)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.St).HasColumnName("st");
            entity.Property(e => e.TranNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usd)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("usd");
        });

        modelBuilder.Entity<TblBusinessDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Business_Detail");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.LeftActive).HasColumnName("Left_Active");
            entity.Property(e => e.LeftId)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("LeftID");
            entity.Property(e => e.RegNo).HasMaxLength(200);
            entity.Property(e => e.RightActive).HasColumnName("Right_Active");
            entity.Property(e => e.RightId)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("RightID");
            entity.Property(e => e.TotalLeftId)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalLeftID");
            entity.Property(e => e.TotalRightId)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalRightID");
        });

        modelBuilder.Entity<TblControlMst>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_ControlMst");

            entity.ToTable("Tbl_ControlMst");

            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<TblCountryMaster>(entity =>
        {
            entity.HasKey(e => e.CountryMasterId).HasName("PK__tblCount__A62FF2C9BB5AA125");

            entity.ToTable("tblCountryMaster");

            entity.Property(e => e.CountryCode).HasMaxLength(100);
            entity.Property(e => e.CountryName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblDirectIncome>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Direct_Income");

            entity.Property(e => e.Admin).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Income).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetAmt).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.RegNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SID");
            entity.Property(e => e.Tds)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("TDS");
            entity.Property(e => e.TotalAmt).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblEwallet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Ewallet");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ActiveDate).HasColumnType("datetime");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CreditIn)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Ifsc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IFSC");
            entity.Property(e => e.RegNo).HasMaxLength(200);
            entity.Property(e => e.Usdtaddress)
                .HasMaxLength(100)
                .HasColumnName("USDTAddress");
            entity.Property(e => e.Wallet)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Withdraw).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblKycDetail>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Tbl_KYC_Detail");

            entity.Property(e => e.AccountName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Account_name");
            entity.Property(e => e.AccountNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Activedate)
                .HasColumnType("datetime")
                .HasColumnName("activedate");
            entity.Property(e => e.AppMstRegNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Bank)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("bank");
            entity.Property(e => e.Branch).HasMaxLength(100);
            entity.Property(e => e.Createdate)
                .HasColumnType("datetime")
                .HasColumnName("createdate");
            entity.Property(e => e.CryptoAddress)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Ifsc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IFSC");
            entity.Property(e => e.PancardDoc)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PancardNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblLevelDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_level_Details");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Pid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PID");
            entity.Property(e => e.RegNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblLevelIncome>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Level_Income");

            entity.Property(e => e.Admin).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Income).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.NetAmt).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.RegNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SID");
            entity.Property(e => e.Tds)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TDS");
            entity.Property(e => e.TotalAmt).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<TblMail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Mail");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Message).HasMaxLength(3000);
            entity.Property(e => e.RegNo).HasMaxLength(200);
            entity.Property(e => e.Reply)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SendTo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Subject).HasMaxLength(2000);
            entity.Property(e => e.Ticket)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ToRegNo).HasMaxLength(200);
        });

        modelBuilder.Entity<TblManageFund>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Manage_Fund");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FromId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("From_ID");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.RegNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblNews>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_News");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<TblRoi>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_ROI");

            entity.Property(e => e.Admin).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Doj)
                .HasColumnType("datetime")
                .HasColumnName("DOJ");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.NetAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RegNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Roi)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("ROI");
            entity.Property(e => e.Tds)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TDS");
            entity.Property(e => e.TotalAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TranId).HasColumnName("Tran_id");
        });

        modelBuilder.Entity<TblTransactionDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Transaction_Details");

            entity.Property(e => e.ActiveDate).HasColumnType("datetime");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 8)");
            entity.Property(e => e.FromId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("From_ID");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.RegNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(2000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblTreeMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_TreeMaster");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LeftNode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PID");
            entity.Property(e => e.RegNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RightNode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SID");
        });
        modelBuilder.Entity<ModDashboard>()
            .HasKey(m => m.RegNo);
        modelBuilder.Entity<ModTeamDetails>()
            .HasKey(m => m.regno);
        modelBuilder.Entity<modFundReceive>()
            .HasNoKey()
            .ToView(null);
        modelBuilder.Entity<modUsdtRequest>()
            .HasNoKey()
            .ToView(null);
        modelBuilder.Entity<ModPassword>()
            .HasNoKey()
            .ToView(null);
        modelBuilder.Entity<topupbal>()
            .HasNoKey()
            .ToView(null);
        modelBuilder.Entity<topupreport>()
            .HasNoKey()
            .ToView(null);
        modelBuilder.Entity<Withdrawreport>()
            .HasNoKey()
            .ToView(null);
        modelBuilder.Entity<ModDirectIncome>()
               .HasNoKey()
               .ToView(null);
        modelBuilder.Entity<ModLevelIncome>()
           .HasNoKey()
           .ToView(null);
        modelBuilder.Entity<ModRoiIncome>()
           .HasNoKey()
           .ToView(null);
        modelBuilder.Entity<modCompose>()
          .HasNoKey()
          .ToView(null);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
