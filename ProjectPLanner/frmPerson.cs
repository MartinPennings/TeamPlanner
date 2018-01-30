using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBLayer;

namespace TeamPLanner
{
    public partial class frmPerson : Form
    {
        public static int InstanceCount = 0;

        public frmPerson()
        {
            InitializeComponent();
        }

        private void frmPerson_Load(object sender, EventArgs e)
        {
            List<Person> lst;

            using (DBManager dm = new DBManager())
            {
                lst = Person.ListAll(dm);
            }

            dgPersons.DataSource = lst;


        }

        private void frmPerson_FormClosed(object sender, FormClosedEventArgs e)
        {
            InstanceCount -= 1;
        }
    }
}
