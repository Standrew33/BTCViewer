using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCViewer.Models
{
    [Table("bpi")]
    public class Bpi
    {
        public int Id { get; set; }
        public int Btc_data_id { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public string Rate { get; set; }
        public string Description { get; set; }
        public decimal Rate_float { get; set; }
        public string Note { get; set; }

        [ForeignKey("Btc_data_id")]
        public BtcData BtcData { get; set; }
    }
}
