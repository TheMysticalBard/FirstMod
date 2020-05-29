using FirstMod.Buffs;
using FirstMod.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace FirstMod.Items
{
    class FirstSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.mana = 10;
            item.damage = 10;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.width = 40;
            item.height = 40;
            item.useTime = 28;
            item.useAnimation = 28;
            item.shootSpeed = 8f;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item44;
            item.buffType = BuffType<CuteMinionBuff>();
            item.shoot = ProjectileType<CuteMinion>();
            item.noMelee = true;
            item.summon = true;
            item.value = 550;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2, true);
            position = Main.MouseWorld;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
