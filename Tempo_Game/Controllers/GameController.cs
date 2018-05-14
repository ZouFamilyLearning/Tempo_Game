using System;
using System.Collections.Generic;
using System.Drawing;
using Tempo_Game.GameObjects;

namespace Tempo_Game.Controllers
{
    public class GameController
    {
        enum GameState { PAUSE, RUNNING, }

        // 生成GameObject的list陣列
        public List<GameObject> gameObjects = new List<GameObject>();
        public Player player;
        public Background background1;
        public Background background2;
        public Gameform form;

        public bool hasJumped = false;
        public bool hasCollided = false;

        // 做出list陣列所需要的變數
        public int amountOfTempo = 300;
        public int amountOfObjects = 300;
        public int distanceOfObjectes = 300;
        public int heightOfJump = 200;
        public int distanceOfShowObject = 2000;

        public int platformScore = 10;
        public int healboxHp = 20;
        public int obstracleMinusHp = 40;
        public int obstracleMinusScore = 25;

        // Music
        public System.Windows.Media.MediaPlayer backGroundMusic;
        public System.Media.SoundPlayer playerJumpMusic;
        public System.Media.SoundPlayer obstacleMusic;

        private System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath).Parent.Parent;

        public Random random = new Random();

        public GameController(Gameform form)
        {
            this.form = form;
            createMedia();
            createGameObjects();

            background1 = new Background("Images//gameBackground_4.jpg", 0, 0, 3072, 768);
            background2 = new Background("Images//gameBackground_4.jpg", 3072, 0, 3072, 768);

            player = new Player(this, "Images//runner.gif", gameObjects[0], gameObjects[1]);
        }

        public void createGameObjects() // 創造gameobjects list
        {
            List<int> positionOfSheet = new List<int>(); // 將位置先儲存起來

            for (int i = 0; i < amountOfTempo; i++)
            {
                positionOfSheet.Add(i);
            }

            for (int i = 0; i < amountOfObjects; i++)
            {
                int positionIndex = random.Next(0, positionOfSheet.Count);
                int position = positionOfSheet[positionIndex] * distanceOfObjectes;
                positionOfSheet.RemoveAt(positionIndex);


                // 生成GameObject用隨機的方式
                // 要再改成有一定規則
                switch (random.Next(0, 5))
                {
                    case 0:
                        gameObjects.Add(new Obstacle(this, obstracleMinusHp, obstracleMinusScore, position));
                        break;

                    case 1:
                        gameObjects.Add(new HealBox(this, healboxHp, position));
                        break;

                    case 2:
                        gameObjects.Add(new Obstacle(this, obstracleMinusHp, obstracleMinusScore, position));
                        break;

                    default:
                        gameObjects.Add(new Platform(this, platformScore, position));
                        break;
                }
            }

            gameObjects.Sort();


            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (i + 1 != gameObjects.Count)
                {
                    if (gameObjects[i] is Obstacle && gameObjects[i + 1] is Obstacle)
                    {
                        int doubleObstacle = gameObjects[i + 1].x;
                        if (i % 3 == 0)
                        {
                            gameObjects[i + 1] = new HealBox(this, healboxHp, doubleObstacle);
                        }
                        else
                        {
                            gameObjects[i + 1] = new Platform(this, platformScore, doubleObstacle);
                        }
                    }
                }
            }


            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (i + 1 < gameObjects.Count)
                {
                    gameObjects[i].nextGameObject = gameObjects[i + 1];
                }
            }
        }

        private void createMedia()
        {
            backGroundMusic = new System.Windows.Media.MediaPlayer();
            obstacleMusic = new System.Media.SoundPlayer();
            playerJumpMusic = new System.Media.SoundPlayer();

            backGroundMusic.Open(new Uri(dir.FullName + "\\Sounds\\" + "MainMusic_Unity.wav"));
            backGroundMusic.Play();

            playerJumpMusic.SoundLocation = dir.FullName + "\\Sounds\\" + "JumpingMusic.wav";
            playerJumpMusic.Load();

            obstacleMusic.SoundLocation = dir.FullName + "\\Sounds\\" + "Obstacle (online-audio-converter.com).wav";
            obstacleMusic.Load();
        }

        public void tick()
        {
            int distanceOfJumping = player.targetJumpPosition - player.startJumpPosition;

            player.x += player.speed * distanceOfJumping / distanceOfObjectes;

            background1.x += player.speed / 2 * distanceOfJumping / distanceOfObjectes;
            background2.x += player.speed / 2 * distanceOfJumping / distanceOfObjectes;
            background1.x %= 6144;
            background2.x %= 6144;

            if (player.x > player.startGameObject.x)
            {
                player.startJumpPosition = player.startGameObject.x;
                player.targetJumpPosition = player.targetGameObject.x;
            }

            int x = player.x - player.startJumpPosition;

            GameObject currentObject = null;

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.checkCollided(player))
                {
                    currentObject = gameObject;
                    break;
                }
            }

            if (currentObject != null)
            {
                if (currentObject.collide(player))
                {
                    if (player.keyPressOfJump == true)
                    {
                        if (!(currentObject is Obstacle))
                        {
                            player.state = Player.State.JUMP_FARTHER;
                            player.targetGameObject = player.targetGameObject.nextGameObject;
                            player.keyPressOfJump = false;
                        }
                    }
                }
            }
            else
            {
                player.keyPressOfJump = false;
            }

            float center = distanceOfJumping / 2;
            float a = heightOfJump / (center * center);

            player.y = (int)(-(x - center) * (x - center) * a) + heightOfJump;
        }

        public void draw(Graphics g)
        {
            //background1.draw(g);
            //background2.draw(g);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.draw(g, player);
            }


            player.draw(g);
        }
    }
}

