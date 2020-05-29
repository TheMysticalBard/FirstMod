using FirstMod.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace FirstMod.Projectiles.Minions
{
    class CuteMinion : ModProjectile
    {
        Vector2 targetCenter;
        float finalDist;
        bool foundTarget;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rolly Boi");
            Main.projFrames[projectile.type] = 25;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetDefaults()
        {
            projectile.friendly = false;
            projectile.width = 56;
            projectile.height = 56;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if(player.dead || !player.active)
            {
                player.ClearBuff(BuffType<CuteMinionBuff>());
            }
            if (player.HasBuff(BuffType<CuteMinionBuff>()))
            {
                projectile.timeLeft = 2;
            }

            finalDist = 800f;
            targetCenter = projectile.position;
            foundTarget = false;

            if(player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                float dist = Vector2.Distance(npc.Center, projectile.Center);
                if(dist < 2300f)
                {
                    finalDist = dist;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if(!foundTarget)
            {
                foreach(NPC npc in Main.npc)
                {
                    if(npc.CanBeChasedBy())
                    {
                        float dist = Vector2.Distance(npc.Center, projectile.Center);
                        bool isClosestAndInRange = finalDist > dist;
                        if(dist < 800f && isClosestAndInRange)
                        {
                            finalDist = dist;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            projectile.friendly = foundTarget;

            if(foundTarget)
            {
                float attackVelocity = 10f;
                float attackInertia = 40f;
                Vector2 direction = targetCenter - projectile.Center;
                direction.Normalize();
                direction *= attackVelocity;
                projectile.velocity = (projectile.velocity * (attackInertia - 1) + direction) / attackInertia;

            }
            if(!foundTarget) {
                Vector2 idleCenter = player.Center;
                float idleRadius = 5f;
                if (Vector2.Distance(idleCenter, projectile.Center) > idleRadius)
                {
                    //Move to idle circle path
                    //IDEA: Find parabola such that the vertex is the top of the idleCircle, and one point on the parabola is the center of the projectile.
                    //Will look decent since the first derivatives are the same at the top of idleCircle.
                    Vector2 vertex = new Vector2(idleCenter.X, idleCenter.Y + idleRadius);
                    Vector2 point = projectile.Center;
                    //y = a(x-h)^2 + k, where h is vertex.X, and k is vertex.Y
                    //Solving for a yeilds a = (y-k)/(x-h)^2
                    float coeff = (float)((point.Y - vertex.Y) / Math.Pow(point.X - vertex.X, 2));
                    //Velocity is the first derivative of position w/ respect to time. Parameterize.
                    //y = a(t-h)^2 + k; x = t
                    //y' = 2a(t-h); x' = 1
                    float returnSpeed = 8f;
                    //Vector2 direction = new Vector2((point.X - vertex.X > 0) ? -1f : 1f, 2 * coeff * Math.Abs(point.X - vertex.X));
                    //direction.Normalize();
                    //direction *= returnSpeed;
                    //projectile.velocity = direction;
                    float inertia = 50f;
                    Vector2 direction = vertex - projectile.Center;
                    direction.Normalize();
                    direction *= returnSpeed;
                    projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
                }
            }

            if(Vector2.Distance(player.Center, projectile.Center) > 4000f)
            {
                projectile.Center = player.Center;
            }

            int frameSpeed = 5;
            projectile.frameCounter++;
            if (projectile.frameCounter >= frameSpeed)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(projectile.velocity.X != oldVelocity.X)
            {
                float randomScaling = 1f;
                if(Math.Abs(oldVelocity.X) < 2f)
                {
                    randomScaling = 4.5f;
                }
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -oldVelocity.X * Main.rand.NextFloat(1.0f, randomScaling);
            }
            if(projectile.velocity.Y != oldVelocity.Y)
            {
                float randomScaling = 1f;
                if (Math.Abs(oldVelocity.Y) < 2f)
                {
                    randomScaling = 4.5f;
                }
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -oldVelocity.Y * Main.rand.NextFloat(1.0f, randomScaling);
            }
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            Vector2 toTarget = targetCenter - projectile.Center;
            if(foundTarget)
            {
                if (toTarget.Y > 0f && Math.Abs(toTarget.X) < 160)
                {
                    fallThrough = true;
                }
                else
                {
                    fallThrough = false;
                }
            }
            else
            {
                fallThrough = false;
            }
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
    }
}
