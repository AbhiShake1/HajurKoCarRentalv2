namespace HajurKoCarRental.Server.Lib.Core;

// a cool trick: just like method overloading, even class overloading works
// due to namespaces
public abstract class BaseMappings
{
    // creating a separate method instead of letting children (derived classes) add mappings to
    // constructor since someone who hasn't worked with the codebase may not know what exactly
    // happening and not add mappings to the constructor. a separate method with this name would help
    public abstract void AddMappings(WebApplication app);
}

public abstract class BaseMappings<T> : BaseMappings where T : BaseView
{
    private readonly Lazy<T> _view = new(Activator.CreateInstance<T>);

    /// <summary>
    /// Lazily creates instance of generic type T.
    /// T extends BaseView. Helps mappings to call views
    /// </summary>
    protected T View => _view.Value;
}