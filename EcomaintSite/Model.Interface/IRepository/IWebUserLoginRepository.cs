using System;
namespace Model.Interface
{
   public interface IWebUserLoginRepository : IDisposable
    {
        bool CheckExists(string Username);
        void Add(string Username);
        void Delete(string Username);
        int Count();
        void SaveChanges();
    }
}
