using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using TestTask;
using Xunit;

namespace TestTask.Tests
{
    [AttributeUsage(System.AttributeTargets.Method)]
    public class StudentVMTests : Attribute
    {
        [Fact]
        public void SelectedStudentProperty()
        {
            // Arrange
            StudentVM controller = new StudentVM(new Book());
            Student s = new Student() { Id=5, Name = "Петров Пётр Иванович" };
            Student s2 = new Student() { Id = 5, Name = "Иванов Пётр Иванович" };

            // Act
            controller.SelectedStudent = s;

            // Assert
            Assert.Equal("Петров Пётр Иванович", controller.Name);
            Assert.Equal(s, controller.SelectedStudent);
            Assert.NotEqual(s2, controller.SelectedStudent);            
        }
        //CloseWindow.Execute(obj);
        [Fact]
        public void TestComand()
        {
            Book b = new Book();
            StudentVM controller = new StudentVM(b);
            Thread t = new Thread(newwind);
            t.SetApartmentState(ApartmentState.STA);
            controller.SelectedStudent = new Student() { Name = "Петров Пётр Иванович" };
            controller.Students.Clear();
            controller.Students.Clear();
            controller.Students.Add(controller.SelectedStudent);
            //Window w = new Window();
            controller.DellStudent.Execute(new object());
            Assert.Empty(controller.Students);
            void newwind()
            {
                Window w = new Window();
                controller.DellStudent.Execute(w);
            }
            t.Join();
        }
        //CloseWindow
        [Fact]
        public void CloseComand()
        {
            Book b = new Book();
            Thread t = new Thread(newwind);
            Assert.True(false);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            void newwind()
            {
                StudentVM controller = new StudentVM(b);
                controller.SelectedStudent = new Student() { Name = "Петров Пётр Иванович" };
                controller.Students.Clear();
                controller.Students.Add(controller.SelectedStudent);
                Window w = new Window();
                controller.GiveBook.Execute(w);
                Assert.NotEqual(b.FullName, controller.SelectedStudent.Name);
                Assert.True(false);
            }
            t.Join();
            
        }
    }
}
