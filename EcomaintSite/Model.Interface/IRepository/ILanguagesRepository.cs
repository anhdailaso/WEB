
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface ILanguagesRepository 
    {
        void Add(Languages lang);
        bool CheckExists(string text, int typeLanguage);
        void SaveChanges();
        IEnumerable<GetLanguageWebObj> GetLanguages(string form);
    }
}
