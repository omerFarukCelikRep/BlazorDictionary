using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Users;
using BlazorDictionary.Common.Infrastracture;
using BlazorDictionary.Common.Infrastracture.Exceptions;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Users.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await _userRepository.GetSingleAsync(x => x.Email == request.Email);

        if (existsUser != null)
        {
            throw new DatabaseValidationException("User Already Exists!");
        }

        var dbUser = _mapper.Map<User>(request);

        var rows = await _userRepository.AddAsync(dbUser);

        //Email Changed/Created
        if (rows > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = dbUser.Email
            };

            QueueFactory.SendMessageToExhange(exchangeName: DictionaryConstants.UserExhangeName,
                                              exchangeType: DictionaryConstants.DefaultExchangeType,
                                              queueName: DictionaryConstants.UserEmailChangedQueueName,
                                              obj: @event);
        }

        return dbUser.Id;
    }
}
