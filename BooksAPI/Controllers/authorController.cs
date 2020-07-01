using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BooksAPI.Controllers
{
    public class authorController : ApiController
    {
        // GET: api/author
        [Route("api/authordetails")]
        [HttpGet]
        public IEnumerable<AuthorInfo> authordetails()
        {
            using (PracticeEntities x = new PracticeEntities())
            {
                return x.AuthorInfoes.ToList();
            }
        }

        // GET: api/author/5
        [Route("api/authordetails/{id}")]
        public AuthorInfo Get(int id)
        {
            using (PracticeEntities x = new PracticeEntities())
            {
                return x.AuthorInfoes.FirstOrDefault(e=>e.AID == id);
            }
        }
        [Route("api/addauthordetails")]
        // POST: api/books
        public void Post(AuthorInfo b)
        {
            using (PracticeEntities x = new PracticeEntities())
            {
                x.AuthorInfoes.Add(b);
                x.SaveChanges();
            }

        }
        [Route("api/updateauthordetails/{id}")]
        // PUT: api/author/5
        public HttpResponseMessage Put(int id, AuthorInfo b)
        {
            try
            {
                using (PracticeEntities x = new PracticeEntities())
                {
                    var y = x.AuthorInfoes.FirstOrDefault(e => e.AID == id);
                    if (y == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Author with Id=" + id.ToString() + " not found");
                    }
                    else
                    {
                        y.AuthorName = b.AuthorName;
                        y.City = b.City;
                        x.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, y);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }



        // DELETE: api/author/5
        [Route("api/deleteauthordetails/{id}")]
        
        public HttpResponseMessage Delete(int id)
        {

            try
            {
                using (PracticeEntities x = new PracticeEntities())
                {
                    var y = x.AuthorInfoes.FirstOrDefault(e => e.AID == id);
                    if (y == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Author with Id=" + id.ToString() + " not found");
                    }
                    else
                    {
                        x.AuthorInfoes.Remove(x.AuthorInfoes.FirstOrDefault(e => e.AID == id));
                        x.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, y);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }
    }
}
