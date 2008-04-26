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

namespace BHS.Logic
{
    class Emitter
    {
        public double minTime;
        public double maxTime;
        public double lastShot;
        public Sprite sprite;

        List<SpaceObject> objects = new List<SpaceObject>();

        public ObjectType objectType;
        public ContentManager content;

        public Emitter()
        {
            this.sprite = new Sprite();
            lastShot = Director.GameTime.TotalGameTime.TotalSeconds;
        }

        public void Initialize(Vector2 position, ObjectType objectType, float minTime, float maxTime)
        {
            this.objectType = objectType;
            this.minTime = minTime;
            this.maxTime = maxTime;
            this.sprite.Initialize(position, "Rect", 500f);
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            this.sprite.LoadContent(this.content, @"Content\Graphics\Objects\Emitter");
            this.sprite.physicsBody.IsStatic = true;
            this.sprite.physicsBody.Enabled = false;
        }

        public void Update(BlackHole bh)
        {
            if (Director.GameTime.TotalGameTime.TotalSeconds - lastShot > minTime)
            {
                lastShot = Director.GameTime.TotalGameTime.TotalSeconds;
                this.Shoot();
            }

            foreach (SpaceObject o in objects)
            {
                o.Update(bh);
            }
        }

        public void Shoot()
        {
            switch(this.objectType)
            {
                case ObjectType.Asteroid:
                    Asteroid asteroid = new Asteroid();
                    asteroid.Initialize(this.sprite.Position);
                    asteroid.LoadContent(this.content);
                    objects.Add(asteroid);
                    break;

                case ObjectType.Comet:

                    break;

                default:

                    break;
            }
        }

        public void UnloadContent()
        {
            sprite.UnloadContent();
            foreach (SpaceObject o in objects)
            {
                o.UnloadContent();
            }
        }
    }
}