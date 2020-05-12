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
    /// Interaction logic for AddNewBook.xaml
    /// </summary>
    public partial class AddNewBook : Window
    {
        SqlConnection c = new SqlConnection(@"Data Source=DESKTOP-SOMLP49;Initial Catalog=LibraryManagementDB;Integrated Security=True;Pooling=False");
        public AddNewBook()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            c.Open();
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Book values('"+textbox_title.Text+"','"+textbox_author.Text+"','"+textbox_publisher.Text+"','"+datepicker_purchase.DisplayDate+"',"+textbox_price.Text+","+textbox_amount.Text+",'"+textbox_genre.Text+"')";
            cmd.ExecuteNonQuery();
            c.Close();
            textbox_title.Text = "";
            textbox_author.Text = "";
            textbox_publisher.Text = "";
            textbox_price.Text = "";
            textbox_amount.Text = "";
            textbox_genre.Text = "";
            MessageBox.Show("Book: " + textbox_title.Text + " was added into library");
        }

       
    }
}
