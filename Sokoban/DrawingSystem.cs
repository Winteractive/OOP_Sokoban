using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban
{
   public class DrawingSystem : ServiceSystem<IDrawMe, DrawingSystem>
   {
      public void Draw(SpriteBatch batch, Vector2 cameraOffset)
      {
         if (objectsToService == null || objectsToService.Count == 0) return;
         foreach (var obj in objectsToService)
         {
            obj.Draw(batch, cameraOffset);
         }
      }
   }
}
