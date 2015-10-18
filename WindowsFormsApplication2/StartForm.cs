using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimonSays
{
    public partial class StartForm : Form
    {
        private Game game;
        public StartForm()
        {
            game = new Game();
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }
        
        private void StartForm_OnKeyDown(object sender, EventArgs e)
        {
            Console.Write("DUPA");
        }

        private void startGame_Click(object sender, EventArgs e)
        {
            if (game.isFinished == true) {
                game.destroy();
                game = new Game();
                game.start();
            } else
            {
                game.start();
            }
        }
    }
}
