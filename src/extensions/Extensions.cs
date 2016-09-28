using System.Collections.Generic;
using System.IO;

namespace LinqFaroShuffleExtensions
{
    public static class Extensions
    {
        /// <summary>
        /// This is a Extension Method
        /// An extension method is a special purpose static method. 
        /// You can see the addition of the this modifier on the first argument to the method. 
        /// That means you call the method as though it were a member method of the type of the first argument.
        /// Extension methods can be declared only inside static classes.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IEnumerable<T> InterleaveSequenceWith<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIterator = first.GetEnumerator();
            var secondIterator = second.GetEnumerator();

            while (firstIterator.MoveNext() && secondIterator.MoveNext())
            {
                yield return firstIterator.Current;
                yield return secondIterator.Current;
            }
        }

        /// <summary>
        /// Set the deck back to its original order
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool SequenceEquals<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIterator = first.GetEnumerator();
            var secondIterator = second.GetEnumerator();

            while (firstIterator.MoveNext() && secondIterator.MoveNext())
            {
                if (!firstIterator.Current.Equals(secondIterator.Current))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Create Log File
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static IEnumerable<T> LogQuery<T>(this IEnumerable<T> sequence, string tag)
        {
            using (var writer = File.AppendText("debug.log"))
            {
                writer.WriteLine($"Executing Query {tag}");
            }
            
            return sequence;
        }

    }
}
