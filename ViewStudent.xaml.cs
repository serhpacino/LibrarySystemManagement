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
    /// Interaction logic for ViewStudent.xaml
    /// </summary>
    public partial class ViewStudent : Window
    {
        SqlConnection c = new SqlConnection(@"Data Source=DESKTOP-SOMLP49;Initial Catalog=LibraryManagementDB;Integrated Security=True;Pooling=False");
        public ViewStudent()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                c.Open();
                SqlCommand cmd = c.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id,student_FirstName,student_Lastname,student_Email,student_Password,student_PhoneNumber,student_semestr,Specialization_Name from Student inner join Specialization on Student.student_specialization = Specialization.spec_id ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                datagrid_studentviewer.ItemsSource = dt.DefaultView;
                c.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void datagrid_studentviewer_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
            //DataRowView rowview = datagrid_studentviewer.SelectedItem as DataRowView;
            //string id = rowview.Row[0].ToString();
            //try
            //{
            //    c.Open();
            //    SqlCommand cmd = c.CreateCommand();
            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandText = "select * from Book where id =" + id + "";
            //    cmd.ExecuteNonQuery();
            //    DataTable dt = new DataTable();
            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //    sda.Fill(dt);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        textbox_title.Text = dr["Title"].ToString();
            //        textbox_author.Text = dr["BookAuthor"].ToString();
            //        textbox_publisher.Text = dr["PublisherName"].ToString();
            //        textbox_price.Text = dr["Price"].ToString();
            //        textbox_amount.Text = dr["Amount"].ToString();
            //        textbox_genre.Text = dr["Genre"].ToString();
            //        //DateTime Date_of_purchase = Convert.ToDateTime(dr["Date_of_purchase"].ToString());
            //        datepicker_purchasedate.SelectedDate = Convert.ToDateTime(dr["Date_of_purchase"].ToString());

            //    }
            //    c.Close();
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message);
            //}
        }

      
    }
}
