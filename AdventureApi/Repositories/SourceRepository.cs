using System;
using System.Linq;
using System.Threading.Tasks;
using AdventureApi.Entities;
using Microsoft.EntityFrameworkCore;
using TbspRpgLib.Settings;

namespace AdventureApi.Repositories
{
    public interface ISourceRepository
    {
        public Task<string> GetSourceForKey(Guid key, string language = null);
    }
    
    public class SourceRepository : ISourceRepository
    {
        private readonly AdventureContext _context;

        public SourceRepository(AdventureContext context)
        {
            _context = context;
        }
        
        public Task<string> GetSourceForKey(Guid key, string language = null)
        {
            IQueryable<Source> query = null;
            if (language == null || language == Languages.ENGLISH)
            {
                query = _context.SourcesEn.AsQueryable();
            }
            
            if (language == Languages.SPANISH)
            {
                query = _context.SourcesEsp.AsQueryable();
            }

            if (query != null)
                return query.Where(s => s.Key == key)
                    .Select(s => s.Text)
                    .FirstOrDefaultAsync();
            
            throw new ArgumentException($"invalid language {language}");
        }
    }
}