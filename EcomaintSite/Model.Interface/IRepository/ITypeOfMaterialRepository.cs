using Model.Data;
using System;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface ITypeOfMaterialRepository
    {
        IEnumerable<TypeOfMaterial> ListAll();
        TypeOfMaterial GetTypeOfMaterial(string vatTuID);
    }
}
