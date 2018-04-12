using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Frustration
{
    public class Button : Components
    {
        #region Fields

        Texture2D texture;
        SpriteFont font;
        bool isHovering;
        MouseState curMouse;
        MouseState prevMouse;

        #endregion

        #region Props

        public event EventHandler Click;
        public bool Clicked { get; private set; }

        public Vector2 Pos { get; set; }
        public Color Paint { get; set; }
        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)Pos.X, (int)Pos.Y, texture.Width, texture.Height);
            }
        }
        public string Text { get; set; }

        #endregion



        public Button(Texture2D Texture, SpriteFont Font)
        {
            texture = Texture;
            font = Font;
            Paint = Color.Black;
        }


        #region Methods

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;


            if(isHovering)
            {
                colour = Color.Gray;
            }

            spriteBatch.Draw(texture, Rect, colour);

            if(!string.IsNullOrEmpty(Text))
            {
                var x = (Rect.X + (Rect.Width / 2) - (font.MeasureString(Text).X / 2));
                var y = (Rect.Y + (Rect.Height / 2) - font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font, Text, new Vector2(x,y), Paint);
            }

        }

        public override void Update(GameTime gameTime)
        {
            isHovering = false;

            prevMouse = curMouse;
            curMouse = Mouse.GetState();

            var mouseRect = new Rectangle(curMouse.X, curMouse.Y, 1, 1);

            if(mouseRect.Intersects(Rect))
            {
                isHovering = true;

                if(curMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this,new EventArgs());
                }
            }
        }

        #endregion
    }
}
