using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Core.Infrastructure.Mappers
{
    internal static class IdentityResultToExceptions
    {
        internal static IEnumerable<Exception> MapToExceptions(this IdentityResult identityResult)
        {
            return identityResult.Errors.Select(
                error => new Exception($"{error.Description} - Code : {error.Code}"));
        }
    }
}
