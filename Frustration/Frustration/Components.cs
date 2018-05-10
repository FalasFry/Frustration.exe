using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frustration
{
    public abstract class Components
    {
        #region Shhh dont look this is just for other classes

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        #endregion
    }
}
