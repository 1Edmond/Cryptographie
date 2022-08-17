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
            var list = Connection.Table<UserHistorique>().OrderByDescending(x => x.DateOperation).ToListAsync();
            return list.Result;
        }

        public async Task<List<UserHistorique>> GetByValue(string text)
        {
            if (Connection == null)
                return new List<UserHistorique>();
            var result = await Connection.Table<UserHistorique>().
                Where(x => x.Libelle.Contains(text) || x.Description.Contains(text)).ToListAsync();
            return result;
        }

        public async Task<UserHistorique> GetByLibelle(string his)
        {
            if (Connection == null)
                return null;
            return await Connection.Table<UserHistorique>().Where(x => x.Libelle == his).FirstAsync();
        }
    }
}
