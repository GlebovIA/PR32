using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PR32.Classes
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public string Description { get; set; }
        public static IEnumerable<State> AllState()
        {
            List<State> allStates = new List<State>();
            DataTable reequestStates = DBConnection.Connection("Select * from [dbo].[State]");
            foreach (DataRow state in reequestStates.Rows)
            {
                allStates.Add(new State
                {
                    Id = Convert.ToInt32(state[0]),
                    Name = Convert.ToString(state[1]),
                    Subname = Convert.ToString(state[2]),
                    Description = Convert.ToString(state[3]),
                });
            }
            return allStates;
        }
        public void Save(bool Update = false)
        {
            if (!Update)
            {
                Classes.DBConnection.Connection($"Insert into [dbo].[State] ([Name], [Subname], [Description]) values (N'{this.Name}', N'{this.Subname}', N'P{this.Description}');");
                this.Id = AllState().Where(x => x.Name == this.Name && x.Subname == this.Subname && x.Description == this.Description).First().Id;
            }
            else
            {
                Classes.DBConnection.Connection("Update [dbo].[State] Set" +
                    $"[Name] = N'{this.Name}' " +
                    $"[Subname] = N'{this.Subname}' " +
                    $"[Description] = N'{this.Description}' " +
                    $"where [Id] = {this.Id};");
            }
        }
        public void Delete()
        {
            Classes.DBConnection.Connection($"Delete from [dbo].[State] where [Id] = {this.Id};");
        }
    }
}
