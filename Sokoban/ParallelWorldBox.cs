using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class ParallelWorldBox : Box
    {
        public ParallelWorldBox(int x, int y, ContentManager contentManager, Board board) : base(x, y, contentManager, board)
        {
        }

        public override bool GetIsLevelBound()
        {
         return false;
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
         batch.Draw(sprite, drawPosition + offset, Color.Black);
      }
    }
}
