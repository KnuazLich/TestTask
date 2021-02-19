using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TestTask
{
    public class CommandsBookVMMethods
    {
        public delegate void DbRemoveChanged(Book b);
        public delegate void DbUpdateChanged(Book b);
        public event DbRemoveChanged BookRemove;
        public event DbUpdateChanged BookBack;
        public void DoAddCommand(object parameter)
        {
            if (BookVM.srealBook == null)
            {

                AppVM.Books.Add(BookVM.sbook);
                using (AppContext db = new AppContext())
                {
                    db.Books.Add(BookVM.sbook);
                    db.SaveChanges();
                }
            }
            else
            {
                AppVM.Books[AppVM.Books.IndexOf(BookVM.srealBook)] = BookVM.sbook;
                using (AppContext db = new AppContext())
                {
                  db.Books.Update(BookVM.sbook);
                    db.SaveChanges();
                }
            }
            Window w = parameter as Window;
            (w)?.Close();
        }

    }
}
