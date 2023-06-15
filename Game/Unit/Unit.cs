using System.Collections.Generic;

namespace MonoHex {
    public class Unit {
        public Player Owner { get; private set; }
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }

        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public int MaxArmor { get; private set; }
        public int Armor { get; private set; }
        public int Damage { get; private set; }
        public int Strength { get; private set; }
        public int Speed { get; private set; }
        public int Initiative { get; private set; }
        public int Range { get; private set; }

        // These stats get refreshed every turn
        public int Movement { get; set; }
        public bool HasActivated { get; set; }

        public List<Ability> Abilities { get; private set; }
        
        public Unit(Player owner, string name, Sprite sprite) {
            Owner = owner;
            Name = name;
            Sprite = sprite;
            Abilities = new List<Ability>();
        }
        
        public void SetStats(int health, int armor, int damage, int strength, int speed, int initiative, int range=1) {
            MaxHealth = health;
            Health = health;
            MaxArmor = armor;
            Armor = armor;
            Damage = damage;
            Strength = strength;
            Speed = speed;
            Movement = speed;
            Initiative = initiative;
            Range = range;
        }

        public void AddAbility(Ability a) {
            Abilities.Add(a);
        }

        public void AttackUnit(Unit target) {
            HasActivated = true;
            Movement = 0;
            if (target.Armor >= Damage) {
                target.Armor -= Damage;
            } else {
                int d = Damage - target.Armor;
                target.Armor = 0;
                target.Health -= d;
                if (target.Health < 0) { target.Health = 0; }
            }
        }

        public void Upkeep() {
            Movement = Speed;
            HasActivated = false;
        }

        public void PrintStats() {
            System.Console.WriteLine($"{Name}");
            System.Console.WriteLine($"  HP:     {Health}/{MaxHealth}");
            System.Console.WriteLine($"  Armor:  {Armor}/{MaxArmor}");
            System.Console.WriteLine($"  Damage: {Damage}\t\tStrength: {Strength}");
            System.Console.WriteLine($"  Speed:  {Speed}\t\tInitiative: {Initiative}");
            System.Console.WriteLine($"  Range:  {Range}");

            if (Abilities.Count > 0) {
                System.Console.WriteLine("  Abilities:");
                int i = 1;
                foreach(Ability a in Abilities) {
                    System.Console.Write($"    {i}: ");
                    a.Print();
                    i++;
                }
            }
        }
    }
}
