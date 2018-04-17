using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frustration
{
    public class Enemy
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Color color;
        float speed;
        float rotation;
        int Index = 0;
        List<Enemy> enemyList = new List<Enemy>();

        public void AddEnemy(Enemy enemy)
        {
            enemyList.Add(enemy);
        }

        public Enemy(Texture2D enemyTexture, Vector2 enemyStartPos, float enemySpeed, Vector2 enemyScale, float enemyRotation, Color enemyColor)
        {
            texture = enemyTexture;
            position = enemyStartPos;
            speed = enemySpeed;
            moveDir = new Vector2(-1,0);
            scale = enemyScale;
            offset = (enemyTexture.Bounds.Size.ToVector2() / 2) * scale;
            rectangle = new Rectangle((enemyStartPos - offset).ToPoint(), (enemyTexture.Bounds.Size.ToVector2() * enemyScale).ToPoint());
            color = enemyColor;
            rotation = enemyRotation;
            Index = enemyList.Count + 1;
        }
        public void Destroy (Enemy enemy)
        {
            enemyList.RemoveAt(enemy.Index);
        }
        public void Update()
        { 
            position += (moveDir * speed);
            rectangle.Location = (position - offset).ToPoint();
            rotation = (float)Math.Atan2(moveDir.X, moveDir.Y) * -1;
            
        }
        public void Update1Sec()
        {

        }
        public void DrawEnemy(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, offset, scale, SpriteEffects.None, 0);
        }
    }
}