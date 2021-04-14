using System;
using System.Drawing;
using System.Windows.Forms;

using UnityEngine;

using ArtilleryGame.GameObjects;

namespace ArtilleryGame
{
    public partial class LauncherForm : Form
    {
        public LauncherForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Close();

            using (var game = new GameEngine())
            {
                game.ClearColor = Color.White;
                game.VSync = OpenTK.VSyncMode.Off;
                game.Width = 1280;
                game.Height = 900;
                game.WindowBorder = OpenTK.WindowBorder.Fixed;

                game.RegisterObject(new Background(@"Textures/Background/Background.jpg"));

                game.Run();
            }
        }
    }
}
