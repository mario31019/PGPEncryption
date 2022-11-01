using System.Diagnostics;

namespace WINTEST1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Debug.WriteLine("Main========================");
            Application.Run(new Form1());
        }
    }
}