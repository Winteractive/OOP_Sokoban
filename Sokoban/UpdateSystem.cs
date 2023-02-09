using System.Collections.Generic;
using System.Linq;

namespace Sokoban
{
   public class UpdateSystem : ServiceSystem<IUpdate, UpdateSystem>
   {
      public void Update(float dt)
      {
         if (objectsToService == null || objectsToService.Count == 0) return;
         foreach (var obj in objectsToService)
         {
            obj.Update(dt);
         }
      }
   }
}
