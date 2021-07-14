using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace Model.Repository
{
    public class LanguagesRepository : ILanguagesRepository, IDisposable
    {
        Model.Data.Model1 db;
        public LanguagesRepository() => db = new Model1();            

        public void Add(Languages lang) => db.LANGUAGES.Add(lang);

        public bool CheckExists(string text, int typeLanguage) => db.LANGUAGES.Any(x => typeLanguage == 0 ? (x.VietNam == text) : x.English == text);      

        public void SaveChanges() => db.SaveChanges();
        
        public IEnumerable<GetLanguageWebObj> GetLanguages(string fORM) =>
            db.Database.SqlQuery<GetLanguageWebObj>("GetlanguageWeb @Form", new object[]
            {
                new SqlParameter("@Form", fORM)
            }).ToList();

        private bool disposed = false;
        protected  void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
