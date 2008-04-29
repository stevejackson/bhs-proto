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

namespace BHS.Logic
{
    public class SpaceObject
    {
        public Sprite sprite;
        public ContentManager content;

        public SpaceObject()
        {
            sprite = new Sprite();
        }

        virtual public void Initialize(Vector2 position)
        {
            sprite.Initialize(position, "Circle", 10f);
        }

        virtual public void LoadContent(ContentManager content)
        {
            string fn = @"Content\Graphics\Objects\Asteroid";
            sprite.LoadContent(content, fn);
            sprite.InitPhysics();
            sprite.Layer = 0.1f;
            sprite.Origin = new Vector2(sprite.Size.X / 2, sprite.Size.Y / 2);
            this.content = content;
        }

        virtual public void Update(BlackHole bh)
        {
            bool attract = true;

            if (attract)
            {
                //get the distance between the 2 objects from edge-to-edge (not center-to-center)
                float dist = Vector2.Distance(bh.sprite.Position, this.sprite.Position);
                dist -= bh.sprite.Size.X / 2f;
                //dist -= (this.sprite.Size.X / 1f);

                //get a vector in the proper direction            
                Vector2 diff = new Vector2();
                diff = bh.sprite.Position - this.sprite.Position;

                /* calculate the power of the pull
                 * C/(d^p)
                 * C = power constant
                 * d = distance
                 * p = 1 - linear movement
                 *     2 - fast as it gets closer
                 *     3 - insanely fast as it gets closer.. 
                 */
                float power = 100f / ((float)Math.Pow(dist, 1.25f));

                //retain the direction, but give it proper power
                diff.Normalize();
                diff *= power;

                this.sprite.physicsBody.ApplyForce(diff);
            }
        }

        public void UnloadContent()
        {
            sprite.UnloadContent();
        }
    }
}
