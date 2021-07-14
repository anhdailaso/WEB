using Microsoft.VisualStudio.TestTools.UnitTesting;
using EcomaintSite.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Language.Flow;
using Model.Interface;
using Model.Data;

namespace EcomaintSite.Controllers.Tests
{
    [TestClass()]
    public class OrgUnitControllerTests
    {
        private Mock<Model.Interface.IOrgUnitRepository> mockOrgUnitRepository;
        [TestMethod()]
        public void OrgUnitControllerTest()
        {
            Assert.Fail();
        }

        [TestInitialize]
        public void SetupTests()
        {
            //Model1 db = new Model1();
            //mockOrgUnitRepository = new Mock<IOrgUnitRepository>();
            //mockOrgUnitRepository.Setup<OrgUnit>(m => m.CheckIDExist("01")).Returns((OrgUnit o) => { return db.OrgUnit.Any( x => x.ID == o.ID) });
        }


    }
}