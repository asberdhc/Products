﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Products.Models;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private DataProductsContext db = new DataProductsContext();
        string URL;
        private string RandomImageForProduct(string name)
        {
            string ApiUrl = "https://api.unsplash.com/search/photos?query=" + name + "&client_id=RR8zTp6LTR2TmVYodb76GyD0Z5SaXaGUoYxX3lr4TJg";
            var request = (HttpWebRequest)WebRequest.Create(ApiUrl);
            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            JObject objs = JObject.Parse(content);
            int c = (Int32)objs["total"];
            if (c >= 1)
            {
                URL = objs["results"][0]["urls"]["small"].ToString();
            }
            else
            {
                URL = RandomImageForProduct("random");
            }

            return URL;
        }

        //GET api/products/{id}
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetById(int id)
        {
            try
            {
                var productById = db.Products
                    .Where(p => p.Id == id && p.IsEnabled == true)
                    .Select(p => new ProductDTO
                    {
                        IdProduct = p.Id,
                        Name = p.Nombre,
                        Description = p.Description,
                        Price = p.PriceClient,
                        Image = db.ImagesProduct.FirstOrDefault(i => i.IdImageProduct == id).Decription
                    })
                    .FirstOrDefault();

                if (productById == null)
                {
                    return NotFound();
                }

                new AcademyLog.Log().ConnectToWebAPI(new AcademyLog.LogEntity
                {
                    aplicacion = "Products API: GetByID",
                    mensaje = id + " Product look up by user",
                    fecha = DateTime.Now
                });
                return Ok(productById);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET api/products/page/numPages
        [Route("page/numPages")]
        [HttpGet]
        public ActionResult GetNumPages()
        {
            try
            {
                int NumPages = 0;
                var products = db.Products.Count(p => p.IsEnabled == true);
                if (products % 10 > 0)
                    NumPages = products / 10 + 1;
                else
                    NumPages = products / 10;

                return Ok(NumPages);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET api/products/page/{pageNumber}
        [Route("page/{pageNumber}")]
        [HttpGet]
        public ActionResult GetAll(int pageNumber = 1)
        {
            try
            {
                //obtaining the number of pages
                int NumPages = 0;
                int products = db.Products.Count(p => p.IsEnabled == true);
                if (products % 10 > 0)
                    NumPages = products / 10 + 1;
                else
                    NumPages = products / 10;

                //validat that the page exist
                if (pageNumber < 0 || pageNumber > NumPages)
                    return BadRequest("The page number should be between 0 and " + NumPages);

                //geting the specific page based on pageLength
                int pageLength = 10;
                var prod = db.Products
                .Where(p => p.IsEnabled == true)
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageLength)
                .Take(pageLength)
                .Select(p => new ProductDTO
                {
                    IdProduct = p.Id,
                    Name = p.Nombre,
                    Description = p.Description,
                    Price = p.PriceClient,
                    Image = db.ImagesProduct.FirstOrDefault(i => i.IdImageProduct == p.Id).Decription

                });


                //validate that there are products available
                if (prod.Count() == 0)
                {
                    return NotFound();
                }

                new AcademyLog.Log().ConnectToWebAPI(new AcademyLog.LogEntity
                {
                    aplicacion = "Products API: GetAll",
                    mensaje = " User Consult All Products ",
                    fecha = DateTime.Now
                });

                return Ok(prod);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET api/products/finByName?name={productName}
        [Route("findByName")]
        [HttpGet]
        public ActionResult GetByName(string name)
        {
            try
            {
                var productByName = db.Products
                    .Where(p => p.Nombre.Contains(name) && p.IsEnabled == true)
                    .Select(p => new ProductDTO
                    {
                        IdProduct = p.Id,
                        Name = p.Nombre,
                        Description = p.Description,
                        Price = p.PriceClient,
                        Image = db.ImagesProduct.FirstOrDefault(i => i.IdImageProduct == p.Id).Decription
                    });

                if (productByName.Count() == 0)
                {
                    return NotFound();
                }

                new AcademyLog.Log().ConnectToWebAPI(new AcademyLog.LogEntity
                {
                    aplicacion = "Products API: GetByName",
                    mensaje = name + " Product look up by user",
                    fecha = DateTime.Now
                });

                return Ok(productByName);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //POST api/products
        [Route("")]
        [HttpPost]
        public ActionResult Add([FromBody]ProductDTO prod)
        {
            try
            {
                //Adding New product
                Models.Products newProd = new Models.Products
                {
                    IdCatalog = 3,
                    Nombre = prod.Name,
                    Description = prod.Description,
                    PriceClient = prod.Price,
                    Title = "",
                    Observations = "",
                    Keywords = "",
                    IsEnabled = true,
                    DateUpdate = DateTime.Now.Date
                };
                db.Products.Add(newProd);

                //ading img
                Models.ImagesProduct newImage = new Models.ImagesProduct
                {
                    IdImageProduct = newProd.Id,
                    Image = Encoding.ASCII.GetBytes(""),
                    Decription = RandomImageForProduct(prod.Name),
                    DateUpdate = DateTime.Now.Date.ToString(),
                    IsEnabled = 1.ToString()
                };
                db.ImagesProduct.Add(newImage);
                db.SaveChanges();

                //Log
                new AcademyLog.Log().ConnectToWebAPI(new AcademyLog.LogEntity
                {
                    aplicacion = "Products API: Add",
                    mensaje = " User Insert Products " + newProd.Id,
                    fecha = DateTime.Now
                });

                prod.Image = newImage.Decription;
                prod.IdProduct = newProd.Id;
                return Ok(prod);
            }


            catch (Exception e)
            {
                return BadRequest("Product  not inserted on DB error: " + e.ToString());
            }
        }

        //PUT api/products/5
        [Route("{id}")]
        [HttpPut]
        public ActionResult Update(int id, [FromBody]ProductDTO prod)
        {
            try
            {
                //verify that the user does not include the id within json file
                if (prod.IdProduct != 0)
                    return BadRequest("The id should be included only in the URL");

                //updating the product info
                var prodModified = db.Products.Where(p => p.Id == id && p.IsEnabled == true).FirstOrDefault();
                if (prodModified == null)
                    return NotFound();
                prodModified.Nombre = prod.Name;
                prodModified.Description = prod.Description;
                prodModified.PriceClient = prod.Price;

                //updating the product image
                var image = db.ImagesProduct.FirstOrDefault(i => i.IdImageProduct == id);
                if (image == null)
                    db.ImagesProduct.Add(new ImagesProduct
                    {
                        IdImageProduct = id,
                        Decription = prod.Image,
                        Image = new byte[0],
                        DateUpdate = DateTime.Now.ToShortDateString(),
                        IsEnabled = "1"
                    });
                else
                    image.Decription = prod.Image;

                db.SaveChanges();

                //return the updated ProductDTO
                prod.IdProduct = id;

                new AcademyLog.Log().ConnectToWebAPI(new AcademyLog.LogEntity
                {
                    aplicacion = "Products API: Update",
                    mensaje = " User modified a Product" + id,
                    fecha = DateTime.Now
                });

                return Ok(prod);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //DELETE api/products/5
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var productOnDB = db.Products.FirstOrDefault(p => p.Id == id);

                //validate that the product exist
                if (productOnDB == null || productOnDB.IsEnabled == false)
                    return NotFound();

                //deleting the product and it's images
                productOnDB.IsEnabled = false;
                var images = db.ImagesProduct.Where(i => i.IdImageProduct == id);
                foreach (ImagesProduct image in images)
                    image.IsEnabled = "0";

                db.SaveChanges();

                new AcademyLog.Log().ConnectToWebAPI(new AcademyLog.LogEntity
                {
                    aplicacion = "Products API: Delete",
                    mensaje = $" Product {id} Was Deleted",
                    fecha = DateTime.Now
                });

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}