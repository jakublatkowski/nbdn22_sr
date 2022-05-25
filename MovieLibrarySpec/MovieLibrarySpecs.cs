using System;
using System.Collections.Generic;
using TrainingPrep.collections;
using Machine.Specifications;
using TrainingPrep.specs.MovieLibrarySpecs;


/* The following set of Contexts (TestFixture) are in place to specify the functionality that you need to complete for the MovieLibrary class.
 * MovieLibrary is an aggregate root for the Movie class. it exposes the ability to search,sort, and iterate over all of the movies that it aggregates.
 */

namespace TrainingPrep.specs
{
    namespace MovieLibrarySpecs
    {
        public abstract class movie_library_concern 
        {
            protected static IList<Movie> movie_collection;
            protected static MovieLibrary subject;

            private Establish context = () =>
            {
                movie_collection = new List<Movie>();

                subject = new MovieLibrary(movie_collection);
            };
        } ;

        [Subject(typeof(MovieLibrary))]
        public class when_counting_the_number_of_movies : movie_library_concern
        {
            static int number_of_movies;

            Establish context = () =>
                movie_collection.add_all(new Movie(), new Movie());

            Because of = () =>
                number_of_movies = subject.all_movies().CountAll();

            It should_return_the_number_of_all_movies_in_the_library = () =>
                number_of_movies.ShouldEqual(2);
        }

        [Subject(typeof(MovieLibrary))]
        public class when_asked_for_all_of_the_movies : movie_library_concern
        {
            static Movie first_movie;
            static Movie second_movie;
            static IEnumerable<Movie> all_movies;

            Establish context = () =>
            {
                first_movie = new Movie();
                second_movie = new Movie();

                movie_collection.add_all(first_movie, second_movie);
            };

            Because of = () => { all_movies = subject.all_movies(); };

            It should_receive_a_set_containing_each_movie_in_the_library = () =>
                all_movies.ShouldContainOnly(first_movie, second_movie);
        }
        
        [Subject(typeof(MovieLibrary))]
        public class when_adding_a_movie_to_the_library : movie_library_concern
        {
            static Movie movie;

            Establish context = () => movie = new Movie();

            Because of = () =>
                subject.add(movie);

            It should_store_it_in_the_movie_collection = () =>
            {
                subject.all_movies().ShouldContainOnly(movie);
            };
        }


        [Subject(typeof(MovieLibrary))]
        public class when_adding_an_existing_movie_in_the_collection_again : movie_library_concern
        {
            static Movie movie;

            Establish context = () =>
            {
                movie = new Movie();
                movie_collection.Add(movie);
            };

            Because of = () =>
                subject.add(movie);

            It should_not_restore_the_movie_in_the_collection = () =>
                subject.all_movies().CountAll().ShouldEqual(1);
        }

        
        [Subject(typeof(MovieLibrary))]
        public class when_adding_two_different_copies_of_the_same_movie : movie_library_concern
        {
            static Movie another_copy_of_speed_racer;
            static Movie speed_racer;

            Establish context = () =>
            {
                speed_racer = new Movie { title = "Speed Racer" };
                another_copy_of_speed_racer = new Movie { title = "Speed Racer" };
                movie_collection.Add(speed_racer);
            };

            Because of = () =>
                subject.add(another_copy_of_speed_racer);

            It should_store_only_1_copy_in_the_collection = () =>
                subject.all_movies().CountAll().ShouldEqual(1);
        }

        [Subject(typeof(MovieLibrary))]
        public class when_trying_to_change_the_set_of_movies_returned_by_the_movie_library_to_a_mutable_type :
    movie_library_concern
        {
            static Movie first_movie;
            static Movie second_movie;

            Establish context = () =>
            {
                first_movie = new Movie();
                second_movie = new Movie();
                movie_collection.add_all(first_movie, second_movie);
            };

            Because of = () =>
                exception_thrown_by_the_subject =
                    Catch.Exception(() => { var x = (IList<Movie>)subject.all_movies(); });

            It should_get_an_invalid_cast_exception = () =>
                exception_thrown_by_the_subject.ShouldBeOfExactType<InvalidCastException>();

            static Exception exception_thrown_by_the_subject;
        }

        public class when_trying_to_iterate_over_the_set_of_movies_returned_by_the_movie_library_to_a_mutable_type :
            movie_library_concern
        {
            static Movie first_movie;
            static Movie second_movie;

            Establish context = () =>
            {
                first_movie = new Movie();
                second_movie = new Movie();
                movie_collection.add_all(first_movie, second_movie);
            };

            Because of = () =>
                x = subject.all_movies();

            private It should_get_an_invalid_cast_exception = () =>
            {
                foreach (var m in x)
                {
                    
                }
            };

            static Exception exception_thrown_by_the_subject;
            private static IEnumerable<Movie> x;
        }
        [Subject(typeof(MovieLibrary))]
        public class when_searching_for_movies : concern_for_searching_and_sorting
        {
            private It should_be_able_to_find_all_movies_published_by_pixar = () =>
            {
                Criteria<Movie> criteria = Where_Movie.HasAn(m => m.production_studio).EqualTo(ProductionStudio.Pixar);
                var results = subject.all_movies().ThatSatisfy(criteria);

                results.ShouldContainOnly(cars, a_bugs_life);
            };

        }

        [Subject(typeof(MovieLibrary))]
        public class when_sorting_movies : concern_for_searching_and_sorting
        {
            It should_be_able_to_sort_all_movies_by_title_descending = () =>
            {
                var results = subject.sort_all_movies_by_title_descending();

                results.ShouldContainOnlyInOrder(theres_something_about_mary, the_ring, shrek,
                    pirates_of_the_carribean, indiana_jones_and_the_temple_of_doom,
                    cars, a_bugs_life);
            };
            It should_be_able_to_find_all_movies_published_by_pixar_or_disney = () =>
            {
                var results = subject.all_movies_published_by_pixar_or_disney();

                results.ShouldContainOnly(a_bugs_life, pirates_of_the_carribean, cars);
            };

