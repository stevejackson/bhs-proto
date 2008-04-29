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
    public class Nova
    {
        public double minTime;
        public double maxTime;
        public double lastShot;
        public Sprite sprite;

        protected List<NovaShot> shots = new List<NovaShot>();
        
        public NovaShotType shotType;
        public ContentManager content;

        public Nova()
        {
            this.sprite = new Sprite();
            lastShot = Director.GameTime.TotalGameTime.TotalSeconds;
        }

        public void Initialize(Vector2 position, NovaShotType shotType, float minTime, float maxTime)
        {
            this.shotType = shotType;
            this.minTime = minTime;
            this.maxTime = maxTime;
            this.sprite.Initialize(position, "Circle", 500f);
        }

        virtual public void LoadContent(ContentManager content)
        {
            this.content = content;
            this.sprite.LoadContent(this.content, @"Content\Graphics\Objects\Emitter");
            this.sprite.InitPhysics();
            this.sprite.physicsBody.IsStatic = false;
        }

        virtual public void Update(BlackHole bh)
        {
            if (Director.GameTime.TotalGameTime.TotalSeconds - lastShot > minTime)
            {
                lastShot = Director.GameTime.TotalGameTime.TotalSeconds;
                this.Shoot();
            }

            foreach (NovaShot o in shots)
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
                    float multiplier = 1.5f; //modify this for easier scaling of power from 100k
                    float power = multiplier * 100000f / ((float)Math.Pow(dist, 1.25f));

                    //retain the direction, but give it proper power
                    diff.Normalize();
                    diff *= power;

                    this.sprite.physicsBody.ApplyForce(diff);
                }

                o.Update(bh);
            }
        }

        virtual public void Shoot()
        {

        }

        public void UnloadContent()
        {
            sprite.UnloadContent();

            foreach (NovaShot o in shots)
            {
                o.UnloadContent();
            }
        }
    }
}