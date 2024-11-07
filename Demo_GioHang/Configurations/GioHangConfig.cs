using Demo_GioHang.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_GioHang.Configurations
{
    public class GioHangConfig : IEntityTypeConfiguration<GioHang>
    {
        public void Configure(EntityTypeBuilder<GioHang> builder)
        {
            //CẤU HÌNH KHÓA CHÍNH
            builder.HasKey(x => x.Id);

            //CÁU HÌNH 1-1 VS ACCOUNT
            //KHI CẤU HÌNH MQH 1-1 MÌNH PHẢI THỎA MÃN 2 ĐẦU
            builder.HasOne(x => x.Account)
                .WithOne(x => x.GioHang)
                .HasForeignKey<GioHang>(x => x.AccountID);

        }
    }
}
