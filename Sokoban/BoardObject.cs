using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sokoban
{
   public abstract class BoardObject : IUpdate, IDrawMe
   {
      protected Vector2 drawPosition;
      private Vector2 truePosition;
      private float moveTimer;
      protected Texture2D sprite;
      protected int x;
      protected int y;

      public BoardObject(int x, int y)
      {
         this.x = x;
         this.y = y;
         drawPosition = new Vector2(x * 32, y * 32);
         truePosition = drawPosition;
         UpdateSystem.INSTANCE.Add(this);
         DrawingSystem.INSTANCE.Add(this);
      }

      public int GetX() => x;
      public int GetY() => y;

      protected void DoMove(int xMove, int yMove)
      {
         x += xMove;
         y += yMove;

         moveTimer = 0;
         truePosition = new Vector2(x * SokobanGame.CELL_SIZE, y * SokobanGame.CELL_SIZE);
      }

      public void Update(float dt)
      {
         moveTimer += dt * 2;
         moveTimer = moveTimer > 1 ? 1 : moveTimer;
         drawPosition = Vector2.Lerp(drawPosition, truePosition, moveTimer);
      }

      public abstract bool AttemptMove(int xMove, int yMove, int depth);

      public virtual void Draw(SpriteBatch batch, Vector2 offset)
      {
         batch.Draw(sprite, drawPosition + offset, Color.White);
      }

      public virtual bool GetIsLevelBound() => true;
   }
}