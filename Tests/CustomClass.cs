namespace Tests;
public class CustomClass
{
    public CustomClass(string resourceName)
    {
        ResourceName = resourceName;
    }
    public string ResourceName { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        CustomClass other = obj as CustomClass;

        return this.ResourceName == other.ResourceName;
    }
}
