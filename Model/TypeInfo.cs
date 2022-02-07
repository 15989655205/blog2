using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TypeInfo :BaseId
    {
        [SugarColumn(ColumnDataType ="nvarchar(12)")]
        public string Name { get; set; }
    }
}