            It should_be_able_to_find_all_movies_not_published_by_pixar = () =>
            {
                var results = subject.all_movies_not_published_by_pixar();

                results.ShouldNotContain(cars, a_bugs_life);
            };
            It should_be_able_to_find_all_movies_published_by_disney = () =>
            {
                var results = subject.all_movies_published_by_disney();

                results.ShouldContain(pirates_of_the_carribean);
            };
            It should_be_able_to_find_all_movies_published_after_a_certain_year = () =>
            {
                var results = subject.all_movies_published_after(2004);

                results.ShouldContainOnly(the_ring, shrek, theres_something_about_mary);
            };
   
            It should_be_able_to_find_all_movies_published_between_a_certain_range_of_years = () =>
            {
                var results = subject.all_movies_published_between_years(1982, 2003);

                results.ShouldContainOnly(indiana_jones_and_the_temple_of_doom, a_bugs_life, pirates_of_the_carribean);
            };


            private It should_be_able_to_find_all_kid_movies = () =>
            {
                var criteria = Where_Movie.HasAn(m => m.genre).EqualTo(Genre.kids);
                var results = subject.all_movies().ThatSatisfy(criteria);

                results.ShouldContainOnly(a_bugs_life, shrek, cars);
            };

            It should_be_able_to_find_all_action_movies = () =>
            {
                var results = subject.all_action_movies();

                results.ShouldContainOnly(indiana_jones_and_the_temple_of_doom, pirates_of_the_carribean);
            };


            It should_be_able_to_find_recient_kid_movies = () =>
            {
                var results = subject.all_kid_movies_published_after(2003);

                results.ShouldContainOnly(shrek, cars);
            };

            It should_be_able_to_find_horror_Or_actionmovies = () =>
            {
                var results = subject.all_horror_or_action();

                results.ShouldContainOnly(indiana_jones_and_the_temple_of_doom, pirates_of_the_carribean, the_ring);
            };

            It should_be_able_to_find_MGM_Or_comedy = () =>
            {
                var results = subject.all_MGM_or_comedy();

                results.ShouldContainOnly(the_ring, theres_something_about_mary);
            };

        }

        public abstract class concern_for_searching_and_sorting : movie_library_concern
        {
            protected static Movie a_bugs_life;
            protected static Movie cars;
            protected static Movie indiana_jones_and_the_temple_of_doom;
            protected static Movie pirates_of_the_carribean;
            protected static Movie shrek;
            protected static Movie the_ring;
            protected static Movie theres_something_about_mary;

            Establish context = () => { populate_with_default_movie_set(movie_collection); };

            static void populate_with_default_movie_set(IList<Movie> movieList)
            {
                indiana_jones_and_the_temple_of_doom = new Movie
                {
                    title = "Indiana Jones And The Temple Of Doom",
                    date_published = new DateTime(1982, 1, 1),
                    genre = Genre.action,
                    production_studio = ProductionStudio.Universal,
                    rating = 10
                };
                cars = new Movie
                {
                    title = "Cars",
                    date_published = new DateTime(2004, 1, 1),
                    genre = Genre.kids,
                    production_studio = ProductionStudio.Pixar,
                    rating = 10
                };

                the_ring = new Movie
                {
                    title = "The Ring",
                    date_published = new DateTime(2005, 1, 1),
                    genre = Genre.horror,
                    production_studio = ProductionStudio.MGM,
                    rating = 7
                };
                shrek = new Movie
                {
                    title = "Shrek",
                    date_published = new DateTime(2006, 5, 10),
                    genre = Genre.kids,
                    production_studio = ProductionStudio.Dreamworks,
                    rating = 10
                };
                a_bugs_life = new Movie
                {
                    title = "A Bugs Life",
                    date_published = new DateTime(2000, 6, 20),
                    genre = Genre.kids,
                    production_studio = ProductionStudio.Pixar,
                    rating = 10
                };
                theres_something_about_mary = new Movie
                {
                    title = "There's Something About Mary",
                    date_published = new DateTime(2007, 1, 1),
                    genre = Genre.comedy,
                    production_studio = ProductionStudio.MGM,
                    rating = 5
                };
                pirates_of_the_carribean = new Movie
                {
                    title = "Pirates of the Carribean",
                    date_published = new DateTime(2003, 1, 1),
                    genre = Genre.action,
                    production_studio = ProductionStudio.Disney,
                    rating = 10
                };

                movieList.Add(cars);
                movieList.Add(indiana_jones_and_the_temple_of_doom);
                movieList.Add(pirates_of_the_carribean);
                movieList.Add(a_bugs_life);
                movieList.Add(shrek);
                movieList.Add(the_ring);
                movieList.Add(theres_something_about_mary);
            }


        }
    }
}

namespace TrainingPrep.specs.MovieLibrarySpecs
{
    internal class Where_Movie  
    {
        public static CriteriaBuilder HasAn(Func<Movie, ProductionStudio> selector)
        {
            return new CriteriaBuilder(selector);
        }
    }

    internal class CriteriaBuilder
    {
        private readonly Func<Movie, ProductionStudio> _selector;

        public CriteriaBuilder(Func<Movie, ProductionStudio> selector)
        {
            _selector = selector;
        }

        public Criteria<Movie> EqualTo(ProductionStudio studio)
        {
            return new AnonymousCriteria<Movie>(movie => _selector(movie) == studio);
        }
    }
}