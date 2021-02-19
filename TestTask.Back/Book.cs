using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestTask
{
    public class Book : INotifyPropertyChanged, ICloneable
    {
        private int invNumber;
        private string bookName;
        private string authors;
        private int year;
        private DateTime entranceDate;
        private DateTime issueDateDate;
        private string fullName;
        public int Id { get; set; }
        public int InvNumber
        {
            get
            {
                return invNumber;
            }
            set
            {
                invNumber = value;
                OnPropertyChanged("InvNumber");
            }
        }
        public string BookName
        {
            get
            {
                return bookName;
            }
            set
            {
                bookName = value;
                OnPropertyChanged("BookName");
            }
        }
        public string Authors
        {
            get
            {
                return authors;
            }
            set
            {
                authors = value;
                OnPropertyChanged("Authors");
            }
        }
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
                OnPropertyChanged("Year");
            }
        }
        public DateTime EntranceDate
        {
            get
            {
                return entranceDate;
            }
            set
            {
                entranceDate = value;
                OnPropertyChanged("EntranceDate");
            }
        }
        public DateTime IssueDateDate
        {
            get
            {
                return issueDateDate;
            }
            set
            {
                issueDateDate = value;
                OnPropertyChanged("IssueDateDate");
            }
        }
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
