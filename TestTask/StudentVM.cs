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
        private Student selectedStudent;
        private RelayCommand giveBook;
        private RelayCommand addStudent;
        private RelayCommand dellStudent;
        public RelayCommand closeWindow;
        public ObservableCollection<Student> Students { get; set; }
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
                  (giveBook = new RelayCommand(obj =>
                  {
                      book.FullName = selectedStudent.Name;
                      book.IssueDateDate = DateTime.Now;
                      update_bd();
                      CloseWindow.Execute(obj);
                  },
                    (o) => selectedStudent != null));
            }
        }       
        public RelayCommand DellStudent
        {
            get
            {
                return dellStudent ??
                  (dellStudent = new RelayCommand(obj =>
                  {
remove_from_bd();
                      Students.Remove(selectedStudent);
                          
                  },
                    (o) => selectedStudent != null));
            }
        }
        public RelayCommand AddStudent
        {
            get
            {
                return addStudent ??
                  (addStudent = new RelayCommand(obj =>
                  {
                      Student studen = new Student() {Name = ((TextBox)obj)?.Text};
                      Students.Add(studen);
                      add_student_to_bd(studen);
                  },
                  obj => ((TextBox)obj)?.Text!=""));
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
        public StudentVM(Book b)
        {
            book = b;
            using (AppContext db = new AppContext())
            {
                Students = new ObservableCollection<Student>(db.Students.ToList());
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
        private void remove_from_bd()
        {
            using (AppContext db = new AppContext())
            {
                db.Students.Remove(selectedStudent);
                db.SaveChanges();
            }
        }
        private void add_student_to_bd(Student s)
        {
            using (AppContext db = new AppContext())
            {
               db.Students.Add(s);
                db.SaveChanges();
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
