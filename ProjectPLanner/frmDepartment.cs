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
    public partial class frmDepartment : Form
    {

        public static int InstanceCount = 0;

        public frmDepartment()
        {
            InitializeComponent();
        }

        private void frmDepartment_Load(object sender, EventArgs e)
        {
            dgvDepartments.AutoGenerateColumns = false;
            loadGrid();
        }

        private void frmDepartment_FormClosed(object sender, FormClosedEventArgs e)
        {
            InstanceCount -= 1;
        }

        private void loadGrid()
        {
            using (DBManager dm = new DBManager())
            {
                dgvDepartments.DataSource = DBLayer.Department.ListAll(dm);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmEditDepartment frm = new frmEditDepartment();
            frm.department = null;
            frm.ShowDialog();
            loadGrid();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows[0] == null)
            {
                MessageBox.Show("Please select a department");
                return;
            }
            else
            {
                Department department = new Department();
                using (DBManager dm = new DBManager())
                {
                    int id = (int)dgvDepartments.SelectedRows[0].Cells[0].Value;

                    department.Find(dm, id);
                }

                frmEditDepartment frm = new frmEditDepartment();
                frm.department = department;
                frm.ShowDialog();
                loadGrid();
            }

            //dgvDepartments.SelectedRows[0].Cells[0].Value;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows[0] == null)
            {
                MessageBox.Show("Please select a department");
                return;
            }
        }
    }
}
