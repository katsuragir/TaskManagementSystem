using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(MapToResponse);
        }

        public async Task<UserResponse?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user != null ? MapToResponse(user) : null;
        }

        public async Task<UserResponse> CreateAsync(CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email
            };

            await _repository.AddAsync(user);
            return MapToResponse(user);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUserRequest request)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            if (!string.IsNullOrEmpty(request.Name)) existing.Name = request.Name;
            if (!string.IsNullOrEmpty(request.Email)) existing.Email = request.Email;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        private UserResponse MapToResponse(User user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}