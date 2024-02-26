using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalleryApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Images (*.jpg;*.jpeg)|*.jpg;*.jpeg|PNG Images(*.png)|*.png|All Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*jpeg;*.png;*.bmp";
            ofd.FilterIndex = 3;
            ofd.Multiselect = true;
            ofd.Title = "Open Images";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string[] FileLocations = ofd.FileNames;

                foreach (string FL in FileLocations)
                {

                    Panel pnl = new Panel();
                    pnl.BorderStyle = BorderStyle.FixedSingle;
                    pnl.Width = 75;
                    pnl.Height = 75;

                    PictureBox pb = new PictureBox();
                    
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.ImageLocation = FL;
                    pb.Dock = DockStyle.Fill;

                    pb.Click += Pb_Click;

                    flowLayoutPanel1.Controls.Add(pnl);
                    pnl.Controls.Add(pb);
                }
            }
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            Form frm = new Form();
            frm.Text = "Preview";
            frm.Height = 300;
            frm.Width = 600;


            PictureBox pbnew = new PictureBox();
            pbnew.ImageLocation = pb.ImageLocation;
            pbnew.SizeMode = pb.SizeMode;
            pbnew.Dock = DockStyle.Fill;

            frm.Controls.Add(pbnew);
            frm.Show();
        }

        private void mnuKaydet_Click(object sender, EventArgs e)
        {
            List<string> kaydedilecekDosyalar = new List<string>();

            foreach (Panel item in flowLayoutPanel1.Controls)
            {
                PictureBox picturebox = (PictureBox)item.Controls[0];
                kaydedilecekDosyalar.Add(picturebox.ImageLocation);
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Gallery";
            sfd.Filter = "Save File (*.txt;)| *.txt";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllLines(sfd.FileName, kaydedilecekDosyalar);
                MessageBox.Show("Gallery Save");
            }
        }

        private void mnuAc_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Gallery";
            ofd.Filter = "Open File (*.txt;)| *.txt";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] dosyalar = System.IO.File.ReadAllLines(ofd.FileName);

                foreach (string FL in dosyalar)
                {
                    Panel pnl = new Panel();
                    pnl.BorderStyle = BorderStyle.FixedSingle;
                    pnl.Width = 75;
                    pnl.Height = 75;

                    PictureBox pb = new PictureBox();

                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.ImageLocation = FL;
                    pb.Dock = DockStyle.Fill;

                    pb.Click += Pb_Click;

                    flowLayoutPanel1.Controls.Add(pnl);
                    pnl.Controls.Add(pb);
                }
            }
        }
    }
}
