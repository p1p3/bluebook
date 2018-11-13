using Identity.Core.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Services.Abstractions
{
    public interface IExternalLoginService
    {
        Task<ExternalLoginValidationResult> IsExternalLogin(string returnUrl);
        Task<bool> IsValidExternalReturnUrl(string returnUrl);
    }
}
