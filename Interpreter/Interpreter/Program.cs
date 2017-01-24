using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Interpreter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //initialize my lists
            Storage.stringNames = new List<string>();
            Storage.stringVars = new List<string>();
            Storage.intNames = new List<string>();
            Storage.intVars = new List<int>();
            Storage.doubleNames = new List<string>();
            Storage.doubleVars = new List<double>();
            Storage.boolNames = new List<string>();
            Storage.boolVars = new List<bool>();
            Storage.charNames = new List<string>();
            Storage.charVars = new List<char>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    class Storage
    {
        //this class holds everything I need for variable storage.
        static public List<string> stringNames;
        static public List<string> stringVars;

        static public List<string> intNames;
        static public List<int> intVars;

        static public List<string> doubleNames;
        static public List<double> doubleVars;

        static public List<string> boolNames;
        static public List<bool> boolVars;

        static public List<string> charNames;
        static public List<char> charVars;
    }
}
