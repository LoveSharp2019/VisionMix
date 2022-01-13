﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Yoga.Common.VBAEngine
{
	internal class HotKeyManager {

		public static bool Enable = true;

		public static void AddHotKey(Form form, Action function, Keys key, bool ctrl = false, bool shift = false, bool alt = false) {
			form.KeyPreview = true;

			form.KeyDown += delegate(object sender, KeyEventArgs e) {
				if (IsHotkey(e, key, ctrl, shift, alt)) {
					function();
				}
			};
		}

		public static bool IsHotkey(KeyEventArgs eventData, Keys key, bool ctrl = false, bool shift = false, bool alt = false) {
			return eventData.KeyCode == key && eventData.Control == ctrl && eventData.Shift == shift && eventData.Alt == alt;
		}


	}
}
