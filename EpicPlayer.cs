using Terraria.ModLoader;

namespace FirstMod
{
    class EpicPlayer : ModPlayer
    {
        public bool epicHero = false;
        public override void ResetEffects()
        {
            epicHero = false;
        }
        public override void PostUpdateRunSpeeds()
        {
            if(epicHero)
            {
                player.maxRunSpeed += 3;
                player.moveSpeed += 0.10f;
            }
        }
    }
}
