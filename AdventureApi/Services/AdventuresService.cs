using AdventureApi.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AdventureApi.Repositories;
using AdventureApi.ViewModels;

namespace AdventureApi.Services {
    public interface IAdventuresService {
        Task<List<AdventureViewModel>> GetAll();
        Task<AdventureViewModel> GetByName(string name);
        ValueTask<AdventureViewModel> GetById(string id);
    }

    public class AdventuresService : IAdventuresService {
        private readonly IAdventuresRepository _adventuresRepository;

        public AdventuresService(IAdventuresRepository adventuresRepository) {
            _adventuresRepository = adventuresRepository;
        }
        
        public async Task<List<AdventureViewModel>> GetAll() {
            var adventures = await _adventuresRepository.GetAllAdventures();
            return adventures.Select(adv => new AdventureViewModel(adv)).ToList();
        }

        public async Task<AdventureViewModel> GetByName(string name) {
            var adventure = await _adventuresRepository.GetAdventureByName(name);
            return adventure == null ? null : new AdventureViewModel(adventure);
        }

        public async ValueTask<AdventureViewModel> GetById(string id) {
            if(!Guid.TryParse(id, out _))
                return null;
            var adventure = await _adventuresRepository.GetAdventureById(Guid.Parse(id));
            return adventure == null ? null : new AdventureViewModel(adventure);
        }
    }
}