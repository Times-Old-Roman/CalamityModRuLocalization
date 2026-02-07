using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static ItemSets.ItemSets;
using static CalamityModRuLocalization.CommonTooltips.CommonTooltips;
using static CalamityModRuLocalization.Misc.Misc;
using static CalamityModRuLocalization.VanillaAccs.VanillaAccs;
using static CalamityModRuLocalization.VanillaArmor.VanillaArmor;
using static CalamityModRuLocalization.VanillaItems.VanillaItems;
using static CalamityModRuLocalization.VanillaPotions.VanillaPotions;
using static CalamityModRuLocalization.VanillaWeapons.VanillaWeapons;
using static CalamityModRuLocalization.VanillaWings.VanillaWings;

// Because for some reason Calamity changes the vanilla descriptions via code and not localization files
// I hope this changes at some point, because otherwise it's gonna need constant code upkeep along with localization files updates

namespace CalamityModRuLocalization {
	internal class CalamityVanillaDescOverrides : GlobalItem {

		static string getKnockbackString(float knockback) {
			if (knockback <= float.Epsilon) return Language.GetTextValue("LegacyTooltip.14");        // No knockback
			if (knockback <= 1.5) return Language.GetTextValue("LegacyTooltip.15");                  // Extremely weak
			if (knockback <= 3) return Language.GetTextValue("LegacyTooltip.16");                    // Very weak
			if (knockback <= 4) return Language.GetTextValue("LegacyTooltip.17");                    // Weak
			if (knockback <= 6) return Language.GetTextValue("LegacyTooltip.18");                    // Average
			if (knockback <= 7) return Language.GetTextValue("LegacyTooltip.19");                    // Strong
			if (knockback <= 9) return Language.GetTextValue("LegacyTooltip.20");                    // Very strong
			if (knockback <= 11) return Language.GetTextValue("LegacyTooltip.21");                   // Extremely strong
			return Language.GetTextValue("LegacyTooltip.22");										  // Insanely strong
		}
		void repWingTooltip(ref List<TooltipLine> tooltips, string revertStrName, string revertStr, string statString, List<string> additionalStats) {
			List<string> srcLines = new List<string>();
			int repInd = 0;
			foreach (TooltipLine line in tooltips) {
				if (line.Name.Equals(revertStrName)) {
					srcLines.AddRange(line.Text.Split('\n')[1..]);
					repInd = tooltips.IndexOf(line);
					break;
				}
			}
			if (srcLines.Count < 4)
				return;
			List<TooltipLine> newLines = new List<TooltipLine>();
			tooltips[repInd].Text = revertStr;
			string pattern = @": ";
			string flightTime = srcLines[3].Split(pattern)[^1];
			string flightTimeSeconds = (Math.Round(int.Parse(flightTime) / 60f, 2)).ToString();
			string horSpeed = srcLines[0].Split(pattern)[^1];
			string speedAccel = srcLines[1].Split(pattern)[^1];
			string vertSpeed = srcLines[2].Split(' ')[0];

			string vertSpeedLocalized = Common["WingsVertSpeed" + vertSpeed.ToLower()];

			statString = statString.FormatWith(horSpeed, speedAccel, vertSpeedLocalized, flightTime, flightTimeSeconds);
			int count = revertStrName.Equals("Tooltip0") ? 1 : 0;
			foreach (string stat in statString.Split('\n')) {
				tooltips.Insert(repInd + count, new TooltipLine(Mod, "Tooltip" + count.ToString(), stat));
				count++;
			}
			foreach (string stat in additionalStats) {
				tooltips.Insert(repInd + count, new TooltipLine(Mod, "Tooltip" + count.ToString(), stat));
				count++;
			}
		}
		void repHookTooltip(ref List<TooltipLine> tooltips, string revertStrName, string revertStr, string statString) {
			List<string> srcLines = new List<string>();
			int repInd = 0;
			foreach (TooltipLine line in tooltips) {
				if (line.Name.Equals(revertStrName)) {
					srcLines.AddRange(line.Text.Split('\n')[1..]);
					repInd = tooltips.IndexOf(line);
					break;
				}
			}
			List<TooltipLine> newLines = new List<TooltipLine>();
			if (srcLines.Count < 4)
				return;
			tooltips[repInd].Text = revertStr;
			string pattern = @"[\d]+";
			string reach = Regex.Match(srcLines[0], pattern).Value;
			string launchSpeed = Regex.Match(srcLines[1], pattern).Value;
			string reelSpeed = Regex.Match(srcLines[2], pattern).Value;
			string pullSpeed = Regex.Match(srcLines[3], pattern).Value;

			statString = statString.FormatWith(reach, launchSpeed, reelSpeed, pullSpeed);
			int count = 0;
			foreach (string stat in statString.Split('\n')) {
				tooltips.Insert(repInd + 1 + count, new TooltipLine(Mod, "Tooltip" + count.ToString(), stat));
				count++;
			}
		}

		void repDodgeTooltip(ref List<TooltipLine> tooltips, string revertStrName, string reworkString) {
			string srcTooltip = "";
			int repInd = 0;
			foreach (TooltipLine line in tooltips) {
				if (line.Name.Equals(revertStrName)) {
					srcTooltip = line.Text;
					repInd = tooltips.IndexOf(line);
					break;
				}
			}
			List<TooltipLine> newLines = new List<TooltipLine>();
			if (srcTooltip.Length == 0)
				return;
			string pattern = @"[\d]+";
			var regexMatches = Regex.Matches(srcTooltip, pattern);
			if (regexMatches.Count < 3)
				return;
			string fromTime = regexMatches[1].Value;
			string toTime = regexMatches[2].Value;

			reworkString = reworkString.FormatWith(fromTime, toTime);
			tooltips[repInd].Text = reworkString;
		}

		// Simply replaces the tooltip to the new one
		void repTooltipTotal(ref List<TooltipLine> tooltips, List<string> specialReplacements) {
			int start = 0, end = 0;
			for (int i = 0; i < tooltips.Count; i++) {
				if (tooltips[i].Name.ToLower().Contains("tooltip")) {
					if (start == 0) start = i;
					end = i;
				}
				else if (start != 0) break;
			}
			if (start != 0)
				tooltips.RemoveRange(start, Math.Max(0, end - start + 1));
			int count = 0;
			foreach (var line in specialReplacements)
				tooltips.Insert(start + count++, new TooltipLine(Mod, "Tooltip" + count.ToString(), line));
		}

		// Reverts the tooltips, indicated by their name, to vanilla strings and adds the changes afterwards in a "Tooltip#{num}" format
		void repTooltipRevert(ref List<TooltipLine> tooltips, List<string> specialReplacements, string revertStrName, string revertStr, List<string> insertAfterTooltip) {
			int indOfInsert = 0;
			bool anyInsertFound = false;
			int latestInsert = 0;
			for (int i = 0; i < tooltips.Count; i++) {
				if (tooltips[i].Name.Equals(revertStrName)) {
					tooltips[i].Text = revertStr;
					indOfInsert = i;
				}
				if (latestInsert < insertAfterTooltip.Count)
					for (int j = latestInsert; j < insertAfterTooltip.Count; j++)
						if (tooltips[i].Name.Equals(insertAfterTooltip[j])) {
							indOfInsert = i;
							latestInsert = j;
							anyInsertFound = true;
						}
				}
			if (!anyInsertFound)
				throw new Exception("No string found to insert after in " + tooltips[0].Name);
			int count = 0;
			foreach (string tooltipStr in specialReplacements) {
				tooltips.Insert(indOfInsert + 1 + count, new TooltipLine(Mod, "Tooltip" + count.ToString(), tooltipStr));
				count++;
			}
		}

