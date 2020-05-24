using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibrarySystemManagement
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddNewBook addb = new AddNewBook();
            addb.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            BookViewer bv = new BookViewer();
            bv.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            AddStudent ast = new AddStudent();
            ast.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            ViewStudent vst = new ViewStudent();
            vst.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            LendingBook lb = new LendingBook();
            lb.Show();
        }

        private void returningBook_Click(object sender, RoutedEventArgs e)
        {
            BookReturning br = new BookReturning();
            br.Show();
        }

        private void studentDebts_Click(object sender, RoutedEventArgs e)
        {
            StudentDebts sd = new StudentDebts();
            sd.Show();
        }
    }
}
