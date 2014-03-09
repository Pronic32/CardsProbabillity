using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CardsProbability
{
    public partial class ExperienceForm : Form
    {
        public Experience Exp = new Experience(0, 0);
        public ExperienceForm()
        {
            InitializeComponent();
        }

        private void Probability_Load(object sender, EventArgs e)
        {
            MinimumSize = MaximumSize = Size;
            try
            {
                nupd_extracted.Value = Exp.ExtractedCount;
            }
            catch (ArgumentException)
            {
                nupd_extracted.Value = nupd_extracted.Minimum;
            }
            cmbx_total.Text = (Exp.TotalCount != 0) ? Exp.TotalCount.ToString() : cmbx_total.Items[0].ToString();

        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            Exp.ExtractedCount = (byte)nupd_extracted.Value;
            Exp.TotalCount = byte.Parse(cmbx_total.Text);
            if (Exp.ExtractedCount > Exp.TotalCount)
            {
                MessageBox.Show("Число вынутых карт не можеть число всех карт в колоде");
            }
            else
            {
                Close();
            }
        }

        private void cmbx_total_SelectedIndexChanged(object sender, EventArgs e)
        {
            nupd_extracted.Maximum = byte.Parse(cmbx_total.Text);
            
        }
    }
}
