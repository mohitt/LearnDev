using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ads.Helper.Constants;
using Ads.Model.Entities;
using Ads.Model.Repositories;
using Ads.Services;
using Ads.Web.Controllers;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using Moq;
using NUnit.Framework;

namespace Ads.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        Mock<IUserRepository> userRepo;
        Mock<IMembershipService> memberService;
        Mock<IFormsAuthenticationService> formService;
        Mock<IOpenIdService> openIdService;
        Mock<HttpSessionStateBase> sessionState;
        Mock<HttpContextBase> mockhttpContext;
        private Mock<HttpRequestBase> requestObject;


        [SetUp]
        public void SetUpMethod()
        {
            userRepo = new Moq.Mock<IUserRepository>();
            memberService = new Moq.Mock<IMembershipService>();
            formService = new Moq.Mock<IFormsAuthenticationService>();
            openIdService = new Moq.Mock<IOpenIdService>();
            sessionState = new Moq.Mock<HttpSessionStateBase>();
            requestObject = new Moq.Mock<HttpRequestBase>();
            mockhttpContext = new Mock<HttpContextBase>();
            
            //requestObject.Setup(req => req.IsAjaxRequest()).Returns(true);
            mockhttpContext.SetupGet(k => k.Session).Returns(sessionState.Object);
            mockhttpContext.SetupGet(k => k.Request).Returns(requestObject.Object);
        }
        [Test]
        public void Can_Return_OpenIdPartialView_WhenAjax()
        {
            requestObject.SetupGet(x => x.Headers).Returns(
                    new System.Net.WebHeaderCollection {
                        {"X-Requested-With", "XMLHttpRequest"}
                    });
            var ac = new AccountController(formService.Object, memberService.Object, userRepo.Object, openIdService.Object);
            ac.ControllerContext = new ControllerContext(mockhttpContext.Object, new RouteData(), ac);
            // it returns the default view
            var result = ac.OpenIdPopup()
                           .ReturnsPartialViewResult()
                           .ForView("_OpenIdHtml");
            
        }

        [Test]
        public void Can_Return_OpenIdPopupView_When_NotAjax() {
            
            var ac = new AccountController(formService.Object, memberService.Object, userRepo.Object, openIdService.Object);
            ac.ControllerContext = new ControllerContext(mockhttpContext.Object, new RouteData(), ac);
            // it returns the default view
            var result = ac.OpenIdPopup()
                           .ReturnsViewResult()
                           .ForView("");

        }

        [Test]
        public void Can_Redirect_IfUserHasSessionAlready()
        {
            var ac = new AccountController(formService.Object, memberService.Object, userRepo.Object, openIdService.Object);
            sessionState.SetupGet(k => k[SessionConstants.OpenId]).Returns("blah blah");
            ac.ControllerContext = new ControllerContext(mockhttpContext.Object, new RouteData(), ac);
            var result = ac.LogOn().ReturnsRedirectToRouteResult().RedirectsTo<HomeController>(ctrller => ctrller.Index());
        }

        [Test]
        public void Can_Redirect_ToHomeIndex_IfUserExists()
        {
            var identifier = Identifier.Parse("http://uniqueId/");

            var mockAuthResponse = new Mock<IAuthenticationResponse>();
            mockAuthResponse.SetupGet(res => res.ClaimedIdentifier).Returns(identifier);
            //mockAuthResponse.SetupGet(res => res.FriendlyIdentifierForDisplay).Returns("FriendlyId");
            mockAuthResponse.SetupGet(res => res.Status).Returns(AuthenticationStatus.Authenticated);


            userRepo.Setup(k => k.FindByOpenId(identifier.ToString())).Returns(new User() { DisplayName = "Test Display Name"});
            
            openIdService.Setup(k => k.GetResponse()).Returns(mockAuthResponse.Object);

            sessionState.SetupSet(k => { k[SessionConstants.OpenId] = identifier; });
            sessionState.SetupSet(k => { k[SessionConstants.DisplayName] = "Test Display Name"; });


            var ac = new AccountController(formService.Object, memberService.Object, userRepo.Object, openIdService.Object);
            ac.ControllerContext = new ControllerContext(mockhttpContext.Object, new RouteData(), ac);
            var result = ac.LogOn().ReturnsRedirectToRouteResult().RedirectsTo<HomeController>(ctrller=>ctrller.Index());

            sessionState.VerifyAll();
            mockAuthResponse.VerifyAll();
            userRepo.VerifyAll();
            openIdService.VerifyAll();
        }
        [Test]
        public void Can_Redirect_ToUserRegister_IfUserNotThere() {
            var identifier = Identifier.Parse("http://uniqueId/");

            var mockAuthResponse = new Mock<IAuthenticationResponse>();
            mockAuthResponse.SetupGet(res => res.ClaimedIdentifier).Returns(identifier);
            //mockAuthResponse.SetupGet(res => res.FriendlyIdentifierForDisplay).Returns("FriendlyId");
            mockAuthResponse.SetupGet(res => res.Status).Returns(AuthenticationStatus.Authenticated);


            userRepo.Setup(k => k.FindByOpenId(identifier.ToString())).Returns(null as User);

            openIdService.Setup(k => k.GetResponse()).Returns(mockAuthResponse.Object);

            sessionState.SetupSet(k => { k[SessionConstants.OpenId] = identifier; });


            var ac = new AccountController(formService.Object, memberService.Object, userRepo.Object, openIdService.Object);
            ac.ControllerContext = new ControllerContext(mockhttpContext.Object, new RouteData(), ac);
            var result = ac.LogOn().ReturnsRedirectToRouteResult().RedirectsTo<UserController>(ctrller => ctrller.Register());

            sessionState.VerifyAll();
            mockAuthResponse.VerifyAll();
            userRepo.VerifyAll();
            openIdService.VerifyAll();
        }
    }
}