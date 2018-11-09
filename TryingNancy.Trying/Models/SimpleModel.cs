using System;
using System.Collections.Generic;
using System.Text;

namespace TryingNancy.Trying.Models
{
    public class SimpleModel
    {
        public string AlphaNumericData1 { get; set; }

        public string AlphaNumericData2 { get; set; }

        public int NumberData { get; set; }

        public DateTime DateTimeData { get; set; }

        public long LongNumberData { get; set; }

        public IList<string> ListOfAlphaNumericData { get; set; } = new List<string>();

        public bool BooleanData { get; set; }
    }
}