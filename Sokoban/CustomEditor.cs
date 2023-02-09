using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban
{
   public class CustomEditor : IRecieveInput
   {
      private int x;
      private int y;
      private Texture2D sprite;
      private Board board;

      public CustomEditor(ContentManager content, Board board)
      {
         sprite = content.Load<Texture2D>("1X1");
         this.board = board;
      }

      public void RecieveInput(string input)
      {
         switch (input)
         {
            case "EDITOR_LEFT":
               x -= 1;
               break;

            case "EDITOR_RIGHT":
               x += 1;
               break;

            case "EDITOR_UP":
               y -= 1;
               break;

            case "EDITOR_DOWN":
               y += 1;
               break;

            case "ADD_GROUND":
               board.SetGroundType('O', x, y);
               break;

            case "ADD_WALL":
               board.SetGroundType('X', x, y);
               break;

            case "ADD_TARGET":
               board.SetGroundType('T', x, y);
               break;

            case "ADD_HERO":
               board.AddObject('H', x, y);
               break;

            case "ADD_BOX":
               board.AddObject('B', x, y);
               break;

            case "DELETE_OBJECT":
               board.RemoveObject(x, y);
               break;
         }

         if (x < 0) x = 0;
         if (x > board.GetBoardWidth()) x = board.GetBoardWidth();

         if (y < 0) y = 0;
         if (y > board.GetBoardHeight()) y = board.GetBoardHeight();

      }

      public void DrawMe(SpriteBatch batch, Vector2 offset)
      {
         batch.Draw(sprite, new Vector2(x * 32, y * 32) + offset, Color.White);
      }

      public bool GetIsLevelBound()
      {
         return false;
      }
   }
}