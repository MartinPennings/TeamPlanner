using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Department { get; set; }

        public string DepartmentName
        {
            get
            {
                List<Department> lst;
               if (DBLayer.Department.StaticList == null)
                {
                    using (DBManager dm = new DBManager())
                    {
                        lst = DBLayer.Department.ListAll(dm);
                    }
                }
                else
                {
                    lst = DBLayer.Department.StaticList;
                }

                DBLayer.Department d = (from r in lst where r.ID.Equals(this.Department) select r ).FirstOrDefault();
                if (d != null)
                {
                    return d.Name;
                }
                else
                {
                    return "";
                }
   
                    
            }
        }

        public static string LastError = "";

        public static List<Person> ListAll(DBManager dm)
        {
            var lst = new List<Person>();
            try
            {
                lst = dm.connection.Query<Person>(
                    @"SELECT ID, FirstName, LastName, Department
                      FROM Person
                      ").ToList();

               
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }

            return lst;
        }

        public bool Find( DBManager dm,int Id)
        {
            bool b = true;
            try
            {
                Person result = dm.connection.Query<Person>(
                    @"SELECT ID, FirstName, LastName, Department
                      FROM Person
                      WHERE ID = @ID", new { Id}).FirstOrDefault();

                this.ID = Id;
                this.FirstName = result.FirstName;
                this.LastName = result.LastName;
                this.Department = result.Department;

            }
            catch (Exception ex)
            {
                b = false;
                LastError = ex.Message;
            }

            return b;
        }

        public bool Insert( DBManager dm)
        {
            bool b = true;
            try
            {
                this.ID = dm.connection.Query<int>(
                    @"INSERT INTO Person ( FirstName, LastName, Department ) VALUES 
                    ( @FirstName, @LastName, @Department );
                    select last_insert_rowid()", this).First();
            }
            catch (Exception ex)
            {
                b = false;
                LastError = ex.Message;
            }
            
            return b;
        }

        public bool Update( DBManager dm)
        {
            bool b = true;
            try
            {
                var result = dm.connection.Execute(
                   @"UPDATE Person SET FirstName=@FirstName, LastName=@LatsName, Department=@Department WHERE ID=@ID;", this);
            }
            catch (Exception ex)
            {
                b = false;
                LastError = ex.Message;
            }

            return b;
        }


        public bool Delete(DBManager dm)
        {
            bool b = true;
            try
            {
                var result = dm.connection.Execute(
                   @"Delete FROM  Person  WHERE ID=@ID;", this);
                                
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
