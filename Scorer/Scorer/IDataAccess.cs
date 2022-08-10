using System.Collections.Generic;

namespace Scorer
{
    public interface IDataAccess
    {
        List<T> LoadData<T>(string prm);
        void SaveData<T>(T prm, string sql);
    }
}