using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopASP.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić imię")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzć nazwisko")]
        [StringLength(150)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Nie wprowadzono adresu")]
        [StringLength(150)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Wprowadź kod pocztowy i miasto")]
        [StringLength(50)]
        public string CodeAndCity { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić numer telefonu")]
        [StringLength(20)]
        [RegularExpression(@"(\+\d{2})*[\d\s-]+",
                      ErrorMessage = "Błędny format numeru telefonu.")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }

    }

    public enum OrderStatus
    {
        New,

        Shipped
    }
}