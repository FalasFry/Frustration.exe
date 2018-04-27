using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Frustration
{
    public class GameOverState : States
    {
        Texture2D gameOver;
        float score;
        float timeElapsed;
        Vector2 scorePos = new Vector2(325, 240);
        Vector2 timePos = new Vector2(325, 290);
        List<Components> buttons;
        SpriteFont font;

        public GameOverState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content, float points, int time) : base(Game, graphicsDevice, content)
        {
            gameOver = content.Load<Texture2D>("gameover");
            score = points;
            timeElapsed = time;
            font = content.Load<SpriteFont>("font");

            Texture2D buttonTexture = content.Load<Texture2D>("button");
            SpriteFont buttonFont = content.Load<SpriteFont>("font");

            Button menu = new Button(buttonTexture, buttonFont)
            {
                Pos = new Vector2(325, 340),
                Text = "Main Menu"
            };
            menu.Click += Menu_Click;

            buttons = new List<Components>()
            {
                menu,
            };
        }

        #region Button Click
        private void Menu_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                game.PopStack();
            }
            
        }
        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(gameOver, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.DrawString(font, "You Got A Score Of " + score + " points", scorePos, Color.White);
            spriteBatch.DrawString(font, "You survived " + timeElapsed + " seconds", timePos, Color.White);

            foreach(Button components in buttons )
            {
                components.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        public override bool Update(GameTime gameTime)
        {
            foreach(Button components in buttons)
            {
                components.Update(gameTime);
            }

            return true;
        }
    }
}
