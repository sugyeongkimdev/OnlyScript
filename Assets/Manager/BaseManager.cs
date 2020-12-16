public abstract class BaseManager<T> where T : class, new()
{
    public static T instasnce;

    public virtual T Init ()
    {
        return instasnce;
    }
}
