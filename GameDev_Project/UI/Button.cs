using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.UI
{
    public class Button : UIElement
    {
        private Action action;
        public Button(Texture2D texture, int width, int height, Vector2 position) : base(texture, width, height, position)
        {
        }
        public void DoAction()
        {

        }
    }
}
