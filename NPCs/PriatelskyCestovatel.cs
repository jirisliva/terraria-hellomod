using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelloMod.NPCs
{
    /// <summary>
    /// Přátelský Cestovatel – pasivní NPC spawnující se na povrchu.
    ///
    /// Chování:
    ///   - Chodí sem a tam (pasivní AI)
    ///   - Nezaútočí na hráče
    ///   - Má 250 HP
    ///   - Spawne se vzácně na povrchu ve dne
    /// </summary>
    public class PriatelskyCestovatel : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // Jméno je v Localization/en-US.hjson (tModLoader 1.4.4+)

            // Počet snímků v spritesheet animaci (výchozí Terraria NPC mají 25)
            Main.npcFrameCount[Type] = 25;

            // Přiřadíme NPC do kategorie "přátelských" pro bestiář
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            // --- Rozměry ---
            NPC.width = 18;
            NPC.height = 40;

            // --- Bojové vlastnosti ---
            NPC.friendly = true;          // nezaútočí na hráče
            NPC.lifeMax = 250;
            NPC.defense = 10;
            NPC.damage = 0;               // žádné poškození
            NPC.knockBackResist = 0.5f;

            // --- Zvuky ---
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            // --- Pohyb ---
            NPC.aiStyle = NPCAIStyleID.Passive;   // pasivní AI – náhodné chůze
            AIType = NPCID.Bunny;                 // stejná AI jako králík

            // --- Animace ---
            AnimationType = NPCID.Guide;          // animace jako Guide NPC
        }

        /// <summary>
        /// Určuje šanci na spawn. Zavolá se každý pokus o spawn NPC.
        /// </summary>
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Spawne se pouze na povrchu ve dne s malou pravděpodobností
            bool isOverworld = spawnInfo.Player.ZoneOverworldHeight;
            bool isDay = Main.dayTime;
            bool notRaining = !Main.raining;

            if (isOverworld && isDay && notRaining)
                return 0.005f; // 0.5% šance na každý pokus o spawn

            return 0f;
        }

        /// <summary>
        /// Přidá NPC do bestiáře (encyklopedie nepřátel).
        /// </summary>
        public override void SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase database,
                                         Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new Terraria.GameContent.Bestiary.IBestiaryInfoElement[]
            {
                // Zobrazí se v bestiáři jako "vyskytuje se na povrchu ve dne"
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            });
        }

        /// <summary>
        /// Volá se, když NPC zavolá hráče / promluví (po kliknutí).
        /// Lze použít pro obchod, dialog, questy, atd.
        /// </summary>
        public override string GetChat()
        {
            string[] dialogy =
            {
                "Zdravím tě, cestovateli!",
                "Krásný den na dobrodružství, viď?",
                "Dávej si pozor v jeskyních...",
                "Slyšel jsem, že za lesem je dungeon.",
            };

            // Vrátí náhodný dialog
            return Main.rand.Next(dialogy);
        }
    }
}