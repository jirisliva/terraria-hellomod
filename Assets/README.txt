Složka pro textury (obrázky) modu.

Struktura odpovídá struktuře C# tříd:

  Assets/Items/SlunecniMec.png        ← textura pro Items/SlunecniMec.cs
  Assets/Projectiles/OhnivaKoule.png  ← textura pro Projectiles/OhnivaKoule.cs
  Assets/NPCs/PriatelskyCestovatel.png ← textura pro NPCs/PriatelskyCestovatel.cs

POZOR: Cesta k textuře se odvozuje od namespace + název třídy.
Například pro "HelloMod.Items.SlunecniMec" hledá tModLoader:
  Items/SlunecniMec.png

Doporučené rozměry:
  - Itemy (meče, nástroje):  32×32 nebo 40×40 px
  - Projektily:              14×14 nebo 16×16 px
  - NPC:                     18×(40*25) px = spritesheet s 25 snímky animace

Formát: PNG (RGBA, průhledné pozadí)
Editor: Aseprite (doporučeno), GIMP, Paint.NET, Photoshop
