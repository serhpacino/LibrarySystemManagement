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
using System.Windows.Navigation;

namespace LibrarySystemManagement
{
    /// <summary>
    /// Interaction logic for LendingBook.xaml
    /// </summary>
    public partial class LendingBook : Window
    {
        SqlConnection c = new SqlConnection(@"Data Source=DESKTOP-SOMLP49;Initial Catalog=LibraryManagementDB;Integrated Security=True;Pooling=False");
        public int bookId=0;
        public LendingBook()
        {
            InitializeComponent();
        }

        private void button_search_by_id_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Student inner join Specialization on Student.student_specialization = Specialization.spec_Id where Student.Id ='"+textbox_studentId.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if (i == 0)
            {
                MessageBox.Show("Invalid Id or this Id is not found");
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    label_student_firstname.Content = dr["student_FirstName"].ToString();
                    label_student_lastname.Content = dr["student_LastName"].ToString();
                    label_student_email.Content = dr["student_Email"].ToString();
                    label_student_semestr.Content = dr["student_semestr"].ToString();
                    label_student_specialization.Content = dr["Specialization_Name"].ToString();
                }
            }
        }

        private void lending_book_Load(object sender, RoutedEventArgs e)
        {
            if(c.State==ConnectionState.Open)
            {
                c.Close();
            }
            c.Open();
        }

        private void textbox_bookname_KeyUp(object sender, KeyEventArgs e)
        {
            int count = 0;
            
            
            if(e.Key != Key.Enter)
            {
                listbox_bookname.Items.Clear();
                SqlCommand cmd = c.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Book where Title like('%" + textbox_bookname.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                if(count > 0)
                {
                    listbox_bookname.Visibility = Visibility.Visible;
                    foreach (DataRow dr in dt.Rows)
                    {
                        listbox_bookname.Items.Add(dr["Title"].ToString());
                        bookId =Convert.ToInt32( dr["Id"].ToString());
                    }
                }
            }

            
        }

        private void textbox_bookname_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Down)
            {
                listbox_bookname.Focus();
                listbox_bookname.SelectedIndex = 0;
            }
        }

        private void listbox_bookname_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                textbox_bookname.Text = listbox_bookname.SelectedItem.ToString();
                listbox_bookname.Visibility = Visibility.Hidden;
            }
        }

        private void listbox_bookname_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            textbox_bookname.Text = listbox_bookname.SelectedItem.ToString();
            listbox_bookname.Visibility = Visibility.Hidden;
        }

        private void button_book_lending_Click(object sender, RoutedEventArgs e)
        {
            int available_book_amount=0;
            SqlCommand cmd2 = c.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from Book where Title ='"+textbox_bookname.Text+"'";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            sda2.Fill(dt2);
            foreach(DataRow dr2 in dt2.Rows)
            {
                available_book_amount = Convert.ToInt32( dr2["Available"].ToString());
            }
            if (available_book_amount > 0)
            {


                SqlCommand cmd = c.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into BookOrder values('" + textbox_studentId.Text + "','" + bookId +"' ,'"+ datepicker_orderdate.Text +"','')";
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = c.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "update Book set Available = Available-1 where Title='" + textbox_bookname.Text + "'";
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Book Order is successfully added");
            }else
            {
                MessageBox.Show("Not available right now");
            }
        }
    }
}
