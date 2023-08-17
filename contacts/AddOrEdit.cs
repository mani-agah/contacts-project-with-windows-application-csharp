using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace contacts
{
    public partial class AddOrEdit : Form
    {
        ContactEntities1 context = new ContactEntities1();
         MyContactTbl mycontact = new MyContactTbl();
        public AddOrEdit()
        {
            InitializeComponent();
        }
        public AddOrEdit(MyContactTbl contact)
        {
            InitializeComponent();
            txtName.Text = contact.Name;
            txtFamily.Text = contact.Family;
            txtEmail.Text = contact.Email;
            txtMobile.Text = contact.Mobile;
            txtJob.SelectedValue = contact.Job_ID_FK;
            txtCity.SelectedValue = contact.City_ID_FK;
            dateTimePicker1.Value = contact.Birthday;
            var cit = context.CityTbls.Where(e => e.CityID == contact.City_ID_FK);
            var st = context.StateTbls.Where(e => e.StateID == cit.FirstOrDefault().State_ID_FK);
            var coid = context.CountryTbls.Where(e => e.CountryID == st.FirstOrDefault().Country_ID_FK);
            txtState.SelectedValue = st.FirstOrDefault().StateID;
            txtCountry.SelectedValue = coid.FirstOrDefault().CountryID;
        }


        private async void AddOrEdit_Load(object sender, EventArgs e)
        {
            fillcombobox();
        }

        private void fillcombobox()
        {
            txtCountry.DataSource = context.CountryTbls.ToList();
            txtCountry.ValueMember = "CountryID";
            txtCountry.DisplayMember = "CountryName";

            txtJob.DataSource = context.JobTbls.ToList();
            txtJob.ValueMember = "JobID";
            txtJob.DisplayMember = "JobName";
        }

        private void txtState_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txtState.ValueMember != string.Empty)
            {
                List<CityTbl> list = context.CityTbls.Where(x => x.State_ID_FK == (int)txtState.SelectedValue).ToList();
                txtCity.DataSource = list;
                txtCity.DisplayMember = "CityName";
                txtCity.ValueMember = "CityID";
            }
        }

        private void txtCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txtCountry.ValueMember != string.Empty)
            {
                List<StateTbl> list = context.StateTbls.Where(x => x.Country_ID_FK == (int)txtCountry.SelectedValue).ToList();
                txtState.DataSource = list;
                txtState.DisplayMember = "StateName";
                txtState.ValueMember = "StateID";
            }
        }
          
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isvalid())
            {

                string s = Path.GetFileName(textImage.Text) ;
                mycontact.addContact(txtName.Text, txtFamily.Text, txtEmail.Text, txtMobile.Text, dateTimePicker1.Value, (int)txtCity.SelectedValue, (int)txtJob.SelectedValue, s);
                File.Copy(textImage.Text, Path.Combine(Environment.CurrentDirectory + @"\images\", Path.GetFileName(textImage.Text)), true);
                Form1 frm = new Form1();
                this.Hide();
                frm.ShowDialog();
                frm.Refresh();

            }

        }
        public bool isvalid()
        {
            bool t = true;
            List<TextBox> list = new List<TextBox>() { txtEmail, txtFamily, txtMobile, txtName };

            foreach (var item in list)
            {
                if (item.Text == string.Empty)
                {
                    MessageBox.Show("Please fill " + item.Name.Remove(0, 3), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    t = false;
                    break;
                }
                else
                {
                    t = true;
                }

            }
            return t;

        }
        private void txtjob_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog photo = new OpenFileDialog();
            photo.Filter = "Image Files(*.jpg; *.png; *.jpeg;)| *.jpg; *.png; *.bmp;";
            if (photo.ShowDialog() == DialogResult.OK)
            {
                textImage.Text = photo.FileName;
                pictureBox1.Image = new Bitmap(photo.FileName);

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
