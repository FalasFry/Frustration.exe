using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frustration
{
    public abstract class States
    {

        #region Fields

        protected ContentManager contentManager;

        protected GraphicsDevice graphDevice;

        protected Game1 game;


        #endregion

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract bool Update(GameTime gameTime);

        public States(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            game = Game;
            graphDevice = graphicsDevice;
            contentManager = content;
        }

        #endregion

    }
}
