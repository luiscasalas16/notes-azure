using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace NetWafWebMvc.Controllers
{
    public class TestMvcController : Controller
    {
        [HttpGet]
        public ActionResult NoParametersGet()
        {
            return Json(new
            {
                result = "ok"
            });
        }

        [HttpGet]
        public ActionResult ParametersGet(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                id = id + " " + DateTime.Now,
                name = name + " " + DateTime.Now
            });
        }

        [HttpGet]
        public ActionResult ParametersJsonGet(string parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                parameters = parameters + " " + DateTime.Now
            });
        }

        [HttpPost]
        public ActionResult NoParametersPost()
        {
            return Json(new
            {
                result = "ok"
            });
        }

        [HttpPost]
        public ActionResult ParametersPost(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                id = id + " " + DateTime.Now,
                name = name + " " + DateTime.Now
            });
        }

        [HttpPost]
        public ActionResult ParametersPostFormData(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                id = id + " " + DateTime.Now,
                name = name + " " + DateTime.Now
            });
        }

        [HttpPost]
        public ActionResult ParametersJsonPost(string parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                parameters = parameters + " " + DateTime.Now
            });
        }

        [HttpPost]
        public ActionResult ParametersJsonPostFormData(string parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                parameters = parameters + " " + DateTime.Now
            });
        }

        public string decode(string base64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }
    }
}