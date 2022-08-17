using CryptoAppV2.Model;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAppV2.Data
{
    public class UserHistoriqueManager
    {
        public static SQLiteAsyncConnection Connection { get; set; }

        public UserHistoriqueManager(string database)
        {
            Connection = new SQLiteAsyncConnection(database);
            Connection.CreateTableAsync<UserHistorique>().Wait();
        }
        public async Task<bool> Add(UserHistorique entity)
        {

            if (Connection == null)
                return false;
            if (entity != null && entity.Id == 0)
                await Connection.InsertAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> Delete(UserHistorique entity)
        {
            if (Connection == null)
                return false;
            if (entity != null)
                await Connection.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public UserHistorique Get(int entity)
        {
            if (Connection == null)
                return null;
            return Connection.GetAsync<UserHistorique>(entity).Result;
        }

        public List<UserHistorique> GetAll()
        {
            if (Connection == null)
                return new List<UserHistorique>();
            var list = Connection.Table<UserHistorique>().ToListAsync();
            return list.Result;
        }

        public async Task<IEnumerable<UserHistorique>> GetByValue(string text)
                    => Connection == null ? null : await Connection.Table<UserHistorique>()
                        .Where(x => x.Libelle == text || x.Description == text)
                        .ToListAsync();
        
    }
}
