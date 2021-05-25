using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTSPRODUCT.Models;
namespace NTSPRODUCT.Hubs
{

    public class CounterInit
    {
        public int GetCountTotal()
        {
            NTSWEBEntities db = new NTSWEBEntities();

            int countResult = 0;
            try
            {
                countResult = db.LichSuTruyCaps.Sum(u => u.countTotal);
            }
            catch (Exception)
            { }
            return countResult;
        }
        public int GetCountMonth(int month,int year)
        {
            NTSWEBEntities db = new NTSWEBEntities();

            int countResult = 0;
            try
            {
                countResult = db.LichSuTruyCaps.Where(u=>u.viewMonth==month && u.viewYear==year).Sum(u => u.countTotal);
            }
            catch (Exception)
            { }
            return countResult;
        }
        public void PushCount()
        {
            try
            {
                NTSWEBEntities db = new NTSWEBEntities();
                var dateNow = DateTime.Now;
                var data = db.LichSuTruyCaps.FirstOrDefault(u => u.viewDay == dateNow.Day && u.viewMonth == dateNow.Month && u.viewYear == dateNow.Year);
                if (data != null)
                {
                    data.countTotal = data.countTotal + 1;
                    db.SaveChanges();
                }
                else
                {
                    LichSuTruyCap ls = new LichSuTruyCap();
                    ls.id = Guid.NewGuid().ToString();
                    ls.viewDay = dateNow.Day;
                    ls.viewMonth = dateNow.Month;
                    ls.viewYear = dateNow.Year;
                    ls.countTotal = 1;
                    db.LichSuTruyCaps.Add(ls);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            { }
            
        }
    }
}