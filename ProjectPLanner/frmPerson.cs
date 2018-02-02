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

namespace TeamPlanner
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

            dgPersons.AutoGenerateColumns = false;
            loadGrid();
                    

        }
        private void loadGrid()
        {
            using (DBManager dm = new DBManager())
            {
                dgPersons.DataSource = Person.ListAll(dm);
            }


        }
        private void frmPerson_FormClosed(object sender, FormClosedEventArgs e)
        {
            InstanceCount -= 1;
        }

        private void dgPersons_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmEditPerson frm = new frmEditPerson();
            frm.person = null;
            frm.ShowDialog();
            loadGrid();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgPersons.SelectedRows[0] == null)
            {
                MessageBox.Show("Please select a person");
                return;
            }
            else
            {
                Person person = new Person();
                using (DBManager dm = new DBManager())
                {
                    int id = (int)dgPersons.SelectedRows[0].Cells[0].Value;

                    person.Find(dm, id);
                }

                frmEditPerson frm = new frmEditPerson();
                frm.person = person;
                frm.ShowDialog();
                loadGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgPersons.SelectedRows[0] == null)
            {
                MessageBox.Show("Please select a person");
                return;
            }
            else
            {
                Person person = new Person();

                using (DBManager dm = new DBManager())
                {
                    int id = (int)dgPersons.SelectedRows[0].Cells[0].Value;

                    person.Find(dm, id);
                }

                if (MessageBox.Show("Are you sure? you want to delete person " + person.LastName + "?", "Delete Person", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (DBManager dm = new DBManager())
                    {

                        if (!person.Delete(dm))
                        {
                            MessageBox.Show(Person.LastError);
                        }
                    }

                    loadGrid();
                }
            }

        }

        private void dgPersons_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.btnEdit_Click(sender, e);
        }
    }
}
