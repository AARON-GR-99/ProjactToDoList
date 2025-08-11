namespace Api.Services.Extencion;

public interface IHashService
{

    string Hash(string password);


    bool Verify(string password, string storedHash);
}