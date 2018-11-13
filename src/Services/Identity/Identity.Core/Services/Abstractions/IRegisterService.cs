using Identity.Core.Models;
using System.Threading.Tasks;

namespace Identity.Core.Services.Abstractions
{
    public interface IRegisterService
    {
        Task Register(RegistrationData data);
    }
}
