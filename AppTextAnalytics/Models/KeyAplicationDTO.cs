using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTextAnalytics.Models
{
    public class KeyAplicationDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string KeyApp { get; set; }
        public bool IsFreeAccount { get; set; }
        public int NumOnlyFree { get; set; }
        public KeyAplicationDTO()
        {
            IsFreeAccount = true;
            NumOnlyFree = 50;
        }
    }
}
