using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace TestTask
{
    public interface ICommandsMethods
    {
        public delegate void DbRemoveChanged(Book b);
        public delegate void DbUpdateChanged(Book b);


        public void DoChuseGivenBooksCommand(object parameter);
        public void DoReturnCommand(object parameter);
        public void DoGiveCommand(object parameter);
        public void DoChangeCommand(object parameter);
        public void DoAddBookCommand(object parameter);
        public void DoCloseWindowCommand(object parameter);
        public bool ChekReturnAllowed(object parameter);
        public bool ChekSelected(object parameter);
        public bool ChekGiveAllowed(object parameter);
    }
}
