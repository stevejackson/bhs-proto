/* bhs-proto
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D.Core;
using F2D.Input;
using F2D.Graphics;

namespace BHS.Logic
{
    class Gameplay : GameScreen
    {
        #region Game content

        Sprite bh; //black hole        

        #endregion

        public Gameplay()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            /* load stuff here */
            bh = new Sprite();
            bh.Initialize(new Vector2(200, 200), "Circle", 100f);
            bh.LoadContent(this.content, @"Content\Graphics\Objects\black_hole");
            bh.Origin = new Vector2(bh.Size.X / 2, bh.Size.Y / 2);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            bh.UnloadContent();

            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            /* code here */

            Farseer.Physics.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void HandleInput(InputState input)
        {
            /* debug input */
            if (input.IsNewKeyPress(Keys.C))
            {
                Director.RenderCells = !Director.RenderCells;
            }

            /* generic input */
            if (input.IsNewKeyPress(Keys.Escape))
            {
                OnCancel();
            }

            /* game input */
            if (Director.Rat.LState == Rat.State.Down)
            {
                Vector2 diff = Director.Rat.Position - bh.Position;
                float power = 100f;
                diff.Normalize();
                diff *= power;
                bh.physicsBody.LinearVelocity = diff;
            }
            else
            {
                bh.physicsBody.LinearVelocity = new Vector2(0, 0);
            }

        }


        public void OnCancel()
        {
            GameScreen[] scr = new GameScreen[2];
            scr[0] = new Menu.Background();
            scr[1] = new Menu.MainMenu();
            Director.SwitchScreen(false, scr);
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle fullscreen = new Rectangle(0, 0, 1600, 1200);
            byte fade = TransitionAlpha;
        }

    }
}
