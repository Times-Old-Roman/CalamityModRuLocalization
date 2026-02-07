using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

// Shared by multiple items
namespace CalamityModRuLocalization.CommonTooltips {
	internal class CommonTooltips : GlobalItem {
		public static Dictionary<string, string> Common = new Dictionary<string, string>();

		public override void SetStaticDefaults() {
			Common["ZenithRogue"] = Mod.GetLocalization("CommonTooltips.ZenithRogue").Value;
			Common["Zenithrogue"] = Mod.GetLocalization("CommonTooltips.Zenithrogue").Value;

			Common["SlimeFireBuff"] = Mod.GetLocalization("CommonTooltips.SlimeFireBuff").Value;
			Common["AltarBreaker"] = Mod.GetLocalization("CommonTooltips.AltarBreaker").Value;
			Common["GoldTierPickaxe"] = Mod.GetLocalization("CommonTooltips.GoldTierPickaxe").Value;
			Common["LunarTierPickaxeDrill"] = Mod.GetLocalization("CommonTooltips.LunarTierPickaxeDrill").Value;

			Common["Campfire"] = Mod.GetLocalization("CommonTooltips.Campfire").Value;
			Common["Tombstone"] = Mod.GetLocalization("CommonTooltips.Tombstone").Value;

			Common["AbyssLightSmall"] = Mod.GetLocalization("CommonTooltips.AbyssLightSmall").Value;
			Common["AbyssLightMedium"] = Mod.GetLocalization("CommonTooltips.AbyssLightMedium").Value;
			Common["AbyssLightHigh"] = Mod.GetLocalization("CommonTooltips.AbyssLightHigh").Value;

			Common["AbyssBreathMedium"] = Mod.GetLocalization("CommonTooltips.AbyssBreathMedium").Value;
			Common["AbyssBreathHigh"] = Mod.GetLocalization("CommonTooltips.AbyssBreathHigh").Value;

			Common["FleshKnucklesHP"] = Mod.GetLocalization("CommonTooltips.FleshKnucklesHP").Value;
			Common["PutsShell"] = Mod.GetLocalization("CommonTooltips.PutsShell").Value;
			Common["ZoomVisibility"] = Mod.GetLocalization("CommonTooltips.ZoomVisibility").Value;
			Common["TitanGloveEffect"] = Mod.GetLocalization("CommonTooltips.TitanGloveEffect").Value;
			Common["YoyoAdd"] = Mod.GetLocalization("CommonTooltips.YoyoAdd").Value;
			Common["IronArmor"] = Mod.GetLocalization("CommonTooltips.IronArmor").Value;
			Common["LeadArmor"] = Mod.GetLocalization("CommonTooltips.LeadArmor").Value;
			Common["GoldHelmet"] = Mod.GetLocalization("CommonTooltips.GoldHelmet").Value;
			Common["NotConsumable"] = Mod.GetLocalization("CommonTooltips.NotConsumable").Value;

			Common["ImmunityToFireBlocksChange"] = Mod.GetLocalization("CommonTooltips.ImmunityToFireBlocksChange").Value;
			Common["FlasksMeleeSummonRogue"] = Mod.GetLocalization("CommonTooltips.FlasksMeleeSummonRogue").Value;

			Common["AlcoholVanilla"] = Mod.GetLocalization("CommonTooltips.AlcoholVanilla").Value;

			Common["Cobalt"] = Mod.GetLocalization("CommonTooltips.CobaltWeapons").Value;
			Common["Palladium"] = Mod.GetLocalization("CommonTooltips.PalladiumWeapons").Value;
			Common["Mythril"] = Mod.GetLocalization("CommonTooltips.MythrilWeapons").Value;
			Common["Orichalcum"] = Mod.GetLocalization("CommonTooltips.OrichalcumWeapons").Value;
			Common["Adamantite"] = Mod.GetLocalization("CommonTooltips.AdamantiteWeapons").Value;
			Common["Titanium"] = Mod.GetLocalization("CommonTooltips.TitaniumWeapons").Value;
			Common["Hallowed"] = Mod.GetLocalization("CommonTooltips.HallowedWeapons").Value;
			Common["ChristmasWeapons"] = Mod.GetLocalization("CommonTooltips.ChristmasWeapons").Value;

			Common["TotalDefenseIgnore"] = Mod.GetLocalization("CommonTooltips.TotalDefenseIgnore").Value;
			Common["HalfDefenseIgnore"] = Mod.GetLocalization("CommonTooltips.HalfDefenseIgnore").Value;

			Common["WingsVertSpeedbad"] = Mod.GetLocalization("CommonTooltips.WingsVertSpeedbad").Value;
			Common["WingsVertSpeedaverage"] = Mod.GetLocalization("CommonTooltips.WingsVertSpeedaverage").Value;
			Common["WingsVertSpeedgood"] = Mod.GetLocalization("CommonTooltips.WingsVertSpeedgood").Value;
			Common["WingsVertSpeedgreat"] = Mod.GetLocalization("CommonTooltips.WingsVertSpeedgreat").Value;
			Common["WingsVertSpeedexcellent"] = Mod.GetLocalization("CommonTooltips.WingsVertSpeedexcellent").Value;

			Common["WingsStats"] = Mod.GetLocalization("CommonTooltips.WingsStats").Value;
			Common["HookStats"] = Mod.GetLocalization("CommonTooltips.HookStats").Value;
			Common["DodgeReworkTooltip"] = Mod.GetLocalization("CommonTooltips.DodgeReworkTooltip").Value;
		}
	}
}
