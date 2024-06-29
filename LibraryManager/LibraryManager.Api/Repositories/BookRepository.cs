﻿using LibraryManager.Api.Commons;
using LibraryManager.Api.Data;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Core.DTOs.Author.ViewModel;
using LibraryManager.Core.DTOs.Book.InputModel;
using LibraryManager.Core.DTOs.Book.ViewModel;
using LibraryManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace LibraryManager.Api.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;
        private readonly CacheHandler _cacheHandler;

        public BookRepository(LibraryDbContext dbContext, CacheHandler cacheHandler)
        {
            _dbContext = dbContext;
            _cacheHandler = cacheHandler;
        }

        public async Task<CreateBookDTO> RegisterBook(CreateBookDTO createBookDTO)
        {
            var model = new BookModel()
            {
                Title = createBookDTO.Title,
                Description = createBookDTO.Description,
                Price = createBookDTO.Price,
                Category = createBookDTO.Category,
                AuthorId = createBookDTO.AuthorId,
                PublishedTime = createBookDTO.PublishedTime,
            };

            await _dbContext.Books.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return createBookDTO;
        }

        public async Task<List<CreateBookDTO>> RegisterBooks(List<CreateBookDTO> createBookDTOList)
        {
            List<BookModel> modelList = [];

            foreach(var books in createBookDTOList)
            {
                var model = new BookModel()
                {
                    Title = books.Title,
                    Description = books.Description,
                    Price = books.Price,
                    Category = books.Category,
                    AuthorId = books.AuthorId,
                    PublishedTime = books.PublishedTime,
                };

                modelList.Add(model);
            }

            await _dbContext.AddRangeAsync(modelList);
            await _dbContext.SaveChangesAsync();

            return createBookDTOList;
        }

        public async Task<List<ViewBookDTO>> GetAllBooks()
        {
            List<ViewBookDTO> booksDTO = [];

            var models = await _dbContext.Books
                .AsNoTracking()
                .Include(x => x.Author)
                .ToListAsync();

            foreach (var booksModel in models)
            {
                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };

                booksDTO.Add(DTO);
            }

            return booksDTO;
        }

        public async Task<ViewBookDTO> GetBookById(long id)
        {
            var model = await _dbContext.Books
                .AsNoTracking()
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id) ??
                throw new Exception("The book is not found");

            var AuthorDTO = new ViewAuthorInBooksDTO()
            {
                Id = model.AuthorId,
                Name = model.Author.Name,
                Bio = model.Author.Bio,
                DateOfBirth = model.Author.DateOfBirth
            };

            var DTO = new ViewBookDTO()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                Category = model.Category,
                AuthorId = model.AuthorId,
                Author = AuthorDTO,
                PublishedTime = model.PublishedTime
            };

            return DTO;
        }

        public async Task<List<ViewBookDTO>> GetBookByCategory(string category)
        {
            List<ViewBookDTO> booksDTO = [];

            var models = await _dbContext.Books
                .AsNoTracking()
                .Where(x => x.Category == category)
                .Include(x => x.Author)
                .ToListAsync();
               

            foreach (var booksModel in models)
            {
                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };

                booksDTO.Add(DTO);
            }

            return booksDTO;
        }

        public async Task<List<ViewBookDTO>> GetBooksByCategories(List<string> categorysList)
        {
            List<ViewBookDTO> booksDTO = [];

            var models = await _dbContext.Books
                .AsNoTracking()
                .Where(x => categorysList.Contains(x.Category))
                .Include(x => x.Author)
                .ToListAsync();


            foreach (var booksModel in models)
            {
                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };

                booksDTO.Add(DTO);
            }

            return booksDTO;
        }

        public async Task<List<ViewBookDTO>> GetBookByAuthor(string authorName)
        {
            List<ViewBookDTO> booksDTO = [];

            var models = await _dbContext.Books
                .AsNoTracking()
                .Where(x => x.Author.Name.Contains(authorName))
                .Include(x => x.Author)
                .ToListAsync();

            foreach ( var booksModel in models)
            {
                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };

                booksDTO.Add(DTO);
            }

            return booksDTO;
                
        }

        public async Task<List<ViewBookDTO>> GetBooksByAuthors(List<string> authorNameList)
        {
            List<ViewBookDTO> booksDTO = [];

            var models = await _dbContext.Books
                .AsNoTracking()
                .Where(x => authorNameList.Contains(x.Author.Name))
                .Include(x => x.Author)
                .ToListAsync();

            foreach (var booksModel in models)
            {
                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };

                booksDTO.Add(DTO);
            }

            return booksDTO;
        }


        public async Task<List<ViewBookDTO>> GetBookByName(string name)
        {
            List<ViewBookDTO> booksDTO = [];


            var models = await _dbContext.Books
                .AsNoTracking()
                .Where(x => _dbContext.FuzzySearch(x.Title).Contains(_dbContext.FuzzySearch(name)))
                .Include(x => x.Author)
                .ToListAsync();


            foreach (var booksModel in models)
            {

                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };

                booksDTO.Add(DTO);
            }

            return booksDTO;
        }

        public async Task<UpdateBookDTO> UpdateBook(long id, UpdateBookDTO updateBookDTO)
        {
            var model = await _dbContext.Books
                .FindAsync(id) ??
                throw new Exception("The book is not found");

            model.Title = updateBookDTO.Title;
            model.Description = updateBookDTO.Description;
            model.Price = updateBookDTO.Price;
            model.Category = updateBookDTO.Category;
            model.AuthorId = updateBookDTO.AuthorId;
            model.PublishedTime = updateBookDTO.PublishedTime;

            _dbContext.Books.Update(model);
            await _dbContext.SaveChangesAsync();

            return updateBookDTO;

        }

        public async Task<bool> DeleteBook(long id)
        {
            var model = await _dbContext.Books
                .FindAsync(id) ??
                throw new Exception("The book is not found");

            _dbContext.Books.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
