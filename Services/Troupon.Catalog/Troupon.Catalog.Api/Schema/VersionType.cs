//using HotChocolate;
//using HotChocolate.Resolvers;
//using HotChocolate.Types;
//using TR.Catalog.Dtos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Threading.Tasks;
//using DealDto = TR.Catalog.Dtos.DealDto;

//namespace TR.Catalog.Schema
//{
//    public class DealType:ObjectType<DealDto>
//    {
//        protected override void Configure(IObjectTypeDescriptor<DealDto> descriptor)
//        {
//            base.Configure(descriptor);
//            descriptor.Field(p => p.Id).Type<IdType>();

//            descriptor.Field(x => x.Major).Type<StringType>();
//            descriptor.Field(p => p.Minor).Type<StringType>();
//            descriptor.Field<DealType>(p => ResolveTenant(default,default,default))
//                .Name("tenant")
//                .Argument("tenantId", a => a.Type<TenantType>().DefaultValue(Tenant.MTL))
//                .Type<TenantType>();
//        }

//        /// <summary>
//        /// Example of Simple Custom Resolver
//        /// </summary>
//        /// <param name="parent"></param>
//        /// <param name="context"></param>
//        /// <param name="tenant"></param>
//        /// <returns></returns>
//        private Task<string> ResolveTenant([Parent] DealDto parent,IResolverContext context,Tenant tenant)
//        {
//            switch (tenant)
//            {
//                case Tenant.MTL:
//                    return Task.FromResult("Montreal");
//                case Tenant.Guelph:
//                    return Task.FromResult("Guelph");
//                case Tenant.Regina:
//                    return Task.FromResult("Regina");
//                default:
//                    return Task.FromResult("Montreal");
//            }
//        }
//    }
//}


