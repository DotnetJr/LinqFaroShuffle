using System;
using System.Collections.Generic;
using System.Linq;
using LinqFaroShuffleExtensions;
using LinqFaroShuffleTypes;
using static LinqFaroShuffleTypes.PlayingCards;

namespace LinqFaroShuffle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Show all cards");

            var startingDeck = (from s in Suits().LogQuery("Suit Generation")
                                from r in Ranks().LogQuery("Value Generation")
                                //select new { Suit = s, Rank = r })
                                select new PlayingCards(s, r))
                                .LogQuery("Starting Deck")
                                .ToList();
            
            foreach (var sd in startingDeck)
            {
                Console.WriteLine(sd);
            }

            Console.ReadLine();
            Console.WriteLine("Interleave the order of the deck");

            var top = startingDeck.Take(26);
            var bottom = startingDeck.Skip(26);
            var shuffle = top.InterleaveSequenceWith(bottom);

            foreach (var sh in shuffle)
            {
                Console.WriteLine(sh);
            }

            Console.ReadLine();
            Console.WriteLine("Set the deck back to its original order");

            var times = 0;
            var shuffleSD = startingDeck;

            do
            {
                // It takes 8 times
                //shuffleSD = shuffleSD.Take(26).InterleaveSequenceWith(shuffleSD.Skip(26));
                //shuffleSD = shuffleSD.Take(26).LogQuery("Top Half")
                //            .InterleaveSequenceWith(shuffleSD.Skip(26).LogQuery("Bottom Half")).LogQuery("Shuffle").ToArray();;

                // LINQ queries are evaluated lazily. 
                // The sequences are generated only as the elements are requested. 
                // Usually, that's a major benefit of LINQ. 
                // However, in a use such as this program, this causes exponential growth in execution time.
                // In one run, it executes 2592 queries, including all the value and suit generation.
                // In other words, it'll take a long time to execute
                //shuffleSD = shuffleSD.Skip(26).InterleaveSequenceWith(shuffleSD.Take(26));
 
                // There is an easy way to update this program to avoid all those executions.
                // There are LINQ methods ToArray() and ToList() that cause the query to run, 
                // and store the results in an array or a list, respectively. 
                // You use these methods to cache the data results of a query rather than execute the source query again.
                // It now executes 162 queries
                // Don't misinterpret this example by thinking that all queries should run eagerly. 
                // This example is designed to highlight the use cases where lazy evaluation can cause performance difficulties.
                // LINQ enables both lazy and eager evaluation.
                shuffleSD = shuffleSD.Skip(26).LogQuery("Bottom Half")
                            .InterleaveSequenceWith(shuffleSD.Take(26).LogQuery("Top Half")).LogQuery("Shuffle").ToList();

                foreach (var ssd in shuffleSD)
                {
                    Console.WriteLine(ssd);
                }

                //Console.ReadLine();
                times++;
                Console.WriteLine(times);
            }
            while (!startingDeck.SequenceEquals(shuffleSD));
            
            Console.WriteLine(times);
            Console.ReadLine();
        }

        private static IEnumerable<Suit> Suits()
        {
            yield return Suit.Clubs;
            yield return Suit.Diamonds;
            yield return Suit.Hearts;
            yield return Suit.Spades;
        }

        private static IEnumerable<Rank> Ranks()
        {
            yield return Rank.Two;
            yield return Rank.Three;
            yield return Rank.Four;
            yield return Rank.Five;
            yield return Rank.Six;
            yield return Rank.Seven;
            yield return Rank.Eight;
            yield return Rank.Nine;
            yield return Rank.Ten;
            yield return Rank.Jack;
            yield return Rank.Queen;
            yield return Rank.King;
            yield return Rank.Ace;
        }
    }
}
