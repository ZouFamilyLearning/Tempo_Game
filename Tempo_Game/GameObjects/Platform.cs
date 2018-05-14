using Tempo_Game.Controllers;

namespace Tempo_Game.GameObjects
{
    public class Platform : GameObject
    {
        // 碰到Platform，分數的計算
        private int score;

        public override void handleCollision(Player player)
        {
            player.score += score;
        }

        public Platform(GameController controller, int score, int x) : base(controller, "Images//Platform.png", x)
        {
            this.score = score;
        }

        // 加減的分數要記錄到哪裡
    }
}
