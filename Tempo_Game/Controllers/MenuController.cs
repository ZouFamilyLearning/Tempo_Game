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

        List<MyButton> buttons = new List<MyButton>();

        public MenuController(Gameform form)
        {
            // Set Parent Form
            this.form = form;

            // Set Menu Status
            this.menuStatus = MENU_STATUS_WAIT;

            buttons.Add(new MyButton(300, 300, 200, Color.Red, "Start GAME!"));
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

        public void draw(Graphics g)
        {
            foreach(MyButton button in buttons)
            {
                button.draw(g);
            }
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

        class MyButton
        {
            int x;
            int y;
            int r;
            SolidBrush textBrush;
            SolidBrush circleBrush;
            Font font;
            string text;

            public MyButton(int x, int y, int r, Color color, string text)
            {
                this.x = x;
                this.y = y;
                this.r = r;
                this.text = text;
                font = new Font("Arial", r / 3);
                textBrush = new SolidBrush(Color.Black);
                circleBrush = new SolidBrush(color);
            }

            public void draw(Graphics g)
            {
                g.FillEllipse(circleBrush, x - r, y - r, 2 * r, 2 * r);
                g.DrawString(text, font, textBrush, new RectangleF(x - r * 0.8f, y - r * 0.8f, r * 1.6f, r * 1.6f));
            }
        }
    }
}
