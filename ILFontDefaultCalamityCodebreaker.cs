using CalamityMod;
using CalamityMod.DataStructures;
using CalamityMod.UI.DraedonSummoning;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModRuLocalization {

	[ExtendsFromMod("CalamityMod")]
	public class CalamityCrossmodSystem : ModSystem {
		public override void Load() {
			if (!Main.dedServ && Language.ActiveCulture.Name == "ru-RU")
				ApplyILEdits();
		}

		private void ApplyILEdits() {
			PropertyInfo font = typeof(CodebreakerUI).GetProperty("DialogFont", BindingFlags.Static | BindingFlags.Public);

			// This is probably heresy by tmodloader standarts, but who knew Reflection is so good?
			// I am simply resetting the font to the default teraria one, because cyrillics are not present in calamity yet
			// If time comes and cyrillics are still not in the calamity font, I will make my own
			font.SetValue(null, FontAssets.MouseText.Value);
		}
	}
}