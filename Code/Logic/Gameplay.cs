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
using F2D.Math;

namespace BHS.Logic
{
    class Gameplay : GameScreen
    {
        #region Game content

        BlackHole bh;
        Emitter astEmit;
        List<Sprite> borders = new List<Sprite>();

        #endregion

        public Gameplay()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            Grid.Initialize(400, new Vector2(5000, 5000), 2);
            Grid.LoadContent(this.content, @"Content\Graphics\bgCell");
            /* load stuff here */
            bh = new BlackHole();
            bh.Initialize(new Vector2(200, 200));
            bh.LoadContent(this.content);

            astEmit = new Emitter();
            astEmit.Initialize(new Vector2(800, 600), ObjectType.Asteroid, 2f, 10f);
            astEmit.LoadContent(this.content);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            bh.UnloadContent();
            astEmit.UnloadContent();

            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            /* code here */
            Camera.Follow(bh.sprite);

            astEmit.Update(bh);

            /* attract all objects towards bh */

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
                Vector2 diff = (Director.Rat.Position + Camera.Position) - bh.sprite.Position;
                float power = 100f;
                diff.Normalize();
                diff *= power;
                bh.sprite.physicsBody.LinearVelocity = diff;
                Camera.Follow(bh.sprite);
            }
            else
            {
                bh.sprite.physicsBody.LinearVelocity = new Vector2(0, 0);
            }

           // Director.Game.Window.Title = Camera.Position.ToString();
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
