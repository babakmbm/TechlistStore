using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechlistShop.Domain.Entities;
using TechlistShop.Domain.Abstract;
using TechlistShop.WebUI.Models;

namespace TechlistShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public OrderController(IProductRepository repo, IOrderProcessor proc)
        {
            this.repository = repo;
            this.orderProcessor = proc;
        }

        public ActionResult Index()
        {
            return View(orderProcessor.Orders);
        }

    }
}
