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
using System.Data.SqlClient;
using System.Data;

namespace LibrarySystemManagement
{
    /// <summary>
    /// Interaction logic for StudentDebts.xaml
    /// </summary>
    public partial class StudentDebts : Window
    {
        SqlConnection c = new SqlConnection(@"Data Source=DESKTOP-SOMLP49;Initial Catalog=LibraryManagementDB;Integrated Security=True;Pooling=False");
        public StudentDebts()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(c.State==ConnectionState.Open)
            {
                c.Close();
            }
            c.Open();
            fillDataGrid();
        }
        private void fillDataGrid()
        {
            
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Id,Title,BookAuthor,PublisherName,Genre,Amount,Available from Book";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            datagrid_bookinfo.ItemsSource = dt.DefaultView;
        }

        private void datagrid_bookinfo_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView rowview = datagrid_bookinfo.SelectedItem as DataRowView;
            string id = rowview.Row[0].ToString();
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select BookOrder.Id,Book.Id,Student.Id,student_FirstName,student_LastName,student_Email,OrderBookDate from BookOrder inner join Student on BookOrder.StudentId = Student.Id inner join Book on BookOrder.BookId = Book.Id where Book.Id = '" + id + "' and ReturnBookDate='' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            datagrid_debts.ItemsSource = dt.DefaultView;
        }
    }
}
