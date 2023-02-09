/*
 _    _ _       _                      _   _              _________    _____  _____  _____  _____ 
| |  | (_)     | |                    | | (_)            / /  __ \ \  / __  \|  _  |/ __  \/ __  \
| |  | |_ _ __ | |_ ___ _ __ __ _  ___| |_ ___   _____  | || /  \/| | `' / /'| |/' |`' / /'`' / /'
| |/\| | | '_ \| __/ _ \ '__/ _` |/ __| __| \ \ / / _ \ | || |    | |   / /  |  /| |  / /    / /  
\  /\  / | | | | ||  __/ | | (_| | (__| |_| |\ V /  __/ | || \__/\| | ./ /___\ |_/ /./ /___./ /___
 \/  \/|_|_| |_|\__\___|_|  \__,_|\___|\__|_| \_/ \___| | | \____/| | \_____/ \___/ \_____/\_____/                                                         \_\     /_/                                                                                                                               
For educational use only
 */


using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Sokoban;

namespace EMMA_ENGINE.Input;

public class InputSystem : ServiceSystem<IRecieveInput, InputSystem>
{
   private InputList list;

   private InputHandler firstHandler;

   private Keys[] prevKeys;

   public void SetInputList(InputList _list)
   {
      list = _list;
   }

   public InputList GetInputList() => list;

   public void ParseAndSendInputs(float dt)
   {
      if (list == null) return;

      var keyboardState = Keyboard.GetState();

      foreach (var input in list.inputs)
      {
         string choosenInput = input.GetInput();

         if (input.input == "UP") choosenInput = "DOWN";
         else if (input.input == "DOWN") choosenInput = "UP";

         if (input.GetIsHeld())
         {
            if (input.AdvanceTimer(dt))
            {
               Send(choosenInput);
            }
         }
         else
         {
            foreach (var key in input.GetKeys())
            {
               if (keyboardState.IsKeyDown(key))
               {
                  if (prevKeys == null)
                  {
                     Send(choosenInput);
                  }
                  else if (prevKeys.Contains(key) == false)
                  {
                     Send(choosenInput);
                  }
               }
            }
         }

         bool held = false;

         foreach (var key in input.GetKeys())
         {
            if (keyboardState.IsKeyDown(key) && prevKeys.Contains(key))
            {
               held = true;
               break;
            }
         }

         input.SetHeld(held);
      }

      prevKeys = keyboardState.GetPressedKeys();
   }

   public void Send(string input)
   {
      if (firstHandler == null)
      {
         SendFinalInput(input);
      }
      else
      {
         SendFinalInput(firstHandler.RecieveInput(input));
      }
   }

   public void AddHandler(InputHandler newHandler)
   {
      Debug.Log("adding input handler: " + newHandler);
      if (firstHandler == null) firstHandler = newHandler;

      else if (IsInputHandlerPresentInChain(newHandler, firstHandler))
      {
         Debug.LogWarning("handler: " + newHandler + " already present in chain");
      }

      else GetLastHandler().SetNextHandler(newHandler);
   }

   public void RemoveHandler(InputHandler remove)
   {

      if (firstHandler == null) return;

      if (firstHandler == remove)
      {
         firstHandler = firstHandler.GetNextHandler();
         return;
      }
      if (IsInputHandlerPresentInChain(remove, firstHandler) == false) return;

      InputHandler parent = GetParentHandlerFor(remove, firstHandler);

      // we're not removing The Last handler
      if (GetLastHandler() != remove)
      {
         InputHandler child = remove.GetNextHandler();

         parent.SetNextHandler(child);
      }
      else
      {
         parent.SetNextHandler(null);
      }

   }

   private InputHandler GetParentHandlerFor(InputHandler h, InputHandler evaluatedParent)
   {
      if (firstHandler == h) return h;

      if (evaluatedParent.GetNextHandler() == h) return evaluatedParent;

      return GetParentHandlerFor(h, evaluatedParent.GetNextHandler());
   }

   private bool IsInputHandlerPresentInChain(InputHandler h, InputHandler evaluatedParent)
   {
      if (evaluatedParent.GetNextHandler() == null) return false;
      if (firstHandler == null) return false;
      if (firstHandler == h) return true;

      if (evaluatedParent.GetNextHandler() == h) return true;

      return IsInputHandlerPresentInChain(h, evaluatedParent.GetNextHandler());
   }

   public InputHandler GetLastHandler()
   {
      return firstHandler.GetLastHandler();
   }

   private void SendFinalInput(string input)
   {
      if (objectsToService == null || objectsToService.Count == 0) return;

      foreach (var obj in objectsToService)
      {
         obj.RecieveInput(input);
      }
   }
 }