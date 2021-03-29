using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities;

namespace Troupon.Catalog.Infra.Persistence.Repositories
{
    public class MerchantRepo : IMerchantRepo
    {
        private CatalogDbContext _dbContext;
        private IMapper _mapper { get; }
        public MerchantRepo(IMapper mapper, IDbContextFactory<CatalogDbContext> factory)
        {
            _dbContext = factory.CreateDbContext();
            _mapper = mapper;
        }

        public MerchantDto GetFirstMerchant(Guid id)
        {
            return _mapper.Map<MerchantDto>(_dbContext.Merchants.FirstOrDefault(x => x.Id == id));
        }

        public MerchantDto AddApplication(MerchantDto Application)
        {
            var added = _dbContext.Merchants.Add(_mapper.Map<MerchantEntity>(Application));
            _dbContext.SaveChangesAsync();
            return _mapper.Map<MerchantDto>(added);
        }
        public IList<MerchantDto> GetMerchant()
        {
            return _mapper.Map<IList<MerchantDto>>(_dbContext.Merchants);
        }
        //public ApplicationDto GetApplication(Guid id)
        //{
        //    return _mapper.Map<ApplicationDto>(_dbContext.Applications.FirstOrDefault(x => x.Id == id));
        //}
    }
}
