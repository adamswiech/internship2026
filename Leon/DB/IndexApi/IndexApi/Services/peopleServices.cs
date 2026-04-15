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
            string? lastName,
            string? orderBy,
            bool? reqPages)
        {
            _context.Database.SetCommandTimeout(150);
            var query = _context.Persons.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(x => EF.Functions.Like(x.first_name, firstName + "%"));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(x => EF.Functions.Like(x.last_name, lastName + "%"));

            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy.Trim().ToLower())
                {
                    case "first_name":
                        query = query.OrderBy(x => x.first_name);
                        break;

                    case "last_name":
                        query = query.OrderBy(x => x.last_name);
                        break;

                    case "middle_name":
                        query = query.OrderBy(x => x.middle_name);
                        break;

                    case "id":
                        query = query.OrderBy(x => x.id);
                        break;

                    case "age":
                        query = query.OrderBy(x => x.age);
                        break;

                    case "city":
                        query = query.OrderBy(x => x.city);
                        break;

                    case "country":
                        query = query.OrderBy(x => x.country);
                        break;

                    case "height_cm":
                        query = query.OrderBy(x => x.height_cm);
                        break;

                    case "weight_kg":
                        query = query.OrderBy(x => x.weight_kg);
                        break;

                    case "favorite_number":
                        query = query.OrderBy(x => x.favorite_number);
                        break;

                    default:
                        query = query.OrderBy(x => x.id);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(x => x.id);
            }



            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            int? total = null;
            int? pages = null;
            if (reqPages == true)
            {
                total = await query
                    .OrderBy(x => 0)
                    .CountAsync();
                pages = (int)(total + pageSize - 1) / pageSize;
            }
            return new PagedDTO<Person>
            {
                Items = items,
                TotalCount = reqPages == true ? total : null,
                TotalPages = reqPages == true ? pages : null
            };
        }
    }
}
