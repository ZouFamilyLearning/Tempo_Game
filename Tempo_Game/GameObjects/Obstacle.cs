using Tempo_Game.Controllers;

namespace Tempo_Game.GameObjects
{
    public class Obstacle : GameObject
    {
        private int hp;
        private int score;

        public Obstacle(GameController controller, int hp, int score, int position) : base(controller, "Images//Obstacle_2.png", position)
        {
            this.controller = controller;
            this.hp = hp;
            this.score = score;
        }

        public override void handleCollision(Player player)
        {
            controller.obstacleMusic.Play();
            player.hp -= hp;
            player.score -= score;
        }

    }
}
