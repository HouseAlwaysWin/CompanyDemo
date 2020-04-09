
//using DBAccess.Entities;
//using DBAccess.Repositories;
//using System.Security.Claims;

//namespace DBAccess.Proxies
//{
//    public class ClaimProxy : Claim
//    {
//        private readonly UnitOfWork _unitOfWork;
//        private readonly UserRepository _userRepository;
//        private UserProxy _userProxy;

//        internal ClaimProxy(UnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//            _userRepository = unitOfWork.UserRepository as UserRepository;
//        }

//        public override User User
//        {
//            get { return _userProxy ?? (_userProxy = _userRepository.FindProxyById(UserId)); }
//            set
//            {
//                _userProxy = _userRepository.GetUserProxy(value);
//                UserId = value.UserId;
//            }
//        }
//    }
//}
