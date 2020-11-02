using System;
using System.Web;

namespace Band_Web.Models
{
    public class PostModel
    {
        public int PostId { get; set; }
        public string PostContent { get; set; }
        public string PostContent_Decode { get { return HttpUtility.HtmlDecode(this.PostContent); } }
        public int PostUserId { get; set; }
        public int PostReplyCount { get; set; }
        public int PostLikeCount { get; set; }
        public string PostMainImage_Path { get; set; }
        public DateTime LastEditDate { get; set; }
    }
}