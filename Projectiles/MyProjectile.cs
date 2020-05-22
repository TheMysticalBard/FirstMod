using Terraria.ModLoader;
using Terraria;
using Main = Terraria.Main;
using System;

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
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.0f, 1.0f, Main.rand.NextFloat(0.0f, 1.0f));
            projectile.ai[0] += 1f;
            if(projectile.ai[0] >= 16f && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(projectile.position, projectile.velocity.RotatedBy(-30 / 90 * Math.PI), projectile.type, projectile.damage, projectile.knockBack);
                Projectile.NewProjectile(projectile.position, projectile.velocity.RotatedBy(30 / 90 * Math.PI), projectile.type, projectile.damage, projectile.knockBack);
            }
        }
    }
}
