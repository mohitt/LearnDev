using System.Web.Mvc;
using Ads.Model.Entities;
using Ads.Services;
using Ads.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Ads.Tests
{
    [TestFixture]
    public class PostControllerTestFixture
    {
        [Test]
        public void Can_Call_Process() {
            var mockAdProcessorService = new Mock<IAdProcessorService>();
            var mocAdAddressParser = new Mock<IAdComponentParser>();
            var mockMaxMinParser = new Mock<IAdComponentParser>();
            var mocDescriptionParser = new Mock<IAdComponentParser>();
            var mockAd = new Ad();
            var adTobeParsed = "";

            //mocAdAddressParser.Setup(cparser => cparser.Parse(mockAd, adTobeParsed));
           // mockMaxMinParser.Setup(cparser => cparser.Parse(mockAd, adTobeParsed));
           // mocDescriptionParser.Setup(cparser => cparser.Parse(mockAd, adTobeParsed));

            mockAdProcessorService.Setup(processor => processor.Process(It.IsAny<Ad>(), adTobeParsed));
            //mockAdProcessorService.SetupGet(ps => ps.ComponentParsers).Returns(() => (new List<IAdComponentParser>()
            //                                                                              {
            //                                                                                  mocAdAddressParser.Object,
            //                                                                                  mockMaxMinParser.Object,
            //                                                                                  mocDescriptionParser.Object
            //                                                                              }).AsEnumerable());
            var postController = new PostController(mockAdProcessorService.Object);
            postController.Index(new FormCollection()
                                     {
                                         {"searchPost",adTobeParsed}
                                     });

            mockAdProcessorService.VerifyAll();
           // mocAdAddressParser.VerifyAll();
            //mockMaxMinParser.VerifyAll();
           // mocDescriptionParser.VerifyAll();

        }
    }
}

