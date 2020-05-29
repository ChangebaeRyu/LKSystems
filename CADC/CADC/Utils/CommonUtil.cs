using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CADC.Utils
{
	class CommonUtil
    {
        // String Null 체크
        public static bool IsNull(String s)
        {
            if (s == null || s.Equals("")) return true;
            return false;
        }

		// 소수점 이하 몇자리까지 있는지 return한다.(Max 소수점 2자리까지만 계산)
		public static int GetDecimalPoint(float fStep)
		{
			int cvt = (int)(Math.Round(fStep, 3) * 1000);
			if (cvt % 1000 == 0) return 0;
			else if (cvt % 100 == 0) return 1;
			else if (cvt % 10 == 0) return 2;
			else return 3;
		}

		public static int String2Int(string s)
        {
            const int DEFAULT = 0;
            int result;
            if (int.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return DEFAULT;
            }
        }

        public static float String2Float(string s, int decimalPoint = 2)
        {
            int dValue = (int)Math.Pow(10, decimalPoint);
            float reselt = 0;
            int cValue = 0;
            if(float.TryParse(s, out reselt))
            {
                cValue = (int)Math.Round(reselt * dValue);
                reselt = (float)Math.Round((float)cValue / dValue, 2);
                return reselt;
            }
            else
            {
                return 0;
            }
        }

        // Byte Array to Hex String
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2} ", b);
            return hex.ToString();
        }

        public static void ShowKeyboard()
        {
            try
            {
				OperatingSystem os = Environment.OSVersion;
				int osVer = os.Version.Major * 10 + os.Version.Minor;
				////MessageBox.Show(string.Format("OS Version : {0} Version Convert : {1}", os.Version, osVer), "시스템 정보");
				if (osVer <= 61)
				{
					// windows 7 가상키보드
					var path64 = @"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_6.1.7600.16385_none_06b1c513739fb828\osk.exe";
					var path32 = @"C:\windows\system32\osk.exe";
					var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
					Process.Start(path);
				}
				else
				{
					// windows 10 가상키보드
					var path = @"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_10.0.18362.449_none_0098d787eb84df09\osk.exe";
					Process.Start(path);
				}
			}
            catch(Exception ex)
            {
                MessageBox.Show("가상키보드 프로그램을 찾을 수 없습니다. exception : " + ex.ToString(), "키보드 오류");
            }
        }

		///// <summary>
		///// 시작프로그램 Registry 등록
		///// </summary>
		//public static void AddRegistry()
		//{
		//	try
		//	{
		//		string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
		//		RegistryKey strUpKey = Registry.LocalMachine.OpenSubKey(runKey);
		//		if(strUpKey.GetValue("CADC") == null)
		//		{
		//			strUpKey.Close();
		//			strUpKey = Registry.LocalMachine.OpenSubKey(runKey, true);
		//			// 시작프로그램 등록명과 exe경로를 레지스트리에 등록
		//			strUpKey.SetValue("CADC", Application.ExecutablePath.Replace('/', '\\'));
		//			MessageBox.Show("Add Startup Success");
		//		} else
		//		{
		//			MessageBox.Show("이미 등록되어 있음");
		//		}
		//	}
		//	catch
		//	{
		//		MessageBox.Show("Add Startup Fail");
		//	}
		//}

		///// <summary>
		///// 시작프로그램 Registry 삭제
		///// </summary>
		//public static void RemoveRegistry()
		//{
		//	try
		//	{
		//		string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
		//		RegistryKey strUpKey = Registry.LocalMachine.OpenSubKey(runKey, true);
		//		// 레지스트리값 제거
		//		strUpKey.DeleteValue("CADC");
		//		MessageBox.Show("Remove Startup Success");
		//	}
		//	catch
		//	{
		//		MessageBox.Show("Remove Startup Fail");
		//	}
		//}

		public static void CreateShortCut()
		{
			string ShortCutPath = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup";
			// 바탕화면 경로 string
			//var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
			//var desktopFolder2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			// 바탕화면 폴더정보
			DirectoryInfo shortCutDir = new DirectoryInfo(ShortCutPath);
			// 바로가기 파일명 지정(경로포함)
			string linkFileName = shortCutDir.FullName + "\\CADC.lnk";
			// 바로가기 파일 정보 생성
			FileInfo linkFile = new FileInfo(linkFileName);
			// 바로가기 파일이 있다면 종료
			if (linkFile.Exists) return;
			try
			{
				WshShell shell = new WshShell();
				IWshShortcut link = (IWshShortcut)shell.CreateShortcut(linkFile.FullName);
				// 원본파일 경로
				link.TargetPath = Application.ExecutablePath;
				link.WorkingDirectory = Application.StartupPath; // Path to the Original folder
				link.Save();
			}
			catch(Exception ex)
			{
				Debug.Write("바로가기 생성오류 : " + ex.ToString());
				//MessageBox.Show("바로가기 생성오류", "알림");
			}
		}

		/// <summary>
		/// Version Text로부터 Build된 일시를 구합니다.
		/// </summary>
		/// <returns></returns>
		public static DateTime getBuildDateTime()
		{
			//1. Assembly.GetExecutingAssembly().FullName의 값은 
			//'ApplicationName, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
			//와 같다. 
			string strVersionText = Assembly.GetExecutingAssembly().FullName
					.Split(',')[1]
					.Trim()
					.Split('=')[1];

			//2. Version Text의 세번째 값(Build Number)은 2000년 1월 1일부터 
			//Build된 날짜까지의 총 일(Days) 수 이다.
			int intDays = Convert.ToInt32(strVersionText.Split('.')[2]);
			DateTime refDate = new DateTime(2000, 1, 1);
			DateTime dtBuildDate = refDate.AddDays(intDays);

			//3. Verion Text의 네번째 값(Revision NUmber)은 자정으로부터 Build된
			//시간까지의 지나간 초(Second) 값 이다.
			int intSeconds = Convert.ToInt32(strVersionText.Split('.')[3]);
			intSeconds = intSeconds * 2;
			dtBuildDate = dtBuildDate.AddSeconds(intSeconds);


			//4. 시차조정
			DaylightTime daylingTime = TimeZone.CurrentTimeZone
					.GetDaylightChanges(dtBuildDate.Year);
			if (TimeZone.IsDaylightSavingTime(dtBuildDate, daylingTime))
				dtBuildDate = dtBuildDate.Add(daylingTime.Delta);


			return dtBuildDate;
		}

		public static void shutdownCom(int mode = 0)
		{
			if(mode == 0)
			{
				// 종료
				//Process.Start("shutdown.exe", "-s"); // 기본적으로 30초 후 종료
				Process.Start("shutdown.exe", "-s -t 2"); // xx 초 후 종료
			}
		}

	}
}
