# Get started

We will use the `FacadeExample` project as support to understand how you could implement `Arium`.
This example was created as UI automation support but the logic could apply to other type of network requiring navigation.

# Definitions
Some definitions of the `FacadeExample` to put you in context.

## AUT
The `AUT` is a representation of the application under test. This is the entry point of the example.

## Page/Navigable
The word `Page` references to a representation of a page of your AUT. It could implements a [Page Model Object Pattern](https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/). In this example, the terms `Page` and `Navigable` are interchangeable, `Navigable` being the generic term of the network's objects. 

## Map
The word `Map` references to an object containing the declaration of your AUT's pages.

## Observers
The `Observers` are the objects that watch any change of a page's state.

## Log
A `Log` contains the historic of the navigation. It also notify the page's observers of a change of state.

## Navigator
The `Navigator` is the object that will resolved the navigation at run time. You should use the `Browser` instead of the `Navigator` to navigate.

## Browser
The `Browser` is the object provides methods to navigate. Most of these methods are returning the browser itself so they can be chained.

# The chicken and egg problem
The first problem we will face is the implementation of the pages. Given the recursive nature of the system to resolve the navigation at real time, pages need references to other pages, declared in Map, that need reference to these first ones. Basically, Map needs to reference the Pages, and the Pages need to reference Map.
The solution implemented in this example is to delayed the instantiation of the pages declared in Map. The example uses AutoFac as dependency resolver, so Map has access to the container's scope. This is one way to implement it but you can also implement your own resolver not based on dependency injection. But you will probably ending up to delay the pages instantiation in all case.

# Define classes
Because of the chicken and egg problem, there is some back and forth will creating the classes the first time.

The order of creation used for this example is:
> IMapFacade (:IMap) -> Map (:IMapInterface) -> BasePage (:Navigable), PageA (:BasePage), PageB (:BasePage), PageC (:BasePage)-> AUT (:IDisposable) -> DI

## IMapFacade
New implemented pages of your AUT are declared here. 

```csharp
public interface IMapFacade : IMap
{
    PageA PageA { get; }
    PageB PageB { get; }
    PageC PageC { get; }
}
```

## Map
The `Map` class implements IMapFacade and IMap. The class is named `Map` rather than `MapFacade` to ease the code writing.
As previously seen, it uses the DI container's scope to delay the creation of the pages.
`Nodes`, `Graph` and `DynamicNeighbors` also contains the page and are therefore delayed too.

```csharp
public Map(ILifetimeScope scope, TimeSpan findControlTimeout)
{
    this.scope = scope;
    findControlTimeoutPara = new TypedParameter(typeof(TimeSpan), findControlTimeout);
}

public HashSet<INavigable> Nodes => scope.Resolve<HashSet<INavigable>>();
public IGraph Graph => scope.Resolve<IGraph>();
public HashSet<DynamicNeighbor> DynamicNeighbors => new HashSet<DynamicNeighbor>();
public PageA PageA => scope.Resolve<PageA>(findControlTimeoutPara);
public PageB PageB => scope.Resolve<PageB>(findControlTimeoutPara);
public PageC PageC => scope.Resolve<PageC>(findControlTimeoutPara);
```

## BasePage
The only purpose of the `BasePage` is to reduce the amount of code in page classes. It is based on the abstract `Navigable` class to help with the implementation of INavigable.

## Page classes
For the moment we will create a basic implementation of the page classes:

```csharp
public class PageA : BasePage
{
    public PageA(IMapFacade map, ILog log, TimeSpan controlTimeout) : base(map, log, controlTimeout)
    {
    }

    /// <summary>
    /// The function returning the Exist status.
    /// </summary>
    /// <returns>The Exists status.</returns>
    protected override Func<bool> Exist() => () => { return true; };

    /// <summary>
    /// The function returning the Ready status.
    /// </summary>
    /// <returns>The Ready status.</returns>
    protected override Func<bool> Ready() => () => { return true; };
}
```