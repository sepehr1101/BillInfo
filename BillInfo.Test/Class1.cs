using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using StructureMap;

namespace BillInfo.Test
{
    [TestFixture]
    public class BillInfoTesting
    {
        [Test]
        public void TestGenerateBillId()
        {
            var container = new Container(x =>
            {
                x.For<ICheckDigit>().Use<CheckDigit>();
                x.For<IBillIdManager>().Use<BillIdManager>();
            });

            var iBill = container.GetInstance<IBillIdManager>();

            var billId1 = iBill.GenerateBillId("1001", "183", GovermenralServiceProvider.Abfa);
            var billId2 = iBill.GenerateBillId("224979711", "163", GovermenralServiceProvider.Abfa);
            Assert.That(billId1, Is.EqualTo("10018315"));
            Assert.That(billId2, Is.EqualTo("2249797116314"));
        }
    }
}
