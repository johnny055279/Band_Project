using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using ClassLibrary;

namespace Band_Web.Models
{
    public class PostModel
    {
        public int PostId { get; set; }
        public string PostContent { get; set; }
        public int PostLikeCount { get; set; }
        public int PostDislikeCount { get; set; }
        public int PostReplyCount { get; set; }
        public int PostUserId { get; set; }
        public string[] PostTag { get; set; }
        public HttpPostedFileBase PostMainImage { get; set; }
        public string PostMainImage_Path { get; set; }
        public DateTime LastEditDate { get; set; }
    }
}