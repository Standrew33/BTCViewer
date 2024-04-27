using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCViewer.Models.ViewModels
{
    public class DbBTCViewModel
    {
        public int Id { get; set; }
        public string Updated { get; set; }
        public string UpdatedISO { get; set; }
        public string UpdatedUK { get; set; }
        public string ChartName { get; set; }
        public string Disclaimer { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public string Rate { get; set; }
        public string Description { get; set; }
        public decimal RateFloat { get; set; }
        public string Note { get; set; }
    }
}
