using System;
using System.Collections;
using System.Collections.Generic;

namespace TrainingPrep.collections
{
    public class Movie : IEquatable<Movie>
    {
        public bool Equals(Movie other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return title == other.title;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Movie)obj);
        }

        public override int GetHashCode()
        {
            return (title != null ? title.GetHashCode() : 0);
        }

        public static bool operator ==(Movie left, Movie right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Movie left, Movie right)
        {
            return !Equals(left, right);
        }

        public string title { get; set; }
        public ProductionStudio production_studio { get; set; }
        public Genre genre { get; set; }
        public int rating { get; set; }
        public DateTime date_published { get; set; }

        public static Criteria<Movie> IsPublishedAfter(int year)
        {
            return new PublishedAfterCriteria(year);
        }

        public static Criteria<Movie> IsPublishedBetween(int startYear, int endYear)
        {
            return new PublishedBetweenCriteria(startYear, endYear);
        }

        public static Criteria<Movie> IsOfGenre(params Genre[] genre)
        {
            return new AnyGenreCriteria(genre);
        }

        public static Criteria<Movie> IsPublishedBy(ProductionStudio disney)
        {
            return new PublishedCriteria(disney);
        }

        public class PublishedBetweenCriteria : Criteria<Movie>
        {
            private readonly int _startYear;
            private readonly int _endYear;

            public PublishedBetweenCriteria(int startYear, int endYear)
            {
                _startYear = startYear;
                _endYear = endYear;
            }

            public bool IsSatisfiedBy(Movie movie)
            {
                return movie.date_published.Year >= _startYear && movie.date_published.Year <= _endYear;
            }
        }

        public class AnyGenreCriteria : Criteria<Movie>
        {
            private readonly Genre[] _genre;

            public AnyGenreCriteria(Genre[] genre)
            {
                _genre = genre;
            }

            public bool IsSatisfiedBy(Movie item)
            {
                return ((IList)_genre).Contains(item.genre);
            }
        }

        public class PublishedAfterCriteria : Criteria<Movie>
        {
            private readonly int _year;

            public PublishedAfterCriteria(int year)
            {
                _year = year;
            }

            public bool IsSatisfiedBy(Movie movie)
            {
                return movie.date_published.Year > _year;
            }
        }

        public class PublishedCriteria : Criteria<Movie>
        {
            private readonly ProductionStudio _studio;

            public PublishedCriteria(ProductionStudio studio)
            {
                _studio = studio;
            }

            public bool IsSatisfiedBy(Movie movie)
            {
                return movie.production_studio == _studio;
            }
        }

    }

}