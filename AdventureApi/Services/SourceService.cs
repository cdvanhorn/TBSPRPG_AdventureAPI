using System;
using System.Threading.Tasks;
using AdventureApi.Repositories;

namespace AdventureApi.Services
{
    public interface ISourceService
    {
        public Task<string> GetSourceForKey(Guid key, string language = null); 
    }

    public class SourceService : ISourceService
    {
        private readonly ISourceRepository _repository;

        private const string InvalidSourceKey = "invalid source key {0}";

        public SourceService(ISourceRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetSourceForKey(Guid key, string language = null)
        {
            var text = await _repository.GetSourceForKey(key, language);
            return text ?? string.Format(InvalidSourceKey, key);
        }
    }
}