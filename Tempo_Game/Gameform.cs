using System;
using System.Drawing;
using System.Windows.Forms;
using Tempo_Game.Controllers;
using Tempo_Game.GameObjects;

namespace Tempo_Game
{
    public partial class Gameform : Form
    {
        enum GameState { SCREEN_STATUS_MENU, SCREEN_STATUS_GAME_NORMAL, SCREEN_STATUS_HOW_TO_PLAY, SCREEN_STATUS_GAME_END };
        GameState state = GameState.SCREEN_STATUS_MENU;

        // Controllers
        public MenuController menuController;
        public GameController gameController;

        public Gameform()
        {
            InitializeComponent();
            Size = new Size(1240, 768);     // 限制form大小
            DoubleBuffered = true;          // 將原本的物件保留一下，等下一個出現後再消失
        }

        private void Gameform_Load(object sender, EventArgs e)
        {
            gameController = new GameController(this);
            menuController = new MenuController(this);
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            if (state == GameState.SCREEN_STATUS_GAME_NORMAL)
            {
                gameController.tick();
            }

            Invalidate();           // 全部洗掉再印一次，會去觸發OnPaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (state == GameState.SCREEN_STATUS_MENU)
            {
                menuController.draw(e.Graphics);
            }
            else if (state == GameState.SCREEN_STATUS_GAME_NORMAL)
            {
                gameController.draw(e.Graphics);
            }
        }

        private void Jumping_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
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
