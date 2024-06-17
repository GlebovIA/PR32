using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace PR32.Classes
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Format { get; set; }
        public int Size { get; set; }
        public int IdManufacturer { get; set; }
        public float Price { get; set; }
        public int IdState { get; set; }
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
                    IdManufacturer = Convert.ToInt32(row[5]),
                    Price = float.Parse(row[6].ToString()),
                    IdState = Convert.ToInt32(row[7]),
                    Description = Convert.ToString(row[8]),
                });
            }
            return records;
        }
        public void Save(bool Update = false)
        {
            string CorrectPrice = this.Price.ToString().Replace(",", ".");
            if (!Update)
            {
                Classes.DBConnection.Connection("Insert into [dbo].[Record]" +
                    "[Name], [Year], [Format], [Size], [IdManufacturer], [Price], [IdState], [Description]" +
                    "Values" +
                    $"(N'{this.Name}', {this.Year}, {this.Format}, {this.Size}, {this.IdManufacturer}, {CorrectPrice}, {this.IdState}, N'{this.Description}');");
                this.Id = Record.AllRecords().Where(x => x.Name == this.Name &&
                                                    x.Year == this.Year &&
                                                    x.Format == this.Format &&
                                                    x.Size == this.Size &&
                                                    x.IdManufacturer == this.IdManufacturer &&
                                                    x.Price == this.Price &&
                                                    x.IdState == this.IdState &&
                                                    x.Description == this.Description).First().Id;
            }
            else
            {
                Classes.DBConnection.Connection("Update [dbo].[Record]" +
                    $"Set [Name] = N'{this.Name}', [Year] = {this.Year}, [Format] = {this.Format}, [Size] = {this.Size}, [IdManufacturer] = {this.IdManufacturer}, [Price] = {this.Price}, [IdState] = {this.IdState}, [Description] = N'{this.Description}'" +
                    $"where [Id] = {this.Id}");
            }
        }
        public void Delete()
        {
            Classes.DBConnection.Connection($"Delete from [dbo].[Record] where [Id] = {this.Id}");
        }
        public static void Export()
        {
            DataTable recordQuery = Classes.DBConnection.Connection("Select * from [dbo].[Record]");
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.Sheets[1];
            int index = 1;
            foreach (DataRow row in recordQuery.Rows)
            {
                for (int i = 0; i < recordQuery.Columns.Count; i++)
                {
                    worksheet.Cells[index, i + 1].Value = row[i];
                }
                index++;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "ExportedData";
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = ".xlsx";
            saveFileDialog.InitialDirectory = $@"{Environment.CurrentDirectory}/Data";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog.FileName);
            }
            workbook.Close();
            excelApp.Quit();
        }
    }
}
