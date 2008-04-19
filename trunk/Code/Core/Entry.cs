/* bhs-proto
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using System;

namespace BHS.Core
{
    static class Entry
    {
        static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
}

