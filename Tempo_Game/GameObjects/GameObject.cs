using System;
using System.Drawing;
using Tempo_Game.Controllers;

namespace Tempo_Game.GameObjects
{
    public abstract class GameObject : IComparable
    {
        public int x;               // GameObject的X軸位置
        public int y = -55;         // GameObject的Y軸位置
        public int width = 96;      // GameObject的width
        public int height = 96;     // GameObject的heigth
        public bool canJumpOrNot = false;   // 判定可不可以跳躍
        public Bitmap image;
        public GameObject nextGameObject;
        public GameController controller;

        // Obstacle、HealBox、Platform都是GameObject，所以用多型來繼承覆寫
        public abstract void handleCollision(Player player);

        public GameObject(GameController controller, string imageName, int x)
        {
            this.controller = controller;
            this.x = x;                                   // 從GameController得到的位置存入
            image = SourceController.getImage(imageName);   // 圖片要讀入的位置，圖片位置回從各自的class中給
        }

        public bool collide(Player player)
        {
            // 當人物要跳躍的targetGameObject等於他現在抵達的這個位置，就執行if的內容
            if (player.targetGameObject == this)
            {
                handleCollision(player);

                canJumpOrNot = true;// 判定跳躍成功
                player.startGameObject = this;// 將startGameObject變成從上一個變成現在人物站的這一個位置
                if (true)
                {
                    player.targetGameObject = this.nextGameObject;//將targetGameObject從這一個位置變成下一個位置
                }
                player.state = Player.State.JUMP_NORMALLY;// 將人物速度設定成JUMP_NORMALLY
            }

            // 回傳跳躍成功&&人物目前速度為JUMP_NORMALLY
            return canJumpOrNot && player.state == Player.State.JUMP_NORMALLY;
        }

        // 圖片生成的位置
        public void draw(Graphics g, Player player)
        {
            if (x - player.x < 1300 && x - player.x > -100)
            {
                int iCanSeeYou = x - player.x + player.pictureOffsetX;
                g.DrawImage(image, iCanSeeYou - width / 2, player.pictureOffsetY - y, width, height);
            }
        }

        // 檢查是否碰撞
        public bool checkCollided(Player player)
        {
            return Math.Abs(player.x - x) < (player.width / 2 + width / 2) && Math.Abs(player.y) < 75;
        }

        public int CompareTo(object obj)
        {
            GameObject gameObject = obj as GameObject;
            return x.CompareTo(gameObject.x);
        }
    }
}
