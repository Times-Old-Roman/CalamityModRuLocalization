using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

// Everything that can't be categorized by me. Mostly one-off changes for singular items
namespace CalamityModRuLocalization.VanillaPotions {
	internal class VanillaPotions : GlobalItem {
		public static Dictionary<string, string> Potions = new Dictionary<string, string>();

		public override void SetStaticDefaults() {
			Potions["BottledHoney"] = Mod.GetLocalization("VanillaPotions.BottledHoney").Value;
			Potions["RegenerationPotion"] = Mod.GetLocalization("VanillaPotions.RegenerationPotion").Value;
			Potions["WarmthPotion"] = Mod.GetLocalization("VanillaPotions.WarmthPotion").Value;
			Potions["IronskinPotion"] = Mod.GetLocalization("VanillaPotions.IronskinPotion").Value;
			Potions["ArcheryPotion"] = Mod.GetLocalization("VanillaPotions.ArcheryPotion").Value;
			Potions["SwiftnessPotion"] = Mod.GetLocalization("VanillaPotions.SwiftnessPotion").Value;
			Potions["GillsPotion"] = Mod.GetLocalization("VanillaPotions.GillsPotion").Value;
			Potions["ShinePotion"] = Mod.GetLocalization("VanillaPotions.ShinePotion").Value;
			Potions["MagicPowerPotion"] = Mod.GetLocalization("VanillaPotions.MagicPowerPotion").Value;
			Potions["FeatherfallPotion"] = Mod.GetLocalization("VanillaPotions.FeatherfallPotion").Value;
			Potions["HealingPotions"] = Mod.GetLocalization("VanillaPotions.HealingPotions").Value;
			Potions["Ale"] = Mod.GetLocalization("VanillaPotions.Ale").Value;
			Potions["Sake"] = Mod.GetLocalization("VanillaPotions.Sake").Value;
			Potions["FlaskofPoison"] = Mod.GetLocalization("VanillaPotions.FlaskofPoison").Value;
			Potions["FlaskofFire"] = Mod.GetLocalization("VanillaPotions.FlaskofFire").Value;
			Potions["FlaskofParty"] = Mod.GetLocalization("VanillaPotions.FlaskofParty").Value;
			Potions["FlaskofGold"] = Mod.GetLocalization("VanillaPotions.FlaskofGold").Value;
			Potions["FlaskofCursedFlames"] = Mod.GetLocalization("VanillaPotions.FlaskofCursedFlames").Value;
			Potions["FlaskofIchor"] = Mod.GetLocalization("VanillaPotions.FlaskofIchor").Value;
			Potions["FlaskofNanites"] = Mod.GetLocalization("VanillaPotions.FlaskofNanites").Value;
			Potions["FlaskofVenom"] = Mod.GetLocalization("VanillaPotions.FlaskofVenom").Value;
		}
	}
}
