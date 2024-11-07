using Demo_GioHang.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_GioHang.Configurations
{
    public class GHCTConfig : IEntityTypeConfiguration<GHCT>
    {
        public void Configure(EntityTypeBuilder<GHCT> builder)
        {
            //tạo khóa chính
            builder.HasKey(x => x.Id);
            //tạo mqh 1-n
            //hasone: chỉ ra mqh 1
            //withMany: chỉ ra bảng nhiều
            //withOne: chỉ ra bảng 1
            //HasForeignKey: chỉ ra khóa ngoại
            builder.HasOne<GioHang>(x => x.GioHang)
                .WithMany(x => x.GHCTs)
                .HasForeignKey(x => x.GioHangId);
            builder.HasOne<SanPham>(x=>x.SanPham).WithMany(x=>x.GHCTs)
                .HasForeignKey(x=>x.SanPhamId);
        }
    }
}
