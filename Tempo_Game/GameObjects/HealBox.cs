using Tempo_Game.Controllers;

namespace Tempo_Game.GameObjects
{
    public class HealBox : GameObject
    {
        // 碰到HealBox，血量的計算
        private int hp;

        public override void handleCollision(Player player)
        {
            player.hp += hp;
        }

        public HealBox(GameController controller, int hp, int position) : base(controller, "Images//Healbox_1.png", position)
        {
            this.hp = hp;
        }

        // 加減的分數要記錄到哪裡

    }
}
