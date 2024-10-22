using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StockMaster.Entitys;

public partial class StockContext : DbContext
{
    public StockContext()
    {
    }

    public StockContext(DbContextOptions<StockContext> options)  : base(options)
    {
    }

    public virtual DbSet<Alert> Alerts { get; set; }

    public virtual DbSet<AnalystRating> AnalystRatings { get; set; }

    public virtual DbSet<Companies> Companies { get; set; }

    public virtual DbSet<Dividend> Dividends { get; set; }

    public virtual DbSet<Portfolio> Portfolios { get; set; }

    public virtual DbSet<PortfolioStock> PortfolioStocks { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sectors> Sectors { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WatchlistStock> WatchlistStocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=TikTak;Initial Catalog=stock;Integrated Security=True;Trust Server Certificate=True");

    public static List<User> GetAllUsers()
    {
        using (var context = new StockContext()) // Замените YourDbContext на ваш контекст
        {
            return context.Users.ToList();
        }
    }
    public static void AddNewUser(User newUser)
    {
        using (var context = new StockContext()) // Замените YourDbContext на ваш контекст
        {
            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }

    public static void AddNewPortfolio(Portfolio newPortfolio)
    {
        using (var context = new StockContext()) // Замените YourDbContext на ваш контекст
        {
            context.Portfolios.Add(newPortfolio);
            context.SaveChanges();
        }
    }

    public static List<Portfolio> GetAllPortfolios()
    {
        using (var context = new StockContext()) // Замените YourDbContext на ваш контекст
        {
            return context.Portfolios.ToList();
        }
    }
    public static List<Stock> GetAllStocks()
    {
        using (var context = new StockContext()) // Замените YourDbContext на ваш контекст
        {
            return context.Stocks.ToList();
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(e => e.AlertId).HasName("PK__Alerts__4B8FB03AC030704A");

            entity.Property(e => e.AlertId).HasColumnName("alert_id");
            entity.Property(e => e.AlertType)
                .HasMaxLength(255)
                .HasColumnName("alert_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.StockId).HasColumnName("stock_id");
            entity.Property(e => e.ThresholdValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("threshold_value");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Stock).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__Alerts__stock_id__59063A47");

            entity.HasOne(d => d.User).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Alerts__user_id__5812160E");
        });

        modelBuilder.Entity<AnalystRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__AnalystR__D35B278BDFF00010");

            entity.Property(e => e.RatingId).HasColumnName("rating_id");
            entity.Property(e => e.AnalystName)
                .HasMaxLength(255)
                .HasColumnName("analyst_name");
            entity.Property(e => e.Rating)
                .HasMaxLength(20)
                .HasColumnName("rating");
            entity.Property(e => e.RatingDate).HasColumnName("rating_date");
            entity.Property(e => e.StockId).HasColumnName("stock_id");

            entity.HasOne(d => d.Stock).WithMany(p => p.AnalystRatings)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__AnalystRa__stock__5070F446");
        });

        modelBuilder.Entity<Companies>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__3E2672353C7A3A7A");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Ceo)
                .HasMaxLength(255)
                .HasColumnName("ceo");
            entity.Property(e => e.Headquarters)
                .HasMaxLength(255)
                .HasColumnName("headquarters");
            entity.Property(e => e.Industry)
                .HasMaxLength(255)
                .HasColumnName("industry");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Dividend>(entity =>
        {
            entity.HasKey(e => e.DividendId).HasName("PK__Dividend__7635F10C6E8EC3FD");

            entity.Property(e => e.DividendId).HasColumnName("dividend_id");
            entity.Property(e => e.AnnouncementDate).HasColumnName("announcement_date");
            entity.Property(e => e.DividendAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("dividend_amount");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.StockId).HasColumnName("stock_id");

            entity.HasOne(d => d.Stock).WithMany(p => p.Dividends)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__Dividends__stock__4CA06362");
        });

        modelBuilder.Entity<Portfolio>(entity =>
        {
            entity.HasKey(e => e.PortfolioId).HasName("PK__Portfoli__42EE526F045A4C58");

            entity.Property(e => e.PortfolioId).HasColumnName("portfolio_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PortfolioName)
                .HasMaxLength(255)
                .HasColumnName("portfolio_name");
            entity.Property(e => e.TotalValue)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("total_value");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Portfolios)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Portfolio__user___45F365D3");
        });

        modelBuilder.Entity<PortfolioStock>(entity =>
        {
            entity.HasKey(e => e.PortfolioStockId).HasName("PK__Portfoli__2698A757F6A3F8DB");

            entity.Property(e => e.PortfolioStockId).HasColumnName("portfolio_stock_id");
            entity.Property(e => e.PortfolioId).HasColumnName("portfolio_id");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            entity.Property(e => e.PurchasePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("purchase_price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StockId).HasColumnName("stock_id");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.PortfolioStocks)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK__Portfolio__portf__48CFD27E");

            entity.HasOne(d => d.Stock).WithMany(p => p.PortfolioStocks)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__Portfolio__stock__49C3F6B7");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC4EF2FAFF");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Sectors>(entity =>
        {
            entity.HasKey(e => e.SectorId).HasName("PK__Sectors__17DFCD53759DF56F");

            entity.Property(e => e.SectorId).HasColumnName("sector_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__Stocks__E86668625509EEBF");

            entity.Property(e => e.StockId).HasColumnName("stock_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CurrentPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("current_price");
            entity.Property(e => e.MarketCap)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("market_cap");
            entity.Property(e => e.SectorId).HasColumnName("sector_id");
            entity.Property(e => e.Symbol)
                .HasMaxLength(10)
                .HasColumnName("symbol");

            entity.HasOne(d => d.Company).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Stocks__company___3D5E1FD2");

            entity.HasOne(d => d.Sector).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("FK__Stocks__sector_i__3E52440B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F2B153D4A");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61644552D36D").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("FK__Users__role__4222D4EF");
        });

        modelBuilder.Entity<WatchlistStock>(entity =>
        {
            entity.HasKey(e => e.WatchlistStockId).HasName("PK__Watchlis__B9BA5942578F31C6");

            entity.Property(e => e.WatchlistStockId).HasColumnName("watchlist_stock_id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("added_at");
            entity.Property(e => e.StockId).HasColumnName("stock_id");

            entity.HasOne(d => d.Stock).WithMany(p => p.WatchlistStocks)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__Watchlist__stock__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
