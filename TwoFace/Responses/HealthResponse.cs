namespace TwoFace.Responses
{
    public class HealthResponse
    {
        public override string ToString()
        {
            return $"{Message} | {Version}";
        }

        #region Properties
        public string Message { get; set; }
        public string Version { get; set; }
        #endregion
    }
}
