using EMMA_ENGINE.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Sokoban
{
   public class SokobanGame : Game
   {
      List<char[,]> levels;

      private enum GameStates { GAME, EDITOR };
      private GameStates state;
      private CustomEditor editor;

      private InputList gameInput;
      private InputList editorInput;

      public const int WIDTH = 640;
      public const int HEIGHT = 480;
      public const int GAME_UPSCALE = 2;
      public const int CELL_SIZE = 32;

      private GraphicsDeviceManager _graphics;
      private SpriteBatch spriteBatch;
      private Board board;
      private Camera camera;
      private int currentLevel;


      public SokobanGame()
      {
         _graphics = new GraphicsDeviceManager(this);
         Content.RootDirectory = "Content";
         IsMouseVisible = false;
      }

      protected override void Initialize()
      {
         _graphics.PreferredBackBufferWidth = WIDTH * GAME_UPSCALE;
         _graphics.PreferredBackBufferHeight = HEIGHT * GAME_UPSCALE;
         _graphics.ApplyChanges();

         state = GameStates.GAME;

         gameInput = new InputList();
         gameInput.AddBaseInputs();

         editorInput = new InputList();

         editorInput.AddInput(new List<Keys> { Keys.W, Keys.Up    },  "EDITOR_UP",    false);
         editorInput.AddInput(new List<Keys> { Keys.A, Keys.Left  },  "EDITOR_LEFT",  false);
         editorInput.AddInput(new List<Keys> { Keys.S, Keys.Down  },  "EDITOR_DOWN",  false);
         editorInput.AddInput(new List<Keys> { Keys.D, Keys.Right },  "EDITOR_RIGHT", false);
         editorInput.AddInput(Keys.T, "ADD_TARGET", false);
         editorInput.AddInput(Keys.G, "ADD_GROUND", false);
         editorInput.AddInput(Keys.X, "ADD_WALL", false);
         editorInput.AddInput(Keys.H, "ADD_HERO", false);
         editorInput.AddInput(Keys.B, "ADD_BOX", false);
         editorInput.AddInput(Keys.Delete, "DELETE_OBJECT", false);

         InputSystem.INSTANCE.SetInputList(gameInput);

         board = new Board(Content); 

         editor = new CustomEditor(Content, board);
         InputSystem.INSTANCE.Add(editor);

         camera = new(board);

         levels = new List<char[,]>();

         levels.Add(new char[5, 5]
         {
                { 'O','O','O','O','O', },
                { 'O','O','O','V','O', },
                { 'O','O','H','O','O', },
                { 'O','O','O','O','O', },
                { 'O','O','O','B','T', }
         });

         levels.Add(new char[5, 6]
     {
                { 'H','X','O','O','O','O', },
                { 'X','X','O','O','O','O', },
                { 'O','O','O','O','O','O', },
                { 'O','O','O','O','O','T', },
                { 'O','O','O','O','O','O', }
     });

         levels.Add(new char[5, 6]
         {
                { 'H','O','X','O','O','O', },
                { 'O','B','X','O','O','O', },
                { 'O','O','X','O','O','O', },
                { 'O','O','X','O','O','O', },
                { 'O','O','O','O','T','T', }
         });


         board.SetBoard(levels[0]);


         base.Initialize();
      }

      protected override void LoadContent()
      {
         spriteBatch = new SpriteBatch(GraphicsDevice);
      }

      protected override void Update(GameTime gameTime)
      {
         if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

         if (Keyboard.GetState().IsKeyDown(Keys.F1))
         {
            ChangeGameState(GameStates.GAME);
         }

         if (Keyboard.GetState().IsKeyDown(Keys.F2))
         {
            ChangeGameState(GameStates.EDITOR);
         }

         float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

         InputSystem.INSTANCE.ParseAndSendInputs(dt);
         UpdateSystem.INSTANCE.Update(dt);


         switch (state)
         {
            case GameStates.GAME:
               if (board.IsLevelFinished())
               {
                  LoadNextLevel();
               }
               break;
            case GameStates.EDITOR:
               break;
         }

         base.Update(gameTime);
      }

      private void ChangeGameState(GameStates newState)
      {
         if (newState == state) return;
         state = newState;
         switch (state)
         {
            case GameStates.GAME:
               InputSystem.INSTANCE.SetInputList(gameInput);
               break;
            case GameStates.EDITOR:
               InputSystem.INSTANCE.SetInputList(editorInput);
               break;
         }
      }

      private void LoadNextLevel()
      {
         DrawingSystem.INSTANCE.Clear();
         UpdateSystem.INSTANCE.Clear();
         if (levels.Count - 1 < currentLevel + 1)
         {
            currentLevel = -1;
         }
         currentLevel++;
         board.SetBoard(levels[currentLevel]);
      }

      protected override void Draw(GameTime gameTime)
      {
         GraphicsDevice.Clear(Color.CornflowerBlue);

         Matrix scalingMatrix = Matrix.CreateScale(GAME_UPSCALE);

         spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: scalingMatrix);

         DrawingSystem.INSTANCE.Draw(spriteBatch, camera.GetOffset());

         switch (state)
         {
            case GameStates.GAME:

               break;
            case GameStates.EDITOR:
               editor.DrawMe(spriteBatch, camera.GetOffset());
               break;
            default:
               break;
         }

         spriteBatch.End();

         base.Draw(gameTime);
      }
   }
}