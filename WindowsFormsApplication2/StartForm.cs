using Open.WinKeyboardHook;
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
        private readonly IKeyboardInterceptor _interceptor;
        private Game game;
        private Corale.Colore.Core.Color selectedColor;

        public StartForm()
        {
            _interceptor = new KeyboardInterceptor();
            _interceptor.KeyPress += onKeyPress;
            InitializeComponent();
            colorsListBox.SetSelected(1, true);

        }

        private Corale.Colore.Core.Color updateFormColor(string colorName){
           switch (colorName)
            {
                case "Blue":
                    return Corale.Colore.Core.Color.Blue;
                case "Green":
                    return Corale.Colore.Core.Color.Green;
                case "HotPink":
                    return Corale.Colore.Core.Color.HotPink;
                case "Pink":
                    return Corale.Colore.Core.Color.Pink;
                case "Purple":
                    return Corale.Colore.Core.Color.Purple;
                case "White":
                    return Corale.Colore.Core.Color.White;
                case "Yellow":
                    return Corale.Colore.Core.Color.Yellow;
                case "Orange":
                    return Corale.Colore.Core.Color.Orange;
                default:
                    return Corale.Colore.Core.Color.Blue;
            }
        }

        private void onKeyPress(object sender, KeyPressEventArgs args)
        {
            if (game.isVotingTime)
            {
                Console.WriteLine(args.KeyChar);
                game.verifyPushedButton(args.KeyChar);
            }
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }

        private void startGame_Click(object sender, EventArgs e)
        {
            if (game != null)
            {
                if (game.isFinished == true)
                {
                    _interceptor.StopCapturing();
                    _interceptor.StartCapturing();
                    game = new Game(selectedColor);
                    game.start();
                }
                else
                {
                    _interceptor.StartCapturing();
                    game.start();
                }
            } else
            {
                _interceptor.StopCapturing();
                _interceptor.StartCapturing();
                game = new Game(selectedColor);
                game.start();
            }
        }

        private void restartGame_Click(object sender, EventArgs e)
        {
            game.destroy();
            _interceptor.StopCapturing();
            _interceptor.StartCapturing();
            game = new Game(selectedColor);
            game.start();
        }

        private void colorsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentItem = colorsListBox.SelectedItem;
            selectedColor = updateFormColor(currentItem.ToString());
        }
    }
}
