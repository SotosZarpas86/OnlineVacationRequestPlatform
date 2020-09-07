using AutoMapper;
using OnlineVacationRequestPlatform.BusinessLayer.Interfaces;
using OnlineVacationRequestPlatform.BusinessLayer.Models;
using OnlineVacationRequestPlatform.BusinessLayer.Utilities;
using OnlineVacationRequestPlatform.DataLayer.Entities;
using OnlineVacationRequestPlatform.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var result = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserModel>>(result);
        }

        public async Task<IEnumerable<ExtendedUserModel>> GetExtendedAllAsync()
        {
            var result = await _userRepository.GetExtendedAllAsync();
            return _mapper.Map<IEnumerable<ExtendedUserModel>>(result);
        }

        public async Task<UserModel> GetΒyIdAsync(Guid userId)
        {
            var result = await _userRepository.GetΒyIdAsync(userId);
            return _mapper.Map<UserModel>(result);
        }

        public async Task<UserModel> AddAsync(UserModel user)
        {
            var userDb = _mapper.Map<User>(user);
            userDb.Password = Cryptography.EncryptString(user.Password);
            PopulateSystemicFields(userDb, DateTime.Now, DateTime.Now);
            var result = await _userRepository.AddAsync(userDb);
            return _mapper.Map<UserModel>(result);
        }

        public async Task<UserModel> UpdateAsync(UserModel user)
        {
            var userDb = _mapper.Map<User>(user);
            UpdateSystemicFields(userDb, DateTime.Now);
            var result = await _userRepository.UpdateAsync(userDb);
            return _mapper.Map<UserModel>(result);
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }

        public async Task<ExtendedSingleUserModel> AuthenticateUserAsync(string email, string password, string roleName)
        {
            var user = new ExtendedSingleUserModel();
            try
            {
                var userDb = await _userRepository.GetΒyEmailAsync(email);
                if (userDb != null)
                {
                    var decryptedDbPassword = Cryptography.DecryptString(userDb.Password);
                    if (decryptedDbPassword.Equals(password) && userDb.RoleName.Equals(roleName))
                        user = _mapper.Map<ExtendedSingleUserModel>(userDb);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        private void PopulateSystemicFields(User user, DateTime dateAdded, DateTime dateModified)
        {
            user.DateAdded = dateAdded;
            user.DateModified = dateModified;
        }

        private void UpdateSystemicFields(User user, DateTime dateModified)
        {
            user.DateModified = dateModified;
        }
    }
}
