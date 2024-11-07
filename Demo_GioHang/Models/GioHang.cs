namespace Demo_GioHang.Models
{
    public class GioHang
    {
        public Guid Id { get; set; }
        public string UserName {  get; set; }
        public Guid SanPhamId { get; set; }
        public Account? Account { get; set; }
        public Guid? AccountID { get; set; }
        //ILIST, LIST, ICOLECTION, COLETION
        //THỂ HIỆN 1 GH CÓ NHIỀU GHCT
        //CÒN ĐC DÙNG SỬ DỤNG VÀO NAVIGATION ĐỂ TRỎ ĐẾN KHI CẦN
        public List<GHCT> GHCTs { get; set; }
        
    }
}
