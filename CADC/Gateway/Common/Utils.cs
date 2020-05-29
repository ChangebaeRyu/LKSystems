using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gateway
{
	class Utils
	{
		public static string subChar(string text, int len)
		{
			return text.PadRight(len, ' ');
		}

		public static byte[] GetBytes(string text)
		{
			return Encoding.ASCII.GetBytes(text);
		}

		public static String GetString(byte[] data, int startpos, int len)
		{
			byte[] retData = new byte[len];
			Array.Copy(data, startpos, retData, 0, len);
			string text = Encoding.ASCII.GetString(retData);
			return text;
		}
	}
}
