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

        public ServiceContainer(ITransactionWrapper transactionWrapper, IUserRepository _userRepo)
        {
            this._userRepo = _userRepo;
            _lazyUserService = new Lazy<IUserService>(() => new UserService(transactionWrapper, _userRepo));
        }
        public IUserService UserService => _lazyUserService.Value;
    }
}
