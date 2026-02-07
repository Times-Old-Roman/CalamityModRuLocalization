using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

// Shared by multiple items
namespace CalamityModRuLocalization.VanillaAccs {
	internal class VanillaAccs : GlobalItem {
		public static Dictionary<string, string> Accs = new Dictionary<string, string>();

		public override void SetStaticDefaults() {
			Accs["ObsidianSkull"] = Mod.GetLocalization("VanillaAccs.ObsidianSkull").Value;
			Accs["ObsidianSkullRose"] = Mod.GetLocalization("VanillaAccs.ObsidianSkullRose").Value;
			Accs["MoltenCharm"] = Mod.GetLocalization("VanillaAccs.MoltenCharm").Value;
			Accs["ObsidianHorseshoe"] = Mod.GetLocalization("VanillaAccs.ObsidianHorseshoe").Value;
			Accs["ObsidianShield"] = Mod.GetLocalization("VanillaAccs.ObsidianShield").Value;
			Accs["ObsidianWaterWalkingBoots"] = Mod.GetLocalization("VanillaAccs.ObsidianWaterWalkingBoots").Value;
			Accs["LavaWaders"] = Mod.GetLocalization("VanillaAccs.LavaWaders").Value;
			Accs["LavaSkull"] = Mod.GetLocalization("VanillaAccs.LavaSkull").Value;
			Accs["MoltenSkullRose"] = Mod.GetLocalization("VanillaAccs.MoltenSkullRose").Value;
			Accs["AnkhShield"] = Mod.GetLocalization("VanillaAccs.AnkhShield").Value;
			Accs["ShinyStone"] = Mod.GetLocalization("VanillaAccs.ShinyStone").Value;
			Accs["BandofRegeneration"] = Mod.GetLocalization("VanillaAccs.BandofRegeneration").Value;
			Accs["CharmofMyths"] = Mod.GetLocalization("VanillaAccs.CharmofMyths").Value;
			Accs["FlameWakerBoots"] = Mod.GetLocalization("VanillaAccs.FlameWakerBoots").Value;
			Accs["HellfireTreads"] = Mod.GetLocalization("VanillaAccs.HellfireTreads").Value;
			Accs["FairyBoots"] = Mod.GetLocalization("VanillaAccs.FairyBoots").Value;
			Accs["FrozenTurtleShell"] = Mod.GetLocalization("VanillaAccs.FrozenTurtleShell").Value;
			Accs["FrozenShield"] = Mod.GetLocalization("VanillaAccs.FrozenShield").Value;
			Accs["NeptunesShell"] = Mod.GetLocalization("VanillaAccs.NeptunesShell").Value;
			Accs["MoonShell"] = Mod.GetLocalization("VanillaAccs.MoonShell").Value;
			Accs["MoonStone"] = Mod.GetLocalization("VanillaAccs.MoonStone").Value;
			Accs["SunStone"] = Mod.GetLocalization("VanillaAccs.SunStone").Value;
			Accs["CelestialStone"] = Mod.GetLocalization("VanillaAccs.CelestialStone").Value;
			Accs["CelestialShell"] = Mod.GetLocalization("VanillaAccs.CelestialShell").Value;
			Accs["MagnetFlower"] = Mod.GetLocalization("VanillaAccs.MagnetFlower").Value;
			Accs["ArcaneFlower"] = Mod.GetLocalization("VanillaAccs.ArcaneFlower").Value;
			Accs["ManaCloak"] = Mod.GetLocalization("VanillaAccs.ManaCloak").Value;
			Accs["Magiluminescence.AbyssLightChange"] = Mod.GetLocalization("VanillaAccs.Magiluminescence.AbyssLightChange").Value;
			Accs["Magiluminescence.Other"] = Mod.GetLocalization("VanillaAccs.Magiluminescence.Other").Value;
			Accs["EmpressFlightBooster"] = Mod.GetLocalization("VanillaAccs.EmpressFlightBooster").Value;
			Accs["RifleScope"] = Mod.GetLocalization("VanillaAccs.RifleScope").Value;
			Accs["SniperScope.Visibility"] = Mod.GetLocalization("VanillaAccs.SniperScope.Visibility").Value;
			Accs["SniperScope.Other"] = Mod.GetLocalization("VanillaAccs.SniperScope.Other").Value;
			Accs["ReconScope"] = Mod.GetLocalization("VanillaAccs.ReconScope").Value;
			Accs["MagicQuiver"] = Mod.GetLocalization("VanillaAccs.MagicQuiver").Value;
			Accs["MoltenQuiver.HellfireChange"] = Mod.GetLocalization("VanillaAccs.MoltenQuiver.HellfireChange").Value;
			Accs["FleshKnuckles"] = Mod.GetLocalization("VanillaAccs.FleshKnuckles").Value;
			Accs["HeroShield"] = Mod.GetLocalization("VanillaAccs.HeroShield").Value;
			Accs["MoltenQuiver.Other"] = Mod.GetLocalization("VanillaAccs.MoltenQuiver.Other").Value;
			Accs["TitanGlove"] = Mod.GetLocalization("VanillaAccs.TitanGlove").Value;
			Accs["PowerGlove"] = Mod.GetLocalization("VanillaAccs.PowerGlove").Value;
			Accs["BerserkerGlove.BuffTitanGlove"] = Mod.GetLocalization("VanillaAccs.BerserkerGlove.BuffTitanGlove").Value;
			Accs["BerserkerGlove.BuffFleshKnuckles"] = Mod.GetLocalization("VanillaAccs.BerserkerGlove.BuffFleshKnuckles").Value;
			Accs["MechanicalGlove"] = Mod.GetLocalization("VanillaAccs.MechanicalGlove").Value;
			Accs["FireGauntlet.HellfireChange"] = Mod.GetLocalization("VanillaAccs.FireGauntlet.HellfireChange").Value;
			Accs["FireGauntlet.Other"] = Mod.GetLocalization("VanillaAccs.FireGauntlet.Other").Value;
			Accs["YoYoGlove"] = Mod.GetLocalization("VanillaAccs.YoYoGlove").Value;
			Accs["YoyoBag"] = Mod.GetLocalization("VanillaAccs.YoyoBag").Value;
			Accs["TerrasparkBoots"] = Mod.GetLocalization("VanillaAccs.TerrasparkBoots").Value;
			Accs["HandWarmer"] = Mod.GetLocalization("VanillaAccs.HandWarmer").Value;
			Accs["JellyfishNecklace"] = Mod.GetLocalization("VanillaAccs.JellyfishNecklace").Value;
			Accs["JellyfishDivingGear"] = Mod.GetLocalization("VanillaAccs.JellyfishDivingGear").Value;
			Accs["DivingHelmet"] = Mod.GetLocalization("VanillaAccs.DivingHelmet").Value;
			Accs["ArcticDivingGear"] = Mod.GetLocalization("VanillaAccs.ArcticDivingGear").Value;
		}
	}
}
