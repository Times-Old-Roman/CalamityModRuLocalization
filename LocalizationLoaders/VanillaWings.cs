using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

// Calamity changes to vanilla wings
namespace CalamityModRuLocalization.VanillaWings {
	internal class VanillaWings : GlobalItem {
		public static Dictionary<string, string> Wings = new Dictionary<string, string>();

		public override void SetStaticDefaults() {
			Wings["Angel"] = Mod.GetLocalization("VanillaWings.AngelWings").Value;
			Wings["Demon"] = Mod.GetLocalization("VanillaWings.DemonWings").Value;
			Wings["Fairy"] = Mod.GetLocalization("VanillaWings.FairyWings").Value;
			Wings["Fin"] = Mod.GetLocalization("VanillaWings.FinWings").Value;
			Wings["Butterfly"] = Mod.GetLocalization("VanillaWings.ButterflyWings").Value;
			Wings["Harpy"] = Mod.GetLocalization("VanillaWings.HarpyWings").Value;
			Wings["Flame"] = Mod.GetLocalization("VanillaWings.FlameWings").Value;
			Wings["Frozen"] = Mod.GetLocalization("VanillaWings.FrozenWings").Value;
			Wings["Bat"] = Mod.GetLocalization("VanillaWings.BatWings").Value;
			Wings["Bee"] = Mod.GetLocalization("VanillaWings.BeeWings").Value;
			Wings["Bone"] = Mod.GetLocalization("VanillaWings.BoneWings").Value;
			Wings["Ghost"] = Mod.GetLocalization("VanillaWings.GhostWings").Value;
			Wings["Leaf"] = Mod.GetLocalization("VanillaWings.LeafWings").Value;
			Wings["Hoverboard"] = Mod.GetLocalization("VanillaWings.Hoverboard").Value;
			Wings["Mothron"] = Mod.GetLocalization("VanillaWings.MothronWings").Value;
			Wings["Beetle"] = Mod.GetLocalization("VanillaWings.BeetleWings").Value;
			Wings["Steampunk"] = Mod.GetLocalization("VanillaWings.SteampunkWings").Value;
			Wings["TatteredFairy"] = Mod.GetLocalization("VanillaWings.TatteredFairyWings").Value;
			Wings["Spooky"] = Mod.GetLocalization("VanillaWings.SpookyWings").Value;
			Wings["Festive"] = Mod.GetLocalization("VanillaWings.FestiveWings").Value;
			Wings["Solar"] = Mod.GetLocalization("VanillaWings.WingsSolar").Value;
			Wings["Vortex"] = Mod.GetLocalization("VanillaWings.WingsVortex").Value;
			Wings["Nebula"] = Mod.GetLocalization("VanillaWings.WingsNebula").Value;
			Wings["Stardust"] = Mod.GetLocalization("VanillaWings.WingsStardust").Value;
		}
	}
}
