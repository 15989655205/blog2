using SqlSugar;
using System;

namespace Model
{
    public class BaseId
    {
        [SugarColumn(IsIdentity =true,IsPrimaryKey =true)]
        public int id { get; set; }
    }
}
