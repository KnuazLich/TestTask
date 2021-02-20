using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TestTask
{
    public class CommandsAppVMMethods: ICommandsMethods
    {

        public void DoChuseGivenBooksCommand(object parameter)
        {
            CheckBox c = parameter as CheckBox;
            if (c.IsChecked == true)
            {
                List<Book> GivenBooks = new List<Book>();
                foreach (Book item in AppVM.Books)
                {
                    if (item.IssueDateDate != new DateTime(1, 1, 1))
                    {
                        GivenBooks.Add(item);
                    }
                }
                AppVM.Books.Clear();
                foreach (Book item in GivenBooks)
                {
                    AppVM.Books.Add(item);
                }

            }
            else
            {
                AppVM.Books.Clear();
                using (AppContext db = new AppContext())
                {
                    List<Book> GivenBooks = db.Books.ToList();
                    foreach (Book item in GivenBooks)
                    {
                        AppVM.Books.Add(item);
                    }
                }
            }
        }
        public void DoReturnCommand(object parameter)
        {
            Book selectedBook = parameter as Book;
            MessageBoxResult result = MessageBox.Show($"Принять книгу {selectedBook.BookName}, у студента {selectedBook.FullName}?", "Принять", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            {
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        selectedBook.FullName = null;
                        selectedBook.IssueDateDate = new DateTime(1, 1, 1);
                        update_book_db(selectedBook);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }
        public void DoGiveCommand(object parameter)
        {
            var myWindow = new ChuseStudent();
            myWindow.Show();
        }
        public void DoChangeCommand(object parameter)
        {
            Book selectedBook = parameter as Book;
            var myWindow = new NewBook();
            var model = myWindow.DataContext as BookVM;
            model.Book = selectedBook;
            model.AppVMMethods = new CommandsAppVMMethods();
            myWindow.Show();
        }
        public void DoAddBookCommand(object parameter)
        {
            var myWindow = new NewBook();
            var model = myWindow.DataContext as BookVM;
            model.Book = null;
            model.AppVMMethods = new CommandsAppVMMethods();
            myWindow.Show();
        }
        public void DoCloseWindowCommand(object parameter)
        {
            Window w = parameter as Window;
            (w)?.Close();
        }
        public void DoRemoveBookCommand(object parameter)
        {
            Book selectedBook = parameter as Book;
            MessageBoxResult result = MessageBox.Show($"Книга {selectedBook.BookName}, будет удалена?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            {
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        remove_from_bd(selectedBook);
                        AppVM.Books.Remove(selectedBook);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }
        public bool ChekReturnAllowed(object parameter)
        {
            Book selectedBook = parameter as Book;
            if (selectedBook != null)
            {
                if (selectedBook?.IssueDateDate != new DateTime(1, 1, 1))
                    return true;
            }
            return false;
        }
        public bool ChekSelected(object parameter)
        {
            Book selectedBook = parameter as Book;
            if (selectedBook != null)
                return true;
            else return false;
        }
        public bool ChekGiveAllowed(object parameter)
        {
            Book selectedBook = parameter as Book;
            if (selectedBook != null)
            {
                if (selectedBook?.IssueDateDate == new DateTime(1, 1, 1))
                    return true;
            }
            return false;
        }
        public void update_book_db(Book book)
        {
            using (AppContext db = new AppContext())
            {
                db.Books.Update(book);
                db.SaveChanges();
            }
        }

        public void remove_from_bd(Book book)
        {
            using (AppContext db = new AppContext())
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }
        }
    }
}
