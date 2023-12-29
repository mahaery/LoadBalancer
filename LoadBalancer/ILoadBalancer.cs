namespace LoadBalancer;
public interface ILoadBalancer<T>
    where T : class
{
    void AddResource(T resource);
    T Next();
}
