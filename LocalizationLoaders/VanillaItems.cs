using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModRuLocalization.VanillaItems {
	internal class VanillaItems : GlobalItem {
		public static Dictionary<string, string> Items = new Dictionary<string, string>();

		public override void SetStaticDefaults() {
			Items["TruffleWorm"] = Mod.GetLocalization("VanillaItems.TruffleWorm").Value;
			Items["Abeemination"] = Mod.GetLocalization("VanillaItems.Abeemination").Value;
			Items["BloodySpine"] = Mod.GetLocalization("VanillaItems.BloodySpine").Value;
			Items["ClothierVoodooDoll"] = Mod.GetLocalization("VanillaItems.ClothierVoodooDoll").Value;
			Items["DeerThing"] = Mod.GetLocalization("VanillaItems.DeerThing").Value;
			Items["GuideVoodooDoll"] = Mod.GetLocalization("VanillaItems.GuideVoodooDoll").Value;
			Items["LihzahrdPowerCell"] = Mod.GetLocalization("VanillaItems.LihzahrdPowerCell").Value;
			Items["MechanicalWorm"] = Mod.GetLocalization("VanillaItems.MechanicalWorm").Value;
			Items["MechanicalEye"] = Mod.GetLocalization("VanillaItems.MechanicalEye").Value;
			Items["MechanicalSkull"] = Mod.GetLocalization("VanillaItems.MechanicalSkull").Value;
			Items["QueenSlimeCrystal"] = Mod.GetLocalization("VanillaItems.QueenSlimeCrystal").Value;
			Items["WormFood"] = Mod.GetLocalization("VanillaItems.WormFood").Value;
			Items["AegisCrystal"] = Mod.GetLocalization("VanillaItems.AegisCrystal").Value;
			Items["DD2ElderCrystal"] = Mod.GetLocalization("VanillaItems.DD2ElderCrystal").Value;

			Items["Pwnhammer"] = Mod.GetLocalization("VanillaItems.Pwnhammer").Value;
			Items["Hammush"] = Mod.GetLocalization("VanillaItems.Hammush").Value;

			Items["GoldPickaxe"] = Mod.GetLocalization("VanillaItems.GoldPickaxe").Value;
			Items["PlatinumPickaxe"] = Mod.GetLocalization("VanillaItems.PlatinumPickaxe").Value;

			Items["SolarFlarePickaxe"] = Mod.GetLocalization("VanillaItems.SolarFlarePickaxe").Value;
			Items["VortexPickaxe"] = Mod.GetLocalization("VanillaItems.VortexPickaxe").Value;
			Items["NebulaPickaxe"] = Mod.GetLocalization("VanillaItems.NebulaPickaxe").Value;
			Items["StardustPickaxe"] = Mod.GetLocalization("VanillaItems.StardustPickaxe").Value;
			Items["SolarFlareDrill"] = Mod.GetLocalization("VanillaItems.SolarFlareDrill").Value;
			Items["VortexDrill"] = Mod.GetLocalization("VanillaItems.VortexDrill").Value;
			Items["NebulaDrill"] = Mod.GetLocalization("VanillaItems.NebulaDrill").Value;
			Items["StardustDrill"] = Mod.GetLocalization("VanillaItems.StardustDrill").Value;

			Items["ShadowOrb"] = Mod.GetLocalization("VanillaItems.ShadowOrb").Value;
			Items["CrimsonHeart"] = Mod.GetLocalization("VanillaItems.CrimsonHeart").Value;
			Items["MagicLantern"] = Mod.GetLocalization("VanillaItems.MagicLantern").Value;
			Items["FairyBell"] = Mod.GetLocalization("VanillaItems.FairyBell").Value;
			Items["DD2PetGhost"] = Mod.GetLocalization("VanillaItems.DD2PetGhost").Value;
			Items["WispinaBottle"] = Mod.GetLocalization("VanillaItems.WispinaBottle").Value;
			Items["SuspiciousLookingTentacle"] = Mod.GetLocalization("VanillaItems.SuspiciousLookingTentacle").Value;
			Items["GolemPetItem"] = Mod.GetLocalization("VanillaItems.GolemPetItem").Value;
			Items["FairyQueenPetItem"] = Mod.GetLocalization("VanillaItems.FairyQueenPetItem").Value;
			Items["PumpkingPetItem"] = Mod.GetLocalization("VanillaItems.PumpkingPetItem").Value;
		}
	}
}
