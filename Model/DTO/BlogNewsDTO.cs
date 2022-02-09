using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class BlogNewsDTO
    {
        public string Title { get; set; }

         
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public int BrowseCount { get; set; }
        public int LikeCount { get; set; }
        public int TypeId { get; set; }
        public int WriterId { get; set; }
         
        public TypeInfo TypeInfo { get; set; }
         
        public WriterInfo WriterInfo { get; set; }
    }
}
