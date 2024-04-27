using System;

namespace BTCViewer
{
    public class Bpi
    {
        public Currency USD { get; set; }
        public Currency GBP { get; set; }
        public Currency EUR { get; set; }
    }

    public class Currency
    {
        public string code { get; set; }
        public string symbol { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
        public decimal rate_float { get; set; }
    }

    public class BTCInfo
    {
        public Time time { get; set; }
        public string disclaimer { get; set; }
        public string chartName { get; set; }
        public Bpi bpi { get; set; }
    }

    public class Time
    {
        public string updated { get; set; }
        public string updatedISO { get; set; }
        public string updateduk { get; set; }
    }
}
