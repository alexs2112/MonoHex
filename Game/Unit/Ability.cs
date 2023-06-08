namespace MonoHex {
    public abstract class Ability {
        public string Name { get; protected set; }

        public abstract void Call(Unit unit, Hex hex, Main main);

        public void Print() {
            System.Console.WriteLine($"{Name}");
        }
    }
}
