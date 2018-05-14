using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tempo_Game.Controllers
{
    public class MenuController
    {
        // Static variable
        private const int buttonWidth = 100;
        private const int buttonHeight = 40;

        public const int MENU_STATUS_START_CLICKED = 1;
        public const int MENU_STATUS_WAIT = 0;

        // Parent Panel
        private Gameform form;

        // Menu Object
        private Button startBtn;

        // Menu Status
        private int menuStatus;


        public MenuController(Gameform form)
        {
            // Set Parent Form
            this.form = form;

            // Set Menu Status
            this.menuStatus = MENU_STATUS_WAIT;
        }


        public void showMenu()
        {
            // clear form
            form.Controls.Clear();

            // set up button
            startBtn = new Button();

            startBtn.Location = new System.Drawing.Point((form.ClientSize.Width - buttonWidth) / 2, (form.ClientSize.Height - buttonHeight) / 2);
            startBtn.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            startBtn.Text = "Start Game";
            startBtn.Click += new System.EventHandler(startBtn_Click);
            
            
            // add object
            form.Controls.Add(startBtn);
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            menuStatus = MENU_STATUS_START_CLICKED;
        }

        // 預設值
        public int GetMenuStatus()
        {
            int result = menuStatus;
            menuStatus = MENU_STATUS_WAIT;
            return result;
        }

    }
}
