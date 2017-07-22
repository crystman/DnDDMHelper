using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DMHelper.Engine;

namespace DMHelper.Visuals
{
    public partial class EntityInformation : UserControl
    {
        public Being me;

        public EntityInformation()
        {
            InitializeComponent();
        }

        public void setCharacter(Being thing)
        {
            me = thing;
            updateAllValues();
        }

        // not very efficient, but it will work for now
        public void updateAllValues()
        {
            label_name.Text = me.name;
            label_ac.Text = me.armorClass.ToString();
            label_ini.Text = me.initiative.ToString();
            label_speed.Text = me.speed.ToString();
            
            label_str_base.Text = me.trait(Trait.STRENGTH).ToString();
            label_str_mod.Text = me.modifier(Trait.STRENGTH).ToString();
            label_dex_base.Text = me.trait(Trait.DEXTERITY).ToString();
            label_dex_mod.Text = me.modifier(Trait.DEXTERITY).ToString();
            label_con_base.Text = me.trait(Trait.CONSTITUTION).ToString();
            label_con_mod.Text = me.modifier(Trait.CONSTITUTION).ToString();
            label_int_base.Text = me.trait(Trait.INTELLIGENCE).ToString();
            label_int_mod.Text = me.modifier(Trait.INTELLIGENCE).ToString();
            label_wis_base.Text = me.trait(Trait.WISDOM).ToString();
            label_wis_mod.Text = me.modifier(Trait.WISDOM).ToString();
            label_cha_base.Text = me.trait(Trait.CHARISMA).ToString();
            label_cha_mod.Text = me.modifier(Trait.CHARISMA).ToString();

            label_health.Text = $"{me.health.Current}/{me.health.Max}";
            label_health_temp.Text = $"({me.health.Temporary})";
            label_perception.Text = me.perception.ToString();
            label_hitdie.Text = me.hitDie.ToString();
        }

    }
}
