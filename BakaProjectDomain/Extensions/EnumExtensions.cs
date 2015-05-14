﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace BakaProjectDomain.Extensions {
	public static class EnumExtensions {
		public static T ParseEnum<T>(string value) {
			return (T)Enum.Parse(typeof(T), value, true);
		}

		public static string GetDescription(this Enum value) {
			Type type = value.GetType();
			string name = Enum.GetName(type, value);
			if (name != null) {
				FieldInfo field = type.GetField(name);
				if (field != null) {
					DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
					if (attr != null) {
						return attr.Description;
					}
				}
			}
			return null;
		}

	}
}