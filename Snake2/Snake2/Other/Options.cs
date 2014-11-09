using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake2
{
    public partial class frmOptions : Form
    {
        int numberOfPlayers;

        public frmOptions()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rb1Player.Checked)
            {
                numberOfPlayers = 1;
            } 
            else if (rb2Player.Checked)
            {
                numberOfPlayers = 2;
            } 
            else if (rb3Player.Checked)
            {
                numberOfPlayers = 3;
            } 
            else if (rb4Player.Checked)
            {
                numberOfPlayers = 4;
            }
            //Settings.numberOfPlayers = numberOfPlayers;

            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
