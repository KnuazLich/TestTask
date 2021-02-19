using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TestTask
{
    public class CommandsStudentVMMethods
    {
        public void DoDellStudentCommand(object parameter)
        {
            remove_from_bd(StudentVM.selectedStudent);
            StudentVM.Students.Remove(StudentVM.selectedStudent);
        }
        public void DoGiveStudentCommand(object parameter)
        {
            AppVM.selectedBook.FullName = StudentVM.selectedStudent.Name;
            AppVM.selectedBook.IssueDateDate = DateTime.Now;
            update_bd(AppVM.selectedBook);
            Window w = parameter as Window;
            (w)?.Close();
        }
        public void DoAddStudentCommand(object parameter)
        {
            Student studen = new Student() { Name = ((TextBox)parameter)?.Text };
            StudentVM.Students.Add(studen);
            add_student_to_bd(studen);
        }
        public bool ChekSelected(object parameter)
        {
            Student s = parameter as Student;
            if (StudentVM.selectedStudent != null)
                return true;
            return false;
        }
        public bool ChekNotEmpty(object parameter)
        {
            TextBox t = parameter as TextBox;
            if (t?.Text != "")
                return true;
            return false;
        }

        private void update_bd(Book b)
        {
            using (AppContext db = new AppContext())
            {
                db.Books.Update(b);
                db.SaveChanges();
            }
        }
        private void remove_from_bd(Student s)
        {
            using (AppContext db = new AppContext())
            {
                db.Students.Remove(s);
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
    }
}
