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
  public partial class ProbabilitiesForm : Form
  {
    public ProbabilitiesForm()
    {
      InitializeComponent();
    }

    private void Probabilities_Load(object sender, EventArgs e)
    {
      richTextBox1.Select(0, 1);
      richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font.Name, richTextBox1.Font.Size, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));


    }
  }
}
