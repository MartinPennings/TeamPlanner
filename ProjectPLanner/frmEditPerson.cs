using DBLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TeamPlanner
{
    public partial class frmEditPerson : Form
    {

        public Person person = null;

        public frmEditPerson()
        {
            InitializeComponent();
        }

        private void frmEditPerson_Load(object sender, EventArgs e)
        {
            List<Department> lstDepartments;

            using (DBManager dm = new DBManager())
            {
                 lstDepartments = Department.ListAll(dm);
            }

            cbDepartment.DataSource = lstDepartments;
            cbDepartment.DisplayMember = "Name";
            cbDepartment.ValueMember = "ID";

            if (person == null)
            {
                this.Text = "New Person";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                cbDepartment.SelectedIndex = 0;
            }
            else
            {
                this.Text = "Edit Person";
                txtFirstName.Text = person.FirstName;
                txtLastName.Text = person.LastName;
                cbDepartment.SelectedValue = person.Department;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (person != null)
            {
                // UPDATE
                person.FirstName = txtFirstName.Text;
                person.LastName = txtLastName.Text;
                person.Department = (int)cbDepartment.SelectedValue;

                using (DBManager dm = new DBManager())
                {
                    if (!person.Update(dm))
                    {
                        MessageBox.Show(Person.LastError);
                    }
                }
                Close();
            }
            else
            {
                // INSERT
                person = new Person();
                person.FirstName = txtFirstName.Text;
                person.LastName = txtLastName.Text;
                person.Department = (int)cbDepartment.SelectedValue;

                using (DBManager dm = new DBManager())
                {
                    if (!person.Insert(dm))
                    {
                        MessageBox.Show(Department.LastError);
                    }
                }
                Close();

            }
        }
    }
}
