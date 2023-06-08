using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoHex {
    public class World {
        private int Radius;
        public List<Hex> Hexes { get; private set; }
        public List<Player> Players { get; private set; }
        public Dictionary<Hex, Biome.Type> Biomes { get; private set; }
        public Dictionary<Hex, Unit> Units { get; private set; }
        public Dictionary<Hex, Structure> Structures { get; private set; }

        public World(int radius) {
            Radius = radius;
            GenerateTiles();

            Players = new List<Player>();
            Players.Add(new Player("Player A", Color.Aquamarine));
            Players.Add(new Player("Player B", Color.OrangeRed));

            Units = new Dictionary<Hex, Unit>();
            foreach (Hex h in Hexes) { Units[h] = null; }

            Structures = new Dictionary<Hex, Structure>();
            foreach (Hex h in Hexes) { Structures[h] = null; }
        }

        // ***** //
        // HEXES //
        // ***** //
        public bool InWorld(Hex h) { return Hexes.Contains(h); }

        // ***** //
        // UNITS //
        // ***** //
        public void AddUnit(Unit u, Hex h) { Units[h] = u; }
        public Unit GetUnit(Hex h) { 
            if (!Units.ContainsKey(h)) { return null; }
            return Units[h];
        }
        public void MoveUnit(Hex a, Hex b) {
            Unit u = GetUnit(a);
            if (u == null) { return; }
            Units[a] = null;
            Units[b] = u;
        }
        public bool IsValidMove(Hex a, Hex b) {
            List<Hex> moves = ValidMoves(a);
            if (moves.Contains(b)) { return true; }
            else { return false; }
        }
        public List<Hex> ValidMoves(Hex hex) {
            List<Hex> moves = new List<Hex>();
            Unit u = GetUnit(hex);
            if (u == null) { return moves; }
            foreach (Hex h in Hexes) {
                if (GetUnit(h) != null) { continue; }
                if (Biomes[h] == Biome.Type.MOUNTAIN) { continue; }
                if (Hex.Distance(hex, h) <= u.Speed) { moves.Add(h); }
            }
            return moves;
        }
        public bool IsValidAttack(Hex a, Hex b) {
            List<Hex> attacks = ValidAttacks(a);
            if (attacks.Contains(b)) { return true; }
            else { return false; }
        }
        public List<Hex> ValidAttacks(Hex h) {
            List<Hex> attacks = new List<Hex>();
            Unit u = GetUnit(h);
            if (u == null) { return attacks; }
            foreach ((Hex hex, Unit unit) in Units) {
                if (unit == null) { continue; }
                if (Hex.Distance(h, hex) > u.Range) { continue; }
                if (unit.Owner == u.Owner) { continue; }
                attacks.Add(hex);
            }
            return attacks;
        }
        public void ResolveCombat(Hex a, Hex b) {
            Unit attacker = GetUnit(a);
            Unit target = GetUnit(b);
            attacker.AttackUnit(target);

            // TODO: Handle ranged combat
            if (target.Health <= 0) {
                Units[b] = attacker;
                Units[a] = null;
                if (Constants.Verbosity > 0) { System.Console.WriteLine($"{attacker.Name} kills {target.Name}."); }
            }
        }

        // ********** //
        // STRUCTURES //
        // ********** //
        public void AddStructure(Structure s, Hex h) { Structures[h] = s; }
        public Structure GetStructure(Hex h) { return Structures[h]; }
        public List<Structure> GetStructures() { return GetStructures(null); }
        public List<Structure> GetStructures(Player p) {
            List<Structure> o = new List<Structure>();
            foreach (Hex h in Hexes) {
                Structure s = GetStructure(h);
                if (s != null) {
                    if (p == null || s.Owner == p) { o.Add(s); }
                }
            }
            return o;
        }

        // ***** //
        // SETUP //
        // ***** //
        private void GenerateTiles() {
            Hexes = new List<Hex>();
            Biomes = new Dictionary<Hex, Biome.Type>();
            for (int q = -Radius; q <= Radius; q++) {
                for (int r = Radius; r >= -Radius; r--) {
                    if (System.Math.Abs(q + r) > 3) { continue; } // Small hack to make the map in boundaries
                    Hex hex = new Hex(q, r, -(q + r));
                    Hexes.Add(hex);
                    Biomes[hex] = Biome.RandomType();
                }
            }
        }

        // Temporary method for setting up a test map
        public void SetupMap(UnitFactory units, StructureFactory structures) {
            (int q, int r, int s, int f)[] start = new (int, int, int, int)[] { (0, 3, -3, 0), (0, -3, 3, 1), (-3, 2, 1, 0), (3, -2, -1, 1) };
            foreach ((int q, int r, int s, int player) hex in start) {
                Unit u = units.NewWorker(Players[hex.player]);
                AddUnit(u, new Hex(hex.q, hex.r, hex.s));

                if (Players[hex.player].Capital == null) {
                    Players[hex.player].Capital = new Hex(hex.q, hex.r, hex.s);
                    Structure s = structures.NewCapital(Players[hex.player]);
                    AddStructure(s, new Hex(hex.q, hex.r, hex.s));
                }
            }
        }
    }
}
