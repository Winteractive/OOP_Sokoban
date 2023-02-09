using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban
{
   public class Box : BoardObject
   {
      private Board board;
      
      public Box(int x, int y, ContentManager contentManager, Board board) : base(x, y)
      {
         sprite = contentManager.Load<Texture2D>("box");
         this.board = board;
      }

      public override bool AttemptMove(int xMove, int yMove, int depth)
      {
         if (depth == 0) return false;
         if (board.IsSpaceWalkable(x + xMove, y + yMove) == false) return false;
         if (board.IsPositionOutsideOfBoard(x + xMove, y + yMove)) return false;
         var searched = board.GetObjectAtPosition(x + xMove, y + yMove);
         if (searched != null)
         {
            if (searched.GetType() == typeof(Box))
            {
               if (searched.AttemptMove(xMove, yMove, depth - 1) == false)
               {
                  return false;
               }
            }
         }

         DoMove(xMove, yMove);

         return true;

      }
   }
}