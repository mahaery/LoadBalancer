namespace LoadBalancer;
public class LoadBalancer<T> : ILoadBalancer<T>
    where T : class
{
    private static List<T> _resources = new List<T>();
    private static int _currentResourceIndex = -1;

    public void AddResource(T resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource));

        if (_resources.Contains(resource))
            throw new Exception("The resource is already exists in the balancer");

        _resources.Add(resource);
    }

    public T Next()
    {
        if (_resources.Count == 0)
            throw new Exception("The balancer does not have any resources inside.");

        if (_currentResourceIndex + 1 < _resources.Count)
        {
            return _resources.ElementAt(++_currentResourceIndex);
        }
        else
        {
            _currentResourceIndex = 0;
            return _resources.ElementAt(_currentResourceIndex);
        }
    }
}
