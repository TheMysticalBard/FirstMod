using Terraria.ModLoader;
using Terraria;
using Main = Terraria.Main;
using System;
using Microsoft.Xna.Framework;

namespace FirstMod.Projectiles
{
    class MyProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.0f, 1.0f, Main.rand.NextFloat(0.0f, 1.0f));
            projectile.rotation += 0.2f;
            projectile.velocity *= 1.001f;
            projectile.ai[0] += 0.25f;
            if(projectile.ai[0] >= 30f && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(projectile.position, Vector2.Transform(projectile.velocity, Matrix.CreateRotationZ(-MathHelper.PiOver2/3)), projectile.type, projectile.damage, projectile.knockBack);
                Projectile.NewProjectile(projectile.position, Vector2.Transform(projectile.velocity, Matrix.CreateRotationZ(MathHelper.PiOver2/3)), projectile.type, projectile.damage, projectile.knockBack);
                projectile.ai[0] = 0f;
                projectile.netUpdate = true;
            }
        }
    }
}
