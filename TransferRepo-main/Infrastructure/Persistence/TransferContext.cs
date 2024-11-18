using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class TransferContext : DbContext
    {
        public TransferContext(DbContextOptions<TransferContext> options) : base(options)
        {
        }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferType> TransferTypes { get; set; }
        public DbSet<TransferStatus> TransferStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("Transfer");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();

                entity.Property(t => t.Amount).HasColumnType("decimal(18,2)");



                entity.HasOne(t => t.TransferType)
                      .WithMany(tt=>tt.Transfers)
                      .HasForeignKey(t => t.TypeId);

                entity.HasOne(t=>t.Status)
                      .WithMany(s=>s.Transfers)
                      .HasForeignKey(t => t.StatusId);
            });
            modelBuilder.Entity<TransferType>(entity =>
            {
                entity.ToTable("TransferType");
                entity.HasKey(tt => tt.TransferTypeId);


                entity.HasData(
                    new TransferType
                    {
                        TransferTypeId = 1,
                        Name = "Varios"
                    },
                    new TransferType
                    {
                        TransferTypeId = 2,
                        Name = "Alquileres"
                    },
                    new TransferType
                    {
                        TransferTypeId = 3,
                        Name = "Cuotas"
                    },
                    new TransferType
                    {
                        TransferTypeId = 4,
                        Name = "Facturas"
                    },
                    new TransferType
                    {
                        TransferTypeId = 5,
                        Name = "Seguros"
                    },
                    new TransferType
                    {
                        TransferTypeId = 6,
                        Name = "Honorarios"
                    },
                    new TransferType
                    {
                        TransferTypeId = 7,
                        Name = "Prestamos"
                    }

                    );
            });

            modelBuilder.Entity<TransferStatus>(entity => 
            {
                entity.ToTable("TransferStatus");
                entity.HasKey(ts => ts.TransferStatusId);


                entity.HasData(
                    new TransferStatus
                    {
                        TransferStatusId = 1,
                        Status = "Pending"
                    },
                    new TransferStatus
                    {
                        TransferStatusId = 2,
                        Status = "Accepted"
                    },
                    new TransferStatus
                    {
                        TransferStatusId = 3,
                        Status = "Denied"
                    }


                );
            });
            
        }

    }
}
