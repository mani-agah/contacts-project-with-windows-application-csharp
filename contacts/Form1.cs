using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace contacts
{
    public partial class Form1 : Form
    {
        ContactEntities1 Context = new ContactEntities1();
        public Form1()
        {
            InitializeComponent();

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            allData();
            int id = (int)dgvContact.CurrentRow.Cells[0].Value;
            var contact1 = Context.MyContactTbls.Where(r => r.ID == id).FirstOrDefault();
            string location = Environment.CurrentDirectory + $@"\images\{contact1.Image}";
            pictureBox1.Image = new Bitmap(location, true);

        }

        private void allData()
        {
            var q = from a in Context.JobTbls
                    join b in Context.MyContactTbls on a.JobID equals b.Job_ID_FK
                    join c in Context.CityTbls on b.City_ID_FK equals c.CityID
                    join d in Context.StateTbls on c.State_ID_FK equals d.StateID
                    join f in Context.CountryTbls on d.Country_ID_FK equals f.CountryID
                    select new { b.ID, b.Name, b.Family, b.Email, b.Mobile, b.Birthday, a.JobName, c.CityName, d.StateName, f.CountryName };
            dgvContact.DataSource = q.ToList();
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            AddOrEdit frm = new AddOrEdit();
            this.Hide();
            frm.ShowDialog();
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {
            int id = (int)dgvContact.CurrentRow.Cells[0].Value;
            var contact1 = Context.MyContactTbls.FirstOrDefault(r => r.ID == id);
            Context.MyContactTbls.Remove(contact1);
            Context.SaveChanges();

            dgvContact.DataSource = Context.MyContactTbls.ToList();

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click_1(object sender, EventArgs e)
        {
            int id = (int)dgvContact.CurrentRow.Cells[0].Value;
            var contact = Context.MyContactTbls.FirstOrDefault(x => x.ID == id);
            AddOrEdit edit = new AddOrEdit(contact);
            edit.ShowDialog();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            if (txtSearch.Text == string.Empty)
            {
                allData();
            }
            else
            {
                var q = from a in Context.MyContactTbls
                        where a.Name.Contains(txtSearch.Text)
                        select a;
                dgvContact.DataSource = q.ToList();
            }
        }

        private void dgvContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            int id = (int)dgvContact.CurrentRow.Cells[0].Value;
            var contact1 = Context.MyContactTbls.Where(r => r.ID == id).FirstOrDefault();
            string location = Environment.CurrentDirectory + $@"\images\{contact1.Image}";
            pictureBox1.Image = new Bitmap(location, true);
        }

        private void dgvContact_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
