using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace eShop.CoreBusiness.Models
{
    public class Order
    {
        public int? OrderId { get; set; }
        public string UniqueId { get; set; }
        public DateTime? DatePlaced { get; set; }
        public DateTime? DateProcessed { get; set; }

        [Required(ErrorMessage = "Customer Name is required.")]
        [StringLength(100, ErrorMessage = "Customer Name cannot exceed 100 characters.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Customer Address is required.")]
        [StringLength(250, ErrorMessage = "Customer Address cannot exceed 250 characters.")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Customer City is required.")]
        [StringLength(50, ErrorMessage = "Customer City cannot exceed 50 characters.")]
        public string CustomerCity { get; set; }

        [Required(ErrorMessage = "Customer State / Province is required.")]
        [StringLength(50, ErrorMessage = "Customer State / Province cannot exceed 50 characters.")]
        public string CustomerStateProvince { get; set; }

        [Required(ErrorMessage = "Customer Country is required.")]
        [StringLength(50, ErrorMessage = "Customer Country cannot exceed 50 characters.")]
        public string CustomerCountry { get; set; }

        public string AdminUser { get; set; }
        public List<OrderLineItem> LineItems { get; set; }

        public Order()
        {
            LineItems = new List<OrderLineItem>();
        }

        public void AddProduct(int productId, int qty, double price)
        {
            var item = LineItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity += qty;
            }
            else
            {
                LineItems.Add(new OrderLineItem
                {
                    ProductId = productId,
                    Quantity = qty,
                    Price = price,
                    OrderId = OrderId ?? 0
                });
            }
        }

        public void RemoveProduct(int productId)
        {
            var item = LineItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                LineItems.Remove(item);
            }
        }

        public int TotalItems => LineItems != null ? LineItems.Sum(x => x.Quantity) : 0;

        public double TotalPrice => LineItems != null ? LineItems.Sum(x => x.Price * x.Quantity) : 0;

        public bool Validate()
        {
            if (LineItems == null || LineItems.Count <= 0) return false;

            foreach (var item in LineItems)
            {
                if (item.ProductId <= 0 || item.Price <= 0 || item.Quantity <= 0) return false;
            }

            if (string.IsNullOrWhiteSpace(CustomerName) ||
                string.IsNullOrWhiteSpace(CustomerAddress) ||
                string.IsNullOrWhiteSpace(CustomerCity) ||
                string.IsNullOrWhiteSpace(CustomerStateProvince) ||
                string.IsNullOrWhiteSpace(CustomerCountry))
            {
                return false;
            }

            return true;
        }
    }
}
