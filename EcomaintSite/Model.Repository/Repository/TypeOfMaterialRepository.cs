using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class TypeOfMaterialRepository : ITypeOfMaterialRepository, IDisposable
    {
        Model.Data.Model1 db;
        public TypeOfMaterialRepository() => db = new Model1();

        public TypeOfMaterialRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<TypeOfMaterial> ListAll() => db.TypeOfMaterial.ToList();

        public TypeOfMaterial GetTypeOfMaterial(string materialID) => db.TypeOfMaterial.SingleOrDefault(x=>x.ID == materialID);

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
