using System;
using System.Collections.Generic;

namespace TrainingPrep.collections
{
    public class MovieLibrary
    {
        IList<Movie> movies;

        public MovieLibrary(IList<Movie> list_of_movies)
        {
            this.movies = list_of_movies;
        }

        public IEnumerable<Movie> all_movies()
        {
            return new ReadOnlySetOf<Movie>(movies);
        }

        public void add(Movie movie)
        {
            foreach (var m in movies)
            {
                if (m.title==movie.title)
                    return;
            }
            movies.Add(movie);
        }

        public IEnumerable<Movie> all_movies_published_by_pixar()
        {
            foreach (var movie in movies)
            {
                if (movie.production_studio==ProductionStudio.Pixar)
                {
                    yield return movie;
                }
            }
        }

        public IEnumerable<Movie> sort_all_movies_by_title_descending()
        {
            var ret = new List<Movie>(movies);
            ret.Sort((m1,m2)=> (-1)*m1.title.CompareTo(m2.title));
            return ret;
        }
    }
}