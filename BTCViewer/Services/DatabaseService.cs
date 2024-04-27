using BTCViewer.Models;
using BTCViewer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace BTCViewer.Services
{
    public class DatabaseService
    {
        DataContext DataContext;
        public DatabaseService() 
        {
            DataContext = new DataContext();
            DataContext.Bpi.Include(i => i.BtcData).ToList();
        }

        public List<DbBTCViewModel> Load()
        {
            return DataContext.Bpi.Local.Select(s => new DbBTCViewModel
            {
                Id = s.Id,
                Updated = s.BtcData.Updated,
                UpdatedISO = s.BtcData.Updated_iso,
                UpdatedUK = s.BtcData.Updated_uk,
                ChartName = s.BtcData.Chart_name,
                Disclaimer = s.BtcData.Disclaimer,
                Code = s.Code,
                Symbol = s.Symbol,
                Rate = s.Rate,
                Description = s.Description,
                RateFloat = s.Rate_float,
                Note = s.Note
            }).ToList();
            //return DataContext.Bpi.Local.ToBindingList();
        }

        public void Save(BTCInfo bTCInfo)
        {
            BtcData btcDataNew = DataContext.BtcData.Add(new BtcData
            {
                Updated = bTCInfo.time.updated,
                Updated_iso = bTCInfo.time.updatedISO,
                Updated_uk = bTCInfo.time.updateduk,
                Chart_name = bTCInfo.chartName,
                Disclaimer = bTCInfo.disclaimer
            });
            DataContext.SaveChanges();

            DataContext.Bpi.Add(new Models.Bpi
            {
                Btc_data_id = btcDataNew.Id,
                Code = bTCInfo.bpi.USD.code,
                Symbol = bTCInfo.bpi.USD.symbol,
                Rate = bTCInfo.bpi.USD.rate,
                Description = bTCInfo.bpi.USD.description,
                Rate_float = bTCInfo.bpi.USD.rate_float,
            });

            DataContext.Bpi.Add(new Models.Bpi
            {
                Btc_data_id = btcDataNew.Id,
                Code = bTCInfo.bpi.GBP.code,
                Symbol = bTCInfo.bpi.GBP.symbol,
                Rate = bTCInfo.bpi.GBP.rate,
                Description = bTCInfo.bpi.GBP.description,
                Rate_float = bTCInfo.bpi.GBP.rate_float,
            });

            DataContext.Bpi.Add(new Models.Bpi
            {
                Btc_data_id = btcDataNew.Id,
                Code = bTCInfo.bpi.EUR.code,
                Symbol = bTCInfo.bpi.EUR.symbol,
                Rate = bTCInfo.bpi.EUR.rate,
                Description = bTCInfo.bpi.EUR.description,
                Rate_float = bTCInfo.bpi.EUR.rate_float,
            });
            DataContext.SaveChanges();
        }

        public void Delete(List<DbBTCViewModel> data)
        {
            foreach (DbBTCViewModel item in data)
            {
                Models.Bpi bpi = DataContext.Bpi.Find(item.Id);
                if (bpi != null)
                {
                    DataContext.Bpi.Remove(bpi);
                    DataContext.SaveChanges();
                }
            }
        }

        public void Update(List<DbBTCViewModel> data)
        {
            foreach (DbBTCViewModel item in data)
            {
                Models.Bpi bpi = DataContext.Bpi.Find(item.Id);
                if (bpi != null)
                {
                    bpi.Code = item.Code;
                    bpi.Symbol = item.Symbol;
                    bpi.Rate = item.Rate;
                    bpi.Description = item.Description;
                    bpi.Note = item.Note;
                    bpi.Rate_float = item.RateFloat;

                    DataContext.SaveChanges();

                    BtcData btcData = DataContext.BtcData.Find(bpi.Btc_data_id);
                    if (btcData != null)
                    {
                        btcData.Updated = item.Updated;
                        btcData.Updated_iso = item.UpdatedISO;
                        btcData.Updated_uk = item.UpdatedUK;
                        btcData.Chart_name = item.ChartName;
                        btcData.Disclaimer = item.Disclaimer;

                        DataContext.SaveChanges();
                    }
                }
            }
        }
    }
}
