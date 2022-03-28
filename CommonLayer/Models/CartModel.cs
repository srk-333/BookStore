namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class CartModel
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public BookModel bookmodel { get; set; }
    }
}
