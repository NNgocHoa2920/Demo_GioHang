using Microsoft.EntityFrameworkCore;

namespace Demo_GioHang.Models
{
    public class GioHangDBContext : DbContext
    {
        //nếu cho chuỗi kn vào class này thì thêm contructor k có tham
        //nếu để chuỗi kn r appseting thì có hoặc k cũng đc
        public GioHangDBContext(DbContextOptions options) : base(options)
        {
        }
        //khởi tạo các dbset
        //đại diện cho tưng bảng
        //có bn class thì có bấy nhiêu db context
        public DbSet<Account> Accounts { get; set; }
        public DbSet<GioHang> GioHangs {  get; set; }
        public DbSet<GHCT> GHCTs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
    }
}
