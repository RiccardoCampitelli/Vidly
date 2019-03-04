namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreAndMovies : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (1, 'Action')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (2, 'Thriller')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (3, 'Family')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (4, 'Romance')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (5, 'Comedy')");
            Sql("SET IDENTITY_INSERT Genres OFF");

            Sql("SET IDENTITY_INSERT Movies ON");
            Sql("INSERT INTO Movies ( Id, Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES (1, 'Die Hard', 1, CAST('1989-02-03' AS DATETIME), CAST('2016-05-28' AS DATETIME), 1 )");
            Sql("INSERT INTO Movies ( Id, Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES (2, 'Fargo', 2, CAST('1996-05-31' AS DATETIME), CAST('2016-06-27' AS DATETIME), 2 )");
            Sql("INSERT INTO Movies ( Id, Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES (3, '2001 Space Odyssey', 2, CAST('1968-05-10' AS DATETIME), CAST('2016-07-26' AS DATETIME), 3 )");
            Sql("INSERT INTO Movies ( Id, Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES (4, 'Kill Bill', 1, CAST('2004-04-20' AS DATETIME), CAST('2016-08-25' AS DATETIME), 4 )");
            Sql("SET IDENTITY_INSERT Movies OFF");

           
        }
        
        public override void Down()
        {
        }
    }
}
