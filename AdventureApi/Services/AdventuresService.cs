using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureApi.Entities;
using AdventureApi.Repositories;

namespace AdventureApi.Services {
    public interface IAdventuresService {
        Task<List<Adventure>> GetAll();
        Task<Adventure> GetByName(string name);
        ValueTask<Adventure> GetById(Guid id);
    }

    public class AdventuresService : IAdventuresService {
        private readonly IAdventuresRepository _adventuresRepository;

        public AdventuresService(IAdventuresRepository adventuresRepository) {
            _adventuresRepository = adventuresRepository;
        }
        
        public async Task<List<Adventure>> GetAll() {
            return await _adventuresRepository.GetAllAdventures();
        }

        public async Task<Adventure> GetByName(string name) {
            return await _adventuresRepository.GetAdventureByName(name);
        }

        public async ValueTask<Adventure> GetById(Guid id) {
            return await _adventuresRepository.GetAdventureById(id);
        }
    }
}