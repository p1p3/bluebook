using Identity.Core.Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Core.Models.Exceptions
{
    public class RegistrationGeneralException : AggregateException
    {
        public RegistrationGeneralException(IdentityResult identityResult)
            : base("We encoutered several exceptions",identityResult.MapToExceptions())
        {
          
        }
    }
}
