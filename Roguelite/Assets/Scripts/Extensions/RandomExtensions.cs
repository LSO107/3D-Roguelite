using Random = System.Random;

namespace Extensions
{
    public static class RandomExtensions
    {
        public static float RandomFloat(this Random random, float minimum, float maximum)
        {
            var next = (float)random.NextDouble();
            return next * (maximum - minimum) + minimum;
        }
    }
}
