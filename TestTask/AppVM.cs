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
        public static Book selectedBook;
        private RelayCommand newWindAdd;
        private RelayCommand newWindChange;
        private RelayCommand closeWindow;
        private RelayCommand remove;
        private RelayCommand giveBook;
        private RelayCommand returnBook;
        private RelayCommand chuseGivenBooks;
        public event PropertyChangedEventHandler PropertyChanged;

        private Action<object> close;
        private Action<object> give;
        private Action<object> removeBook;
        private Action<object> newAdd;
        private Action<object> changeBook;
        private Action<object> take_away;
        private Action<object> chuse;

        private Func<object, bool> chekRemoveBook;
        private Func<object, bool> chekGiveBook;
        private Func<object, bool> chekNail;

        public static ObservableCollection<Book> Books { get; set; }

        public RelayCommand ChuseGivenBooks
        {
            get
            {
                return chuseGivenBooks ?? (chuseGivenBooks = new RelayCommand(chuse));
            }
        }
        public RelayCommand ReturnBook
        {
            get
            {
                return returnBook ?? (returnBook = new RelayCommand(take_away, chekNail));
            }
        }
        public RelayCommand NewWindAdd
        {
            get
            {
                return newWindAdd ??
                    (newWindAdd = new RelayCommand(newAdd));
            }
        }
        public RelayCommand GiveBook
        {
            get
            {
                return giveBook ?? (giveBook = new RelayCommand(give,chekGiveBook));
            }
        }
        public RelayCommand NewWindChange
        {
            get
            {
                return newWindChange ?? (newWindChange = new RelayCommand(changeBook, chekRemoveBook));
            }
        }
        public RelayCommand Remove
        {
            get
            {
                return remove ?? (remove = new RelayCommand(o=>{ removeBook(o); DellComand= new NotifyTaskCompletion<int>(MyStaticService.TaskDelay()); }, chekRemoveBook));
            }
        }
        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow ?? (closeWindow = new RelayCommand(close));
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
        public NotifyTaskCompletion<int> dellComand;
        public NotifyTaskCompletion<int> DellComand 
        {
            get { return dellComand; }
            set
            {
                dellComand = value;
                OnPropertyChanged("DellComand");
            }
        } 
        public AppVM(ICommandsMethods commandsMethods)
        {
            DellComand = new NotifyTaskCompletion<int>(MyStaticService.DoNothing());
            this.close = commandsMethods.DoCloseWindowCommand;
            removeBook = commandsMethods.DoRemoveBookCommand;
            chekRemoveBook = commandsMethods.ChekSelected;
            newAdd = commandsMethods.DoAddBookCommand;
            changeBook = commandsMethods.DoChangeCommand;
            give = commandsMethods.DoGiveCommand;
            chekGiveBook = commandsMethods.ChekGiveAllowed;
            take_away = commandsMethods.DoReturnCommand;
            chekNail = commandsMethods.ChekReturnAllowed;
            chuse = commandsMethods.DoChuseGivenBooksCommand;
            using (AppContext db = new AppContext())
            {
                Books = new ObservableCollection<Book>(db.Books.ToList());
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
