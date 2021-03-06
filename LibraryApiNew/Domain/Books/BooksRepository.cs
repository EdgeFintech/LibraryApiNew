using System;
using System.Linq;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Internal.Query;
using JsonApiDotNetCore.Services;
using LibraryApiNew.Models;
using LibraryApiNew.Repositories;
using LibraryApiNew.Services;
using Microsoft.Extensions.Logging;

namespace LibraryApiNew.Domain.Books
{
    public class BooksRepository : BelongsToUserRepository<Book>
    {
        public BooksRepository(
            ILoggerFactory loggerFactory, 
            IJsonApiContext jsonApiContext, 
            IDbContextResolver contextResolver,
            CurrentUserService currentUser
        ) : base(loggerFactory, jsonApiContext, contextResolver, currentUser)
        { }

        public override IQueryable<Book> Filter(IQueryable<Book> books, FilterQuery filterQuery)
        {
            if (filterQuery.Attribute.Equals("query"))
            {
                return books.Where( a =>
                    a.Title.Contains(filterQuery.Value, StringComparison.OrdinalIgnoreCase)
                );
            }

            return base.Filter(books, filterQuery);
        }
    }
}