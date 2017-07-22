using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMHelper.Engine
{
    public class Being
    {
        public string name { get; private set; }
        public int proficiencyBonus { get; private set; }
        public int perception { get; set; }
        public Health health;
        private Dictionary<Trait, int> statistics;
        public List<Item> items;
        public int armorClass;
        public int speed;
        public HitDice hitDie { get; set; }

        public Being(string _name, HitDice _hitDie, int _proficiencyBonus, int _perception, int _armorClass, int _speed, int _maxHealth, int _currentHealth = -1)
        {
            name = _name;
            hitDie = _hitDie;
            proficiencyBonus = _proficiencyBonus;
            perception = _perception;
            armorClass = _armorClass;
            speed = _speed;
            health = new Health(_maxHealth, _currentHealth);
            statistics = new Dictionary<Trait, int>();
            items = new List<Item>();
        }

        public void setStatistics(int _str, int _dex, int _con, int _int, int _wis, int _cha)
        {
            statistics = new Dictionary<Trait, int>()
            {
                { Trait.STRENGTH, _str },
                { Trait.DEXTERITY, _dex },
                { Trait.CONSTITUTION, _con },
                { Trait.INTELLIGENCE, _int },
                { Trait.WISDOM, _wis },
                { Trait.CHARISMA, _cha }
            };
        }

        public int initiative
        {
            get
            {
                return modifier(Trait.DEXTERITY);
            }
        }

        public int trait(Trait trait)
        {
            return statistics[trait];
        }

        public int modifier(Trait trait)
        {
            int _base = statistics[trait];
            return ((_base / 2) - 5);
        }

        public void heal(int amount)
        {
            health.healthChange(amount);
        }

        public void takeDamage(int amount)
        {
            health.healthChange(-amount);
        }

        public class Health
        {
            private int max;
            private int current;
            private int temp;

            public Health(int _max, int _current = -1, int _temp = 0)
            {
                max = _max;
                current = (_current == -1) ? _max : _current;
                temp = _temp;
            }

            public void healthChange(int change)
            {
                Current += change;
            }

            public int Max
            {
                get
                {
                    return max;
                }
                private set
                {
                    max = value;
                }
            }

            public int Current
            {
                get
                {
                    return current;
                }
                private set
                {
                    current -= value;
                    if (current < 0)
                    {
                        if (current < (max * -1))
                        {
                            current = -999;
                        }
                        else
                        {
                            current = 0;
                        }
                    }
                }
            }

            public int Temporary
            {
                get
                {
                    return temp;
                }
                set
                {
                    temp = value;
                    if (temp < 0)
                    {
                        temp = 0;
                    }
                }
            }

        }
    }

}
