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

        public static Predicate<Movie> IsNotBublishedBy(ProductionStudio productionStudio)
        {
            return m => m.production_studio != productionStudio;
        }

        public static Predicate<Movie> IsPublishedAfter(int year)
        {
            return m => m.date_published.Year > year;
        }

        public static Predicate<Movie> IsPublishedBetween(int startYear, int endYear)
        {
            return m => m.date_published.Year >= startYear && m.date_published.Year <= endYear;
        }

        public static Predicate<Movie> IsOfGenre(params Genre[] genre)
        {
            return m=>((IList)genre).Contains(m.genre);
        }
    }
}