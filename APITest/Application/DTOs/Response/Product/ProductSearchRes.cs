namespace APITest.Application.DTOs.Response
{
    public class ProductSearchRes
    {
        public int total_record { get; set; }
        public int product_id { get; set; }
        public string name { get; set; }
        public string img_url { get; set; }
        public int quantity { get; set; }
        public double unit_price { get; set; }
        public int created_user { get; set; }
        public DateTime created_date { get; set; }
        public int updated_user { get; set; }
        public DateTime updated_date { get; set; }
    }
}
