using System;
using System.Linq;

namespace MonoHex {
    public static class Program {
        [STAThread]
        static void Main(string[] args) {
            ParseArgs(args);
            using (var game = new Main())
                game.Run();
        }

        private static void ParseArgs(string[] args) {
            if (args.Contains("--verbose")) { Constants.Verbosity += 1; }
        }
    }
}
