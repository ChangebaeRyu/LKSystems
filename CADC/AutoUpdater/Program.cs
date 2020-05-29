using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoUpdater
{
    static class Program
    {
		/// <summary>
		/// 해당 응용 프로그램의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//if (args.Length != 1 || !args[0].Equals("PUPG"))
			//{
			//	MessageBox.Show("단독실행이 불가능한 애플리케이션입니다.", "AutoUpdater");
			//	return;
			//}
			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UpdateForm());
        }
    }
}
