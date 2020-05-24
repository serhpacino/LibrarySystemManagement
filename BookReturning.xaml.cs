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
    /// Interaction logic for BookReturning.xaml
    /// </summary>
    public partial class BookReturning : Window
    {
        SqlConnection c = new SqlConnection(@"Data Source=DESKTOP-SOMLP49;Initial Catalog=LibraryManagementDB;Integrated Security=True;Pooling=False");
        public string bookid;
        public BookReturning()
        {
            InitializeComponent();
        }

        private void button_search_studentbyId_Click(object sender, RoutedEventArgs e)
        {
            fillGridView(textbox_studentId.Text);
        }
        public void fillGridView(string idstudent)
        {
            datagrid_rezervation.Columns.Clear();
            datagrid_rezervation.UpdateLayout();
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select BookOrder.Id,student_FirstName,student_Lastname,Title,OrderBookDate,ReturnBookDate from BookOrder inner join Student on BookOrder.StudentId = Student.Id inner join Book on BookOrder.BookId = Book.Id where StudentId = '"+idstudent+"' and ReturnBookDate ='' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            datagrid_rezervation.ItemsSource = dt.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (c.State == ConnectionState.Open)
            {
                c.Close();
            }
            c.Open();
        }

        private void datagrid_rezervation_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView rowview = datagrid_rezervation.SelectedItem as DataRowView;
            string id = rowview.Row[0].ToString();
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select BookOrder.Id,StudentId,BookId,Title,OrderBookDate,ReturnBookDate from BookOrder inner join Student on BookOrder.StudentId = Student.Id inner join Book on BookOrder.BookId = Book.Id where BookOrder.Id = '"+id+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                label_title.Content = dr["Title"].ToString();
                label_rezervation_date.Content = dr["OrderBookDate"].ToString();
                bookid = dr["BookId"].ToString();
            }
        }

        private void button_return_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = datagrid_rezervation.SelectedItem as DataRowView;
            string id = rowview.Row[0].ToString();
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update BookOrder set ReturnBookDate ='"+datepicker_returningdate.Text+"' where Id ='"+id+"'";
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = c.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "update Book set Available = Available + 1 where Id ='"+bookid+"'";
            cmd1.ExecuteNonQuery();
            MessageBox.Show("The Book is successfully returned");
            fillGridView(textbox_studentId.Text);
        }
    }
}
