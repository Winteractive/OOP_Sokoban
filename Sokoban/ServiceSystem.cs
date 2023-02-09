using System.Collections.Generic;
using System.Linq;

namespace Sokoban
{
   public abstract class ServiceSystem<T1, T2> where T2 : new() where T1 : IBound
   {
      public static T2 INSTANCE => CreateIfNeededThenReturn();
      protected static T2 TRUE_INSTANCE;

      private static T2 CreateIfNeededThenReturn()
      {
         if (TRUE_INSTANCE != null) return TRUE_INSTANCE;
         TRUE_INSTANCE = new T2();
         return TRUE_INSTANCE;
      }

      protected List<T1> objectsToService;
      public void Add(T1 t)
      {
         objectsToService ??= new List<T1>();
         if (objectsToService.Contains(t)) return;
         objectsToService.Add(t);
      }

      public void Clear()
      {
         List<T1> toRemove = objectsToService.Where(x => x.GetIsLevelBound()).ToList();
         objectsToService = objectsToService.Except(toRemove).ToList();
      }
   }
}