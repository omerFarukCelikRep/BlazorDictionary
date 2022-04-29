using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Infrastracture;
using BlazorDictionary.Common.Infrastracture.Exceptions;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorDictionary.Api.Application.Features.Commands.Users;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
    }
    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _userRepository.GetSingleAsync(x => x.Email == request.Email);

        if (dbUser == null)
        {
            throw new DatabaseValidationException("User Not Found");
        }

        var pass = PasswordEncryptor.Encrypt(request.Password);

        if (dbUser.Password != pass)
        {
            throw new DatabaseValidationException("Email or Password is wrong");
        }

        if (!dbUser.EmailConfirmed)
        {
            throw new DatabaseValidationException("Email address is not confirmed yet!");
        }

        var mappedUser = _mapper.Map<LoginUserViewModel>(dbUser);

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbUser.Email),
            new Claim(ClaimTypes.Name, dbUser.Username),
            new Claim(ClaimTypes.GivenName, dbUser.FirstName),
            new Claim(ClaimTypes.Surname, dbUser.LastName),
        };

        mappedUser.Token = GenerateToken(claims);

        return mappedUser;
    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:Secret"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiryDate = DateTime.Now.AddDays(10);

        var token = new JwtSecurityToken(claims: claims,
                                         expires: expiryDate,
                                         signingCredentials: credentials,
                                         notBefore: DateTime.Now);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
