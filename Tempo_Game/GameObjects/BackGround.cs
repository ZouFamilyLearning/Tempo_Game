using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempo_Game.Controllers;

namespace Tempo_Game.GameObjects
{
    public class Background
    {
        public Bitmap image;

        public int x;
        public int y;
        public int width;
        public int height;

        public Background(string backGroundImageName, int x, int y, int width, int height)
        {
            image = SourceController.getImage(backGroundImageName);
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public void draw(Graphics g)
        {
            g.DrawImage(image, -x + 3072, y, width, height);
        }
    }
}
