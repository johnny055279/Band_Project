//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Band_Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tTicketPurchase
    {
        public int TicketPurchaseId { get; set; }
        public int UserId { get; set; }
        public int TicketDetailId { get; set; }
        public byte Quantity { get; set; }
    
        public virtual tTicketDetail tTicketDetail { get; set; }
        public virtual tUser tUser { get; set; }
    }
}
