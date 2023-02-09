/*
 _    _ _       _                      _   _              _________    _____  _____  _____  _____ 
| |  | (_)     | |                    | | (_)            / /  __ \ \  / __  \|  _  |/ __  \/ __  \
| |  | |_ _ __ | |_ ___ _ __ __ _  ___| |_ ___   _____  | || /  \/| | `' / /'| |/' |`' / /'`' / /'
| |/\| | | '_ \| __/ _ \ '__/ _` |/ __| __| \ \ / / _ \ | || |    | |   / /  |  /| |  / /    / /  
\  /\  / | | | | ||  __/ | | (_| | (__| |_| |\ V /  __/ | || \__/\| | ./ /___\ |_/ /./ /___./ /___
 \/  \/|_|_| |_|\__\___|_|  \__,_|\___|\__|_| \_/ \___| | | \____/| | \_____/ \___/ \_____/\_____/
                                                         \_\     /_/                                                                                                                               
*/

using System;
using System.Collections.Generic;
using System.Linq;
using EMMA_ENGINE;
using Microsoft.Xna.Framework.Input;

public class InputList
{
   public List<Input> inputs;

   const string UP = "UP";
   const string DOWN = "DOWN";
   const string LEFT = "LEFT";
   const string RIGHT = "RIGHT";
   const string SELECT = "SELECT";
   const string BACK = "BACK";
   const string PAUSE = "PAUSE";
   const string UNDO = "UNDO";
   const string REDO = "REDO";

   public class Input
   {
      public List<Keys> keys;
      public string input;
      public bool held;
      public bool repeatable;
      public float repeaterTime;
      public float onHeldTimerReductionPerIncrement;
      private int heldIncrements;
      public int heldIncrementsLimit;
      private float timer;

      public Input(List<Keys> keys, string input, bool repeatable = false, float repeaterTime = 0.2f, float onHeldTimerReductionPerIncrement = 0.015f, int heldIncrementsLimit = 10)
      {
         this.input = input;
         this.keys = keys;
         this.repeatable = repeatable;
         this.repeaterTime = repeaterTime;
         this.onHeldTimerReductionPerIncrement = onHeldTimerReductionPerIncrement;
         this.heldIncrementsLimit = heldIncrementsLimit;
      }

      public bool AdvanceTimer(float time)
      {
         timer -= time;

         float extra = onHeldTimerReductionPerIncrement * heldIncrements;

         if (!(timer - extra <= 0)) return false;

         if (held)
         {
            heldIncrements += 1;
            heldIncrements = heldIncrements > heldIncrementsLimit ? heldIncrementsLimit : heldIncrements;
         }
         else
         {
            heldIncrements = 0;
         }

         timer = repeaterTime;
         return true;
      }

      public void SetHeld(bool tf)
      {
         held = tf;
         if (held) return;

         heldIncrements = 0; // no longer holding = no more auto-speed up
         timer = repeaterTime; // reset timer from quick tap
      }

      public List<Keys> GetKeys() => keys;
      public string GetInput() => input;
      public bool GetIsRepeatable() => repeatable;
      public float GetRepeaterTime() => repeaterTime;

      private void OnValidate()
      {
         input = input.ToUpper();
      }

      public bool GetIsHeld()
      {
         return repeatable && held;
      }
   }

   public List<Input> GetAllInputs() => inputs;

   public void AddInput(Input input)
   {
      inputs ??= new List<Input>();

      if (inputs.Contains(input))
      {
         Debug.LogWarning("key + input pair already exists");
         return;
      }
      foreach (var item in inputs)
      {
         if (item.GetKeys().SequenceEqual(input.GetKeys()))
         {
            Debug.LogWarning("Keys already exists");
            return;
         }

      }

      inputs.Add(input);

   }

   public void AddInput(Keys key, string input, bool repeatable = false, float repeatTimer = 0.2f)
   {
      List<Keys> singleList = new List<Keys> { key };
      AddInput(singleList, input, repeatable, repeatTimer);
   }

   public void AddInput(List<Keys> key, string input, bool repeatable = false, float repeatTimer = 0.2f)
   {
      input = input.ToUpper();

      inputs ??= new List<Input>();

      Input newInput = new Input(key, input, repeatable, repeatTimer);

      AddInput(newInput);
   }

   public void AddBaseInputs()
   {
      if (inputs == null)
      {
         inputs = new List<Input>();
      }

      Input baseLeft = new Input(new List<Keys> { Keys.Left, Keys.A }, LEFT, true, 0.2f, 0.015f, 10);
      if (inputs.Contains(baseLeft) == false) AddInput(baseLeft);

      Input baseRight = new Input(new List<Keys> { Keys.Right, Keys.D }, RIGHT, true, 0.2f, 0.015f, 10);
      if (inputs.Contains(baseRight) == false) AddInput(baseRight);

      Input baseUp = new Input(new List<Keys> { Keys.Up, Keys.W }, UP, true, 0.2f, 0.015f, 10);
      if (inputs.Contains(baseUp) == false) AddInput(baseUp);

      Input baseDown = new Input(new List<Keys> { Keys.Down, Keys.S }, DOWN, true, 0.2f, 0.015f, 10);
      if (inputs.Contains(baseDown) == false) AddInput(baseDown);

      Input baseSelect = new Input(new List<Keys> { Keys.Enter, Keys.Space }, SELECT, false);
      if (inputs.Contains(baseSelect) == false) AddInput(baseSelect);

      Input baseBack = new Input(new List<Keys> { Keys.Z }, BACK, false);
      if (inputs.Contains(baseBack) == false) AddInput(baseBack);

      Input basePause = new Input(new List<Keys> { Keys.Escape, Keys.P }, PAUSE, false);
      if (inputs.Contains(basePause) == false) AddInput(basePause);
   }


}