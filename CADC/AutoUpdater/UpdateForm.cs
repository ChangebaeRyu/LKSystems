using ApplicationUpdate;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace AutoUpdater
{
	public partial class UpdateForm : Form
	{
		public UpdateForm()
		{
			InitializeComponent();
		}

		private void UpdateForm_Load(object sender, EventArgs e)
		{
			CompareVersions();
		}

		public void CompareVersions()
		{
			string localVersion = Versions.LocalVersion(CommConst.downloadPath + "\\" + CommConst.localVerTxt);
			string remoteVersion = Versions.RemoteVersion(CommConst.remoteURL + CommConst.remoteVerTxt);
			string remoteFile = CommConst.remoteURL + remoteVersion + ".zip";

			if (localVersion != remoteVersion)
			{
				BeginDownload(remoteFile, CommConst.downloadPath, remoteVersion, CommConst.executeFile, CommConst.localVerTxt);
			}
			else
			{
				MessageBox.Show("최신 버전 입니다.", "알림");
				Close();
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
			string downloadPath = us[1];
			string executeTarget = us[2];
			string localVersion = us[3];

			if (!downloadPath.EndsWith("\\")) // Give a trailing \ if there isn't one
				downloadPath += "\\";

			string zipName = downloadPath + currentVersion + ".zip"; // Download folder + zip file
			string sourcePath = downloadPath + currentVersion;         // Download Path
			string targetPath = CommConst.executePath;                 // 실행폴더
			string exePath = targetPath + executeTarget; // Download folder\version\ + executable

			if (new FileInfo(zipName).Exists)
			{
				using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(zipName))
				{
					zip.ExtractAll(downloadPath + currentVersion,
						Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
				}

				CopyFolder(sourcePath, targetPath);

				if (new FileInfo(exePath).Exists)
				{
					Versions.CreateLocalVersionFile(downloadPath, localVersion, currentVersion);
					// 집진기 Controller 종료
					Process[] psList = Process.GetProcessesByName("CADC");
					if (psList.Length > 0)
					{
						psList[0].Kill();
					}

					//MessageBox.Show("Path : " + exePath);
					Process.Start(exePath);
					Close();
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

		private void CopyFolder(string sourcePath, string targetPath)
		{
			string filename;
			string destFile;
			string[] files = Directory.GetFiles(sourcePath);
			foreach(string s in files)
			{
				filename = Path.GetFileName(s);
				destFile = Path.Combine(targetPath, filename);
				File.Copy(s, destFile, true);
			}
		}
	}
}
