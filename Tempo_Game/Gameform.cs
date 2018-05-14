using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tempo_Game.Controllers;
using Tempo_Game.GameObjects;

namespace Tempo_Game
{
    public partial class Gameform : Form
    {
        // Status Define
        private const int SCREEN_STATUS_MENU = 0;
        private const int SCREEN_STATUS_GAME_NORMAL = 1;
        private const int SCREEN_STATUS_HOW_TO_PLAY = 2;
        private const int SCREEN_STATUS_GAME_END = 3;


        // Controllers
        public MenuController menuController;
        public GameController gameController;


        // Program Status Control
        private int screenStatus;

        public Gameform()
        {
            // Init Controllers
            gameController = new GameController(this);
            menuController = new MenuController(this);

            InitializeComponent();
            this.Size = new Size(1240, 768); // 限制form大小


            // Init Program Status
            screenStatus = SCREEN_STATUS_MENU;


            //this.Controls.Clear();
            //menuController.showMenu();

            DoubleBuffered = true; // 將原本的物件保留一下，等下一個出現後再消失
        }


        private void Main_Tick(object sender, EventArgs e)
        {
            // 主選單
            //if (screenStatus == SCREEN_STATUS_MENU)
            //{
            //    if (menuController.GetMenuStatus() == MenuController.MENU_STATUS_START_CLICKED)
            //    {
            //        screenStatus = SCREEN_STATUS_GAME_NORMAL;
            //    }
            //}
            //else if (screenStatus == SCREEN_STATUS_GAME_NORMAL)
            //{

            //    this.Controls.Clear();

            //}
            gameController.tick();

            this.Invalidate(); // 全部洗掉再印一次，會去觸發OnPaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //if (screenStatus == SCREEN_STATUS_MENU)
            //{
            //}
            //else if (screenStatus == SCREEN_STATUS_GAME_NORMAL)
            //{
            //    gameController.draw(e.Graphics);
            //}
            gameController.draw(e.Graphics);
        }

        private void Jumping_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                if (gameController.player.state == Player.State.JUMP_NORMALLY)
                {
                    gameController.playerJumpMusic.Play();
                    gameController.player.keyPressOfJump = true;
                }
            }
        }
    }
}
