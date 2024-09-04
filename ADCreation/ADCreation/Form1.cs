using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADCreation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSign_in_Click(object sender, EventArgs e)
        {
            // Create an instance of the SignInForm
            CreateUser signInForm = new CreateUser();

            // Show the sign-in form
            signInForm.Show();

            // Optionally, hide the current form (if you want to close it or hide it)
            this.Hide();
        }

        private void btnReSet_Click(object sender, EventArgs e)
        {
            Update modify=new Update();
            modify.Show();
            this.Hide();
        }
    }
}

    
