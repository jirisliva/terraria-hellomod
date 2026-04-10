using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelloMod.Items
{

    public class SunstoneBar : ModItem
    {
        public override void SetDefaults()
        {
            // --- Rozměry a grafika ---
            Item.width = 40;
            Item.height = 40;


            // --- Hodnota a vzácnost ---
            Item.value = Item.buyPrice(gold: 4);
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            // Ingredience

            recipe.AddIngredient(ModContent.ItemType<Sunstone>(), 4);   // 4× Hellstone

            // Pracovní stanice – funguje na Hellforge
            recipe.AddTile(TileID.Hellforge);

            recipe.Register();
        }
    }
}