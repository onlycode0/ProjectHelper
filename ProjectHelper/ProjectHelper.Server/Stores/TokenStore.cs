namespace ProjectHelper.Server.Stores
{
    public class TokenStore
    {
        // username => refreshToken
        private readonly Dictionary<string, string> _tokens = new();

        public void SaveRefreshToken(string username, string refreshToken)
        {
            _tokens[username] = refreshToken;
        }

        public bool ValidateRefreshToken(string username, string refreshToken)
        {
            return _tokens.TryGetValue(username, out var storedToken) && storedToken == refreshToken;
        }

        public void RemoveRefreshToken(string username)
        {
            if (_tokens.ContainsKey(username))
                _tokens.Remove(username);
        }
    }
}
