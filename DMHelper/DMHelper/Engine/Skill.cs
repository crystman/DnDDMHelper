using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMHelper.Engine
{
    public class Skill
    {

        public Proficiencies name { get; set; }
        public Trait trait { get; set; }
        public Proficiency level { get; set; }

        public Skill(Proficiencies _name, Trait _trait = Trait.NONE, Proficiency _level = Proficiency.NONE)
        {
            name = _name;
            trait = _trait;
            level = _level;
        }

        public bool isProficient()
        {
            return (level != Proficiency.NONE);
        }

        public int proficientcyMultiplier()
        {
            return (int)level;
        }

    }
}
