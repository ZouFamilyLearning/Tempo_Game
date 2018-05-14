using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tempo_Game.Controllers
{
    public class SourceController
    {
        //private static List<Image> imgs;

        //public SourceController(List<String> imagePath)
        //{
        //    imgs = new List<Image>();
        //    for (int i = 0; i < imagePath.Count; i++)
        //    {
        //        imgs.Add((Image)new Bitmap(imagePath[i]));
        //    }
        //}

        //public Image getImage(int index)
        //{
        //    return imgs[index];
        //}
                                //index,  value  
        private static Dictionary<string, Bitmap> imgs = new Dictionary<string, Bitmap>();

        static public Bitmap getImage(string imagePath)
        {
            if (!imgs.ContainsKey(imagePath))
                imgs.Add(imagePath, new Bitmap(imagePath));

            return imgs[imagePath];
        }
    }
}
