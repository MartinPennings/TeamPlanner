﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using System.IO;
using System.Collections.Specialized;
using System.Configuration;

namespace DBLayer
{
    public class DBManager : IDisposable
    {

        public SQLiteConnection connection { get; set; }

        private string dbPath = string.Empty;
        private string dbFile = string.Empty;
        private string dbFullPath
        {
            get
            {
                return dbPath + "\\" + dbFile;
            }
        }

        private void GetDBPathFromConfig()
        {
            if (string.IsNullOrEmpty(dbPath))
            {
                dbPath = ConfigurationManager.AppSettings.Get("DBPath");
                dbFile = ConfigurationManager.AppSettings.Get("DBFile");
            }
        }

        public DBManager()
        {
            GetDBPathFromConfig();

            CreateDatabaseIfNeeded();
            Open();
        }

        private void Open()
        {
            connection = new SQLiteConnection("Data Source=" + dbFullPath);
            connection.Open();
        }

        private void Close()
        {
            try
            {
                connection.Close();
            }
            catch
            {
                // nothing
            }
        }


        private void CreateDatabaseIfNeeded()
        {

            // TODO: Create Dir
            if (!Directory.Exists(dbPath)){
                Directory.CreateDirectory(dbPath);
            }


            string sql = DBLayer.Properties.Resources.CreateDB_sql;

            if (!File.Exists(dbFullPath))
            {
                using (var con = new SQLiteConnection("Data Source=" + dbFullPath))
                {
                    con.Open();
                    con.Execute(sql);
                }
            }
        }

        
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Close();
                    connection = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DBManager() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
