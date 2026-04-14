using IndexApi.Data;
using IndexApi.Models;
using IndexApi;
using Microsoft.EntityFrameworkCore;

namespace IndexApi.Services
{
    public class peopleServices
    {
        private readonly dbContext _context;

        public peopleServices(dbContext context)
        {
            _context = context;
        }

        public async Task<PagedDTO<Person>> GetPeople(
            int page,
            int pageSize,
            string? firstName,
            string? lastName)
        {
            var query = _context.Persons.AsQueryable();


            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(x => x.first_name.Contains(firstName));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(x => x.last_name.Contains(lastName));

            var total = await query.CountAsync();
            int pages = (int)(total + pageSize - 1) / pageSize;

            var items = await query
                .OrderBy(x => x.id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedDTO<Person>
            {
                Items = items,
                TotalCount = total,
                TotalPages = pages
            };
        }
    }
}
