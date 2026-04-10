using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelloMod.Projectiles
{
    /// <summary>
    /// Ohnivá Koule – magický projektil vystřelený Slunečním Mečem.
    ///
    /// Chování:
    ///   - Letí vpřed jako šíp (aiStyle = Arrow)
    ///   - Prostřelí až 3 nepřátele
    ///   - Vydává oranžové světlo
    ///   - Zapálí zásažené NPC na 3 sekundy
    ///   - Při zániku vytvoří efekt jiskřiček (dust)
    /// </summary>
    public class OhnivaKoule : ModProjectile
    {
        // Jméno je v Localization/en-US.hjson (tModLoader 1.4.4+)

        public override void SetDefaults()
        {
            // --- Hitbox ---
            Projectile.width = 14;
            Projectile.height = 14;

            // --- Animace (4 snímky ve spritesheetu) ---
            Main.projFrames[Projectile.type] = 4;

            // --- AI ---
            // ProjAIStyleID.Arrow = rovný let, gravity = false
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            AIType = ProjectileID.Fireball; // chování identické s Fireballem

            // --- Kolize ---
            Projectile.friendly = true;   // poškozuje nepřátele, ne hráče
            Projectile.hostile = false;
            Projectile.penetrate = 3;     // prostřelí až 3 nepřátele

            // --- Typ poškození ---
            Projectile.DamageType = DamageClass.Magic;

            // --- Životnost ---
            Projectile.timeLeft = 300;    // zanikne po 5 sekundách

            // --- Vizuál ---
            Projectile.light = 0.9f;      // vydává oranžové světlo
            Projectile.alpha = 20;        // mírná průhlednost
        }

        /// <summary>
        /// Voláno každý tik – přidáváme smoke/fire dust efekt.
        /// </summary>
        public override void AI()
        {
            // Animace – přepínáme snímky každých 5 tiků
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            }

            // Každý tik přidáme malou jiskřičku za projektilem (trail efekt)
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

        /// <summary>
        /// Voláno při zásahu NPC.
        /// </summary>
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Zapálíme NPC na 3 sekundy (180 tiků)
            target.AddBuff(BuffID.OnFire, 180);
        }

        /// <summary>
        /// Voláno při zániku projektilu (zásah zdi, expirování, atd.).
        /// </summary>
        public override void Kill(int timeLeft)
        {
            // Efekt exploze – 25 jiskřiček
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