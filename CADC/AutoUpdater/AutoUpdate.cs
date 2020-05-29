using ApplicationUpdate;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace AutoUpdater
{
	public class AutoUpdate
    {
		public void CompareVersions()
		{
			string localVersion = Versions.LocalVersion(CommConst.downloadPath + "\\" + CommConst.localVerTxt);
			string remoteVersion = Versions.RemoteVersion(CommConst.remoteURL + CommConst.remoteVerTxt);
			string remoteFile = CommConst.remoteURL + remoteVersion + ".zip";

			if (localVersion != remoteVersion)
			{
				BeginDownload(remoteFile, CommConst.downloadPath, remoteVersion, CommConst.executeFile, CommConst.localVerTxt);
			}
		}

		private void BeginDownload(string remoteURL, string downloadToPath, string version, string executeTarget, string localVersion)
		{
			string filePath = Versions.CreateTargetLocation(downloadToPath, version);

			Uri remoteURI = new Uri(remoteURL);
			WebClient downloader = new WebClient();

			downloader.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(downloader_DownloadFileCompleted);

			downloader.DownloadFileAsync(remoteURI, filePath + ".zip",
				new string[] { version, downloadToPath, executeTarget, localVersion });
		}


		void downloader_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			string[] us = (string[])e.UserState;
			string currentVersion = us[0];
			string downloadToPath = us[1];
			string executeTarget = us[2];
			string localVersion = us[3];

			if (!downloadToPath.EndsWith("\\")) // Give a trailing \ if there isn't one
				downloadToPath += "\\";

			string zipName = downloadToPath + currentVersion + ".zip"; // Download folder + zip file
			string exePath = downloadToPath + currentVersion + "\\" + executeTarget; // Download folder\version\ + executable

			if (new System.IO.FileInfo(zipName).Exists)
			{
				using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(zipName))
				{
					zip.ExtractAll(downloadToPath + currentVersion,
						Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
				}
				if (new System.IO.FileInfo(exePath).Exists)
				{
					Versions.CreateLocalVersionFile(downloadToPath, localVersion, currentVersion);
					// 집진기 Controller 종료
					Process[] psList = Process.GetProcessesByName("CADC");
					if (psList.Length > 0)
					{
						//psList[0].Kill();
						Application.Restart();
						Environment.Exit(0);
					}
					else
					{
						Process.Start("CADC.exe");
					}

				}
				else
				{
					//Debug.Write("Problem with download. File does not exist.");
					MessageBox.Show("Problem with download. File does not exist.", "Updater");
				}
			}
			else
			{
				//Debug.Write("Problem with download. File does not exist.");
				MessageBox.Show("Problem with download. File does not exist.", "Updater");
			}
		}
	}
}
