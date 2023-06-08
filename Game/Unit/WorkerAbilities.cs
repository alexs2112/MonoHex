namespace MonoHex {
    public class BuildFarm : Ability {
        public BuildFarm() {
            Name = "Build Farm";
        }

        public override void Call(Unit unit, Hex hex, Main main) {
            if (unit.Owner.Material < 10) {
                if (Constants.Verbosity > 0) { System.Console.WriteLine($"Not enough material ({unit.Owner.Material} < 10)."); }
                return;
            }
            if (main.World.Biomes[hex] != Biome.Type.PLAINS) {
                if (Constants.Verbosity > 0) { System.Console.WriteLine($"Can only build a farm on PLAINS ({main.World.Biomes[hex].ToString()} < 10)."); }
                return;
            }
            if (main.World.Structures[hex] != null) {
                if (Constants.Verbosity > 0) { System.Console.WriteLine("Hex already occupied"); }
                return;
            }
            Structure s = main.StructureFactory.NewFarm(unit.Owner);
            main.World.Structures[hex] = s;
            unit.Owner.Material -= 10;
        }
    }
}
