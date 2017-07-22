using DMHelper.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace DMHelper
{
    public partial class Form1 : Form
    {
        Dictionary<string, Being> characters;
        Dictionary<string, Being> liveCharacters;

        public Form1()
        {
            InitializeComponent();
            characters = new Dictionary<string, Being>();
            liveCharacters = new Dictionary<string, Being>();
            readJson("../../Characters/PlayableCharacters.json");
            readJson("../../Characters/Monsters.json");

            comboBox1.DataSource = characters.Select(d => d.Value.name).ToList();
            //entityInformation.setCharacter(characters[0]);
            //setDataGridViewSource();
        }

        public void readJson(string address)
        {
            using (StreamReader r = new StreamReader(address))
            {
                string json = r.ReadToEnd();
                List<BeingMold> array = Deserialize<List<BeingMold>>(json);
                foreach (BeingMold item in array)
                {
                    Being temp = new Being(item.name, (HitDice)item.hitdie, item.proficiency_bonus, item.proficiency_bonus, item.armor_class, item.speed, item.health);
                    temp.setStatistics(item.strength, item.dexterity, item.constitution, item.intelligence, item.wisdom, item.charisma);
                    characters.Add(temp.name, temp);
                }
            }
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }

        public void setDataGridViewSource()
        {
            List<BeingVisualMold> temp = new List<BeingVisualMold>();
            foreach (Being b in liveCharacters.Values)
            {
                temp.Add(new BeingVisualMold() { name = b.name, initiative = b.initiative, information = $"AC:{b.armorClass} - {b.health.Current}/{b.health.Max} ({b.health.Temporary})", });
            }
            List<BeingVisualMold> sorted = temp.OrderByDescending(o => o.initiative).ToList();
            dataGridView1.DataSource = sorted;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        [DataContract]
        class BeingMold
        {
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public int proficiency_bonus { get; set; }
            [DataMember]
            public int perception { get; set; }
            [DataMember]
            public int health { get; set; }
            [DataMember]
            public int strength { get; set; }
            [DataMember]
            public int dexterity { get; set; }
            [DataMember]
            public int constitution { get; set; }
            [DataMember]
            public int intelligence { get; set; }
            [DataMember]
            public int wisdom { get; set; }
            [DataMember]
            public int charisma { get; set; }
            [DataMember]
            public int armor_class { get; set; }
            [DataMember]
            public int speed { get; set; }
            [DataMember]
            public int hitdie { get; set; }
        }

        class BeingVisualMold
        {
            public string name { get; set; }
            public int initiative { get; set; }
            public string information { get; set; }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
            entityInformation.setCharacter(liveCharacters[name]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //set initiative button
            string name = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
            liveCharacters[name].initiative = (int)numericUpDown1.Value;
            entityInformation.setCharacter(liveCharacters[name]);
            setDataGridViewSource();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //damage
            string name = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
            liveCharacters[name].takeDamage((int)numericUpDown2.Value);
            entityInformation.setCharacter(liveCharacters[name]);
            setDataGridViewSource();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //heal
            string name = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
            characters[name].heal((int)numericUpDown2.Value);
            entityInformation.setCharacter(characters[name]);
            setDataGridViewSource();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = comboBox1.SelectedValue.ToString();
            if (liveCharacters.ContainsKey(name))
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!liveCharacters.ContainsKey($"{name}{i}"))
                    {
                        Being old = characters[name];
                        Being temp = new Being($"{name}{i}", old.hitDie, old.proficiencyBonus, old.perception, old.armorClass, old.speed, old.health.Max);
                        temp.setStatistics(old.trait(Trait.STRENGTH), old.trait(Trait.DEXTERITY), old.trait(Trait.CONSTITUTION), old.trait(Trait.INTELLIGENCE), old.trait(Trait.WISDOM), old.trait(Trait.CHARISMA));
                        liveCharacters.Add($"{name}{i}", temp);
                        break;
                    }
                }
            }
            else
            {
                liveCharacters.Add(name, characters[name]);
            }
            setDataGridViewSource();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string name = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
            liveCharacters.Remove(name);
            setDataGridViewSource();
        }
    }
}
