using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HelloMod.Tiles;

namespace HelloMod.Items
{
    public class Meteoriteforge : ModItem
    {
        public override void SetDefaults()
        {
            // --- Rozměry a grafika ---
            Item.width = 40;
            Item.height = 40;

            // --- Hodnota a vzácnost ---
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ItemRarityID.Orange;

            Item.maxStack = 999;

            // Položení dlaždice – propojí item s tile
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<MeteorforgeTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            // Ingredience
            recipe.AddIngredient(ModContent.ItemType<MeteoriteforgeNotLit>(), 1);
            recipe.AddIngredient(ItemID.LavaBucket, 1);

            // Pracovní stanice – funguje na WorkBench
            recipe.AddTile(TileID.WorkBenches);

            recipe.Register();
        }
    }
}
