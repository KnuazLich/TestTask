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
    public class StudentVM
    {
        private Book book;
        public static Student selectedStudent;
        private RelayCommand giveBook;
        private RelayCommand addStudent;
        private RelayCommand dellStudent;
        public RelayCommand closeWindow;
        public CommandsStudentVMMethods StudentVMMethods;
        private CommandsAppVMMethods appVMMethods;
        public static ObservableCollection<Student> Students { get; set; }
        public string Name
        {
            get { return selectedStudent.Name; }
            set
            {
                selectedStudent.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }
        public RelayCommand GiveBook
        {
            get
            {
                return giveBook ??
                  (giveBook = new RelayCommand(StudentVMMethods.DoGiveStudentCommand, StudentVMMethods.ChekSelected));
            }
        }       
        public RelayCommand DellStudent
        {
            get
            {
                return dellStudent ??
                  (dellStudent = new RelayCommand(StudentVMMethods.DoDellStudentCommand,
                    StudentVMMethods.ChekSelected));
            }
        }
        public RelayCommand AddStudent
        {
            get
            {
                return addStudent ??
                  (addStudent = new RelayCommand(StudentVMMethods.DoAddStudentCommand, StudentVMMethods.ChekNotEmpty));
            }
        }
        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow ?? (closeWindow = new RelayCommand(appVMMethods.DoCloseWindowCommand));
            }
        }
        public StudentVM()
        {
            book = AppVM.selectedBook;
            StudentVMMethods =new CommandsStudentVMMethods();
            appVMMethods = new CommandsAppVMMethods();
            using (AppContext db = new AppContext())
            {
                Students = new ObservableCollection<Student>(db.Students.ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
