using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Users;
using BlazorDictionary.Common.Infrastracture;
using BlazorDictionary.Common.Infrastracture.Exceptions;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Users.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _userRepository.GetByIdAsync(request.Id);

        if (dbUser is null)
        {
            throw new DatabaseValidationException("User Not Found");
        }

        var dbUserEmail = dbUser.Email;
        var emailChanged = string.CompareOrdinal(dbUserEmail, request.Email) != 0;

        _mapper.Map(request, dbUser);

        var rows = await _userRepository.UpdateAsync(dbUser);

        //Check if email changed
        if (emailChanged && rows > 0)
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

            dbUser.EmailConfirmed = false;

            await _userRepository.UpdateAsync(dbUser);
        }

        return dbUser.Id;
    }
}
