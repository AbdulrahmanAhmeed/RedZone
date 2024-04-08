﻿using RedZone.App.Auth.Commands.Register;
using RedZone.App.Services.Auth.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.App.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validator == null)
            {
                return await next();
            }

            var ValidationResult = await _validator.ValidateAsync(request);
            if(ValidationResult.IsValid) 
            {
                return await next();
            }

            var errors = ValidationResult.Errors
                .ConvertAll(validationFailure => Error.Validation(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage
                    ));
            return (dynamic)errors;
        }
    }
    
}
