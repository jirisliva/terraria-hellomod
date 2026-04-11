using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace HelloMod.Tiles
{
    public class MeteorforgeTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Rozměry dlaždice – 3 široká × 2 vysoká (jako Hellforge)
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileNoAttach[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.addTile(Type);

            // Pracovní stanice – hráč může craftit, když stojí vedle
            AdjTiles = new int[] { TileID.Furnaces };

            // Mapová barva
            AddMapEntry(new Color(200, 100, 30), CreateMapEntryName());
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
