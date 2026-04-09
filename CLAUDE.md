# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Building and running

tModLoader mods are built and loaded from within Terraria itself — there is no standalone CLI build command. To build:

1. Launch Terraria with tModLoader installed.
2. Go to **Workshop → Develop Mods → HelloMod → Build**.
3. To test in-game, click **Build + Reload** instead.

The mod source folder is `~/Library/Application Support/Terraria/tModLoader/ModSources/HelloMod/`. Built `.tmod` files land in `~/Library/Application Support/Terraria/tModLoader/Mods/`.

There are no unit tests — all testing is done in-game.

## Architecture

This is a tModLoader 1.4.4+ mod targeting Terraria. tModLoader auto-discovers all classes in `Items/`, `NPCs/`, and `Projectiles/` — nothing needs to be manually registered.

**Entry point:** `HelloMod.cs` — extends `Mod`, provides `Load()` and `Unload()` hooks for global setup.

**Content classes:**
- `Items/SlunecniMec.cs` — melee sword (`ModItem`) that shoots a fireball projectile on swing (costs 8 mana), applies OnFire on hit, and has a crafting recipe.
- `Projectiles/OhnivaKoule.cs` — magic fireball (`ModProjectile`) shot by SlunecniMec; uses Arrow AI style, penetrates 3 enemies, applies OnFire, emits dust trail, explodes on death.
- `NPCs/PriatelskyCestovatel.cs` — friendly passive NPC (`ModNPC`) that spawns on the surface during daytime (0.5% chance), wanders like a Bunny, and has random Czech dialogue on click.

**Localization:** All display names and tooltips are in `Localization/en-US.hjson` (tModLoader 1.4.4+ pattern — no `SetStaticDefaults` name strings needed).

**Assets:** Each content class has a matching `.png` sprite in the same folder with the same name (e.g., `Items/SlunecniMec.png`). tModLoader loads textures automatically by convention.

## Conventions

- Comments and in-game dialogue are written in Czech.
- Namespace root: `HelloMod`; sub-namespaces: `HelloMod.Items`, `HelloMod.NPCs`, `HelloMod.Projectiles`.
- Timing: 60 ticks = 1 second (used for buff durations like `300` = 5 s).
