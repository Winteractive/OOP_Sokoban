using Microsoft.Xna.Framework;

namespace Sokoban
{
   public class Camera
   {
      private Board board;

      public Camera(Board board)
      {
         this.board = board;
      }

      public int GetXoffset()
      {
         int offset = SokobanGame.WIDTH / 2;
         offset -= board.GetBoardWidth() * SokobanGame.CELL_SIZE / 2;
         return offset;
      }

      public int GetYOffset()
      {
         int offset = SokobanGame.HEIGHT / 2;
         offset -= board.GetBoardHeight() * SokobanGame.CELL_SIZE / 2;
         return offset;
      }

      public Vector2 GetOffset()
      {
         return new Vector2(GetXoffset(), GetYOffset());
      }

   }
}
