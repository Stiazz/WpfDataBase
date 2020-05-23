using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using WpfDataBase.Models;

namespace WpfDataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MobileContext db;
        public MainWindow()
        {
            InitializeComponent();

            //using (db = new MobileContext())
            //{
            //    var phone = new Phone() { Title = "Title", Company = "Company", Price = 500 };

            //    db.Phones.Add(phone);
            //    db.SaveChanges();
            //}

            db = new MobileContext();
            
            db.Notify += Db_Notify;
            db.Phones.Load(); // загружаем данные
            DataViewGrid.ItemsSource = db.Phones.Local.ToBindingList(); // устанавливаем привязку к кэшу

            this.Closing += MainWindow_Closing;
        }

        private void Db_Notify(string text)
        {
            var a = DataViewGrid.ItemsSource.OfType<Phone>().Except(db.Phones);
            string b = "";
            foreach (var item in a)
            {
                b += item.Title + " ";
            }

            MessageBox.Show(b);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataViewGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < DataViewGrid.SelectedItems.Count; i++)
                {
                    Phone phone = DataViewGrid.SelectedItems[i] as Phone;
                    if (phone != null)
                    {
                        db.Phones.Remove(phone);
                    }
                }
            }
            db.SaveChanges();
        }
    }
}
