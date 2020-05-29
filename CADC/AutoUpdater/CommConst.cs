using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoUpdater
{
    class CommConst
    {
		public const string executePath = "C:\\CADC\\";
		public const string downloadPath = executePath + "Downloads";
		public const string localVerTxt = "version.txt";
		public const string remoteURL = "http://localhost/AutoUpdate/";
		public const string remoteVerTxt = "updateVersion.txt";
		public const string remoteFile = remoteURL + remoteVerTxt + ".zip";
		public const string executeFile = "CADC.exe";
	}
}
