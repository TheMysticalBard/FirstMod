using FirstMod.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace FirstMod.Buffs
{
    class CuteMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Rolly Boi!");
            Description.SetDefault("This boi will bowl over the competition!");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if(player.ownedProjectileCounts[ProjectileType<CuteMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
