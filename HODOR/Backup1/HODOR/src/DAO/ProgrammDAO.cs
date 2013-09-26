using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class ProgrammDAO
    {
        public static List<Programm> getAllProgramme()
        {
            var programmeQueryResult = from p in HodorGlobals.getHodorContext().Programms.Select(p => p) select p;
            return programmeQueryResult.ToList<Programm>();
        }

        public static List<Programm> getProgrammeWithNameContaining(String namePart)
        {
            if (namePart == null || namePart.Length == 0) return null;
            var programmeQueryResult = from p in HodorGlobals.getHodorContext().Programms.Where(p => p.Name.Contains(namePart)) select p;

            return programmeQueryResult.ToList<Programm>();
        }
    }
}