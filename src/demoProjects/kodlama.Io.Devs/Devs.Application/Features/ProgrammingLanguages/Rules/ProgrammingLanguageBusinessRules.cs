using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Devs.Application.Services.Repositories;
using Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devs.Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(p => p.Name == name);
            if (result.Items.Any())
            {
                throw new BusinessException("Programlama Dili Mevcuttur.");
            }
        }

        public async Task ProgrammingLanguageShouldExistWhenRequest(int id)
        {
            ProgrammingLanguage programmingLanguage = await _programmingLanguageRepository.GetAsync(p => p.Id == id);
            if (programmingLanguage == null)
            {
                throw new BusinessException("Id Bulunamadı.");
            }
        }
    }
}
