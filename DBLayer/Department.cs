using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static string LastError = "";

        public static List<Department> StaticList;

        public static List<Department> ListAll(DBManager dm)
        {
            if (StaticList != null) return StaticList;

            var lst = new List<Department>();
            try
            {
                lst = dm.connection.Query<Department>(
                    @"SELECT ID, Name
                      FROM Department ORDER BY Name
                      ").ToList();
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }
            StaticList = lst;
            return lst;
        }

        public bool Find(DBManager dm, int Id)
        {
            bool b = true;
            try
            {
                Department result = dm.connection.Query<Department>(
                    @"SELECT ID, Name
                      FROM Department
                      WHERE ID = @ID", new { Id }).FirstOrDefault();

                this.ID = Id;
                this.Name = result.Name;

            }
            catch (Exception ex)
            {
                b = false;
                LastError = ex.Message;
            }

            return b;
        }

        public bool Insert(DBManager dm)
        {
            bool b = true;
            try
            {
                this.ID = dm.connection.Query<int>(
                    @"INSERT INTO Department ( Name) VALUES 
                    ( @Name );
                    select last_insert_rowid()", this).First();

                StaticList = null;
            }
            catch (Exception ex)
            {
                b = false;
                LastError = ex.Message;
            }

            return b;
        }

        public bool Update(DBManager dm)
        {
            bool b = true;
            try
            {
                var result = dm.connection.Execute(
                   @"UPDATE Department SET Name=@Name WHERE ID=@ID;", this);

                StaticList = null;
            }
            catch (Exception ex)
            {
                b = false;
                LastError = ex.Message;
            }

            return b;
        }




    }
}
