using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class SeedController : ApiController
    {
        private ApplicationDbContext _context;

        public SeedController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPut]
        public IHttpActionResult Seed()
        {

           _context.Database.ExecuteSqlCommand("DELETE FROM Rentals"); 
           _context.Database.ExecuteSqlCommand("DELETE FROM Movies"); 
           _context.Database.ExecuteSqlCommand("DELETE FROM Customers");

            _context.Database.ExecuteSqlCommand(@"
SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT INTO [dbo].[Customers] ([Id], [Name], [IsSubscribedToNewsLetter], [MembershipTypeId], [BirthDate]) VALUES (1, N'Joe Smith', 1, 4, N'2000-05-05 00:00:00')
SET IDENTITY_INSERT [dbo].[Customers] OFF
                                                ");

            _context.Database.ExecuteSqlCommand(@"
SET IDENTITY_INSERT [dbo].[Movies] ON
INSERT INTO [dbo].[Movies] ([Id], [Name], [ReleaseDate], [DateAdded], [NumberInStock], [GenreId], [NumberAvailable]) VALUES (1, N'Die Hard', N'1989-02-03 00:00:00', N'2016-05-28 00:00:00', 1, 1, 1)
INSERT INTO [dbo].[Movies] ([Id], [Name], [ReleaseDate], [DateAdded], [NumberInStock], [GenreId], [NumberAvailable]) VALUES (2, N'Fargo', N'1996-05-31 00:00:00', N'2016-06-27 00:00:00', 2, 2, 2)
INSERT INTO [dbo].[Movies] ([Id], [Name], [ReleaseDate], [DateAdded], [NumberInStock], [GenreId], [NumberAvailable]) VALUES (3, N'2001 Space Odyssey', N'1968-05-10 00:00:00', N'2016-07-26 00:00:00', 3, 2, 3)
INSERT INTO [dbo].[Movies] ([Id], [Name], [ReleaseDate], [DateAdded], [NumberInStock], [GenreId], [NumberAvailable]) VALUES (4, N'Kill Bill', N'2004-04-20 00:00:00', N'2016-08-25 00:00:00', 4, 1, 3)
INSERT INTO [dbo].[Movies] ([Id], [Name], [ReleaseDate], [DateAdded], [NumberInStock], [GenreId], [NumberAvailable]) VALUES (9, N'check', N'2005-05-05 00:00:00', N'2019-05-03 13:48:35', 5, 1, 0)
SET IDENTITY_INSERT [dbo].[Movies] OFF
                                                ");



            return Json("OK");
        }
    }
}
