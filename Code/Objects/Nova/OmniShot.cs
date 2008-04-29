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
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D.Graphics;
using F2D.Core;
using F2D.Input;

namespace BHS.Objects.Nova
{
    public class OmniShot : NovaShot
    {
        public OmniShot()
            : base()
        {

        }

        public override void LoadContent(ContentManager content)
        {
            string fn = @"Content\Graphics\Objects\Shot1";
            sprite.Layer = 0.1f;
            this.content = content;
            sprite.LoadContent(content, fn);
            sprite.Origin = new Vector2(sprite.Size.X / 2, sprite.Size.Y / 2);
            sprite.InitPhysics();
        }
    }
}
