using System;
using System.Threading.Tasks;
using System.Windows;

namespace TestTask
{
    public class MyStaticService
    {
        public static async Task<int> TaskDelay(object parameter)
        {
            Book selectedBook = parameter as Book;
            MessageBoxResult result = MessageBox.Show($"Книга {selectedBook.BookName}, будет удалена?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            {
                //is completed
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        using (AppContext db = new AppContext())
                        {
                            db.Books.Remove(selectedBook);
                            db.SaveChanges();
                        }
                        AppVM.Books.Remove(selectedBook);
                        await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            
            return 0;
        }
        public static async Task<int> DoNothing()
        {
            return 0;
        }
    }
}
