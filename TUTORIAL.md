# Terraria Modding Tutorial (tModLoader)

Tento tutoriál tě provede tvorbou modů pro hru **Terraria** pomocí frameworku **tModLoader**.

---

## Obsah

1. [Co je tModLoader?](#1-co-je-tmodloader)
2. [Instalace a nastavení](#2-instalace-a-nastavení)
3. [Struktura modu](#3-struktura-modu)
4. [Tvůj první mod – HelloMod](#4-tvůj-první-mod--hellomod)
5. [Přidání vlastního předmětu (itemu)](#5-přidání-vlastního-předmětu-itemu)
6. [Přidání vlastního receptu](#6-přidání-vlastního-receptu)
7. [Přidání vlastního projektilu](#7-přidání-vlastního-projektilu)
8. [Přidání vlastního NPC](#8-přidání-vlastního-npc)
9. [Testování a ladění](#9-testování-a-ladění)
10. [Kam dál?](#10-kam-dál)

---

## 1. Co je tModLoader?

**tModLoader** je oficiální modding API pro Terraria, dostupné zdarma přes Steam jako DLC.
Mody se píší v jazyce **C#** a tModLoader se stará o načítání, kompilaci a správu modů.

- Terraria 1.4.x → tModLoader 2022+
- Dokumentace: https://docs.tmodloader.net/
- GitHub: https://github.com/tModLoader/tModLoader

---

## 2. Instalace a nastavení

### 2.1 Instalace tModLoader

1. Otevři Steam → Knihovna → Terraria → DLC
2. Nainstaluj **tModLoader** (je zdarma)
3. Spusť tModLoader místo klasické Terrarie

### 2.2 IDE – vývojové prostředí

Doporučujeme **Visual Studio** (Windows) nebo **Visual Studio Code** s rozšířením pro C#.

```bash
# Nainstaluj .NET SDK (pokud nemáš)
# https://dotnet.microsoft.com/download
```

### 2.3 Nastavení mod source složky

Mody se vyvíjejí v:
```
Windows: %UserProfile%\Documents\My Games\Terraria\tModLoader\ModSources\
Linux:   ~/.local/share/Terraria/tModLoader/ModSources/
Mac:     ~/Library/Application Support/Terraria/tModLoader/ModSources/
```

Každý mod je vlastní složka uvnitř `ModSources/`.

---

## 3. Struktura modu

Typická struktura složky modu vypadá takto:

```
MůjMod/
│
├── build.txt          ← metadata modu (autor, verze, závislosti)
├── description.txt    ← popis zobrazený v menu modů
├── MůjMod.cs          ← hlavní třída modu (nepovinná, ale doporučená)
│
├── Items/             ← vlastní předměty
│   └── MůjMec.cs
│
├── NPCs/              ← vlastní NPC
│   └── MůjNPC.cs
│
├── Projectiles/       ← vlastní projektily
│   └── MůjProjetil.cs
│
├── Tiles/             ← vlastní dlaždice/bloky
│   └── MůjBlok.cs
│
└── Assets/            ← obrázky (textures), zvuky
    └── Items/
        └── MůjMec.png
```

---

## 4. Tvůj první mod – HelloMod

### 4.1 Vytvoř složku modu

V `ModSources/` vytvoř složku `HelloMod`.

### 4.2 Soubor `build.txt`

```
author = TvéJméno
version = 0.1
displayName = Hello Mod
```

### 4.3 Soubor `description.txt`

```
Můj první mod pro Terraria!
Přidává nový meč a recept.
```

### 4.4 Hlavní třída modu `HelloMod.cs`

```csharp
using Terraria.ModLoader;

namespace HelloMod
{
    public class HelloMod : Mod
    {
        // Load() se zavolá při načítání modu.
        // Zde lze přidávat globální hooky, texturové sady, atd.
        public override void Load()
        {
            // Pro náš jednoduchý mod nic nepotřebujeme.
        }

        // Unload() se zavolá při vypnutí / deaktivaci modu.
        // Uvolni zde vše, co jsi alokoval v Load().
        public override void Unload()
        {
        }
    }
}
```

---

## 5. Přidání vlastního předmětu (itemu)

### Soubor `Items/SlunecniMec.cs`

```csharp
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelloMod.Items
{
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
            Item.useStyle = ItemUseStyleID.Swing; // animace – mávnutí
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

        // OnHitNPC – volá se při zásahu NPC
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Zapálíme NPC na 5 sekund (300 tiků = 5s při 60 FPS)
            target.AddBuff(BuffID.OnFire, 300);
        }
    }
}
```

### Textura předmětu

Každý předmět potřebuje obrázek. Vytvoř soubor:
```
Items/SlunecniMec.png
```
Rozměr: libovolný, doporučeno **32×32** nebo **40×40** pixelů (PNG formát).

> **Tip:** Pro testování bez textury můžeš použít placeholder – tModLoader zobrazí
> výchozí obrázek a v logu vypíše varování.

### Lokalizace – jméno a tooltip

V tModLoader 1.4.4+ se jména předmětů a tooltipy **nedefinují v kódu** přes `SetStaticDefaults()`,
ale v souboru `Localization/en-US.hjson`:

```hjson
Mods: {
    HelloMod: {
        Items: {
            SlunecniMec: {
                DisplayName: Sluneční Meč
                Tooltip:
                    '''
                    Meč ukovaný ze síly slunce
                    Zapaluje nepřátele při zásahu
                    [c/FF4500:Výstřelí ohnivou kouli při dostatku many]
                    '''
            }
        }

        Projectiles.OhnivaKoule.DisplayName: Ohnivá Koule
        NPCs.PriatelskyCestovatel.DisplayName: Přátelský Cestovatel
    }
}
```

tModLoader soubor načte automaticky – stačí ho mít ve složce `Localization/`.

---

## 6. Přidání vlastního receptu

Recepty lze přidávat přímo v třídě itemu, nebo v samostatném souboru.

### V souboru itemu – metoda `AddRecipes()`

```csharp
// Tuto metodu přidej do třídy SlunecniMec
public override void AddRecipes()
{
    Recipe recipe = CreateRecipe();

    // Ingredience
    recipe.AddIngredient(ItemID.Meteorite, 15);     // 15× Meteorite
    recipe.AddIngredient(ItemID.Obsidian, 10);      // 10× Obsidian
    recipe.AddIngredient(ItemID.HellstoneBar, 5);   // 5× Hellstone Bar

    // Pracovní stanice
    recipe.AddTile(TileID.MythrilAnvil); // Mythril / Orichalcum Anvil

    // Zaregistruj recept
    recipe.Register();
}
```

---

## 7. Přidání vlastního projektilu

```csharp
// Projectiles/OhnivaKoule.cs
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelloMod.Projectiles
{
    public class OhnivaKoule : ModProjectile
    {
        // Jméno je v Localization/en-US.hjson (tModLoader 1.4.4+)

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = ProjAIStyleID.Arrow; // rovný let jako šíp
            AIType = ProjectileID.Fireball;           // chování identické s Fireballem
            Projectile.friendly = true;               // nezraňuje hráče
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 300;                // životnost (tiky)
            Projectile.penetrate = 3;                 // prostřelí 3 nepřátele
            Projectile.light = 0.5f;                  // vydává světlo
            Projectile.alpha = 20;                    // mírná průhlednost
        }

        // AI – volá se každý tik; přidáváme trail efekt
        public override void AI()
        {
            if (Main.rand.NextBool(3)) // pravděpodobnost 1/3 každý tik
            {
                Dust.NewDust(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Torch,
                    Scale: 1.2f
                );
            }
        }

        // OnHitNPC – efekt při zásahu
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180); // zapálí na 3 sekundy
        }

        // Kill – efekt při zničení projektilu
        public override void Kill(int timeLeft)
        {
            // Vytvoří efekt exploze (dust particles)
            for (int i = 0; i < 25; i++)
            {
                Dust.NewDust(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Torch,
                    Scale: Main.rand.NextFloat(1f, 2f)
                );
            }

            // Zvuk exploze
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
        }
    }
}
```

### Použití projektilu v itemu

V `SetDefaults()` itemu přidej:
```csharp
Item.shoot = ModContent.ProjectileType<Projectiles.OhnivaKoule>();
Item.shootSpeed = 12f;
```

---

## 8. Přidání vlastního NPC

```csharp
// NPCs/PriatelskyCestovatel.cs
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelloMod.NPCs
{
    public class PriatelskyCestovatel : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // Jméno je v Localization/en-US.hjson (tModLoader 1.4.4+)

            // Počet snímků v spritesheet animaci
            Main.npcFrameCount[Type] = 25;

            // Nastavení zobrazení v bestiáři
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.friendly = true;          // nezaútočí na hráče
            NPC.lifeMax = 250;
            NPC.defense = 10;
            NPC.damage = 0;               // žádné poškození
            NPC.knockBackResist = 0.5f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.aiStyle = NPCAIStyleID.Passive;   // pasivní AI – náhodná chůze
            AIType = NPCID.Bunny;                 // stejná AI jako králík
            AnimationType = NPCID.Guide;          // animace jako Guide NPC
        }

        // Přidá NPC do bestiáře
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            });
        }

        // Spawne se na povrchu ve dne za jasného počasí
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool isOverworld = spawnInfo.Player.ZoneOverworldHeight;
            bool isDay = Main.dayTime;
            bool notRaining = !Main.raining;

            if (isOverworld && isDay && notRaining)
                return 0.005f; // 0.5% šance na každý pokus o spawn

            return 0f;
        }

        // Náhodný dialog při kliknutí na NPC
        public override string GetChat()
        {
            string[] dialogy =
            {
                "Zdravím tě, cestovateli!",
                "Krásný den na dobrodružství, viď?",
                "Dávej si pozor v jeskyních...",
                "Slyšel jsem, že za lesem je dungeon.",
            };

            return Main.rand.Next(dialogy);
        }
    }
}
```

---

## 9. Testování a ladění

### Sestavení a načtení modu v tModLoader

1. Spusť tModLoader
2. Hlavní menu → **Workshop** → **Develop Mods**
3. Klikni na svůj mod → **Build + Reload**
4. tModLoader zkompiluje C# kód – chyby se zobrazí v logu

### Log soubor

```
Windows: %UserProfile%\Documents\My Games\Terraria\tModLoader\Logs\client.log
Linux:   ~/.local/share/Terraria/tModLoader/Logs/client.log
```

### Užitečné příkazy v herní konzoli (F2 nebo ~)

```
/give <item>        – přidá předmět do inventáře
/spawnmob <npc>     – spawne NPC
/item <jméno>       – alternativa pro give
```

### Časté chyby

| Chyba | Řešení |
|-------|--------|
| `NullReferenceException` v `SetDefaults` | Zkontroluj překlepy v názvech polí |
| Textura nenalezena | Zkontroluj, že `.png` je ve správné složce se správným názvem |
| Recept se nezobrazuje | Ověř, zda je `recipe.Register()` zavoláno |
| Mod se nenačte | Podívej se do `client.log` na chybovou hlášku |

---

## 10. Kam dál?

- **Dokumentace tModLoader**: https://docs.tmodloader.net/
- **ExampleMod** (oficální příklady): https://github.com/tModLoader/tModLoader/tree/1.4.4/ExampleMod
- **tModLoader Wiki**: https://github.com/tModLoader/tModLoader/wiki
- **Komunita na Discordu**: https://discord.gg/tmodloader
- **Terraria Community Forums**: https://forums.terraria.org/index.php?forums/general-mod-discussion.94/

### Doporučený postup učení

1. Prostuduj `ExampleMod` v oficiálním tModLoader repozitáři
2. Začni s jednoduchými itemy a recepty
3. Postupně přidávej projektily, NPC, biomy
4. Sleduj změny mezi verzemi tModLoader v changelogy

---

> Hotový příklad modu najdeš ve složce `ExampleMod/` vedle tohoto tutoriálu.
