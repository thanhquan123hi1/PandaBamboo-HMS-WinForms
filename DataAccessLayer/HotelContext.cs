using DataAccessLayer.EntityClass;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class HotelContext : DbContext
    {
        public HotelContext() : base("HotelDbConnection") 
        {
        }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<LoaiDV> LoaiDVs { get; set; }
        public DbSet<DatPhong> DatPhongs { get; set; }
        public DbSet<QuanLy> QuanLys { get; set; }
        public DbSet<LapHoaDon> LapHoaDons { get; set; }
        public DbSet<SuDungDichVu> SuDungDichVus { get; set; }
        public DbSet<ThuocLoaiDV> ThuocLoaiDVs { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phong>()
                .HasMany(p => p.DatPhongs)
                .WithRequired(d => d.Phong)
                .HasForeignKey(d => d.MaPH);
        }
    }
}
