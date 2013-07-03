using System;
using System.Collections.Generic;
using System.IO;

namespace Onion.SolutionTransform.IO
{
    public static class DirectoryWalker
    {
        public static List<string> GetFiles(string path, string pattern = "*.*")
        {
            var accum = new List<string>();
            WalkDirectory(path, pattern, ref accum);
            return accum;
        } 

        private static void WalkDirectory(string path, string pattern, ref List<string> accum)
        {
            try
            {
                accum.AddRange(Directory.GetFiles(path, pattern));
                foreach (var d in Directory.GetDirectories(path))
                {
                    WalkDirectory(d, pattern, ref accum);
                }
            }
            catch (Exception)
            {
                //ignore exceptions
            }
        }
    }
}
