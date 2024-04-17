using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PR32.Classes
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryCode { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public static IEnumerable<Manufacturer> AllManufacturers()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            DataTable recordQuery = Classes.DBConnection.Connection("Select * from [dbo].[Manufacturer]");
            foreach (DataRow row in recordQuery.Rows)
                manufacturers.Add(new Manufacturer()
                {
                    Id = Convert.ToInt32(row[0]),
                    Name = Convert.ToString(row[1]),
                    CountryCode = Convert.ToInt32(row[2]),
                    Phone = Convert.ToString(row[3]),
                    Mail = Convert.ToString(row[4]),
                });
            return manufacturers;
        }
        public void Save(bool Update = false)
        {
            if (Update == false)
            {
                Classes.DBConnection.Connection("Insert into [dbo].[Manufacturer] ([Name], [CountryCode], [Phone], [Mail])" +
                    $"Values (N'{this.Name}', {this.CountryCode}, '{this.Phone}', '{this.Mail}')");
                this.Id = Manufacturer.AllManufacturers().Where(x => x.Name == this.Name && x.CountryCode == this.CountryCode && x.Phone == this.Phone && x.Mail == this.Mail).First().Id;
            }
            else
            {
                Classes.DBConnection.Connection("Update [dbo].[Manufacturer] Set" +
                    $"[Name] = N'{this.Name}', [CountryCode] = {this.CountryCode}, [Phone] = '{this.Phone}', [Mail] = '{this.Mail}' where [Id] = {this.Id}");
            }
        }
        public void Delete() => Classes.DBConnection.Connection($"Delete from [dbo].[Manufacturer] where [Id] = {this.Id}");
    }
}
