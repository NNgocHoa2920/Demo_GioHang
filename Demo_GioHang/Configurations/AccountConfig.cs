using Demo_GioHang.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_GioHang.Configurations
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            //cấu hình bảng tương vs model
            builder.HasKey(x => x.Id); // thiết lập khóa chính
            //cấu hình thuộc tính
            builder.Property(x => x.Password)
                .HasColumnName("Mật khẩu")
                .HasColumnType("varchar(255)");
            //tương ứng: name là kiểu nvarchar(256)
            builder.Property(x => x.Name)
                .IsUnicode(true).
                IsFixedLength(true)
                .HasMaxLength(256);
            //cấu hình mqh 1-1 với giỏ hàng
            builder.HasOne(x => x.GioHang)
                .WithOne(x => x.Account)
                .HasForeignKey<GioHang>(x => x.AccountID);
        
        }
    }
}
