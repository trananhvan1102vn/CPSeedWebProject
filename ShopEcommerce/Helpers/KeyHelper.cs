using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPSeed.Helpers {
    public class KeyHelper {

        public static string GetNextKey(string keyType) {
            using(ApplicationDbContext db = new ApplicationDbContext()) {
                CodeTable code = db.CodeTables.FirstOrDefault(s => s.Name == keyType);
                if (code == null) {
                    return string.Empty;
                }
                string prefix = code.Prefix;
                string newKey = string.Empty;
                DateTime now = DateTime.Now;
                int year = now.Year;
                int month = now.Month;
                int day = now.Day;
                int keyVal = 1;
                KeyData key;
                if (code.Continuous) {
                    key = db.KeyDatas.FirstOrDefault(t => t.KeyType == keyType);
                } else {
                    key = db.KeyDatas.SingleOrDefault(t => t.KeyType == keyType && t.KeyYear == year && t.KeyMonth == month && t.KeyDay == day);
                }
                if (key != null) {
                    keyVal = key.KeyValue;
                    key.KeyValue += 1;
                    db.Entry(key).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                } else {
                    key = new KeyData();
                    key.KeyType = keyType;
                    key.KeyYear = year;
                    key.KeyMonth = month;
                    key.KeyDay = day;
                    key.KeyValue = 2;
                    db.KeyDatas.Add(key);
                    db.SaveChanges();
                    keyVal = 1;
                }

                if (code.Continuous) {
                    newKey = prefix + keyVal.ToString("0000");
                } else {
                    newKey = prefix + (year % 100).ToString() + month.ToString("00") + day.ToString("00") + keyVal.ToString("0000");
                }
                return newKey;
            }
        }
    }
}