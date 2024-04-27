using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCViewer.Models
{
    [Table("btc_data")]
    public class BtcData
    {
        public int Id { get; set; }
        public string Updated { get; set; }
        public string Updated_iso { get; set; }
        public string Updated_uk { get; set; }
        public string Chart_name { get; set; }
        public string Disclaimer { get; set; }
    }
}
