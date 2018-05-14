using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Tempo_Game.Controllers;

namespace Tempo_Game.GameObjects
{
    public class Player
    {
        public enum State { JUMP_NORMALLY, JUMPING_HIGHER, JUMP_FARTHER };// 人物跳躍時，速度分為JUMP_NORMALLY、JUMPING_HIGHER、JUMP_FARTHER
        public State state = State.JUMP_NORMALLY;
        public int hp = 200;
        public int score = 0;
        public int speed = 16;
        public GameObject startGameObject;
        public GameObject targetGameObject;
        public GameController controller;
        public Bitmap image;
        public int startJumpPosition;
        public int targetJumpPosition;
        public int pictureOffsetX = 80;     // 人物在螢幕上顯示給玩家看的X軸位置
        public int pictureOffsetY = 385;    // 人物在螢幕上顯示給玩家看的Y軸位置
        public int x = 0;                   // 人物移動時與上個位置之間所產生的X軸差距
        public int y = 0;                   // 人物移動時與上個位置之間所產生的Y軸差距
        public int width = 109;             // 人物的 width
        public int height = 110;            // 人物的 height
        public bool currentlyAnimating = false;
        public bool keyPressOfJump;


        public System.Media.SoundPlayer playerJumpMusic;

        public Player(GameController controller, string playerImageName, GameObject startGameObject, GameObject targetGameObject)
        {
            this.controller = controller;
            this.startGameObject = startGameObject;
            this.targetGameObject = targetGameObject;

            startJumpPosition = startGameObject.x;      // 一開始人物要跳躍時的位置 = 物件一開始的位置
            targetJumpPosition = targetGameObject.x;    // 人物跳下來時的位置 = startGameObject的下個物件位置

            image = new Bitmap(playerImageName);             // 人物的圖片生成
        }

        // 畫Player
        public void draw(Graphics g)
        {
            if (!currentlyAnimating)
            {
                ImageAnimator.Animate(image, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }

            ImageAnimator.UpdateFrames();

            g.DrawImage(image, pictureOffsetX - width / 2, pictureOffsetY - y, width, height);

        }

        public void OnFrameChanged(object o, EventArgs e)
        {
            controller.form.Invalidate();
        }
    }
}

