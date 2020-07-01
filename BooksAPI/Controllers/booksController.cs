using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;

namespace BooksAPI.Controllers
{
    public class booksController : ApiController
    {
        [BasicAuthentication]
        [Route("api/unauthbookslist")]
        [HttpGet]
        public HttpResponseMessage authrequiredbookslist(string author = "ALL")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (PracticeEntities x = new PracticeEntities())
            {
                if (x.BookInfoes.Where(e => e.AuthorName.ToLower() == username.ToLower()) != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, x.BookInfoes.Where(e => e.AuthorName.ToLower() == username.ToLower()).ToList());
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }
        // GET: api/books
        [Route("api/bookslist")]
        [HttpGet]
        public IEnumerable<BookInfo> bookslist()
        
        {
            using (PracticeEntities x = new PracticeEntities())
            {
                return x.BookInfoes.ToList();
            }
        }

        // GET: api/books/5
        [Route("api/bookdetails/{id}")]
        public BookInfo Get(int id)
        {
            using (PracticeEntities x = new PracticeEntities())
            {
                return x.BookInfoes.FirstOrDefault(e => e.ID == id);
            }
        }

        [Route("api/addbookdetails")]
        // POST: api/books
        public void Post(BookInfo b)
        {
            using (PracticeEntities x = new PracticeEntities())
            {
                 x.BookInfoes.Add(b);
                 x.SaveChanges();
            }

        }
        [Route("api/updatebookdetails/{id}")]
        // PUT: api/books/5
        public HttpResponseMessage Put(int id, BookInfo b)
        {
            try
            {
                using (PracticeEntities x = new PracticeEntities())
                {
                    var y = x.BookInfoes.FirstOrDefault(e => e.ID == id);
                    if (y == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with Id=" + id.ToString() + " not found");
                    }
                    else
                    {
                        y.Bookname = b.Bookname;
                        y.AuthorName = b.AuthorName;
                        x.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, y);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }
        [Route("api/deletebookdetails/{id}")]
        // DELETE: api/books/5
        public HttpResponseMessage Delete(int id)
        {

            try
            {
                using (PracticeEntities x = new PracticeEntities())
                {
                    var y = x.BookInfoes.FirstOrDefault(e => e.ID == id);
                    if (y == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with Id=" + id.ToString() + " not found");
                    }
                    else
                    {
                        x.BookInfoes.Remove(x.BookInfoes.FirstOrDefault(e=>e.ID==id));
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
