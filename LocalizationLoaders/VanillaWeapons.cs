using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModRuLocalization.VanillaWeapons {
	internal class VanillaWeapons : GlobalItem {
		public static Dictionary<string, string> Weapons = new Dictionary<string, string>();

		public override void SetStaticDefaults() {
			Weapons["SlimeGun"] = Mod.GetLocalization("VanillaWeapons.SlimeGun").Value;
			Weapons["GelBalloon"] = Mod.GetLocalization("VanillaWeapons.GelBalloon").Value;
			Weapons["SoulDrain"] = Mod.GetLocalization("VanillaWeapons.SoulDrain").Value;
			Weapons["HamBat"] = Mod.GetLocalization("VanillaWeapons.HamBat").Value;
			Weapons["DD2BetsyBow"] = Mod.GetLocalization("VanillaWeapons.DD2BetsyBow").Value;
			Weapons["DeathSickle"] = Mod.GetLocalization("VanillaWeapons.DeathSickle").Value;

			Weapons["CobaltSword"] = Mod.GetLocalization("VanillaWeapons.CobaltSword").Value;
			Weapons["CobaltNaginata"] = Mod.GetLocalization("VanillaWeapons.CobaltNaginata").Value;
			Weapons["PalladiumSword"] = Mod.GetLocalization("VanillaWeapons.PalladiumSword").Value;
			Weapons["PalladiumPike"] = Mod.GetLocalization("VanillaWeapons.PalladiumPike").Value;

			Weapons["MythrilSword"] = Mod.GetLocalization("VanillaWeapons.MythrilSword").Value;
			Weapons["MythrilHalberd"] = Mod.GetLocalization("VanillaWeapons.MythrilHalberd").Value;
			Weapons["OrichalcumSword"] = Mod.GetLocalization("VanillaWeapons.OrichalcumSword").Value;
			Weapons["OrichalcumHalberd"] = Mod.GetLocalization("VanillaWeapons.OrichalcumHalberd").Value;

			Weapons["AdamantiteSword"] = Mod.GetLocalization("VanillaWeapons.AdamantiteSword").Value;
			Weapons["AdamantiteGlaive"] = Mod.GetLocalization("VanillaWeapons.AdamantiteGlaive").Value;
			Weapons["TitaniumSword"] = Mod.GetLocalization("VanillaWeapons.TitaniumSword").Value;
			Weapons["TitaniumTrident"] = Mod.GetLocalization("VanillaWeapons.TitaniumTrident").Value;

			Weapons["Excalibur"] = Mod.GetLocalization("VanillaWeapons.Excalibur").Value;
			Weapons["Gungnir"] = Mod.GetLocalization("VanillaWeapons.Gungnir").Value;
			Weapons["TrueExcalibur"] = Mod.GetLocalization("VanillaWeapons.TrueExcalibur").Value;

			Weapons["CandyCaneSword"] = Mod.GetLocalization("VanillaWeapons.CandyCaneSword").Value;
			Weapons["FruitcakeChakram"] = Mod.GetLocalization("VanillaWeapons.FruitcakeChakram").Value;

			Weapons["StylistKilLaKillScissorsIWish"] = Mod.GetLocalization("VanillaWeapons.StylistKilLaKillScissorsIWish").Value;
			Weapons["BluePhaseblade"] = Mod.GetLocalization("VanillaWeapons.BluePhaseblade").Value;
			Weapons["RedPhaseblade"] = Mod.GetLocalization("VanillaWeapons.RedPhaseblade").Value;
			Weapons["GreenPhaseblade"] = Mod.GetLocalization("VanillaWeapons.GreenPhaseblade").Value;
			Weapons["PurplePhaseblade"] = Mod.GetLocalization("VanillaWeapons.PurplePhaseblade").Value;
			Weapons["WhitePhaseblade"] = Mod.GetLocalization("VanillaWeapons.WhitePhaseblade").Value;
			Weapons["YellowPhaseblade"] = Mod.GetLocalization("VanillaWeapons.YellowPhaseblade").Value;
			Weapons["OrangePhaseblade"] = Mod.GetLocalization("VanillaWeapons.OrangePhaseblade").Value;
			Weapons["BluePhasesaber"] = Mod.GetLocalization("VanillaWeapons.BluePhasesaber").Value;
			Weapons["RedPhasesaber"] = Mod.GetLocalization("VanillaWeapons.RedPhasesaber").Value;
			Weapons["GreenPhasesaber"] = Mod.GetLocalization("VanillaWeapons.GreenPhasesaber").Value;
			Weapons["PurplePhasesaber"] = Mod.GetLocalization("VanillaWeapons.PurplePhasesaber").Value;
			Weapons["WhitePhasesaber"] = Mod.GetLocalization("VanillaWeapons.WhitePhasesaber").Value;
			Weapons["YellowPhasesaber"] = Mod.GetLocalization("VanillaWeapons.YellowPhasesaber").Value;
			Weapons["OrangePhasesaber"] = Mod.GetLocalization("VanillaWeapons.OrangePhasesaber").Value;

			Weapons["AntlionClaw"] = Mod.GetLocalization("VanillaWeapons.AntlionClaw").Value;
			Weapons["BoneSword"] = Mod.GetLocalization("VanillaWeapons.BoneSword").Value;
			Weapons["BreakerBlade"] = Mod.GetLocalization("VanillaWeapons.BreakerBlade").Value;

			Weapons["FalconBlade"] = Mod.GetLocalization("VanillaWeapons.FalconBlade").Value;
		}
	}
}
