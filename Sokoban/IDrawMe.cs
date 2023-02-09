using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban
{
   public interface IDrawMe : IBound
   {
      public void Draw(SpriteBatch batch, Vector2 offset);
     
   }
}