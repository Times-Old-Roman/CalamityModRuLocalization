using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModRuLocalization.VanillaArmor {
	internal class VanillaArmor : GlobalItem {
		public static Dictionary<string, string> Armor = new Dictionary<string, string>();

		public override void SetStaticDefaults() {
			Armor["MagicHat"] = Mod.GetLocalization("VanillaArmor.MagicHat").Value;
			Armor["Gi"] = Mod.GetLocalization("VanillaArmor.Gi").Value;
			Armor["CopperHelmet"] = Mod.GetLocalization("VanillaArmor.CopperHelmet").Value;
			Armor["CopperChainmail"] = Mod.GetLocalization("VanillaArmor.CopperChainmail").Value;
			Armor["CopperGreaves"] = Mod.GetLocalization("VanillaArmor.CopperGreaves").Value;
			Armor["TinHelmet"] = Mod.GetLocalization("VanillaArmor.TinHelmet").Value;
			Armor["TinChainmail"] = Mod.GetLocalization("VanillaArmor.TinChainmail").Value;
			Armor["TinGreaves"] = Mod.GetLocalization("VanillaArmor.TinGreaves").Value;
			Armor["IronHelmet"] = Mod.GetLocalization("VanillaArmor.IronHelmet").Value;
			Armor["AncientIronHelmet"] = Mod.GetLocalization("VanillaArmor.AncientIronHelmet").Value;
			Armor["IronChainmail"] = Mod.GetLocalization("VanillaArmor.IronChainmail").Value;
			Armor["IronGreaves"] = Mod.GetLocalization("VanillaArmor.IronGreaves").Value;
			Armor["LeadHelmet"] = Mod.GetLocalization("VanillaArmor.LeadHelmet").Value;
			Armor["LeadChainmail"] = Mod.GetLocalization("VanillaArmor.LeadChainmail").Value;
			Armor["LeadGreaves"] = Mod.GetLocalization("VanillaArmor.LeadGreaves").Value;
			Armor["SilverHelmet"] = Mod.GetLocalization("VanillaArmor.SilverHelmet").Value;
			Armor["SilverChainmail"] = Mod.GetLocalization("VanillaArmor.SilverChainmail").Value;
			Armor["SilverGreaves"] = Mod.GetLocalization("VanillaArmor.SilverGreaves").Value;
			Armor["TungstenHelmet"] = Mod.GetLocalization("VanillaArmor.TungstenHelmet").Value;
			Armor["TungstenChainmail"] = Mod.GetLocalization("VanillaArmor.TungstenChainmail").Value;
			Armor["TungstenGreaves"] = Mod.GetLocalization("VanillaArmor.TungstenGreaves").Value;
			Armor["GoldHelmet"] = Mod.GetLocalization("VanillaArmor.GoldHelmet").Value;
			Armor["AncientGoldHelmet"] = Mod.GetLocalization("VanillaArmor.AncientGoldHelmet").Value;
			Armor["GoldChainmail"] = Mod.GetLocalization("VanillaArmor.GoldChainmail").Value;
			Armor["GoldGreaves"] = Mod.GetLocalization("VanillaArmor.GoldGreaves").Value;
			Armor["PlatinumHelmet"] = Mod.GetLocalization("VanillaArmor.PlatinumHelmet").Value;
			Armor["PlatinumChainmail"] = Mod.GetLocalization("VanillaArmor.PlatinumChainmail").Value;
			Armor["PlatinumGreaves"] = Mod.GetLocalization("VanillaArmor.PlatinumGreaves").Value;
			Armor["CrimsonHelmet"] = Mod.GetLocalization("VanillaArmor.CrimsonHelmet").Value;
			Armor["CrimsonScalemail"] = Mod.GetLocalization("VanillaArmor.CrimsonScalemail").Value;
			Armor["CrimsonGreaves"] = Mod.GetLocalization("VanillaArmor.CrimsonGreaves").Value;
			Armor["CobaltHat"] = Mod.GetLocalization("VanillaArmor.CobaltHat").Value;
			Armor["PalladiumBreastplate"] = Mod.GetLocalization("VanillaArmor.PalladiumBreastplate").Value;
			Armor["PalladiumLeggings"] = Mod.GetLocalization("VanillaArmor.PalladiumLeggings").Value;
			Armor["MythrilHood"] = Mod.GetLocalization("VanillaArmor.MythrilHood").Value;
			Armor["OrichalcumBreastplate"] = Mod.GetLocalization("VanillaArmor.OrichalcumBreastplate").Value;
			Armor["AdamantiteHeadgear"] = Mod.GetLocalization("VanillaArmor.AdamantiteHeadgear").Value;
			Armor["SquireGreatHelm"] = Mod.GetLocalization("VanillaArmor.SquireGreatHelm").Value;
			Armor["SquirePlating"] = Mod.GetLocalization("VanillaArmor.SquirePlating").Value;
			Armor["SquireGreaves"] = Mod.GetLocalization("VanillaArmor.SquireGreaves").Value;
			Armor["MonkBrows"] = Mod.GetLocalization("VanillaArmor.MonkBrows").Value;
			Armor["MonkShirt"] = Mod.GetLocalization("VanillaArmor.MonkShirt").Value;
			Armor["MonkPants"] = Mod.GetLocalization("VanillaArmor.MonkPants").Value;
			Armor["HuntressJerkin"] = Mod.GetLocalization("VanillaArmor.HuntressJerkin").Value;
			Armor["ApprenticeTrousers"] = Mod.GetLocalization("VanillaArmor.ApprenticeTrousers").Value;
			Armor["SquireAltShirt"] = Mod.GetLocalization("VanillaArmor.SquireAltShirt").Value;
			Armor["SquireAltPants"] = Mod.GetLocalization("VanillaArmor.SquireAltPants").Value;
			Armor["MonkAltHead"] = Mod.GetLocalization("VanillaArmor.MonkAltHead").Value;
			Armor["MonkAltShirt"] = Mod.GetLocalization("VanillaArmor.MonkAltShirt").Value;
			Armor["MonkAltPants"] = Mod.GetLocalization("VanillaArmor.MonkAltPants").Value;
			Armor["HuntressAltShirt"] = Mod.GetLocalization("VanillaArmor.HuntressAltShirt").Value;
			Armor["ApprenticeAltPants"] = Mod.GetLocalization("VanillaArmor.ApprenticeAltPants").Value;
			Armor["ApprenticeAltPants"] = Mod.GetLocalization("VanillaArmor.ApprenticeAltPants").Value;
			Armor["ForbiddenArmorBonus"] = Mod.GetLocalization("VanillaArmor.ForbiddenArmorBonus").Value;
			Armor["GladiatorHelmet"] = Mod.GetLocalization("VanillaArmor.GladiatorHelmet").Value;
			Armor["GladiatorBreastplate"] = Mod.GetLocalization("VanillaArmor.GladiatorBreastplate").Value;
			Armor["GladiatorLeggings"] = Mod.GetLocalization("VanillaArmor.GladiatorLeggings").Value;
			Armor["SolarFlareHelmet"] = Mod.GetLocalization("VanillaArmor.SolarFlareHelmet").Value;
			Armor["SolarFlareBreastplate"] = Mod.GetLocalization("VanillaArmor.SolarFlareBreastplate").Value;
			Armor["SolarFlareLeggings"] = Mod.GetLocalization("VanillaArmor.SolarFlareLeggings").Value;
			Armor["MiningHelmet"] = Mod.GetLocalization("VanillaArmor.MiningHelmet").Value;
			Armor["UltrabrightHelmet"] = Mod.GetLocalization("VanillaArmor.UltrabrightHelmet").Value;
		}
	}
}
