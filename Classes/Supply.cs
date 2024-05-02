using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PR32.Classes
{
    public class Supply
    {
        public int Id { get; set; }
        public int Manufacturer { get; set; }
        public int Record { get; set; }
        public string DateDelivery { get; set; }
        public int Count { get; set; }
        public static IEnumerable<Supply> AllSupplies()
        {
            List<Supply> supplies = new List<Supply>();
            DataTable recordQuery = Classes.DBConnection.Connection("select * from [dbo].[Supply]");
            foreach (DataRow row in recordQuery.Rows)
            {
                DateTime dt = new DateTime();
                DateTime.TryParse(row[3].ToString(), out dt);
                string CorrectDate = dt.Year + "-" + dt.Month + "-" + dt.Day;
                supplies.Add(new Supply()
                {
                    Id = Convert.ToInt32(row[0]),
                    Manufacturer = Convert.ToInt32(row[1]),
                    Record = Convert.ToInt32(row[2]),
                    DateDelivery = CorrectDate,
                    Count = Convert.ToInt32(row[4]),
                });
            }
            return supplies;
        }
        public void Save(bool Update = false)
        {
            if (Update == false)
            {
                Classes.DBConnection.Connection(
                    "insert into [dbo].[Supply]([Manufacturer], [Record], [DateDelivery], [Count]) " +
                    $"values ({Manufacturer}, {Record}, '{DateDelivery}', {Count});");
                Id = Supply.AllSupplies().Where(
                    x => x.Manufacturer == Manufacturer &&
                    x.Record == Record &&
                    x.DateDelivery == DateDelivery &&
                    x.Count == Count).First().Id;
            }
            else
            {
                Classes.DBConnection.Connection(
                    "update [dbo].[Supply] " +
                    "set" +
                    $"[Manufacturer] = {Manufacturer}, " +
                    $"[Record] = {Record}, " +
                    $"[DateDelivery] = {DateDelivery}, " +
                    $"[Count] = {Count} " +
                    $"where [Id] = {Id};");
            }
        }
        public void Delete()
        {
            Classes.DBConnection.Connection($"delete from [dbo].[Supply] where [If] = {Id};");
        }
    }
}
