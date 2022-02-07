using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class WriterInfo:BaseId
    {
        [SugarColumn(ColumnDataType ="nvarchar(12)")]
        public string Name { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(20)")]
        public string UserName { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(50)")]
        public string UserPwd { get; set; }
    }
}
