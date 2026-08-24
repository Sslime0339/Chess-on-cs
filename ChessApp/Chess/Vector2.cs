


namespace Chess
{
    struct Vector2
    {
        public int X;
        public int Y;

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);

        public static Vector2 operator *(int a, Vector2 vec) => new Vector2(vec.X * a, vec.Y * a);
        public static Vector2 operator *(Vector2 vec, int a) => a * vec; // коммутативность

        public override string ToString()
        {
            return $"{{x = {X}, y = {Y}}}";
        }
    }
}