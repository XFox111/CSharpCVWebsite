using System;
using System.Linq;

namespace MyWebsite
{
	public static class Extensions
	{
		public static bool Belongs<T>(this T item, params T[] array) =>
			array?.Contains(item) ?? false;
	}
}