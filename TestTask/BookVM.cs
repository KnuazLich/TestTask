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
        public static Book sbook;
        public static Book srealBook;
        public Book book;
        public Book realBook = null;
        private CommandsBookVMMethods bookVMMethods;
        private CommandsAppVMMethods appVMMethods;


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
        public Book Book
        {
            get
            {
                return book;
            }
            set
            {
                if (value == null)
                {
                    book = new Book { InvNumber = 0, BookName = "", Authors = "", Year = 0, EntranceDate = new DateTime(1, 1, 1) };
                    sbook = book;
                    srealBook = null;
                    realBook = null;
                }
                else
                {
                    srealBook = realBook = value;
                    sbook= book = (Book)value.Clone();
                }
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
        public CommandsBookVMMethods BookVMMethods
        {
            get
            {
                return bookVMMethods;
            }
            set
            {
                bookVMMethods = value;
            }
        }
        public CommandsAppVMMethods AppVMMethods
        {
            get
            {
                return appVMMethods;
            }
            set
            {
                appVMMethods = value;
            }
        }

        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow ?? (closeWindow = new RelayCommand(AppVMMethods.DoCloseWindowCommand));
            }
        }
        public RelayCommand Add
        {
            get
            {
                return add ?? (add = new RelayCommand(BookVMMethods.DoAddCommand));
            }
        }

        public BookVM()
        {
            if(AppVM.selectedBook  == null )
            {
                sbook = book = new Book { InvNumber = 0, BookName = "", Authors = "", Year = 0, EntranceDate = new DateTime(1, 1, 1) };
                srealBook = realBook = null;
            }
            else
            {
                realBook = AppVM.selectedBook;
                book = (Book)AppVM.selectedBook.Clone();
            }
            
            appVMMethods = new CommandsAppVMMethods();
            bookVMMethods = new CommandsBookVMMethods();
        }
        
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
