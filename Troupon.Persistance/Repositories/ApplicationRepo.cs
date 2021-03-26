using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities;

namespace Troupon.Catalog.Infra.Persistence.Repositories
{
    public class ApplicationRepo : IApplicationRepo
    {
        private CatalogDbContext _dbContext;
        private IMapper _mapper { get; }
        public ApplicationRepo(IMapper mapper, IDbContextFactory<CatalogDbContext> factory)
        {
            _dbContext = factory.CreateDbContext();
            _mapper = mapper;
        }

        public ApplicationDto GetFirstApplication(Guid id)
        {
            return _mapper.Map<ApplicationDto>(_dbContext.Applications.FirstOrDefault(x => x.Id == id));
        }

        public ApplicationDto AddApplication(ApplicationDto Application)
        {
            var added = _dbContext.Applications.Add(_mapper.Map<Application>(Application));
            _dbContext.SaveChangesAsync();
            return _mapper.Map<ApplicationDto>(added);
        }
        public IList<ApplicationDto> GetApplications()
        {
            return _mapper.Map<IList<ApplicationDto>>(_dbContext.Applications);
        }
        //public ApplicationDto GetApplication(Guid id)
        //{
        //    return _mapper.Map<ApplicationDto>(_dbContext.Applications.FirstOrDefault(x => x.Id == id));
        //}
    }
}
