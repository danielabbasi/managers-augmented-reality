public sealed class Credentials
{
    public Credentials(string username, string password)
    {
        Username = username;
        Password = password;
    }

    #region Variables



    #endregion

    #region Properties

    public string Username { get; private set; }

    public string Password { get; private set; }

    #endregion

    #region Methods



    #endregion
}