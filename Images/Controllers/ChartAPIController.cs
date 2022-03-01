using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Images.Controllers
{
    public class ChartAPIController : Controller
    {
        // GET: API
        public JsonResult OrderbyModel(string email)
        {
            var orderModel = new OrderDetailsDataModel();
            var data = orderModel.GetOrderbyModel(email);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OrderbyModelPic()
        {
            var orderModel = new OrderDetailsDataModel();
            var data = orderModel.GetOrderbyModelPic();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrderbyModelType()
        {
            var orderModel = new OrderDetailsDataModel();
            var data = orderModel.GetOrderbyModelType();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}