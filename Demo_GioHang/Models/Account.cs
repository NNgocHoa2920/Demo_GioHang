using System.ComponentModel.DataAnnotations;

namespace Demo_GioHang.Models
{
    public class Account
    {
       // [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        //[Required]
        //[StringLength(450,MinimumLength =10, ErrorMessage ="Do dai phai tu 10-450 ki tu")]
        public string UserName {  get; set; }
        public string Password { get; set; }
        //[RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]d{3}[\\s.-]\\d{4}$",
        //    ErrorMessage ="số điên thoại phải đúng format xxx-xxx-xxxx")]
        public DateTime NgaySinh { get; set; }
        public string SDT { get; set; }
        public GioHang? GioHang { get; set; }
    }
}
