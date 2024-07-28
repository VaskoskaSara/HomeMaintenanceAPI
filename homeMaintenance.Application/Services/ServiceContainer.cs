using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homeMaintenance.Application.Services
{
    public class ServiceContainer : IServiceContainer
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public ServiceContainer(ITransactionWrapper transactionWrapper, IUserRepository _userRepo, IMapper _mapper)
        {
            this._userRepo = _userRepo;
            this._mapper = _mapper;
            _lazyUserService = new Lazy<IUserService>(() => new UserService(transactionWrapper, _userRepo, _mapper));
        }
        public IUserService UserService => _lazyUserService.Value;
    }
}
