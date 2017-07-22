using DMHelper.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Being thing1 = new Being("The Dude", HitDice.D10, 2, 14, 14, 30, 22, 14);
            thing1.setStatistics(13, 16, 16, 14, 15, 12);

            entityInformation.setCharacter(thing1);

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
