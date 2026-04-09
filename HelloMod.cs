using Terraria.ModLoader;

namespace HelloMod
{
    /// <summary>
    /// Hlavní třída modu. tModLoader ji automaticky detekuje
    /// a použije jako vstupní bod.
    ///
    /// Všechny itemy, NPC a projektily ve složkách Items/, NPCs/
    /// a Projectiles/ jsou načteny automaticky – nemusíš je zde
    /// registrovat ručně.
    /// </summary>
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
