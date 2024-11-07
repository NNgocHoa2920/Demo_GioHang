namespace Demo_GioHang.Models
{
    public class SanPham
    {
        public Guid SanPhamId { get; set; }
        public string SanPhamName { get; set; }
        public decimal Price {  get; set; }
        public List<GHCT> GHCTs { get; set; }
    }
}
