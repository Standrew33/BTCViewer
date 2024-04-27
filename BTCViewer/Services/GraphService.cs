using BTCViewer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCViewer.Services
{
    public class GraphService
    {
        private List<string> currency = new List<string>{ "USD", "GBP", "EUR" };

        public Dictionary<string, KeyValuePair<DateTime[], decimal[]>> LoadScatter(List<DbBTCViewModel> source)
        {
            Dictionary<string, KeyValuePair<DateTime[], decimal[]>> dicAxis = new Dictionary<string, KeyValuePair<DateTime[], decimal[]>>();

            foreach(string item in currency)
            {
                KeyValuePair<DateTime[], decimal[]> axisTimePrice = new KeyValuePair<DateTime[], decimal[]>(
                    source.Where(x => x.Code == item).Select(s => DateTime.Parse(s.UpdatedISO)).ToArray(),
                    source.Where(x => x.Code == item).Select(s => s.RateFloat).ToArray());
                dicAxis.Add(item, axisTimePrice);

            }

            return dicAxis;
        }
    }
}
