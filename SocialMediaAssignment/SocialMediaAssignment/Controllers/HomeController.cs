using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialMediaAssignment;

namespace ConfigurationDemoSolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly SocialMediaLinkOptions _socialMediaLinksOptions;

        public HomeController(IOptions<SocialMediaLinkOptions> socialMediaLinksOptions)
        {
            _socialMediaLinksOptions = socialMediaLinksOptions.Value;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.SocialMediaLinks = _socialMediaLinksOptions;
            return View();
        }
    }
}