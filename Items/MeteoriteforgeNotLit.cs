using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelloMod.Items
{

    public class MeteoriteforgeNotLit : ModItem
    {
        public override void SetDefaults()
        {
            // --- Rozměry a grafika ---
            Item.width = 40;
            Item.height = 40;


            // --- Hodnota a vzácnost ---
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ItemRarityID.Orange;

            Item.maxStack = 999; // Set the maximum stack size
        }
        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            // Ingredience
            recipe.AddIngredient(ItemID.Meteorite, 10);     // 15× Meteorite
            recipe.AddIngredient(ItemID.Torch, 4);      // 5x Torch
            recipe.AddIngredient(ItemID.Furnace, 1);   // 10× Hellstone
            recipe.AddIngredient(ModContent.ItemType<Sunstone>(), 4);

            // Pracovní stanice – funguje na Hellforge
            recipe.AddTile(TileID.Anvils);

            recipe.Register();
        }
    }
}