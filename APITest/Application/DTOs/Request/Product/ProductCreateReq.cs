using System.ComponentModel.DataAnnotations;

namespace APITest.Application.DTOs.Request.Product
{
    public class ProductCreateReq
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string img_url { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public double unit_price { get; set; }
    }
}
