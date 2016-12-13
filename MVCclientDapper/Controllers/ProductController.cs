
using DapperExercise.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCclientDapper.Controllers
{
    public class ProductController :Controller
    {

        HttpClient client;
        String url = "http://localhost:56651/api/product";
        public ProductController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        //GET: product
        public async Task<ActionResult> Index()
        {

            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Products = JsonConvert.DeserializeObject<List<Product>>(responseData);

                // return Json(Employees,JsonRequestBehavior.AllowGet);
                return View("Index", Products);
            }
            return View("Index");
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> Details(int id)
        {

            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var product = JsonConvert.DeserializeObject<Product>(responseData);

                // return Json(Employee, JsonRequestBehavior.AllowGet);
                return View(product);
            }
            return View("Error");
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View(new Product());
        }

        // POST: Employee/Create
        [HttpPost]
        public async Task<ActionResult> Create(Product emp)
        {
            // HttpResponseMessage responseMessage = await client.PostAsync(url,emp);
            HttpResponseMessage responseMessage = await client.PostAsync(url, new StringContent(
   new JavaScriptSerializer().Serialize(emp), System.Text.Encoding.UTF8, "application/json"));
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");

        }


        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Product = JsonConvert.DeserializeObject<Product>(responseData);

                return View(Product);
            }
            return View("Error");
        }



        // POST: Employee/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Product emp)
        {
            //HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + id, emp);
            HttpResponseMessage responseMessage = await client.PutAsync(url + "/" + id, new StringContent(
  new JavaScriptSerializer().Serialize(emp), System.Text.Encoding.UTF8, "application/json"));
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        // GET: Employee/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var product = JsonConvert.DeserializeObject<Product>(responseData);
                // return Json(Employee, JsonRequestBehavior.AllowGet);
                return View(product);
            }
            return View("Error");
        }


        // POST: Employee/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Product emp)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }


    }
}
