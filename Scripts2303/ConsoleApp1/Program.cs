using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsSqlSearchscripts
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            DB query = new DB();
            DOC doc = new DOC() { NAME = "Doc2"};
            Console.WriteLine(query.Add(doc));
            Console.ReadKey();
        }
    }
}
