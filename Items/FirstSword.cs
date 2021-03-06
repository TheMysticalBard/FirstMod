using FirstMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FirstMod.Items
{
	public class FirstSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("FirstSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("This is no longer a basic modded sword, but a tool for murder and light.");
		}

		public override void SetDefaults() 
		{
			item.damage = 500;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<MyProjectile>();
			item.shootSpeed = 3.0f;
		}

        public override void HoldItem(Player player)
        {
            player.statLifeMax2 += 300;
            player.noFallDmg = true;
			player.GetModPlayer<EpicPlayer>().epicHero = true;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}