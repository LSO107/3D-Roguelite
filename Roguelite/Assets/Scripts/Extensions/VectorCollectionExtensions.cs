using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = System.Random;

namespace Extensions
{
    internal static class VectorCollectionExtensions
    {
        public static Vector3 GetMeanVector(this IEnumerable<Vector3> vectors)
        {
            var vecArray = vectors.ToArray();

            var total = vecArray.Aggregate(Vector3.zero, (current, v) => current + v);

            return total / vecArray.Length;
        }

        public static Vector3 GetRandomVector(this IEnumerable<Vector3> vectors, Random random)
        {
            var vecArray = vectors.ToArray();
            return vecArray[random.Next(vecArray.Length)];
        }
    }
}
