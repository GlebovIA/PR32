using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PR32.Classes
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Format { get; set; }
        public int Size { get; set; }
        public int Manufacturer { get; set; }
        public float Price { get; set; }
        public int State { get; set; }
        public string Description { get; set; }
        public static IEnumerable<Record> AllRecords()
        {
            List<Record> records = new List<Record>();
            DataTable recordQuery = Classes.DBConnection.Connection("Select * from [dbo].[Record]");
            foreach (DataRow row in recordQuery.Rows)
            {
                records.Add(new Record
                {
                    Id = Convert.ToInt32(row[0]),
                    Name = Convert.ToString(row[1]),
                    Year = Convert.ToInt32(row[2]),
                    Format = Convert.ToInt32(row[3]),
                    Size = Convert.ToInt32(row[4]),
                    Manufacturer = Convert.ToInt32(row[5]),
                    Price = float.Parse(row[6].ToString()),
                    State = Convert.ToInt32(row[7]),
                    Description = Convert.ToString(row[8]),
                });
                return records;
            }
        }
        public void Save(bool Update = false)
        {
            string CorrectPrice = this.Price.ToString().Replace(",", ".");
            if (!Update)
            {
                Classes.DBConnection.Connection("Insert into [dbo].[Record]" +
                    "[Name], [Year], [Format], [Size], [Manufacturer], [Price], [State], [Description]" +
                    "Values" +
                    $"(N'{this.Name}', {this.Year}, {this.Format}, {this.Size}, {this.Manufacturer}, {CorrectPrice}, {this.State}, N'{this.Description}');");
                this.Id = Record.AllRecords().Where(x => x.Name == this.Name &&
                                                    x.Year == this.Year &&
                                                    x.Format == this.Format &&
                                                    x.Size == this.Size &&
                                                    x.Manufacturer == this.Manufacturer &&
                                                    x.Price == this.Price &&
                                                    x.State == this.State &&
                                                    x.Description == this.Description).First().Id;
            }
            else
            {
                Classes.DBConnection.Connection("Update [dbo].[Record]" +
                    $"Set [Name] = N'{this.Name}', [Year] = {this.Year}, [Format] = {this.Format}, [Size] = {this.Size}, [Manufacturer] = {this.Manufacturer}, [Price] = {this.Price}, [State] = {this.State}, [Description] = N'{this.Description}'" +
                    $"where [Id] = {this.Id}");
            }
        }
        public void Delete()
        {
            Classes.DBConnection.Connection($"Delete from [dbo].[Record] where [Id] = {this.Id}");
        }
    }
}
