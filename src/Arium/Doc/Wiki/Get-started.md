# Get started

# Create Facade project
- Create a .Net project
- Add NuGet package `Arium`.

# Create the navigable pages
- Create 3 classes named `PageA`, `PageB` and `PageC`.
- Add inheritance from `Arium.Navigable` to each classes.

# Override `Arium.Navigable` methods
- Override `Arium.Exist()` with the function that will validate that the page exists, for each navigable pages.
- Override `Arium.Ready()` with the function that will validate that the page is ready, for each navigable pages.

# Create the `Map`
- Create a `Map` class and add inheritance from `Arium.IMap`.
- Implement `Arium.IMap`:   

## Nodes
The `Nodes` are the Pages that we will add to the `Graph`.
In our example, we use AutoFac to register single instances of the pages implementing `Arium.Navigable` by reflection:
```csharp
private IContainer RegisterPages()
{
    var builder = new ContainerBuilder();
    builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
        .AssignableTo<Navigable>()
        .SingleInstance();

    return builder.Build();
}
```

Then we will use AutoFac again to get the list of page instances:
```csharp
/// <summary>
/// Retrieve all Pages previously registered.
/// </summary>
/// <returns>The Pages.</returns>
private HashSet<INavigable> GetPages()
{
    var navigables = new HashSet<INavigable>();
    var pageTypes = container.ComponentRegistry.Registrations
        .Where(r =>
            typeof(Navigable).IsAssignableFrom(r.Activator.LimitType)
            && !r.Activator.LimitType.IsInterface
            && r.Activator.LimitType.IsPublic
            && !r.Activator.LimitType.IsAbstract
        )
        .Select(r => r.Activator.LimitType)
        .ToList();

    foreach (var pageType in pageTypes)
    {
        navigables.Add(container.Resolve(pageType) as INavigable);
    }

    return navigables;
}

```
