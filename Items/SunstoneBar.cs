using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HelloMod.Tiles;

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

            Item.maxStack = 99; // Set the maximum stack size
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();            // Ingredience
            recipe.AddIngredient(ModContent.ItemType<Sunstone>(), 4);   // 4× Hellstone

            // Pracovní stanice – funguje na Hellforge
            recipe.AddTile(ModContent.TileType<MeteorforgeTile>());

            recipe.Register();
        }
    }
}