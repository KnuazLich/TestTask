using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask
{
    class DbOperations
    {
        public void update_book_db(Book book)
        {
            using (AppContext db = new AppContext())
            {
                db.Books.Update(book);
                db.SaveChanges();
            }
        }

        public  void remove_from_bd(Book book)
        {
            using (AppContext db = new AppContext())
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }
        }
    }
}
