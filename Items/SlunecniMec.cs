using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelloMod.Items
{
    /// <summary>
    /// Sluneční Meč – melee zbraň středně pokročilé úrovně.
    ///
    /// Vlastnosti:
    ///   - 75 poškození (melee)
    ///   - Zapálí nepřítele na 5 sekund při každém zásahu
    ///   - Střílí ohnivé koule (při dostatku many)
    ///   - Recept: 15× Meteorite + 10× Obsidian + 5× Hellstone Bar
    ///             na Mythril / Orichalcum Anvilě
    /// </summary>
    public class SlunecniMec : ModItem
    {
        // Jméno a tooltip jsou v Localization/en-US.hjson (tModLoader 1.4.4+)

        public override void SetDefaults()
        {
            // --- Rozměry a grafika ---
            Item.width = 40;
            Item.height = 40;

            // --- Bojové vlastnosti ---
            Item.damage = 75;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6f;
            Item.crit = 8;              // +8 % kritický úder (přičte se k základu hráče)

            // --- Použití ---
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            // --- Projektil (střelba ohnivých koulí) ---
            Item.shoot = ModContent.ProjectileType<Projectiles.OhnivaKoule>();
            Item.shootSpeed = 10f;
            Item.mana = 8;              // cena many za výstřel

            // --- Hodnota a vzácnost ---
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Orange;
        }

        /// <summary>
        /// Voláno při každém zásahu NPC tímto předmětem.
        /// </summary>
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Zapálíme NPC na 5 sekund (300 tiků při 60 FPS)
            target.AddBuff(BuffID.OnFire, 300);
        }

        /// <summary>
        /// Definice receptu pro výrobu tohoto předmětu.
        /// </summary>
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            // Ingredience
            recipe.AddIngredient(ItemID.Meteorite, 15);     // 15× Meteorite
            recipe.AddIngredient(ItemID.Obsidian, 10);      // 10× Obsidian
            recipe.AddIngredient(ItemID.HellstoneBar, 5);   // 5× Hellstone Bar

            // Pracovní stanice – funguje na Mythril i Orichalcum kovadlině
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();
        }
    }
}