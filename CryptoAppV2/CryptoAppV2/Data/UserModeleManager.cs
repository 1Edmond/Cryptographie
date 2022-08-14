using CryptoAppV2.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAppV2.Data
{
    public class UserModeleManager
    {
        public static SQLiteAsyncConnection Connection { get; set; }

        public UserModeleManager(string database)
        {
            Connection = new SQLiteAsyncConnection(database);
            Connection.CreateTableAsync<UserModele>().Wait();
            if(!Exist(UserModele.BaseModel.Nom))
                Connection.InsertAsync(UserModele.BaseModel);
        }
        public async Task<bool> Add(UserModele entity)
        {

            if (Connection == null)
                return false;
            if (entity != null && entity.Id == 0)
                await Connection.InsertAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> Delete(UserModele entity)
        {
            if (Connection == null)
                return false;
            if (entity != null)
                await Connection.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public UserModele Get(int entity)
        {
            if (Connection == null)
                return null;
            return Connection.GetAsync<UserModele>(entity).Result;
        }

        public async Task<bool> Update(UserModele entity)
        {
            if (Connection == null)
                return false;
            var result = await Connection.UpdateAsync(entity);
            return result > 0 ? await Task.FromResult(true) : await Task.FromResult(false);
        }
        public async Task<bool> UpdateAll(List<UserModele> entity)
        {
            if (Connection == null)
                return false;
            var result = await Connection.UpdateAllAsync(entity);
            return result > 0 ? await Task.FromResult(true) : await Task.FromResult(false);
        }

        public bool Exist(string Nom)
        {
            var test = Connection.Table<UserModele>().Where(t => t.Nom == Nom).FirstOrDefaultAsync().Result;
            if (test == null)
                return false;
            return true;
        }

        public List<UserModele> GetAll()
        {
            if (Connection == null)
                return new List<UserModele>()
                {
                    UserModele.BaseModel
                };
            var list = Connection.Table<UserModele>().ToListAsync();
            if (list == null)
                return new List<UserModele>()
                {
                    UserModele.BaseModel
                };
            return  list.Result;
        }
    }
}
