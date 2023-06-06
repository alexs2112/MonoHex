using static System.Math;

namespace MonoHex {
    // https://www.redblobgames.com/grids/hexagons/implementation.html
    public class Hex {
        public int q { get; private set; }
        public int r { get; private set; }
        public int s { get; private set; }

        public Hex(int q, int r, int s) {
            Plot(q, r, s);
        }

        public void Plot(int q, int r, int s) {
            if (q + r + s != 0) throw new System.Exception($"Tile Plot: {q} + {r} + {s} = {q + r + s} != 0");
            else {
                this.q = q;
                this.r = r;
                this.s = s;
            }
        }

        public override bool Equals(object obj) {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) {
                return false;
            }
            else {
                Hex h = (Hex) obj;
                return (q == h.q) && (r == h.r) && (s == h.s);
            }
        }

        public override int GetHashCode() {
            return (q << 2) ^ r;
        }

        public override string ToString() {
            return $"({q}, {r}, {s})";
        }

        // Static Arithmetic Functions
        public static Hex Add(Hex a, Hex b) {
            return new Hex(a.q + b.q, a.r + b.r, a.s + b.s);
        }
        public static Hex Subtract(Hex a, Hex b) {
            return new Hex(a.q - b.q, a.r - b.r, a.s - b.s);
        }
        public static Hex Multiply(Hex a, int k) {
            return new Hex(a.q * k, a.r * k, a.s * k);
        }

        public static int Length(Hex hex) {
            return (int)((decimal)(Abs(hex.q) + Abs(hex.r) + Abs(hex.s)) / 2);
        }
        public static int Distance(Hex a, Hex b) {
            return Length(Subtract(a, b));
        }

        public static Hex[] Directions = {
            new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1),
            new Hex(-1, 0, 1), new Hex(-1, 1, 0), new Hex(0, 1, -1)
        };
        public static Hex Direction(int d) {
            if (d < 0 || d > 5) { throw new System.Exception($"Hex Direction is {d}, not within 0 to 5"); }
            return Directions[d];
        }
        public static Hex Neighbour(Hex hex, int direction) {
            return Add(hex, Direction(direction));
        }
    }
}