		void repTooltips(ref List<TooltipLine> tooltips, List<(string, string)> replacementsByname) {
			for (int i = 0; i < tooltips.Count; i++) {
				for (int j = 0; j < replacementsByname.Count; j++) {
					if (tooltips[i].Name == replacementsByname[j].Item1)
						tooltips[i].Text = replacementsByname[j].Item2;
				}
			}
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
			/*
					I've wanted to make this localization to be more mindful of other translations and minor variations to strings, so I tried as hard as I can
				to remvoe all cases of String.Replace, because it completely disregards the tooltip in the original language.

					For example, if one uses another localization for vanilla, some strings can have wildly different contents, and a replace won't do anything
				Biggest culprit: Celestial Shell family. Calamity changes removes the melee speed buff, but that is done through replacing the
				" melee speed," substring. I think it is fairly obvious, that this string doesn't exist in languages aside from those it exists in ahah (so only English,
				and only when not using any other resource packs with additional localization)
				Similar with melee gloves, flasks and burning blocks immunity

					Sometimes it's also necessary, because Calamity likes to add strings to existing tooltips, instead of adding new Tooltipline-s.
				In any case it is much easier to modify the code and localizaiton files like this, since every part of the logic of this code
				is actually contained in the files, which makes it much easier to change in the future.

					It would be best for calamity to work on their localization files, though. This code is, frankly, an abomination, due to how Calamity handled
				these changes - by hardcoding the changes, ignoring the fact that it can differ in other languages. I would say "fair enough, not worth the effort", but
				I feel a lot may enjoy to play the game in their own language, and some REQUIRE it, because they don't know English.
				And for others to make a localization mod is fine, but NOT when it requires this much work in tModLoader, imho.

				TL,DR: Calamity hardcoded the changes, which break on Non-English langauges. I've fixed this, but am not happy with the code. Everything is in .hjson localization files now.
			 */
			if (Language.ActiveCulture.Name != "ru-RU") {
				return;
			}

			if (!item.social) {
				List<string> lineReplacementTotal = [];         // Replaces the source "Tooltip" TooltipLine-s with new ones
				List<string> lineReplacementRevertByName = [];  // Reverts the "{Name}" Tooltipline to Vanilla state and adds calamity lines after Tooltipline with another name
				string revertStrName = "";                      // What tooltip to disjoin from the calamity changes?
				List<string> insertAfterTooltip = [];           // Aftrer what to put the new tooltip? Gets the last possible tooltip type, out of ones in the list. Order matters
				string revertStr = "";                          // What do we revert the string to?

				List<string> appendAfter = [];                  // Simply appends another tooltip

				// Uncategorized items, that can have wildly differing tooltips
				switch (item.type) {
					case ItemID.SlimeGun:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.SlimeGun"));
						lineReplacementTotal.Add(Weapons["SlimeGun"]);
						break;
					case ItemID.GelBalloon:
						lineReplacementTotal.AddRange(Weapons["GelBalloon"].Split('\n'));   // AddRange for different tooltip lines
						break;
					case ItemID.RodofDiscord:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.RodofDiscord"));
						lineReplacementTotal.Add(MiscTools["RodofDiscord"]);
						break;
					case ItemID.ObsidianSkull:
						lineReplacementTotal.Add(Accs["ObsidianSkull"]);
						break;
					case ItemID.ObsidianSkullRose:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.ObsidianSkullRose").Split('\n'));
						lineReplacementTotal[0] = Accs["ObsidianSkullRose"];
						break;
					case ItemID.MoltenCharm:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.MoltenCharm").Split('\n'));
						lineReplacementTotal[0] = Accs["MoltenCharm"];
						break;
					case ItemID.ObsidianHorseshoe:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.ObsidianHorseshoe").Split('\n'));
						lineReplacementTotal[1] = Accs["ObsidianHorseshoe"];
						break;
					case ItemID.ObsidianShield:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.ObsidianShield").Split('\n'));
						lineReplacementTotal[1] = Accs["ObsidianShield"];
						break;
					case ItemID.ObsidianWaterWalkingBoots:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.ObsidianWaterWalkingBoots").Split('\n'));
						lineReplacementTotal[1] = Accs["ObsidianWaterWalkingBoots"];
						break;
					case ItemID.LavaWaders:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.LavaWaders").Split('\n'));
						lineReplacementTotal[1] = Accs["LavaWaders"];
						break;
					case ItemID.LavaSkull:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.LavaSkull").Split('\n'));
						lineReplacementTotal[1] = Accs["LavaSkull"];
						break;
					case ItemID.MoltenSkullRose:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.MoltenSkullRose").Split('\n'));
						lineReplacementTotal[1] = Accs["MoltenSkullRose"];
						break;
					case ItemID.AnkhShield:
						lineReplacementTotal.Add(Accs["AnkhShield"]);
						break;
					case ItemID.Pwnhammer:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.Pwnhammer").Split('\n'));
						lineReplacementTotal.Add(Items["Pwnhammer"]);
						break;
					case ItemID.Hammush:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.Hammush").Split('\n'));
						lineReplacementTotal.Add(Items["Hammush"]);
						break;
					case ItemID.HeartLantern:
						lineReplacementTotal.Add(MiscPlaceables["HeartLantern"]);
						break;
					case ItemID.BottledHoney:
						lineReplacementTotal.Add(Potions["BottledHoney"]);
						break;
					case ItemID.ShinyStone:
						lineReplacementTotal.Add(Accs["ShinyStone"]);
						break;
					case ItemID.BandofRegeneration:
						lineReplacementTotal.Add(Accs["BandofRegeneration"]);
						break;
					case ItemID.CharmofMyths:
						lineReplacementTotal.Add(Accs["CharmofMyths"]);
						break;
					case ItemID.RegenerationPotion:
						lineReplacementTotal.Add(Potions["RegenerationPotion"]);
						break;
					case ItemID.GillsPotion:
						for (int i = 0; i < tooltips.Count; i++)
							if (tooltips[i].Name.Equals("BuffTime")) {
								string buffStr = Language.GetTextValue(item.buffTime > 3600 ? "CommonItemTooltip.MinuteDuration" : "CommonItemTooltip.SecondDuration");
								buffStr = String.Format(buffStr, item.buffTime / 60 / 60);
								tooltips[i].Text = buffStr;
								break;
							}
						appendAfter.Add(Potions["GillsPotion"]);
						break;
					case ItemID.ShinePotion:
						for (int i = 0; i < tooltips.Count; i++)
							if (tooltips[i].Name.Equals("BuffTime")) {
								//Console.WriteLine(tooltips[i].Text);
								string buffStr = Language.GetTextValue(item.buffTime > 3600 ? "CommonItemTooltip.MinuteDuration" : "CommonItemTooltip.SecondDuration");
								buffStr = String.Format(buffStr, item.buffTime / 60 / 60);
								tooltips[i].Text = buffStr;
								break;
							}
						appendAfter.Add(Potions["ShinePotion"]);
						break;
					case ItemID.IronskinPotion:
						string repStr = Potions["IronskinPotion"];
						for (int i = 0; i < tooltips.Count; i++) {
							if (tooltips[i].Name.Equals("Tooltip0")) {
								//Console.WriteLine("{0}, {1}", tooltips[i].Text, tooltips[i].Name);
								string defenseAmount = Regex.Match(tooltips[i].Text, @"([\d]+)").Value;
								repStr = String.Format(repStr, defenseAmount);
								break;
							}
						}
						lineReplacementTotal.Add(repStr);
						break;
					case ItemID.SoulDrain:
						lineReplacementTotal.Add(Weapons["SoulDrain"]);
						break;
					case ItemID.HamBat:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.HamBat").Split('\n'));
						lineReplacementTotal[1] = Weapons["HamBat"];
						break;
					case ItemID.AegisCrystal:
						lineReplacementTotal.Add(Items["AegisCrystal"]);
						break;
					case ItemID.WarmthPotion:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.WarmthPotion"));
						lineReplacementTotal.Add(Potions["WarmthPotion"]);
						break;
					case ItemID.ArcheryPotion:
						lineReplacementTotal.Add(Potions["ArcheryPotion"]);
						break;
					case ItemID.SwiftnessPotion:
						lineReplacementTotal.Add(Potions["SwiftnessPotion"]);
						break;
					case ItemID.HandWarmer:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.HandWarmer"));
						lineReplacementTotal.Add(Accs["HandWarmer"]);
						break;
					case ItemID.GoldenFishingRod:
						lineReplacementRevertByName.Add(MiscTools["GoldenFishingRod"]);
						revertStrName = "NeedsBait";
						revertStr = Language.GetTextValue("GameUI.BaitRequired");       // Requires bait
						insertAfterTooltip = ["NeedsBait"];
						break;
					case ItemID.DD2ElderCrystal:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.DD2ElderCrystal"));
						lineReplacementTotal.Add(Items["DD2ElderCrystal"]);
						break;
					case ItemID.DD2BetsyBow:
						lineReplacementTotal.Add(Weapons["DD2BetsyBow"]);
						break;
					case ItemID.Abeemination:
						lineReplacementTotal.Add(Items["Abeemination"]);
						break;
					case ItemID.BloodySpine:
						lineReplacementTotal.Add(Items["BloodySpine"]);
						break;
					case ItemID.ClothierVoodooDoll:
						lineReplacementTotal.Add(Items["ClothierVoodooDoll"]);
						break;
					case ItemID.DeerThing:
						lineReplacementTotal.Add(Items["DeerThing"]);
						break;
					case ItemID.GuideVoodooDoll:
						lineReplacementTotal.Add(Items["GuideVoodooDoll"]);
						break;
					case ItemID.LihzahrdPowerCell:
						lineReplacementTotal.Add(Items["LihzahrdPowerCell"]);
						break;
					case ItemID.MechanicalWorm:
						lineReplacementTotal.Add(Items["MechanicalWorm"]);
						break;
					case ItemID.MechanicalEye:
						lineReplacementTotal.Add(Items["MechanicalEye"]);
						break;
					case ItemID.MechanicalSkull:
						lineReplacementTotal.Add(Items["MechanicalSkull"]);
						break;
					case ItemID.QueenSlimeCrystal:
						lineReplacementTotal.Add(Items["QueenSlimeCrystal"]);
						break;
					case ItemID.TruffleWorm:
						lineReplacementRevertByName.Add(Items["TruffleWorm"]);
						revertStrName = "Consumable";
						revertStr = Language.GetTextValue("LegacyTooltip.35");      // Consumable str
						insertAfterTooltip = ["Material"];
						break;
					case ItemID.WormFood:
						lineReplacementTotal.Add(Items["WormFood"]);
						break;
					case ItemID.DeathSickle:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.DeathSickle"));
						lineReplacementTotal.Add(Weapons["DeathSickle"]);
						break;
					case ItemID.FlaskofPoison:
						lineReplacementTotal.Add(Potions["FlaskofPoison"]);
						break;
					case ItemID.FlaskofFire:
						lineReplacementTotal.Add(Potions["FlaskofFire"]);
						break;
					case ItemID.FlaskofParty:
						lineReplacementTotal.Add(Potions["FlaskofParty"]);
						break;
					case ItemID.FlaskofGold:
						lineReplacementTotal.Add(Potions["FlaskofGold"]);
						break;
					case ItemID.FlaskofCursedFlames:
						lineReplacementTotal.Add(Potions["FlaskofCursedFlames"]);
						break;
					case ItemID.FlaskofIchor:
						lineReplacementTotal.Add(Potions["FlaskofIchor"]);
						break;
					case ItemID.FlaskofNanites:
						lineReplacementTotal.Add(Potions["FlaskofNanites"]);
						break;
					case ItemID.FlaskofVenom:
						lineReplacementTotal.Add(Potions["FlaskofVenom"]);
						break;
					case ItemID.FlameWakerBoots:
						lineReplacementTotal.Add(Accs["FlameWakerBoots"]);
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.FlameWakerBoots"));
						break;
					case ItemID.HellfireTreads:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.HellfireTreads").Split('\n'));
						lineReplacementTotal.Add(Accs["HellfireTreads"]);
						break;
					case ItemID.FairyBoots:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.FairyBoots").Split('\n'));
						lineReplacementTotal.Add(Accs["FairyBoots"]);
						break;
					case ItemID.MoonStone:
						lineReplacementTotal.Add(Accs["MoonStone"]);
						break;
					case ItemID.SunStone:
						lineReplacementTotal.Add(Accs["SunStone"]);
						break;
					case ItemID.CelestialStone:
						lineReplacementTotal.Add(Accs["CelestialStone"]);
						break;
					case ItemID.CelestialShell:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.CelestialShell").Split('\n')[0]);
						lineReplacementTotal.Add(Accs["CelestialShell"]);
						break;
					case ItemID.FrozenTurtleShell:
						lineReplacementTotal.Add(Accs["FrozenTurtleShell"]);
						break;
					case ItemID.FrozenShield:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.FrozenShield").Split('\n'));
						lineReplacementTotal[1] = Accs["FrozenShield"];
						break;
					case ItemID.Ale:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.Ale").Split('\n'));
						lineReplacementTotal[0] = Potions["Ale"];
						break;
					case ItemID.Sake:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.Sake").Split('\n'));
						lineReplacementTotal[0] = Potions["Sake"];
						break;
					case ItemID.MagnetFlower:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.MagnetFlower").Split('\n'));
						lineReplacementTotal[0] = Accs["MagnetFlower"];
						break;
					case ItemID.ArcaneFlower:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.ArcaneFlower").Split('\n'));
						lineReplacementTotal[0] = Accs["ArcaneFlower"];
						break;
					case ItemID.ManaCloak:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.ManaCloak").Split('\n'));
						lineReplacementTotal[0] = Accs["ManaCloak"];
						break;
					case ItemID.Magiluminescence:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.Magiluminescence").Split('\n'));
						lineReplacementTotal[0] = Accs["Magiluminescence.Other"];
						lineReplacementTotal.Insert(2, Accs["Magiluminescence.AbyssLightChange"]);
						break;
					case ItemID.EmpressFlightBooster:
						lineReplacementTotal.Add(Accs["EmpressFlightBooster"]);
						break;
					case ItemID.RifleScope:
						lineReplacementTotal.AddRange(Accs["RifleScope"].Split('\n'));
						break;
					case ItemID.SniperScope:
						for (int i = 0; i < tooltips.Count; i++)
							if (tooltips[i].Name.Equals("Tooltip0")) {
								lineReplacementTotal.Add(tooltips[i].Text.Split('\n')[0]);
								break;
							}
						lineReplacementTotal.Add(Accs["SniperScope.Visibility"]);
						lineReplacementTotal.Add(Accs["SniperScope.Other"]);
						break;
					case ItemID.ReconScope:
						for (int i = 0; i < tooltips.Count; i++)
							if (tooltips[i].Name.Equals("Tooltip0")) {
								lineReplacementTotal.Add(tooltips[i].Text.Split('\n')[0]);
								break;
							}
						lineReplacementTotal.Add(Accs["ReconScope"]);
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.ReconScope").Split('\n')[1..]);
						break;
					case ItemID.MagicQuiver:
						lineReplacementTotal.Add(Accs["MagicQuiver"]);
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.MagicQuiver"));
						break;
					case ItemID.MoltenQuiver:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.MoltenQuiver").Split('\n'));
						lineReplacementTotal[0] = Accs["MoltenQuiver.Other"];
						lineReplacementTotal[2] = Accs["MoltenQuiver.HellfireChange"];
						break;
					case ItemID.MagicPowerPotion:
						lineReplacementTotal.Add(Potions["MagicPowerPotion"]);
						break;
					case ItemID.FeatherfallPotion:
						lineReplacementTotal.AddRange(Potions["FeatherfallPotion"].Split('\n'));
						break;
					case ItemID.MagicHat:
						lineReplacementTotal.Add(Armor["MagicHat"]);
						break;
					case ItemID.FleshKnuckles:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.FleshKnuckles"));
						lineReplacementTotal.Add(Accs["FleshKnuckles"]);
						break;
					case ItemID.HeroShield:
						lineReplacementTotal.Add(Accs["HeroShield"]);
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.HeroShield").Split('\n'));
						break;
					case ItemID.TitanGlove:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.TitanGlove"));
						lineReplacementTotal.Add(Accs["TitanGlove"]);
						break;
					case ItemID.PowerGlove:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.PowerGlove").Split('\n'));
						lineReplacementTotal[1] = Accs["PowerGlove"];
						break;
					case ItemID.BerserkerGlove:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.BerserkerGlove").Split('\n'));
						lineReplacementTotal[1] = Accs["BerserkerGlove.BuffTitanGlove"];
						lineReplacementTotal.Add(Accs["BerserkerGlove.BuffFleshKnuckles"]);
						break;
					case ItemID.MechanicalGlove:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.MechanicalGlove").Split('\n'));
						lineReplacementTotal[1] = Accs["MechanicalGlove"];
						break;
					case ItemID.FireGauntlet:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.FireGauntlet").Split('\n'));
						lineReplacementTotal[0] = Accs["FireGauntlet.HellfireChange"];
						lineReplacementTotal[1] = Accs["FireGauntlet.Other"];
						break;
					case ItemID.YoYoGlove:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.YoYoGlove"));
						lineReplacementTotal.Add(Accs["YoYoGlove"]);
						break;
					case ItemID.YoyoBag:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.YoyoBag"));
						lineReplacementTotal.Add(Accs["YoyoBag"]);
						break;
					case ItemID.TerrasparkBoots:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.TerrasparkBoots").Split('\n'));
						lineReplacementTotal[3] = Accs["TerrasparkBoots"];
						break;
					case ItemID.CobaltSword:
						lineReplacementRevertByName.Add(Weapons["CobaltSword"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.CobaltNaginata:
						lineReplacementRevertByName.Add(Weapons["CobaltNaginata"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.PalladiumSword:
						lineReplacementRevertByName.Add(Weapons["PalladiumSword"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.PalladiumPike:
						lineReplacementRevertByName.Add(Weapons["PalladiumPike"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.MythrilSword:
						lineReplacementRevertByName.Add(Weapons["MythrilSword"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.MythrilHalberd:
						lineReplacementRevertByName.Add(Weapons["MythrilHalberd"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.OrichalcumSword:
						lineReplacementRevertByName.Add(Weapons["OrichalcumSword"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.OrichalcumHalberd:
						lineReplacementRevertByName.Add(Weapons["OrichalcumHalberd"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.AdamantiteSword:
						lineReplacementRevertByName.Add(Weapons["AdamantiteSword"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.AdamantiteGlaive:
						lineReplacementRevertByName.Add(Weapons["AdamantiteGlaive"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.TitaniumSword:
						lineReplacementRevertByName.Add(Weapons["TitaniumSword"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.TitaniumTrident:
						lineReplacementRevertByName.Add(Weapons["TitaniumTrident"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.Excalibur:
						lineReplacementRevertByName.Add(Weapons["Excalibur"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.Gungnir:
						lineReplacementRevertByName.Add(Weapons["Gungnir"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.TrueExcalibur:
						lineReplacementRevertByName.Add(Weapons["TrueExcalibur"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.CandyCaneSword:
						lineReplacementRevertByName.Add(Weapons["CandyCaneSword"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.FruitcakeChakram:
						lineReplacementRevertByName.Add(Weapons["FruitcakeChakram"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.StylistKilLaKillScissorsIWish:
						lineReplacementRevertByName.Add(Weapons["StylistKilLaKillScissorsIWish"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.BluePhasesaber:
						lineReplacementRevertByName.Add(Weapons["BluePhasesaber"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.RedPhasesaber:
						lineReplacementRevertByName.Add(Weapons["RedPhasesaber"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.GreenPhasesaber:
						lineReplacementRevertByName.Add(Weapons["GreenPhasesaber"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.PurplePhasesaber:
						lineReplacementRevertByName.Add(Weapons["PurplePhasesaber"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.WhitePhasesaber:
						lineReplacementRevertByName.Add(Weapons["WhitePhasesaber"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.YellowPhasesaber:
						lineReplacementRevertByName.Add(Weapons["YellowPhasesaber"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.OrangePhasesaber:
						lineReplacementRevertByName.Add(Weapons["YellowPhasesaber"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.BluePhaseblade:
						lineReplacementRevertByName.Add(Weapons["BluePhaseblade"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.RedPhaseblade:
						lineReplacementRevertByName.Add(Weapons["RedPhaseblade"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.GreenPhaseblade:
						lineReplacementRevertByName.Add(Weapons["GreenPhaseblade"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.PurplePhaseblade:
						lineReplacementRevertByName.Add(Weapons["PurplePhaseblade"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.WhitePhaseblade:
						lineReplacementRevertByName.Add(Weapons["WhitePhaseblade"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.YellowPhaseblade:
						lineReplacementRevertByName.Add(Weapons["YellowPhaseblade"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.OrangePhaseblade:
						lineReplacementRevertByName.Add(Weapons["YellowPhaseblade"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.AntlionClaw:
						lineReplacementRevertByName.Add(Weapons["AntlionClaw"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.BoneSword:
						lineReplacementRevertByName.Add(Weapons["AntlionClaw"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.BreakerBlade:
						lineReplacementRevertByName.Add(Weapons["AntlionClaw"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.FalconBlade:
						lineReplacementRevertByName.Add(Weapons["FalconBlade"]);
						revertStrName = "Knockback";
						revertStr = getKnockbackString(item.knockBack);
						insertAfterTooltip = ["Knockback", "Material"];
						break;
					case ItemID.Gi:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.Gi").Split('\n'));
						lineReplacementTotal[1] = Armor["Gi"];
						break;
					case ItemID.CopperHelmet:
						lineReplacementTotal.Add(Armor["CopperHelmet"]);
						break;
					case ItemID.CopperChainmail:
						lineReplacementTotal.Add(Armor["CopperChainmail"]);
						break;
					case ItemID.CopperGreaves:
						lineReplacementTotal.Add(Armor["CopperGreaves"]);
						break;
					case ItemID.TinHelmet:
						lineReplacementTotal.Add(Armor["TinHelmet"]);
						break;
					case ItemID.TinChainmail:
						lineReplacementTotal.Add(Armor["TinChainmail"]);
						break;
					case ItemID.TinGreaves:
						lineReplacementTotal.Add(Armor["TinGreaves"]);
						break;
					case ItemID.IronHelmet:
						lineReplacementTotal.Add(Armor["IronHelmet"]);
						break;
					case ItemID.AncientIronHelmet:
						lineReplacementTotal.Add(Armor["AncientIronHelmet"]);
						break;
					case ItemID.IronChainmail:
						lineReplacementTotal.Add(Armor["IronChainmail"]);
						break;
					case ItemID.IronGreaves:
						lineReplacementTotal.Add(Armor["IronGreaves"]);
						break;
					case ItemID.LeadHelmet:
						lineReplacementTotal.Add(Armor["LeadHelmet"]);
						break;
					case ItemID.LeadChainmail:
						lineReplacementTotal.Add(Armor["LeadChainmail"]);
						break;
					case ItemID.LeadGreaves:
						lineReplacementTotal.Add(Armor["LeadGreaves"]);
						break;
					case ItemID.SilverHelmet:
						lineReplacementTotal.Add(Armor["SilverHelmet"]);
						break;
					case ItemID.SilverChainmail:
						lineReplacementTotal.Add(Armor["SilverChainmail"]);
						break;
					case ItemID.SilverGreaves:
						lineReplacementTotal.Add(Armor["SilverGreaves"]);
						break;
					case ItemID.TungstenHelmet:
						lineReplacementTotal.Add(Armor["TungstenHelmet"]);
						break;
					case ItemID.TungstenChainmail:
						lineReplacementTotal.Add(Armor["TungstenChainmail"]);
						break;
					case ItemID.TungstenGreaves:
						lineReplacementTotal.Add(Armor["TungstenGreaves"]);
						break;
					case ItemID.GoldHelmet:
						lineReplacementTotal.Add(Armor["GoldHelmet"]);
						break;
					case ItemID.AncientGoldHelmet:
						lineReplacementTotal.Add(Armor["AncientGoldHelmet"]);
						break;
					case ItemID.GoldChainmail:
						lineReplacementTotal.Add(Armor["GoldChainmail"]);
						break;
					case ItemID.GoldGreaves:
						lineReplacementTotal.Add(Armor["GoldGreaves"]);
						break;
					case ItemID.PlatinumHelmet:
						lineReplacementTotal.Add(Armor["PlatinumHelmet"]);
						break;
					case ItemID.PlatinumChainmail:
						lineReplacementTotal.Add(Armor["PlatinumChainmail"]);
						break;
					case ItemID.PlatinumGreaves:
						lineReplacementTotal.Add(Armor["PlatinumGreaves"]);
						break;
					case ItemID.CobaltHat:
						lineReplacementTotal.Add(Armor["CobaltHat"]);
						break;
					case ItemID.PalladiumBreastplate:
						lineReplacementTotal.Add(Armor["PalladiumBreastplate"]);
						break;
					case ItemID.PalladiumLeggings:
						lineReplacementTotal.Add(Armor["PalladiumLeggings"]);
						break;
					case ItemID.MythrilHood:
						lineReplacementTotal.Add(Armor["MythrilHood"]);
						break;
					case ItemID.OrichalcumBreastplate:
						lineReplacementTotal.Add(Armor["OrichalcumBreastplate"]);
						break;
					case ItemID.AdamantiteHeadgear:
						lineReplacementTotal.Add(Armor["AdamantiteHeadgear"]);
						break;
					case ItemID.SquireGreatHelm:
						lineReplacementTotal.Add(Armor["SquireGreatHelm"]);
						break;
					case ItemID.SquirePlating:
						lineReplacementTotal.Add(Armor["SquirePlating"]);
						break;
					case ItemID.SquireGreaves:
						lineReplacementTotal.Add(Armor["SquireGreaves"]);
						break;
					case ItemID.MonkBrows:
						lineReplacementTotal.Add(Armor["MonkBrows"]);
						break;
					case ItemID.MonkShirt:
						lineReplacementTotal.Add(Armor["MonkShirt"]);
						break;
					case ItemID.MonkPants:
						lineReplacementTotal.Add(Armor["MonkPants"]);
						break;
					case ItemID.HuntressJerkin:
						lineReplacementTotal.Add(Armor["HuntressJerkin"]);
						break;
					case ItemID.ApprenticeTrousers:
						lineReplacementTotal.Add(Armor["ApprenticeTrousers"]);
						break;
					case ItemID.SquireAltShirt:
						lineReplacementTotal.Add(Armor["SquireAltShirt"]);
						break;
					case ItemID.SquireAltPants:
						lineReplacementTotal.Add(Armor["SquireAltPants"]);
						break;
					case ItemID.MonkAltHead:
						lineReplacementTotal.Add(Armor["MonkAltHead"]);
						break;
					case ItemID.MonkAltShirt:
						lineReplacementTotal.Add(Armor["MonkAltShirt"]);
						break;
					case ItemID.MonkAltPants:
						lineReplacementTotal.Add(Armor["MonkAltPants"]);
						break;
					case ItemID.HuntressAltShirt:
						lineReplacementTotal.Add(Armor["HuntressAltShirt"]);
						break;
					case ItemID.ApprenticeAltPants:
						lineReplacementTotal.Add(Armor["ApprenticeAltPants"]);
						break;
					case ItemID.Picksaw:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.Picksaw"));
						lineReplacementTotal.Add(MiscTools["Picksaw"]);
						break;
					case ItemID.CrimsonHelmet:
						lineReplacementTotal.Add(Armor["CrimsonHelmet"]);
						break;
					case ItemID.CrimsonScalemail:
						lineReplacementTotal.Add(Armor["CrimsonScalemail"]);
						break;
					case ItemID.CrimsonGreaves:
						lineReplacementTotal.Add(Armor["CrimsonGreaves"]);
						break;
					case ItemID.SolarFlareHelmet:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.SolarFlareHelmet").Split('\n'));
						lineReplacementTotal[1] = String.Format(Armor["SolarFlareHelmet"], item.lifeRegen);
						break;
					case ItemID.SolarFlareBreastplate:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.SolarFlareBreastplate").Split('\n'));
						lineReplacementTotal[1] = String.Format(Armor["SolarFlareBreastplate"], item.lifeRegen);
						break;
					case ItemID.SolarFlareLeggings:
						lineReplacementTotal.AddRange(Language.GetTextValue("ItemTooltip.SolarFlareLeggings").Split('\n'));
						lineReplacementTotal[1] = String.Format(Armor["SolarFlareLeggings"], item.lifeRegen);
						break;
					case ItemID.GladiatorHelmet:
						lineReplacementRevertByName.Add(Armor["GladiatorHelmet"]);
						revertStrName = "Defense";
						revertStr = item.defense.ToString() + Language.GetTextValue("LegacyTooltip.25");        // defense
						insertAfterTooltip = ["Defense"];
						break;
					case ItemID.GladiatorBreastplate:
						lineReplacementRevertByName.Add(Armor["GladiatorBreastplate"]);
						revertStrName = "Defense";
						revertStr = item.defense.ToString() + Language.GetTextValue("LegacyTooltip.25");        // defense
						insertAfterTooltip = ["Defense"];
						break;
					case ItemID.GladiatorLeggings:
						lineReplacementRevertByName.Add(Armor["GladiatorLeggings"]);
						revertStrName = "Defense";
						revertStr = item.defense.ToString() + Language.GetTextValue("LegacyTooltip.25");        // defense
						insertAfterTooltip = ["Defense"];
						break;
					case ItemID.GoldPickaxe:
						lineReplacementTotal.Add(Items["GoldPickaxe"]);
						break;
					case ItemID.PlatinumPickaxe:
						lineReplacementTotal.Add(Items["PlatinumPickaxe"]);
						break;
					case ItemID.SolarFlarePickaxe:
						lineReplacementRevertByName.Add(Items["SolarFlarePickaxe"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Material"];
						break;
					case ItemID.VortexPickaxe:
						lineReplacementRevertByName.Add(Items["VortexPickaxe"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Material"];
						break;
					case ItemID.NebulaPickaxe:
						lineReplacementRevertByName.Add(Items["NebulaPickaxe"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Material"];
						break;
					case ItemID.StardustPickaxe:
						lineReplacementRevertByName.Add(Items["StardustPickaxe"]);
						revertStrName = "Material";
						revertStr = Language.GetTextValue("LegacyTooltip.36");     // Material
						insertAfterTooltip = ["Material"];
						break;
					case ItemID.SolarFlareDrill:
						lineReplacementRevertByName.Add(Items["SolarFlareDrill"]);
						revertStrName = "TileBoost";
						revertStr = "+" + item.tileBoost.ToString() + Language.GetTextValue("LegacyTooltip.54");     // Tile range boost
						insertAfterTooltip = ["TileBoost", "Material"];
						break;
					case ItemID.VortexDrill:
						lineReplacementRevertByName.Add(Items["VortexDrill"]);
						revertStrName = "TileBoost";
						revertStr = "+" + item.tileBoost.ToString() + Language.GetTextValue("LegacyTooltip.54");     // Tile range boost
						insertAfterTooltip = ["TileBoost", "Material"];
						break;
					case ItemID.NebulaDrill:
						lineReplacementRevertByName.Add(Items["NebulaDrill"]);
						revertStrName = "TileBoost";
						revertStr = "+" + item.tileBoost.ToString() + Language.GetTextValue("LegacyTooltip.54");     // Tile range boost
						insertAfterTooltip = ["TileBoost", "Material"];
						break;
					case ItemID.StardustDrill:
						lineReplacementRevertByName.Add(Items["StardustDrill"]);
						revertStrName = "TileBoost";
						revertStr = "+" + item.tileBoost.ToString() + Language.GetTextValue("LegacyTooltip.54");     // Tile range boost
						insertAfterTooltip = ["TileBoost", "Material"];
						break;
					case ItemID.MiningHelmet:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.MiningHelmet"));
						lineReplacementTotal.Add(Armor["MiningHelmet"]);
						break;
					case ItemID.ShadowOrb:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.ShadowOrb"));
						lineReplacementTotal.Add(Items["ShadowOrb"]);
						break;
					case ItemID.CrimsonHeart:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.CrimsonHeart"));
						lineReplacementTotal.Add(Items["CrimsonHeart"]);
						break;
					case ItemID.JellyfishNecklace:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.JellyfishNecklace"));
						lineReplacementTotal.Add(Accs["JellyfishNecklace"]);
						break;
					case ItemID.MagicLantern:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.MagicLantern"));
						lineReplacementTotal.Add(Items["MagicLantern"]);
						break;
					case ItemID.UltrabrightHelmet:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.UltrabrightHelmet"));
						lineReplacementTotal.Add(Armor["UltrabrightHelmet"]);
						break;
					case ItemID.DivingHelmet:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.DivingHelmet"));
						lineReplacementTotal.Add(Accs["DivingHelmet"]);
						break;
					case ItemID.JellyfishDivingGear:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.JellyfishDivingGear"));
						lineReplacementTotal.Add(Accs["JellyfishDivingGear"]);
						break;
					case ItemID.ArcticDivingGear:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.ArcticDivingGear"));
						lineReplacementTotal.Add(Accs["ArcticDivingGear"]);
						break;
					case ItemID.FairyBell:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.FairyBell"));
						lineReplacementTotal.Add(Items["FairyBell"]);
						break;
					case ItemID.DD2PetGhost:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.DD2PetGhost"));
						lineReplacementTotal.Add(Items["DD2PetGhost"]);
						break;
					case ItemID.WispinaBottle:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.WispinaBottle"));
						lineReplacementTotal.Add(Items["WispinaBottle"]);
						break;
					case ItemID.SuspiciousLookingTentacle:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.SuspiciousLookingTentacle"));
						lineReplacementTotal.Add(Items["SuspiciousLookingTentacle"]);
						break;
					case ItemID.GolemPetItem:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.GolemPetItem"));
						lineReplacementTotal.Add(Items["GolemPetItem"]);
						break;
					case ItemID.FairyQueenPetItem:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.FairyQueenPetItem"));
						lineReplacementTotal.Add(Items["FairyQueenPetItem"]);
						break;
					case ItemID.PumpkingPetItem:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.PumpkingPetItem"));
						lineReplacementTotal.Add(Items["PumpkingPetItem"]);
						break;
					case ItemID.NeptunesShell:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.NeptunesShell"));
						lineReplacementTotal.Add(Accs["NeptunesShell"]);
						break;
					case ItemID.MoonShell:
						lineReplacementTotal.Add(Language.GetTextValue("ItemTooltip.MoonShell"));
						lineReplacementTotal.Add(Accs["MoonShell"]);
						break;
				}

				//// Shared descriptions in a family of similar items
				if (item.healLife > 0) {
					for (int i = 0; i < tooltips.Count; i++) {
						if (tooltips[i].Name.Equals("HealLife")) {
							tooltips[i].Text = String.Format(Potions["HealingPotions"], item.healLife);
						}
					}
				}
				if (campfireFamily.Contains(item.type)) {
					lineReplacementTotal.Add(Common["Campfire"]);
				}
				if (tombstoneFamily.Contains(item.type)) {
					lineReplacementRevertByName.Add(Common["Tombstone"]);
					revertStrName = "Material";
					revertStr = Language.GetTextValue("LegacyTooltip.36");      // Material
					insertAfterTooltip = ["Material"];
				}
				if (nonConsumableFamily.Contains(item.type)) {
					for (int i = tooltips.Count - 1; i >= 0; i--)
						if (tooltips[i].Name.Equals("Tooltip0")) {
							tooltips[i].Text = tooltips[i].Text.Split('\n')[0];
							break;
						}
					tooltips.Add(new TooltipLine(Mod, "NotConsumable", Common["NotConsumable"]));
				}
				if (forbiddenArmorFamily.Contains(item.type)) {
					for (int i = tooltips.Count - 1; i >= 0; i--)
						if (tooltips[i].Name.Equals("SetBonus")) {
							tooltips[i].Text = tooltips[i].Text.Split('\n')[0] + '\n' + Armor["ForbiddenArmorBonus"];
							break;
						}
				}

				if (dodgeItemsFamily.Contains(item.type)) {
					string dodgeRepStrName = "";
					switch (item.type) {
						case ItemID.BlackBelt:
							dodgeRepStrName = "Tooltip0";
							break;
						case ItemID.MasterNinjaGear:
							dodgeRepStrName = "Tooltip1";
							break;
						case ItemID.BrainOfConfusion:
							dodgeRepStrName = "Tooltip0";
							break;
					}
					if (dodgeRepStrName.Length != 0)
						repDodgeTooltip(ref tooltips,
										dodgeRepStrName,
										Common["DodgeReworkTooltip"]);
				}

				if (hookFamily.Contains(item.type)) {
					repHookTooltip(ref tooltips,
								   "Equipable",
								   Language.GetTextValue("LegacyTooltip.23"),   // Equipable
								   Common["HookStats"]);
				}

				if (item.wingSlot != -1 && item.ModItem == null) {
					List<string> wingsEffects = new List<string>();
					switch (item.type) {
						case ItemID.AngelWings:
							wingsEffects.AddRange(Wings["Angel"].Split('\n'));
							break;
						case ItemID.DemonWings:
							wingsEffects.AddRange(Wings["Demon"].Split('\n'));
							break;
						case ItemID.FairyWings:
							wingsEffects.AddRange(Wings["Fairy"].Split('\n'));
							break;
						case ItemID.FinWings:
							wingsEffects.AddRange(Wings["Fin"].Split('\n'));
							break;
						case ItemID.ButterflyWings:
							wingsEffects.AddRange(Wings["Butterfly"].Split('\n'));
							break;
						case ItemID.HarpyWings:
							wingsEffects.AddRange(Wings["Harpy"].Split('\n'));
							break;
						case ItemID.FlameWings:
							wingsEffects.AddRange(Wings["Flame"].Split('\n'));
							break;
						case ItemID.FrozenWings:
							wingsEffects.AddRange(Wings["Frozen"].Split('\n'));
							break;
						case ItemID.BatWings:
							wingsEffects.AddRange(Wings["Bat"].Split('\n'));
							break;
						case ItemID.BeeWings:
							wingsEffects.AddRange(Wings["Bee"].Split('\n'));
							break;
						case ItemID.BoneWings:
							wingsEffects.AddRange(Wings["Bone"].Split('\n'));
							break;
						case ItemID.GhostWings:
							wingsEffects.AddRange(Wings["Ghost"].Split('\n'));
							break;
						case ItemID.LeafWings:
							wingsEffects.AddRange(Wings["Leaf"].Split('\n'));
							break;
						case ItemID.Hoverboard:
							wingsEffects.AddRange(Wings["Hoverboard"].Split('\n'));
							break;
						case ItemID.MothronWings:
							wingsEffects.AddRange(Wings["Mothron"].Split('\n'));
							break;
						case ItemID.BeetleWings:
							wingsEffects.AddRange(Wings["Beetle"].Split('\n'));
							break;
						case ItemID.SteampunkWings:
							wingsEffects.AddRange(Wings["Steampunk"].Split('\n'));
							break;
						case ItemID.TatteredFairyWings:
							wingsEffects.AddRange(Wings["TatteredFairy"].Split('\n'));
							break;
						case ItemID.SpookyWings:
							wingsEffects.AddRange(Wings["Spooky"].Split('\n'));
							break;
						case ItemID.FestiveWings:
							wingsEffects.AddRange(Wings["Festive"].Split('\n'));
							break;
						case ItemID.WingsSolar:
							wingsEffects.AddRange(Wings["Solar"].Split('\n'));
							break;
						case ItemID.WingsVortex:
							wingsEffects.AddRange(Wings["Vortex"].Split('\n'));
							break;
						case ItemID.WingsNebula:
							wingsEffects.AddRange(Wings["Nebula"].Split('\n'));
							break;
						case ItemID.WingsStardust:
							wingsEffects.AddRange(Wings["Stardust"].Split('\n'));
							break;
					}
					string wingRevertStrName = item.type == ItemID.BetsyWings ? "Equipable" : "Tooltip0";
					string wingRevertStr = Language.GetTextValue(item.type == ItemID.BetsyWings ? "LegacyTooltip.23" : "CommonItemTooltip.FlightAndSlowfall");
					if (devWingsFamily.Contains(item.type)) {
						wingRevertStrName = "Tooltip0";
						wingRevertStr = Language.GetTextValue("CommonItemTooltip.DevItem");
					}
					repWingTooltip(ref tooltips,
								wingRevertStrName,
								wingRevertStr,
								Common["WingsStats"],
								wingsEffects);
				}

				if (lineReplacementTotal.Count > 0) repTooltipTotal(ref tooltips, lineReplacementTotal);
				if (lineReplacementRevertByName.Count > 0 && revertStr != "" && insertAfterTooltip.Count != 0)
					repTooltipRevert(ref tooltips, lineReplacementRevertByName, revertStrName, revertStr, insertAfterTooltip);
				int ind = 0;
				while (ind < tooltips.Count && !tooltips[ind].Name.ToLower().Contains("tooltip")) ind++;
				while (ind < tooltips.Count && tooltips[ind].Name.ToLower().Contains("tooltip")) ind++;

				int count = 0;
				foreach (string str in appendAfter) {
					tooltips.Insert(ind + count, new TooltipLine(Mod, "Tooltip" + count.ToString(), str));
					count++;
				}
			}

			//// General replacements. Can be present in any many items along with other replacements or are seed specific changes
			List<(string, string)> generalReplacementsByname = [
				("Master", MiscStrings["OrRev"]),
				("CalamityDev", MiscStrings["DevItem"]),
				("CalamityDonor", MiscStrings["DonorItem"]),
				("PointBlankShot", MiscStrings["PointBlankShot"])
			];
			repTooltips(ref tooltips, generalReplacementsByname);

			for (int i = tooltips.Count - 1; i >= 0; i--) {
				if (tooltips[i].Name.Equals("CalamityCharge")) {
					string charge = Regex.Match(tooltips[i].Text, @"([\d\.\,]+%)").Value;
					tooltips[i].Text = MiscStrings["DraedonCharge"] + charge;
					break;
				}
			}

			if (PrefixID.Hard <= item.prefix && item.prefix <= PrefixID.Warding) {
				for (int i = 0; i < tooltips.Count; i++) {
					if (tooltips[i].Name == "PrefixAccDefense") {
						//Console.WriteLine(tooltips[i].Text);
						string drString = tooltips[i].Text.Split('\n')[1];
						tooltips[i].Text = tooltips[i].Text.Split('\n')[0];
						string drAmount = Regex.Match(drString, @"(\+[\d\.\,]+%)").Value;
						tooltips[i].Text += '\n' + drAmount + " " + MiscStrings["DamageReductionPercent"];
						break;
					}
				}
			}
			if (item.prefix == PrefixID.Lucky) {
				for (int i = 0; i < tooltips.Count; i++) {
					if (tooltips[i].Name == "PrefixAccCritChance") {
						//Console.WriteLine(tooltips[i].Text);
						string luckString = tooltips[i].Text.Split('\n')[1];
						tooltips[i].Text = tooltips[i].Text.Split('\n')[0];
						string luckAmount = Regex.Match(luckString, @"(\+[\d\.\,]+)").Value;
						tooltips[i].Text += '\n' + luckAmount + " " + MiscStrings["LuckValue"];
						break;
					}
				}
			}

			if (Main.zenithWorld) {
				List<(string, string)> stringZenithReplacement = [];
				stringZenithReplacement.Add((MiscStrings["ZenithRogueBefore"], MiscStrings["ZenithRogueAfter"]));
				stringZenithReplacement.Add((MiscStrings["ZenithrogueBefore"], MiscStrings["ZenithrogueAfter"]));

				for (int i = 0; i < tooltips.Count; i++) {
					foreach ((string, string) tup in stringZenithReplacement)
						tooltips[i].Text = tooltips[i].Text.Replace(tup.Item1, tup.Item2);
				}
			}
			base.ModifyTooltips(item, tooltips);
		}
	}

	internal class CalamityVanillaNPCNamesOverrides : GlobalNPC {
		static List<(string, string)> AnglerNamesReps = [
			("Dazren", "Дазрен"),
			("Johnny Test", "Джонни Тест"),
			("Bling Bling Boy", "Блинг-Блинг Бой"),
			("RICE", "РАЙС")
		];
		static List<(string, string)> ArmsDealerNamesReps = [
			("Drifter", "Дрифтер"),
			("Finchi", "Финчи"),
			("Heniek", "Хайнек"),
			("Fire", "Файр"),
			("Barney Calhoun", "Барни Калхун"),
			("XiaoEn0426", "Чао Ен-0426"),
			("Jeffred", "Джеффред"),
			("The Cooler Arthur", "Артур, но покруче"),
			("Shark", "Шарк"),
			("Sagi", "Саги")
		];
		static List<(string, string)> ClothierNamesReps = [
			("Joeseph Jostar", "Джозеф Джостар"),
			("Storm Havik", "Хавик «Гроза»")
		];
		static List<(string, string)> CyborgNamesReps = [
			("Sylux", "Силукс"),
			("Nemesis", "Немезис")
		];
		static List<(string, string)> DemolitionistNamesReps = [
			("Tavish DeGroot", "Тавиш Де Грут"),
			("Fimmy", "Фимми"),
			("Dorira", "Дорира"),
			("John Helldiver", "Джон Хеллдайвер")
		];
		static List<(string, string)> DryadNamesReps = [
			("Rythmi", "Ритми"),
			("Izuna", "Изуна"),
			("Jasmine", "Жасмин"),
			("Cybil", "Цибил"),
			("Ruth", "Рют")
		];
		static List<(string, string)> GoblinTinkererNamesReps = [
			("Verth", "Верт"),
			("Gormer", "Гормер"),
			("TingFlarg", "Тинг-Фларг"),
			("Driser", "Дрисер"),
			("Eddie Spaghetti", "Эдди-Спагетти"),
			("G'tok", "Г'Ток"),
			("Katto", "Катто"),
			("Him", "Он"),
			("Tooshiboots", "Тошибатинки"),
			("Neesh", "Нииш"),
			("Bars Boldia", "Барс Болдия"),
			("Basel Raiden John Clive Fantasy 16", "Базель Раейден Джон Клайв Фэнтези 16")
		];
		static List<(string, string)> GuideNamesReps = [
			("Lapp", "Лапп"),
			("Ben Shapiro", "Бен Шапиро"),
			("Streakist", "Стрикист"),
			("Necroplasmic", "Некроплазмоид"),
			("Devin", "Девин"),
			("Woffle", "Воффл"),
			("Cameron", "Камерун"),
			("Wilbur", "Уилбур"),
			("Good Game Design", "Хороший Игровой Дизайн"),
			("Danmaku", "Данмаку"),
			("Grylken", "Грилкен"),
			("Outlaw", "Аутлоу"),
			("Alfred Rend", "Альфред Ренд"),
			("Leeman", "Лииманн"),
			("Mihai", "Михай"),
			("Dinkleberg", "Динклберг"),
			("Wamy", "Уами")
		];
		static List<(string, string)> MechanicNamesReps = [
			("Lilly", "Лилия"),
			("Daawn", "Дон"),
			("Robin", "Робин"),
			("Curly", "Керли"),
			("Cobalt", "Кобальт")
		];
		static List<(string, string)> MerchantNamesReps = [
			("Morshu", "Моршу"),
			("Spamton G. Spamton", "СПАМТОН Г. СПАМТОН (#1 Продавец1997)")
		];
		static List<(string, string)> NurseNamesReps = [
			("Farsni", "Фарзни"),
			("Fanny", "Фэнни"),
			("Mausi", "Маузи"),
			("Fiona", "Фиона")
		];
		static List<(string, string)> PainterNamesReps = [
			("Picasso", "Пикассо"),
			("Bew", "Бью")
		];
		static List<(string, string)> PartyGirlNamesReps = [
			("Arin", "Эрин"),
			("Typhäne", "Тифани")
		];
		static List<(string, string)> PirateNamesReps = [
			("Tyler Van Hook", "Тайлер «Ван Крюк»"),
			("Cap'n Deek", "Капитан Дик"),
			("Captain Billy Bones", "Капитан Билли Бонс"),
			("Captain J. Crackers", "Капитан Дж. Крэкерс"),
			("Gol D. Roger", "Гол Д. Роджер")
		];
		static List<(string, string)> PrincessNamesReps = [
			("Catalyst", "Каталист"),
			("Nyapano", "Няпано"),
			("Jade", "Джейд"),
			("Nyavi Aceso", "Няви Ацесо"),
			("everquartz", "еверкварц"),
			("Gwynevere", "Гвинивер"),
			("Hael", "Хаель"),
			("Yumesaki Mirrin", "Юмесаки Миррин")
		];
		static List<(string, string)> SantaClausNamesReps = [
			("Jank", "Хлам")
		];
		static List<(string, string)> SkeletonMerchantNamesReps = [
			("Sans Undertale", "Санс Андертейл"),
			("Papyrus Undertale", "Папирус Андертейл"),
			("Gaster Undertale", "Гастер Андертейл"),
			("Mr. Bones", "Мистер Бонс"),
			("Freakbob", "Фрикбоб")
		];
		static List<(string, string)> SteampunkerNamesReps = [
			("Vorbis", "Ворбис"),
			("Angel", "Анджела"),
			("Mòrag Ladair", "Мораг Ладаир"),
			("Linn", "Линн"),
			("Eira", "Айра")
		];
		static List<(string, string)> StylistNamesReps = [
			("Amber", "Амбер"),
			("Faith", "Вера"),
			("Xsiana", "Ксиана"),
			("Lain", "Лаина"),
			("Hamis", "Хамис")
		];
		static List<(string, string)> TavernkeepNamesReps = [
			("Tim Lockwood", "Тим Локвуд"),
			("Sir Samuel Winchester Jenkins Kester II", "Сэр Самуел Винчестер Дженкинс Кестер II"),
			("Brutus", "Брут"),
			("Sloth", "Слоф")
		];
		static List<(string, string)> TaxCollectorNamesReps = [
			("Emmett", "Эметт"),
			("Casino King Gray", "«Казино Кинг Грей»")
		];
		static List<(string, string)> TravelingMerchantNamesReps = [
			("Stan Pines", "Стэн Пайнс"),
			("Slap Battles", "Слап Баттлз"),
			("Borgus", "Борг")
		];
		static List<(string, string)> TruffleNamesReps = [
			("Aldrimil", "Алдримил"),
			("Wonton", "Вонтон")
		];
		static List<(string, string)> WitchDoctorNamesReps = [
			("Sok'ar", "Сокк'Ар"),
			("Aeroni", "Эйрони"),
			("Mixcoatl", "Микскоатль"),
			("Amnesia Wapers", "«Амнезийный» Уэйперз")
		];
		static List<(string, string)> WizardNamesReps = [
			("Inorim, son of Ivukey", "Инорим, сын Ивукея"),
			("Jensen", "Йенсен"),
			("Merasmus", "МЕРАЗМУС"),
			("Habolo", "Габоло"),
			("Ortho", "Орто"),
			("Chris Tallballs", "Крис «Большие шары»"),
			("Syethas", "Сьетес"),
			("Nextdoor Psycho", "Псих по соседству")
		];
		static List<(string, string)> ZoologistNamesReps = [
			("Kiriku", "Кирику"),
			("Lacuna", "Лакуна"),
			("Mae Borowski", "Мая Боровская"),
			("Fera", "Ферра")
		];
		static List<(string, string)> CalamityBanditNamesReps = [
			("Xplizzy", "Экс-Плиззи"),
			("Freakish", "Фрикиш"),
			("Calder", "Кальдер"),
			("Hunter Jinx", "Охотник «Сглаз»"),
			("Goose", "Гуз"),
			("Jackson", "Джексон"),
			("Altarca", "Алтарка"),
			("Jackie", "Джеки"),
			("Ishmael", "Ишмаил"),
			("Laura", "Лара"),
			("Mie", "Мия"),
			("Bonnie", "Бони"),
			("Sarah", "Сара"),
			("Diane", "Диана"),
			("Kate", "Катя"),
			("Penelope", "Пенелопа"),
			("Marisa", "Мариса"),
			("Maribel", "Марибель"),
			("Valerie", "Валерия"),
			("Jessica", "Джессика"),
			("Rowan", "Роуэн"),
			("Jessie", "Джесси"),
			("Jade", "Джейд"),
			("Hearn", "Хирн"),
			("Amber", "Амбер"),
			("Anne", "Анна"),
			("Indiana", "Индиана")
		];
		static List<List<(string, string)>> TownDogVariationNamesReps = [
			// Labrador
			[
				("Riley", "Райли")
			],
			// PitBull
			[
				("Splinter", "Сплинтер")
			],
			// Beagle
			[
				("Kendra", "Кендра")
			],
			// Corgi
			[],
			// Dalmatian
			[
				("Ozymandias", "Озимандий"),
				("Miss Throws a Lot", "Мисс «Много Блюет»"),
				("Brikwilla", "Бриквилла"),
				("Riley", "Райли"),
				("Splinter", "Сплинтер"),
				("Kendra", "Кендра"),
				("Yoshi", "Йоши")
			],
			// Husky
			[
				("Yoshi", "Йоши")
			]
		];
		static List<List<(string, string)>> TownCatVariationNamesReps = [
			// Siamese
			[],
			// Black
			[
				("Bear", "Бэр"),
				("Storm", "Шторм"),
			],
			// Orange Tabby
			[
				("Felix", "Феликс"),
				("Tardo", "Тардо")
			],
			// Russian Blue
			[],
			// Silver
			[
				("Archie", "Арчи")
			],
			// White
			[
				("Smoogle", "Смугул"),
				("The Meowurer of Gods", "Помяукиватель Богов"),
				("Katsafaros", "Котсафар"),
				("Lucerne", "Люцерн"),
				("Milo", "Милла"),
				("Octo", "Окто"),
				("Chease", "Чииз")
			]
		];
		static List<List<(string, string)>> TownBunnyVariationNamesReps = [
			// White
			[
				("Poco", "Поко")
			],
			// Angora
			[],
			// Dutch
			[],
			// Flemish
			[],
			// Lop
			[],
			// Silver
			[]
		];
		public override void ModifyNPCNameList(NPC npc, List<string> nameList) {
			if (npc.townNPC) {
				//Console.WriteLine("List of names: ");
				//foreach (string name in nameList)
				//	Console.WriteLine("\t" + name);
				//Console.WriteLine("ID is {0} and variation is {1}", npc.type, npc.townNpcVariationIndex);
				List<(string, string)> namesToChange = [];
				switch (npc.type) {
					case NPCID.Angler:
						namesToChange = AnglerNamesReps;
						break;
					case NPCID.ArmsDealer:
						namesToChange = ArmsDealerNamesReps;
						break;
					case NPCID.Clothier:
						namesToChange = ClothierNamesReps;
						break;
					case NPCID.Cyborg:
						namesToChange = CyborgNamesReps;
						break;
					case NPCID.Demolitionist:
						namesToChange = DemolitionistNamesReps;
						break;
					case NPCID.Dryad:
						namesToChange = DryadNamesReps;
						break;
					case NPCID.GoblinTinkerer:
						namesToChange = GoblinTinkererNamesReps;
						break;
					case NPCID.Guide:
						namesToChange = GuideNamesReps;
						break;
					case NPCID.Mechanic:
						namesToChange = MechanicNamesReps;
						break;
					case NPCID.Merchant:
						namesToChange = MerchantNamesReps;
						break;
					case NPCID.Nurse:
						namesToChange = NurseNamesReps;
						break;
					case NPCID.Painter:
						namesToChange = PainterNamesReps;
						break;
					case NPCID.PartyGirl:
						namesToChange = PartyGirlNamesReps;
						break;
					case NPCID.Pirate:
						namesToChange = PirateNamesReps;
						break;
					case NPCID.Princess:
						namesToChange = PrincessNamesReps;
						break;
					case NPCID.SantaClaus:
						namesToChange = SantaClausNamesReps;
						break;
					case NPCID.SkeletonMerchant:
						namesToChange = SkeletonMerchantNamesReps;
						break;
					case NPCID.Steampunker:
						namesToChange = SteampunkerNamesReps;
						break;
					case NPCID.Stylist:
						namesToChange = StylistNamesReps;
						break;
					case NPCID.DD2Bartender:
						namesToChange = TavernkeepNamesReps;
						break;
					case NPCID.TaxCollector:
						namesToChange = TaxCollectorNamesReps;
						break;
					case NPCID.TravellingMerchant:
						namesToChange = TravelingMerchantNamesReps;
						break;
					case NPCID.Truffle:
						namesToChange = TruffleNamesReps;
						break;
					case NPCID.WitchDoctor:
						namesToChange = WitchDoctorNamesReps;
						break;
					case NPCID.Wizard:
						namesToChange = WizardNamesReps;
						break;
					case NPCID.BestiaryGirl:
						namesToChange = ZoologistNamesReps;
						break;
					case NPCID.TownDog:
						namesToChange = TownDogVariationNamesReps[npc.townNpcVariationIndex];
						//Console.WriteLine("Chosen names for town pet:");
						//foreach ((string, string) tup in namesToChange)
						//	Console.WriteLine("{0} -> {1}", tup.Item1, tup.Item2);
						break;
					case NPCID.TownCat:
						namesToChange = TownCatVariationNamesReps[npc.townNpcVariationIndex];
						//Console.WriteLine("Chosen names for town pet:");
						//foreach ((string, string) tup in namesToChange)
						//	Console.WriteLine("{0} -> {1}", tup.Item1, tup.Item2);
						break;
					case NPCID.TownBunny:
						namesToChange = TownBunnyVariationNamesReps[npc.townNpcVariationIndex];
						//Console.WriteLine("Chosen names for town pet:");
						//foreach ((string, string) tup in namesToChange)
						//	Console.WriteLine("{0} -> {1}", tup.Item1, tup.Item2);
						break;
					case 988:       // Calamity Permafrost
						namesToChange = [("Permafrost", "Мерзлота")];
						break;
					case 989:       // Calamity Amidias
						namesToChange = [("Amidias", "Амидиас")];
						break;
					case 990:       // Calamity Bandit
						namesToChange = CalamityBanditNamesReps;
						break;
					case 991:       // Calamity Calamitas
						namesToChange = [("Calamitas", "Каламитас")];
						break;
				}

				foreach ((string, string) nameRep in namesToChange) {
					nameList.Remove(nameRep.Item1);
					nameList.Add(nameRep.Item2);
				}
			}
			base.ModifyNPCNameList(npc, nameList);
		}
	}
}