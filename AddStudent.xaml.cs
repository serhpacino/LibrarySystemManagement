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
using System.IO;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace LibrarySystemManagement
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        int spec = 0;
        SqlConnection c = new SqlConnection(@"Data Source=DESKTOP-SOMLP49;Initial Catalog=LibraryManagementDB;Integrated Security=True;Pooling=False");

        public AddStudent()
        {
            InitializeComponent();
        }

        

        private void button_Register_Click(object sender, RoutedEventArgs e)
        {
            
            c.Open();
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (textbox_Email.Text.Length == 0)
            {
                MessageBox.Show("Enter a Email");
                textbox_Email.Focus();
            }
            else if (!Regex.IsMatch(textbox_Email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Enter a valid email");
                textbox_Email.Select(0, textbox_Email.Text.Length);
                textbox_Email.Focus();
            }
            else if (textbox_Password.Text.Length == 0)
            {
                MessageBox.Show("Enter a password");
                textbox_Password.Focus();
            }
            else if(combobox_Specialization.Text == "Programming")
            {
                spec = 1;
            }else if (combobox_Specialization.Text == "Management")
            {
                spec = 2;
            }
            else if (combobox_Specialization.Text == "Philology")
            {
                spec = 3;
            }
            else if (combobox_Specialization.Text == "Aviation")
            {
                spec = 4;
            }else if(combobox_Specialization.Text == "")
            {
                MessageBox.Show("Enter a specialization");
            }
            cmd.CommandText = "insert into Student values('" + textbox_FirstName.Text + "','" + textbox_LastName.Text + "','" + textbox_Email.Text + "','" + textbox_Password.Text + "','" + textbox_PhoneNumber.Text + "','" + combobox_Semestr.Text + "'," + spec + ")";
            cmd.ExecuteNonQuery();
            c.Close();
            MessageBox.Show("Student:"+textbox_FirstName.Text+","+textbox_LastName.Text+" was added successfully");
            textbox_FirstName.Text = "";
                textbox_LastName.Text = "";
                textbox_Email.Text = "";
                textbox_Password.Text = "";
                textbox_PhoneNumber.Text = "";
                combobox_Semestr.Text = "";
                
        }
    }
}
