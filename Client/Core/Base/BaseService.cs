using HajurKoCarRental.Client.Core.HajurKoServices;

namespace HajurKoCarRental.Client.Core.Base;

public class BaseService
{
    protected readonly HajurKoHttpClient Client;

    protected BaseService(HajurKoHttpClient client)
    {
        Client = client;
    }
}