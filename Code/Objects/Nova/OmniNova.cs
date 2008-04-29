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
using BHS.Logic;

namespace BHS.Objects.Nova
{
    public class OmniNova : Nova
    {
        public OmniNova() : base()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            this.content = content;
            this.sprite.LoadContent(this.content, @"Content\Graphics\Objects\Emitter");
            this.sprite.InitPhysics();
            this.sprite.physicsBody.IsStatic = false;
        }

        public override void Shoot()
        {
            NovaShot shot = new NovaShot();

            switch (this.shotType)
            {
                case NovaShotType.Omni:
                    shot = new OmniShot();
                    break;
            }

            shot.Initialize(this.sprite.Position);
            shot.LoadContent(this.content);
            this.shots.Add(shot);
        }
    }
}