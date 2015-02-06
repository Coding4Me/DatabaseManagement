using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SQLite;

namespace DatabaseManagement
{
    public class Database
    {

        private const string DatabaseName = "sqlite.db";   //DataBase Name 
        private static string DbFullName = Path.Combine(ApplicationData.Current.LocalFolder.Path, DatabaseName);//DataBase FullName 


        private static SQLiteAsyncConnection GetDbConnection()
        {
            try
            {
                return new SQLiteAsyncConnection(DbFullName);
            }
            catch { }
            return null;
        }

        public async static void CreateDataBase()
        {
            if (!CheckDbAsync(DatabaseName).Result)
            {
                var conn = GetDbConnection();
#if DEBUG
                //await conn.CreateTablesAsync<ComicShelfDb, ReadRecordDb>();
                //await conn.CreateTableAsync<User>();
#endif
            }
        }
        private static async Task<bool> CheckDbAsync(string fileName)
        {
            try
            {
                var store = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch { }
            return false;
        }


        public async static Task<int> InsertOrUpdateAsync<T>(T t)
        {
            try
            {
                var conn = GetDbConnection();

                int count = await conn.UpdateAsync(t);
                if (0 == count)
                {
                    count = await conn.InsertAsync(t);
                }
                return count;
            }
            catch { }
            return -1;

        }
        public async static Task<int> InSertContacts<T>(IList<T> ts)
        {
            try
            {
                var conn = GetDbConnection();
                return await conn.InsertAllAsync(ts);
            }
            catch { }
            return -1;
        }
        public async static Task<int> UpdateAllAsync<T>(IList<T> ts)
        {
            try
            {
                var conn = GetDbConnection();
                return await conn.UpdateAllAsync(ts);
            }
            catch { }
            return -1;
        }

        public async static Task<int> DeleteContact<T>(T t)
        {
            try
            {
                var conn = GetDbConnection();
                Type objType = t.GetType();
                if (objType != null)
                {
                    return await conn.DeleteAsync(t);
                }
            }
            catch { }
            return -1;
        }
        public async static Task<int> DeleteContact<T>(IList<T> ts)
        {
            try
            {
                var conn = GetDbConnection();
                int count = 0;
                foreach (T t in ts)
                {
                    Type objType = t.GetType();
                    if (objType != null)
                    {
                        count += await conn.DeleteAsync(t);
                    }
                }
                return count;
            }
            catch { }
            return -1;
        }

        public async static Task<CreateTablesResult> DeleteTable<T>() where T : new()
        {
            var conn = GetDbConnection();
            int nLine = await conn.DropTableAsync<T>();
            if (-1 != nLine)
            {
                return await conn.CreateTableAsync<T>();
            }
            return null;
        }

        public async static Task<int> ExecAsync(string ssql)
        {
            try
            {
                var conn = GetDbConnection();
                return await conn.ExecuteAsync(ssql);
            }
            catch { }
            return -1;
        }

        public async static Task<IList<T>> GetContactsAsync<T>(string ssql, params object[] args) where T : new()
        {
            try
            {
                var conn = GetDbConnection();
                var objects = await conn.QueryAsync<T>(ssql, args);
                if (objects != null)
                {
                    return objects;
                }
            }
            catch { }
            return default(IList<T>);
        }
        public async static Task<T> GetContactAsync<T>(string ssql, params object[] args) where T : new()
        {
            try
            {
                var objects = await GetContactsAsync<T>(ssql, args);
                if (!objects.Count.Equals(0))
                {
                    return objects.FirstOrDefault();
                }
            }
            catch { }
            return default(T);
        }



        public async static Task<T> GetContact<T>(int comicId) where T : new()
        {
            try
            {
                var conn = GetDbConnection();
                var temp = new T();
                string sName = temp.GetType().Name;
                var comics = await conn.QueryAsync<T>("SELECT * FROM " + sName + "s WHERE ID =" + comicId);
                if (comics != null)
                {
                    return comics.FirstOrDefault();
                }
            }
            catch { }
            return default(T);
        }
        public async static Task<IList<T>> GetContact<T>() where T : new()
        {
            try
            {
                var conn = GetDbConnection();
                var comics = await conn.Table<T>().ToListAsync();
                return comics;
            }
            catch { }
            return null;
        }

    }
}
