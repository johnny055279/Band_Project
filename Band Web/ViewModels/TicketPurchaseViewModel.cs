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
        public tTicketPurchase tTicketPurchase { get; set; }
    }
}