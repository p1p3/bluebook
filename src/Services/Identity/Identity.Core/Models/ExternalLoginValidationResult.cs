namespace Identity.Core.Services.Implementations
{
    public struct ExternalLoginValidationResult
    {
        public static ExternalLoginValidationResult NonExternalLogin()
        {
            return new ExternalLoginValidationResult(false, string.Empty);
        }

        public static ExternalLoginValidationResult ExternalLogin(string idP)
        {
            return new ExternalLoginValidationResult(true, idP);
        }

        private ExternalLoginValidationResult(bool isExternal, string idP)
        {
            IsExternal = isExternal;
            IdP = idP;
        }

        public bool IsExternal { get; }
        public string IdP { get; }
    }
}
