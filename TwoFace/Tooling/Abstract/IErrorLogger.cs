using System;

namespace TwoFace.Tooling.Abstract
{
    public interface IErrorLogger
    {
        void LogError(Exception ex, string infoMessage);
    }
}
