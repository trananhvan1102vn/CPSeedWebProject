using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPSeed.Areas.Admin.Models {
    public class SummaryViewModel {
        public int NewOrderCounter { get; set; }
        public int ReviewCounter { get; set; }
        public int ReturnOrderCounter { get; set; }
        public int CustomerCounter { get; set; }

        public decimal OrderPendingToday { get; set; }
        public decimal OrderPendingThisWeek { get; set; }
        public decimal OrderPendingThisMonth { get; set; }
        public decimal OrderPendingThisYear { get; set; }
        public decimal OrderPendingTotal { get; set; }

        public decimal OrderProcessingToday { get; set; }
        public decimal OrderProcessingThisWeek { get; set; }
        public decimal OrderProcessingThisMonth { get; set; }
        public decimal OrderProcessingThisYear { get; set; }
        public decimal OrderProcessingTotal { get; set; }

        public decimal OrderCompletedToday { get; set; }
        public decimal OrderCompletedThisWeek { get; set; }
        public decimal OrderCompletedThisMonth { get; set; }
        public decimal OrderCompletedThisYear { get; set; }
        public decimal OrderCompletedTotal { get; set; }

        public decimal OrderCancelledToday { get; set; }
        public decimal OrderCancelledThisWeek { get; set; }
        public decimal OrderCancelledThisMonth { get; set; }
        public decimal OrderCancelledThisYear { get; set; }
        public decimal OrderCancelledTotal { get; set; }

        public List<OrderNewlest> OrderNewlests { get; set; }

        public List<Bestseller> BestsellerByQuantities { get; set; }
        public List<Bestseller> BestsellerByAmounts { get; set; }
        public List<Bestseller> OrderUnFinisheds { get; set; }

        public SummaryViewModel() {
            OrderNewlests = new List<OrderNewlest>();
            BestsellerByQuantities = new List<Bestseller>();
            BestsellerByAmounts = new List<Bestseller>();
            OrderUnFinisheds = new List<Bestseller>();
        }

    }

    public class OrderNewlest {
        public Guid OrderId { get; set; }
        public string PONumber { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class Bestseller {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}