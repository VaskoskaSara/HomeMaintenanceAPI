using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Infrastructure.Repositories;

namespace homeMaintenance.Application.Services
{
    public class ServiceContainer : IServiceContainer
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly IUserRepository _userRepo;
        private readonly Lazy<IPaymentService> _lazyPaymentService;
        private readonly Lazy<INotificationService> _lazyNotificationService;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IMapper _mapper;

        public ServiceContainer(ITransactionWrapper transactionWrapper, IUserRepository _userRepo, IMapper _mapper, IPaymentRepository _paymentRepo, INotificationService _notificationService)
        {
            this._userRepo = _userRepo;
            this._paymentRepo = _paymentRepo;
            this._mapper = _mapper;
            _lazyUserService = new Lazy<IUserService>(() => new UserService(transactionWrapper, _userRepo, _mapper, _notificationService));
            _lazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(_paymentRepo));
        }
        public IUserService UserService => _lazyUserService.Value;
        public IPaymentService PaymentService => _lazyPaymentService.Value;
        public INotificationService NotificationService => _lazyNotificationService.Value;


    }
}
