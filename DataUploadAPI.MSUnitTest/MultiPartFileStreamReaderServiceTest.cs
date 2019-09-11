using System.Threading.Tasks;
using DataUploadAPI.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataUploadAPI.MSUnitTest
{
    [TestClass]
    public class MultiPartFileStreamReaderServiceTest
    {
        private readonly MultiPartFileStreamReaderService _fileStreamReader;

        public MultiPartFileStreamReaderServiceTest()
        {
            _fileStreamReader = new MultiPartFileStreamReaderService();
        }

        [TestMethod]
        public void ProductParser()
        {
            // Arrange
            string source = "00000002wit/bcup110,2,broek,Gaastra,8,0,1-3 werkdagen,baby,110,wit";

            // Act
            var productParser = _fileStreamReader.ProductParser(source);

            // Assert
            Assert.AreEqual("00000002wit/bcup110", productParser.Key);
            
        }
    }
}