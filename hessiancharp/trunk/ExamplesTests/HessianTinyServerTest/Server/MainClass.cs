using System;
using hessiancsharp.webserver;
using HessianNetTest.hessiancsharp.test;

namespace Server {
	/// <summary>
	/// Zusammenfassung f�r MainClass.
	/// </summary>
	internal class MainClass {
		/// <summary>
		/// Der Haupteinstiegspunkt f�r die Anwendung.
		/// </summary>
		[STAThread]
		private static void Main(string[] args) {
			CWebServer web = new CHessianWebServer();
            
			web.Paranoid = true;
			web.AcceptClient("[\\d\\s]");
			web.Run();
			Console.WriteLine("Server is starting...");
			while(!web.Running) {				
			}

			Console.WriteLine("Server is started");
			Console.WriteLine("Press any key to stop the server");
			for (;; ) {
				string e = Console.ReadLine();
				if (e != "") {
					web.Stop();
					break;
				}
			}
		}
	}
}