using System;
using System.Collections.Generic;
using System.Data;

namespace PR32.Classes
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static IEnumerable<Country> AllCountries()
        {
            List<Country> countries = new List<Country>();
            DataTable requestCountries = DBConnection.Connection("Select * from [dbo].[Country]");
            foreach (DataRow row in requestCountries.Rows)
            {
                countries.Add(new Country
                {
                    Id = Convert.ToInt32(row[0]),
                    Name = Convert.ToString(row[1])
                });
            }
            return countries;
        }
    }
}
