using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Band_Web.Models
{
    public class TicketDetailModel
    {
        public int TicketDetailId { get; set; }
        public DateTime TicketDate { get; set; }
        public decimal TicketPrice { get; set; }
        public int TicketUnitInStore { get; set; }
        public string TicketLocation { get; set; }
        public bool TicketStatus { get; set; }
        public DateTime LastEditDate { get; set; }
        public string TicketVenue { get; set; }
    }
}