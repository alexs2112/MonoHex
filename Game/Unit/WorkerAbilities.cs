namespace MonoHex {
    public class BuildStructure : Ability {
        private string Type;

        public BuildStructure(string type) {
            Type = type;
            Name = $"Build {type}";
        }

        public override void Call(Unit unit, Hex hex, Main main) {
            if (unit.HasActivated) {
                if (Constants.Verbosity > 0) { System.Console.WriteLine($"{unit.Name} has already been activated this round."); }
                return;
            }

            if (Type.Equals("Farm") && main.World.Biomes[hex] != Biome.Type.PLAINS) {
                if (Constants.Verbosity > 0) { System.Console.WriteLine($"Can only build a Farm on Plains (currently on {main.World.Biomes[hex].ToString()})."); }
                return;
            } else if (Type.Equals("Camp") && main.World.Biomes[hex] != Biome.Type.FOREST) {
                if (Constants.Verbosity > 0) { System.Console.WriteLine($"Can only build a Camp on Forest (currently on {main.World.Biomes[hex].ToString()})."); }
                return;
            } else if (Type.Equals("Mine") && main.World.Biomes[hex] != Biome.Type.ROCKS) {
                if (Constants.Verbosity > 0) { System.Console.WriteLine($"Can only build a Mine on Rocks (currently on {main.World.Biomes[hex].ToString()})."); }
                return;
            }

            if (unit.Owner.Material < 10) {
                if (Constants.Verbosity > 0) { System.Console.WriteLine($"Not enough material ({unit.Owner.Material} < 10)."); }
                return;
            }
            if (main.World.Structures[hex] != null) {
                if (Constants.Verbosity > 0) { System.Console.WriteLine("Hex already occupied"); }
                return;
            }

            Structure s = null;
            if (Type.Equals("Farm")) { s = main.StructureFactory.NewFarm(unit.Owner); }
            else if (Type.Equals("Camp")) { s = main.StructureFactory.NewCamp(unit.Owner); }
            else if (Type.Equals("Mine")) { s = main.StructureFactory.NewMine(unit.Owner); }
            else { throw new System.Exception($"Invalid structure type of {Type}."); }
            main.World.Structures[hex] = s;
            unit.Owner.Material -= 10;
            unit.HasActivated = true;
            unit.Movement = 0;
        }
    }
}
