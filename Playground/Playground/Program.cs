using System;

namespace Samples
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MilkShakeSamples game = new MilkShakeSamples())
            {
                game.Run();
            }
        }
    }
#endif
}

