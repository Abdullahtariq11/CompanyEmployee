using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contract;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CompanyService:ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
            
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            try
            {
                var companies = _repositoryManager.Company.GetAllCompanies(trackChanges);
                var companyDto= _mapper.Map<IEnumerable<CompanyDto>>(companies);    
                return companyDto;

            }
            catch (Exception ex) 
            {
                _loggerManager.LogError($"Something went wrong in the " +
                    $"{nameof(GetAllCompanies)} service method {ex}");
                throw;
            }
        }
    }
}
