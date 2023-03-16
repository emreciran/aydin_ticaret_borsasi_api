using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using Shared.ResponseModels;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AuthManager : IAuthService
    {
        IAuthRepository _authRepository;

        public AuthManager(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<UserManagerResponse> ConfirmEmail(string userid, string token)
        {
            return await _authRepository.ConfirmEmail(userid, token);
        }

        public async Task<UserManagerResponse> ForgotPassword(string email)
        {
            return await _authRepository.ForgotPassword(email);
        }

        public async Task<UserManagerResponse> LoginUser(LoginViewModel model)
        {
            return await _authRepository.LoginUser(model);
        }

        public async Task<UserManagerResponse> RegisterUser(RegisterViewModel model)
        {
            return await _authRepository.RegisterUser(model);
        }

        public async Task<UserManagerResponse> ResetPassword(ResetPasswordViewModel model)
        {
            return await _authRepository.ResetPassword(model);
        }
    }
}
