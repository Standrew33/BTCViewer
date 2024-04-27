using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCViewer.Models
{
    public class DataContext : DbContext
    {
        public DataContext(): base("DefaultConnection") { }

        public DbSet<BtcData> BtcData { get; set; }
        public DbSet<Bpi> Bpi { get; set; }
    }
}
