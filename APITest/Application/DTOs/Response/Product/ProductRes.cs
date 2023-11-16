namespace APITest.Application.DTOs.Response.Product
{
    public class ProductRes
    {
        public int product_id { get; set; }
        public string name { get; set; }
        public string img_url { get; set; }
        public int quantity { get; set; }
        public double unit_price { get; set; }
        public DateTime created_date { get; set; }
    }
}
