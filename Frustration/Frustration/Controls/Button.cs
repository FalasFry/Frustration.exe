using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Frustration
{
    public class Button : Components
    {

        // All variables that i am using for this script.
        #region Fields

        Texture2D texture;
        SpriteFont font;
        bool isHovering;
        MouseState curMouse;
        MouseState prevMouse;

        #endregion


        // All properties that is needed so that you can set the values of the buttn outside fo this code.
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


        // The cunstructor for the button.
        // Gives it a texture and a font and a color, you do the rest elsewhere.
        public Button(Texture2D Texture, SpriteFont Font)
        {
            texture = Texture;
            font = Font;
            Paint = Color.Black;
        }

        #region Methods

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color colour = Color.White;

            if(isHovering)
            {
                colour = Color.Gray;
            }

            spriteBatch.Draw(texture, Rect, colour);

            if(!string.IsNullOrEmpty(Text))
            {
                float x = (Rect.X + (Rect.Width / 2) - (font.MeasureString(Text).X / 2));
                float y = (Rect.Y + (Rect.Height / 2) - font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font, Text, new Vector2(x,y), Paint);
            }
        }

        public override void Update(GameTime gameTime)
        {
            isHovering = false;

            prevMouse = curMouse;
            curMouse = Mouse.GetState();

            Rectangle mouseRect = new Rectangle(curMouse.X, curMouse.Y, 1, 1);

            if(mouseRect.Intersects(Rect))
            {
                isHovering = true;

                if(curMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}