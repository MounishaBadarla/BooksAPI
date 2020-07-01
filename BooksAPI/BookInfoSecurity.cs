using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksAPI
{
    public class BookInfoSecurity
    {
        public static bool BookAuth(string username, string pwd)
        {
            using (PracticeEntities e = new PracticeEntities())
            {
                return e.BookInfoes.Any(BookInfo => BookInfo.AuthorName.Equals(username, StringComparison.OrdinalIgnoreCase) && BookInfo.AuthorName == pwd);
            }
        }
    }
}