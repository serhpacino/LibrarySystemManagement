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
using System.Globalization;
using System.Threading;

namespace LibrarySystemManagement
{
    /// <summary>
    /// Interaction logic for BookViewer.xaml
    /// </summary>
    public partial class BookViewer : Window
    {
        SqlConnection c = new SqlConnection(@"Data Source=DESKTOP-SOMLP49;Initial Catalog=LibraryManagementDB;Integrated Security=True;Pooling=False");
        
       
        public BookViewer()
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
                cmd.CommandText = "select * from Book";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                dataGridBookView.ItemsSource = dt.DefaultView;
                c.Close();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            try
            {
                c.Open();
                SqlCommand cmd = c.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Book where Title like ('%"+textbox_search.Text+"%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridBookView.ItemsSource = dt.DefaultView;
                c.Close();
                if (i==0)
                {
                    MessageBox.Show("Please fill the gap to search a title of a book");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void textbox_search_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                c.Open();
                SqlCommand cmd = c.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Book where Title like ('%" + textbox_search.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                dataGridBookView.ItemsSource = dt.DefaultView;
                c.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void dataGridBookView_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            grid_updateinfo.Visibility = Visibility.Visible;
            DataRowView rowview = dataGridBookView.SelectedItem as DataRowView;
            string id = rowview.Row[0].ToString();
            try
            {
                c.Open();
                SqlCommand cmd = c.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Book where id ="+id+"";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    textbox_title.Text = dr["Title"].ToString();
                    textbox_author.Text = dr["BookAuthor"].ToString();
                    textbox_publisher.Text = dr["PublisherName"].ToString();
                    textbox_price.Text = dr["Price"].ToString();
                    textbox_amount.Text = dr["Amount"].ToString();
                    textbox_genre.Text = dr["Genre"].ToString();
                    //DateTime Date_of_purchase = Convert.ToDateTime(dr["Date_of_purchase"].ToString());
                    datepicker_purchasedate.SelectedDate = Convert.ToDateTime(dr["Date_of_purchase"].ToString());

                }
                c.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = dataGridBookView.SelectedItem as DataRowView;
            string id = rowview.Row[0].ToString();
            try
            {
                c.Open();
                SqlCommand cmd = c.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Book set Title ='"+textbox_title.Text+"',BookAuthor='"+textbox_author.Text+"',PublisherName='"+textbox_publisher.Text+"',Date_of_purchase='"+datepicker_purchasedate.SelectedDate+"',Price ="+textbox_price.Text+",Amount="+textbox_amount.Text+",Genre='"+textbox_genre.Text+"' where id ="+id+"";
                cmd.ExecuteNonQuery();
                grid_updateinfo.Visibility = Visibility.Hidden;
                c.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
