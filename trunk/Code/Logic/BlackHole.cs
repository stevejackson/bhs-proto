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
using FarseerGames;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;

namespace BHS.Logic
{
    public class BlackHole
    {
        public Sprite sprite;
        public ContentManager content;

        public BlackHole()
        {
            sprite = new Sprite();
        }

        public void Initialize(Vector2 position)
        {
            sprite.Initialize(position, "Rect", 10f);
        }

        public void LoadContent(ContentManager content)
        {
            string fn = @"Content\Graphics\Objects\black_hole";
            sprite.LoadContent(content, fn);
            sprite.Layer = 0.1f;
            sprite.Origin = new Vector2(sprite.Size.X / 2, sprite.Size.Y / 2);
            this.content = content;
            /*
            sprite.physicsBody = BodyFactory.Instance.CreateCircleBody(this.sprite.Size.X/2, 1000f);
            sprite.PhysicsGeometry = GeomFactory.Instance.CreateCircleGeom(F2D.Core.Farseer.Physics,
                this.sprite.physicsBody, (int)this.sprite.Size.X / 2, 16);

            sprite.physicsBody.IsStatic = false;
            sprite.physicsBody.Position = sprite.Position;
            sprite.physicsBody.Rotation = sprite.Rotation;
             */
        }

        public void Update()
        {
            
        }

        public void UnloadContent()
        {
            sprite.UnloadContent();
        }
    }
}
