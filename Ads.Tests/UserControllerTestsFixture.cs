using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ads.Helper.Constants;
using Ads.Model.Entities;
using Ads.Model.Repositories;
using Ads.Services;
using Ads.Web.Controllers;
using DotNetOpenAuth.OpenId;
using Moq;
using System.Linq;

namespace Ads.Tests
{
    using NUnit.Framework;

    namespace Ads.Tests
    {
        [TestFixture]
        public class UserControllerTestsFixture
        {
            Mock<HttpSessionStateBase> sessionState;
            Mock<HttpContextBase> mockhttpContext;
            Mock<IUserRepository> mockUserRepo;
            Mock<IFormsAuthenticationService> formService;


            [SetUp]
            public void TestInitialize() {
                mockUserRepo = new Mock<IUserRepository>();
                sessionState = new Moq.Mock<HttpSessionStateBase>();
                formService = new Moq.Mock<IFormsAuthenticationService>();


                mockhttpContext = new Mock<HttpContextBase>();
                mockhttpContext.SetupGet(k => k.Session).Returns(sessionState.Object);
            }

            [Test]
            public void Can_ShowRegister() {
                var uctrl = new UserController(mockUserRepo.Object, formService.Object);
                uctrl.Register().ReturnsViewResult().ForView("");
            }

           

            [Test]
            public void Can_SaveUser_Info() {
                var user = new User() { DisplayName = "sdgsdf" };
                var identifier = Identifier.Parse("http://uniqueId/");

                mockUserRepo.Setup(repo => repo.InsertOrUpdate(user)).Callback(() => { user.Id = 2; });
                mockUserRepo.Setup(repo => repo.Save());
                mockUserRepo.Setup(repo => repo.All).Returns(
                    (new System.Collections.Generic.List<User>()
                        {
                            new User() {Id = 1,DisplayName = "Admin"},
                            user
                        }).AsQueryable()
                    );
                formService.Setup(serv => serv.SetAuthCookie(null, false));

                sessionState.SetupSet(k => { k[SessionConstants.DisplayName] = user.DisplayName; });


                var uctrl = new UserController(mockUserRepo.Object, formService.Object);
                uctrl.ControllerContext = new ControllerContext(mockhttpContext.Object, new RouteData(), uctrl);
                sessionState.SetupGet(k => k[SessionConstants.OpenId]).Returns(identifier);

                uctrl
                    .Register(user)
                    .ReturnsRedirectToRouteResult()
                    .RedirectsTo<UserController>(ctr=>ctr.Details(user.Id, true));
                    //.ForView("Detail").WithViewModel<User>();

                sessionState.VerifyAll();
                mockUserRepo.VerifyAll();

                Assert.NotNull(user.Id);
                Assert.NotNull(user.OpenId);
                Assert.NotNull(user.CreatedBy);
                Assert.NotNull(user.CreatedOn);
                Assert.NotNull(user.LastUpdatedBy);
                Assert.NotNull(user.LastUpdatedOn);


                Assert.AreNotEqual(user.Id, default(int));
                Assert.AreEqual(user.Id, 2);
                Assert.AreEqual(user.OpenId, identifier.ToString());
                Assert.AreNotEqual(user.CreatedBy, default(int));
                Assert.AreNotEqual(user.CreatedOn, default(DateTime));
                Assert.AreNotEqual(user.LastUpdatedBy, default(int));
                Assert.AreNotEqual(user.LastUpdatedOn, default(DateTime));

                Assert.AreEqual(user.CreatedBy, 1);

                Assert.AreEqual(user.LastUpdatedBy, 1);



            }
        }
    }

}