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
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public int PostLikeCount { get; set; }
        public int PostDislikeCount { get; set; }
        public int PostReplyCount { get; set; }
        public int PostUserId { get; set; }
        public string[] PostTag { get; set; }

        public byte[] PostBackGroundImage { get; set; }

        public string PostBackGroundImage_base64
        {
            get => ImageConvertion.ByteToImage(PostBackGroundImage);
        }

        public string PostContentImage { get; set; }
        public DateTime LastEditDate { get; set; }
        public Image PostBackGroundImage_post_back { get; set; }
        public Image PostContentImage_post_back { get; set; }
    }
}