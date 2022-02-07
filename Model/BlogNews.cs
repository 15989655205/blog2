using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BlogNews:BaseId
    {
        [SugarColumn(ColumnDataType ="nvarchar(30)")]
        public string Title { get; set; }
        
        [SugarColumn(ColumnDataType = "text")]
        public string Content { get; set; }
        public DateTime Time  { get; set; }
        public int BrowseCount { get; set; }
        public int LikeCount { get; set; }
        public int TypeId { get; set; }
        public int WriterId { get; set; }
        [SugarColumn(IsIgnore =true)]
        public TypeInfo TypeInfo { get; set; }
        [SugarColumn(IsIgnore = true)]
        public TypeInfo WriterInfo { get; set; }

    }
}
