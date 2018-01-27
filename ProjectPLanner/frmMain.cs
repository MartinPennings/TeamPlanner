using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectPLanner
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void personsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmPerson.InstanceCount == 0)
            {
                frmPerson frm = new frmPerson();
                frmPerson.InstanceCount += 1;
                frm.MdiParent = this;
                
                frm.Show();
            }
            else
            {
                MessageBox.Show(this,"A Persons window has already been opened.","Information");
            }
        }

        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmDepartment.InstanceCount == 0)
            {
                frmDepartment frm = new frmDepartment();
                frmDepartment.InstanceCount += 1;
                frm.MdiParent = this;

                frm.Show();
            }
            else
            {
                MessageBox.Show(this, "A Departments window has already been opened.", "Information");
            }
        }
    }
}
