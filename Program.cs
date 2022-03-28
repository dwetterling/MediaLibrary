
using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program started");

            string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            logger.Info(scrubbedFile);
            MovieFile movieFile = new MovieFile(scrubbedFile);

            int selection = 0;
               do
            {
                Console.WriteLine("1) Search by movie title 2) Exit");
                selection = Convert.ToInt32(Console.ReadLine());

             if (selection == 1)
                {

                    Console.WriteLine("Search the Movie name you are looking for");
                    var search = Console.ReadLine();
                
                     var searchMovies = movieFile.Movies.Where(m => m.title.Contains(search, StringComparison.OrdinalIgnoreCase)).Select(m => m.title);
            // LINQ - Count aggregation method
            Console.WriteLine($"There are {searchMovies.Count()} movies with {search} in the title:");
            foreach(string t in searchMovies)
            {
                Console.WriteLine($"  {t}");
            }
                   
                }
            } while (selection != 2);
            
                logger.Trace("Ending Application");

            logger.Info("Program ended");
        }
        
    }
    
}

