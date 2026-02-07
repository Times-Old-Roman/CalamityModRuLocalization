using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModRuLocalization.Misc {
	internal class Misc : GlobalItem {
		public static Dictionary<string, string> MiscStrings = new Dictionary<string, string>();
		public static Dictionary<string, string> MiscTools = new Dictionary<string, string>();
		public static Dictionary<string, string> MiscPlaceables = new Dictionary<string, string>();

		public override void SetStaticDefaults() {
			MiscStrings["OrRev"] = Mod.GetLocalization("Misc.OrRev").Value;
			MiscStrings["DevItem"] = Mod.GetLocalization("Misc.DevItem").Value;
			MiscStrings["DonorItem"] = Mod.GetLocalization("Misc.DonorItem").Value;
			MiscStrings["PointBlankShot"] = Mod.GetLocalization("Misc.PointBlankShot").Value;
			MiscStrings["DraedonCharge"] = Mod.GetLocalization("Misc.DraedonCharge").Value;
			MiscStrings["DamageReductionPercent"] = Mod.GetLocalization("Misc.DamageReductionPercent").Value;
			MiscStrings["LuckValue"] = Mod.GetLocalization("Misc.LuckValue").Value;

			MiscTools["RodofDiscord"] = Mod.GetLocalization("Misc.RodofDiscord").Value;
			MiscTools["GoldenFishingRod"] = Mod.GetLocalization("Misc.GoldenFishingRod").Value;
			MiscTools["Picksaw"] = Mod.GetLocalization("Misc.Picksaw").Value;

			MiscPlaceables["HeartLantern"] = Mod.GetLocalization("Misc.HeartLantern").Value;
		}
	}
}
