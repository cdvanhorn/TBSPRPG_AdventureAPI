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
        ValueTask<AdventureViewModel> GetById(int id);
    }

    public class AdventuresService : IAdventuresService {
        private IAdventuresRepository _adventuresRespository;

        public AdventuresService(IAdventuresRepository adventuresRepository) {
            _adventuresRespository = adventuresRepository;
        }
        
        public async Task<List<AdventureViewModel>> GetAll() {
            var adventures = await _adventuresRespository.GetAllAdventures();
            return adventures.Select(adv => new AdventureViewModel(adv)).ToList();
        }

        public async Task<AdventureViewModel> GetByName(string name) {
            var adventure = await _adventuresRespository.GetAdventureByName(name);
            if(adventure == null)
                return null;
            return new AdventureViewModel(adventure);
        }

        public async ValueTask<AdventureViewModel> GetById(int id) {
            var adventure = await _adventuresRespository.GetAdventureById(id);
            if(adventure == null)
                return null;
            return new AdventureViewModel(adventure);
        }
    }
}