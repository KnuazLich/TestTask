using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace TestTask
{
    public class BookVM
    {
        protected Book book;
        protected Book realBook = null;
        private RelayCommand closeWindow;
        private RelayCommand add;
        public event PropertyChangedEventHandler PropertyChanged;
        public int InvNumber
        {
            get
            {
                return book.InvNumber;
            }
            set
            {
                book.InvNumber = value;
                OnPropertyChanged("InvNumber");
            }
        }
        public string BookName
        {
            get
            {

                return book.BookName;


            }
            set
            {
                book.BookName = value;
                OnPropertyChanged("BookName");
            }
        }
        public string Authors
        {
            get
            {

                return book.Authors;

            }
            set
            {
                book.Authors = value;
                OnPropertyChanged("Authors");
            }
        }
        public int Year
        {
            get
            {

                return book.Year;

            }
            set
            {
                book.Year = value;
                OnPropertyChanged("Year");
            }
        }
        public DateTime EntranceDate
        {
            get
            {

                return book.EntranceDate;

            }
            set
            {
                book.EntranceDate = value;
                OnPropertyChanged("EntranceDate");
            }
        }
        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow ?? (closeWindow = new RelayCommand(o =>
                {
                    ((Window)o)?.Close();
                }));
            }
        }        
        public RelayCommand Add
        {
            get
            {
                return add ??
                    (add = new RelayCommand(obj =>
                    {
                        if (realBook == null)
                        {
                            AppVM.Books.Add(book);
                            add_to_bd();
                        }
                        else
                        {
                            AppVM.Books[AppVM.Books.IndexOf(realBook)] = book;
                            update_bd();
                        }
                        CloseWindow.Execute(obj);
                    }));
            }
        }
        public BookVM(Book b)
        {
            if (b == null)
            {
                book = new Book { InvNumber = 0, BookName = "", Authors = "", Year = 0, EntranceDate = new DateTime(1, 1, 1) };
            }
            else
            {
                realBook = b;
                book = (Book)b.Clone();
            }
        }

        private void update_bd()
        {
            using (AppContext db = new AppContext())
            {
                db.Books.Update(book);
                db.SaveChanges();
            }
        }
        private void add_to_bd()
        {
            using (AppContext db = new AppContext())
            {
                db.Books.Add(book);
                db.SaveChanges();
            }
        }
        
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
