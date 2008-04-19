/* bhs-proto
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using F2D.Core;

namespace BHS.Menu
{
    class MainMenu : MenuScreen
    {
        public MainMenu()
            : base("BHS")
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            // Create our menu entries.
            MenuEntry levelMenuEntry = new MenuEntry("Endurance Mode", @"Content\Graphics\menu\buttons\endmode");
            MenuEntry exitMenuEntry = new MenuEntry("Exit", @"Content\Graphics\menu\buttons\exit");

            levelMenuEntry.LoadContent(this.content);
            exitMenuEntry.LoadContent(this.content);


            // Hook up menu event handlers.
            levelMenuEntry.Selected += LevelMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(levelMenuEntry);
            MenuEntries.Add(exitMenuEntry);

            levelMenuEntry.Button.Position = new Vector2(1200, 200);
            exitMenuEntry.Button.Position = new Vector2(1200, 400);
        }

        void LevelMenuEntrySelected(object sender, EventArgs e)
        {
            Director.SwitchScreen(false, new BHS.Logic.Gameplay());
        }

        protected override void OnCancel()
        {
            Director.Game.Exit();
        }
    }
}

