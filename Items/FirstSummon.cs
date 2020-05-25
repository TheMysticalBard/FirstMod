using FirstMod.Projectiles.Minions;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace FirstMod.Items
{
    class FirstSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.buffType = BuffType<CuteMinionBuff>();
            item.shoot = ProjectileType<CuteMinion>();
        }
    }
}
