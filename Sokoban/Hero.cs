using EMMA_ENGINE.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban
{
   public class Hero : BoardObject, IRecieveInput
   {
      const int strength = 2;
      private Board board;

      public Hero(int x, int y, ContentManager contentManager, Board board) : base(x, y)
      {
         sprite = contentManager.Load<Texture2D>("hero");
         drawPosition = new Vector2(x * 32, y * 32);
         this.board = board;
         InputSystem.INSTANCE.Add(this);
      }

      public void RecieveInput(string input)
      {
         switch (input)
         {
            case "UP":
               AttemptMove(0, 1, 0);
               break;

            case "DOWN":
               AttemptMove(0, -1, 0);
               break;

            case "LEFT":
               AttemptMove(-1, 0, 0);
               break;

            case "RIGHT":
               AttemptMove(1, 0, 0);
               break;
         }
      }

      public override bool AttemptMove(int xMove, int yMove, int depth)
      {
         if (board.IsSpaceWalkable(x + xMove, y + yMove) == false) return false;
         if (board.IsPositionOutsideOfBoard(x + xMove, y + yMove)) return false;

         BoardObject searched = board.GetObjectAtPosition(x + xMove, y + yMove);
         if (searched != null)
         {
            if (searched is Box box)
            {
               if (box.AttemptMove(xMove, yMove, strength) == false)
               {
                  return false;
               }
            }
            else
            {
               return false;
            }
         }

         DoMove(xMove, yMove);
         return true;
      }


   }
}