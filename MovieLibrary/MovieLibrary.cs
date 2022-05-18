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
        public IEnumerable<Movie> sort_all_movies_by_title_descending()
        {
            var ret = new List<Movie>(movies);
            ret.Sort((m1, m2) => (-1) * m1.title.CompareTo(m2.title));
            return ret;
        }
        public IEnumerable<Movie> all_movies_published_by_pixar()
        {
            return movies.ThatSatisfy(Movie.IsPublishedBy(ProductionStudio.Pixar));
        }

        public IEnumerable<Movie> all_movies_published_by_pixar_or_disney()
        {
            return movies.ThatSatisfy(movie => movie.production_studio == ProductionStudio.Pixar || movie.production_studio == ProductionStudio.Disney);
        }

        public IEnumerable<Movie> all_movies_not_published_by_pixar()
        {
            return movies.ThatSatisfy(new Negation<Movie>(Movie.IsPublishedBy(ProductionStudio.Pixar)));
        }

        public IEnumerable<Movie> all_movies_published_after(int year)
        {
            return movies.ThatSatisfy(Movie.IsPublishedAfter(year));
        }

        public IEnumerable<Movie> all_movies_published_between_years(int startYear, int endYear)
        {
            return movies.ThatSatisfy(Movie.IsPublishedBetween(startYear, endYear));
        }

        public IEnumerable<Movie> all_kid_movies()
        {
            return movies.ThatSatisfy(Movie.IsOfGenre(Genre.kids));
        }

        public IEnumerable<Movie> all_action_movies()
        {
            return movies.ThatSatisfy(Movie.IsOfGenre(Genre.action));
        }

        public IEnumerable<Movie> all_kid_movies_published_after(int year)
        {
            return movies.ThatSatisfy(m => m.genre == Genre.kids && m.date_published.Year > year);
        }

        public IEnumerable<Movie> all_horror_or_action()
        {
            return movies.ThatSatisfy(Movie.IsOfGenre(Genre.action, Genre.horror));
        }

        public IEnumerable<Movie> all_movies_published_by_disney()
        {
            return movies.ThatSatisfy(Movie.IsPublishedBy(ProductionStudio.Disney));
        }

        public IEnumerable<Movie> all_MGM_or_comedy()
        {
            return movies.ThatSatisfy(new Alternative(Movie.IsPublishedBy(ProductionStudio.MGM),
                Movie.IsOfGenre(Genre.comedy)));
        }
    }

    public class Alternative : Criteria<Movie>
    {
        public Alternative(Criteria<Movie> isPublishedBy, Criteria<Movie> isOfGenre)
        {
            throw new NotImplementedException();
        }

        public bool IsSatisfiedBy(Movie item)
        {
            throw new NotImplementedException();
        }
    }

    public class Negation<TItem> : Criteria<TItem>
    {
        private readonly Criteria<TItem> _criteriaToNegate;

        public Negation(Criteria<TItem> criteriaToNegate)
        {
            _criteriaToNegate = criteriaToNegate;
        }

        public bool IsSatisfiedBy(TItem item)
        {
            return !_criteriaToNegate.IsSatisfiedBy(item);
        }
    }
}