using Microsoft.Xna.Framework.Graphics;

namespace MonoHex {
    public class Unit {
        public Player Owner { get; private set; }
        public string Name { get; private set; }
        public Texture2D Sprite { get; private set; }

        public int Health { get; private set; }
        public int MaxArmor { get; private set; }
        public int Armor { get; private set; }
        public int Damage { get; private set; }
        public int Strength { get; private set; }
        public int Speed { get; private set; }
        public int Initiative { get; private set; }
        public int Range { get; private set; }
        
        public Unit(Player owner, string name, Texture2D sprite) {
            Owner = owner;
            Name = name;
            Sprite = sprite;
        }
        
        public void SetStats(int health, int armor, int damage, int strength, int speed, int initiative, int range=1) {
            Health = health;
            MaxArmor = armor;
            Armor = armor;
            Damage = damage;
            Strength = strength;
            Speed = speed;
            Initiative = initiative;
            Range = range;
        }

        public void AttackUnit(Unit target) {
            if (target.Armor >= Damage) {
                target.Armor -= Damage;
            } else {
                int d = Damage - target.Armor;
                target.Armor = 0;
                target.Health -= d;
                if (target.Health < 0) { target.Health = 0; }
            }
        }
    }
}
