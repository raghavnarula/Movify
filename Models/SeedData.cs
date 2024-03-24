using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                // Seed movies
                var random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    var upvotes = random.Next(1, 11); // Random upvotes between 1 and 10
                    context.Movie.Add(new Movie
                    {
                        Title = $"Movie {i + 1}",
                        ReleaseDate = DateTime.Now.AddDays(-random.Next(1, 365 * 20)), // Random release date within the last 20 years
                        Genre = "Random Genre", // Adjust the genre as needed
                        Price = (decimal)random.NextDouble() * 20, // Random price between 0 and 20
                        Upvotes = upvotes
                    });
                }

                // Seed users
                if (context.User.Any())
                {
                    return; // DB has been seeded
                }

                // Common usernames
                string[] commonUsernames = { "john_doe", "jane_smith", "mike_jackson", "sarah_williams", "david_brown", "emily_davis", "chris_wilson", "lisa_taylor", "kevin_jones", "laura_martin" };
                
                // Common passwords
                string[] commonPasswords = { "password123", "letmein", "securepass", "p@ssw0rd", "brown123", "welcome123", "123456", "qwerty", "abc123", "passw0rd" };
                
                // Common email domains
                string[] emailDomains = { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com", "aol.com", "icloud.com", "mail.com", "live.com", "msn.com", "protonmail.com" };

                var shuffledUsernames = Shuffle(commonUsernames, random);
                var shuffledPasswords = Shuffle(commonPasswords, random);
                var shuffledEmailDomains = Shuffle(emailDomains, random);

                for (int i = 0; i < 10; i++)
                {
                    context.User.Add(new User
                    {
                        Username = shuffledUsernames[i],
                        Password = shuffledPasswords[i],
                        Email = $"{shuffledUsernames[i]}@{shuffledEmailDomains[i]}"
                    });
                }

                context.SaveChanges();
            }
        }

        // Function to shuffle an array
        private static T[] Shuffle<T>(T[] array, Random random)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = random.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
            return array;
        }
    }
}
