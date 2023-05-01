using System.Net.Mime;
using System.Runtime.InteropServices;
using Xamarin.Essentials;

namespace HajurKoCarRental.Client.Helpers;

public static class NetworkHelper
{
    public static bool IsInternetAvailable()
    {
        if (DeviceInfo.Platform == DevicePlatform.Unknown) return true;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("WEBASSEMBLY")))
        {
            return false;
        }

        return Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
