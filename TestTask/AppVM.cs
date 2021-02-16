using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TestTask
{
    public class AppVM : INotifyPropertyChanged
    {
        private Book selectedBook;
        private RelayCommand newWindAdd;
        private RelayCommand newWindChange;
        private RelayCommand closeWindow;
        private RelayCommand remove;
        private RelayCommand giveBook;
        private RelayCommand returnBook;
        private RelayCommand chuseGivenBooks;
        public event PropertyChangedEventHandler PropertyChanged;

        public static ObservableCollection<Book> Books { get; set; }

        public RelayCommand ChuseGivenBooks
        {
            get
            {
                return chuseGivenBooks ??
                    (chuseGivenBooks = new RelayCommand(o =>
                    {
                        if (((CheckBox)o).IsChecked == true)
                        {
                            List<Book> GivenBooks = new List<Book>();
                            foreach (Book item in Books)
                            {
                                if (item.IssueDateDate != new DateTime(1, 1, 1))
                                {
                                    GivenBooks.Add(item);
                                }
                            }
                            Books.Clear();
                            foreach (Book item in GivenBooks)
                            {
                                Books.Add(item);
                            }
                        }
                        else
                        {
                            Books.Clear();
                            using (AppContext db = new AppContext())
                            {
                                List<Book> GivenBooks = db.Books.ToList();
                                foreach (Book item in GivenBooks)
                                {
                                    Books.Add(item);
                                }
                            }
                        }
                    }));
            }
        }
        public RelayCommand ReturnBook
        {
            get
            {
                return returnBook ??
                    (returnBook = new RelayCommand(obj =>
                    {
                        if (SelectedBook != null)
                        {
                            MessageBoxResult result = MessageBox.Show($"Принять книгу {SelectedBook.BookName}, у студента {SelectedBook.FullName}?", "Принять", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                            {
                                switch (result)
                                {
                                    case MessageBoxResult.Yes:
                                        SelectedBook.FullName = null;
                                        SelectedBook.IssueDateDate = new DateTime(1, 1, 1);
                                        update_bd();
                                        break;
                                    case MessageBoxResult.No:
                                        break;
                                }
                            }
                        }

                    },
                    (obj) =>
                    {
                        if (SelectedBook != null)
                        {
                            if (SelectedBook?.IssueDateDate != new DateTime(1, 1, 1))
                                return true;
                        }
                        return false;
                    }));
            }
        }
        public RelayCommand NewWindAdd
        {
            get
            {
                return newWindAdd ??
                    (newWindAdd = new RelayCommand(o =>
                    {
                        BookVM vm = new BookVM(null);
                        NewBook b = new NewBook(vm);
                        b.Show();
                    }));
            }
        }
        public RelayCommand GiveBook
        {
            get
            {
                return giveBook ??
                    (giveBook = new RelayCommand(o =>
                    {
                        StudentVM s = new StudentVM(selectedBook);
                        ChuseStudent chuse = new ChuseStudent(s);
                        chuse.Show();
                    },
                    (o) => {
                        if (SelectedBook != null)
                        {
                            if (SelectedBook?.IssueDateDate == new DateTime(1, 1, 1))
                                return true;
                        }
                        return false;
                    }));
            }
        }
        public RelayCommand NewWindChange
        {
            get
            {
                return newWindChange ??
                    (newWindChange = new RelayCommand(o =>
                    {
                        BookVM vm = new BookVM(SelectedBook);
                        NewBook b = new NewBook(vm);
                        b.Show();
                    },
                    (o) => SelectedBook != null));
            }
        }
        public RelayCommand Remove
        {
            get
            {
                return remove ??
                    (remove = new RelayCommand(obj =>
                    {
                        MessageBoxResult result = MessageBox.Show($"Книга {SelectedBook.BookName}, будет удалена?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        {
                            switch (result)
                            {
                                case MessageBoxResult.Yes:
                                    remove_from_bd();
                                    Books.Remove(SelectedBook);
                                    
                                    break;
                                case MessageBoxResult.No:
                                    break;
                            }
                        }
                    },
                    (obj) => SelectedBook != null));
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
        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }
        public AppVM()
        {
            using (AppContext db = new AppContext())
            {
                Books = new ObservableCollection<Book>(db.Books.ToList());
            }
        }
        public void update_bd()
        {
            using (AppContext db = new AppContext())
            {
                db.Books.Update(SelectedBook);
                db.SaveChanges();
            }
        }
        public void remove_from_bd()
        {
            using (AppContext db = new AppContext())
            {
                db.Books.Remove(SelectedBook);
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
