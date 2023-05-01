using System.Reflection;
using HajurKoCarRental.Client.Core.HajurKoServices;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Core.Base;

public abstract class BaseRepository
{
}

public abstract class BaseRepository<T, TU> : BaseRepository where T : BaseService where TU : BaseDao
{
    private readonly Lazy<TU> _dao;
    private readonly Lazy<T> _service;

    protected BaseRepository(HajurKoHttpClient httpClient, IJSRuntime runtime)
    {
        // _service = new Lazy<T>(() => (T)Activator.CreateInstance(typeof(T), httpClient)!);
        // _dao = new Lazy<TU>(() => (TU)Activator.CreateInstance(typeof(TU), runtime)!);
        // also instantiate constructors that are inaccessible

        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Default | BindingFlags.Instance;

        _service = new Lazy<T>(() =>
        {
            var type = typeof(T);
            var constructor = type.GetConstructor(flags, new[] { typeof(HajurKoHttpClient) });
            if (constructor is null)
            {
                throw new InvalidOperationException($"Cannot find constructor for type {type}");
            }
            return (T)constructor.Invoke(new object[] { httpClient });
        });
        
        _dao = new Lazy<TU>(() =>
        {
            var type = typeof(TU);
            var constructor = type.GetConstructor(flags, new[] { typeof(IJSRuntime) });
            if (constructor is null)
            {
                throw new InvalidOperationException($"Cannot find constructor for type {type}");
            }
            return (TU)constructor.Invoke(new object[] { runtime });
        });

    }

    /// <summary>
    ///     Lazily creates instance of generic type T.
    ///     T extends BaseService. Helps repository to call service
    /// </summary>
    protected T Service => _service.Value;

    /// <summary>
    ///     Lazily creates instance of generic type TU.
    ///     TU extends BaseService. Helps repository to call dao
    /// </summary>
    protected TU Dao => _dao.Value;

    // protected async Task<TE?> Try<TE>(Func<Task<TE>?> func, Func<string, string>? onError = null)
    // {
    //     try
    //     {
    //         return await func()!;
    //     }
    //     catch (Exception e)
    //     {
    //         onError?.Invoke(e.ToString());
    //         return default;
    //     }
    // }
}