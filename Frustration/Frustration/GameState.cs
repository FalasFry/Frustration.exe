using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frustration
{
    public class GameState : States
    {
        KeyboardState pause = Keyboard.GetState();
        float pressTimer = 0f;

        public GameState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content) : base(Game, graphicsDevice, content)
        {
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override bool Update(GameTime gameTime)
        {
            pressTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(pause.IsKeyDown(Keys.Escape) && pressTimer >= 1f)
            {
                game.ChangeState(new Pause_Menu(game, graphDevice, contentManager));
                pressTimer = 0f;
            }

            return true;
        }

    }
}
