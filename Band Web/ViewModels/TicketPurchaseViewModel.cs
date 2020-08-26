using Band_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Band_Web.ViewModels
{
    public class TicketPurchaseViewModel
    {
        public tUser tUser { get; set; }
        public tTicketDetail tticketDetail { get; set; }
        public int Quantity { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public int TicketDetailId { get; set; }
        public int UserId { get; set; }
        public string TicketDate { get; set; }
        public string TicketLocation { get; set; }
        public string TicketVenue { get; set; }
    }
}