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

namespace ProjectPLanner
{
    public partial class frmEditDepartment : Form
    {

        public Department department = null;

        public frmEditDepartment()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditDepartment_Load(object sender, EventArgs e)
        {
            if (department == null)
            {
                this.Text = "New Department";
            }
            else
            {
                this.Text = "Edit Department";
                txtName.Text = department.Name;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (department != null)
            {
                // UPDATE
                department.Name = txtName.Text;
                using (DBManager dm = new DBManager()) {
                    if (!department.Update(dm))
                    {
                        MessageBox.Show(Department.LastError);
                    }
                 }
                Close();
            }
            else
            {
                // INSERT
                department = new Department();
                department.Name = txtName.Text;
                using (DBManager dm = new DBManager())
                {
                    if (!department.Insert(dm))
                    {
                        MessageBox.Show(Department.LastError);
                    }
                }
                Close();

            }

        }
    }
}
