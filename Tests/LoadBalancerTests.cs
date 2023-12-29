using FluentAssertions;
using Xunit;

namespace Tests;


public class LoadBalancerTests
{
    [Fact]
    public void LoadBalancer_Should_AddAndRetrieveLatest()
    {
        var lb = new LoadBalancer.LoadBalancer<string>();

        var testingLoadBalancer = "url1";

        lb.AddResource(testingLoadBalancer);

        var loadBalancer = lb.Next(); // -> url1

        loadBalancer.Should().NotBeNull();
        loadBalancer.Should().Be(testingLoadBalancer);
    }

    [Fact]
    public void LoadBalancer_Should_ThrowExceptionWhenDuplicateWhenCustomClass()
    {
        var testingLoadBalancer = "url1";

        CustomClass resrouce1 = new(testingLoadBalancer);
        CustomClass resrouce2 = new(testingLoadBalancer);

        var lb = new LoadBalancer.LoadBalancer<CustomClass>();


        lb.AddResource(resrouce1);

        Action act = () => lb.AddResource(resrouce2);

        act.Should()
            .Throw<Exception>()
            .WithMessage("The resource is already exists in the balancer");
    }

    [Fact]
    public void LoadBalancer_Should_ThrowArgumentNullException()
    {
        var lb = new LoadBalancer.LoadBalancer<string>();

        string? testingLoadBalancer = null;

        Action act = () => lb.AddResource(testingLoadBalancer);

        act.Should()
            .Throw<ArgumentNullException>();
    }

    [Fact]
    public void LoadBalancer_Should_ThrowExceptionWhenDuplicate()
    {
        var lb = new LoadBalancer.LoadBalancer<string>();

        var testingLoadBalancer = "url1";

        lb.AddResource(testingLoadBalancer);

        Action act = () => lb.AddResource(testingLoadBalancer);

        act.Should()
            .Throw<Exception>()
            .WithMessage("The resource is already exists in the balancer");
    }

    [Fact]
    public void LoadBalancer_Should_ThrowExceptionWhenResourcesEmpty()
    {
        var lb = new LoadBalancer.LoadBalancer<string>();

        Action act = () => lb.Next();

        act.Should()
            .Throw<Exception>()
            .WithMessage("The balancer does not have any resources inside.");
    }

    [Fact]
    public void LoadBalancer_Should_AddAndRetrieveResourcesInRoundRobin()
    {
        var lb = new LoadBalancer.LoadBalancer<string>();

        var testingLoadBalancer1 = "url1";
        var testingLoadBalancer2 = "url2";
        var testingLoadBalancer3 = "url3";

        lb.AddResource(testingLoadBalancer1);
        lb.AddResource(testingLoadBalancer2);
        lb.AddResource(testingLoadBalancer3);

        lb.Next()
            .Should()
            .Be(testingLoadBalancer1);

        lb.Next()
            .Should()
            .Be(testingLoadBalancer2);

        lb.Next()
            .Should()
            .Be(testingLoadBalancer3);

        lb.Next()
            .Should()
            .Be(testingLoadBalancer1);
    }
}
