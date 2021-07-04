# Get started

We will use the `FacadeExample` project as support to understand how you could implement `Arium`.
This example was created as UI automation support but the logic could apply to other type of network requiring navigation.

# Definitions

Some definitions of the `FacadeExample` project to give you soem context.

## AUT

The `AUT` is a representation of the application under test. This is the entry point of the example.

## Page/Navigable

The word `Page` references to a representation of a page of your AUT. 
It could implements a [Page Model Object Pattern](https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/). In this example, the terms `Page` and `Navigable` are interchangeable, `Navigable` being the generic term of the network's objects. 

See Arium.Navigable for abstract implemention of a base class.

## Map

The word `Map` references to an object containing the declaration of your AUT's pages.
See Arium.Mapper for abstract implemention.

## Observers

The `Observers` are the objects that watch any change of a page's state.

## Log

A `Log` contains the historic of the navigation. It also notifies the 
page's observers of a change of state.

## Navigator

The `Navigator` is the object that will resolved the navigation at run time.
You should use the `Browser` instead of the `Navigator` to navigate.

## Browser

The `Browser` is the object provides methods to navigate. 
Most of these methods are returning the browser itself so they can be chained.

## TestContext

An object to carry parameters from the test tests library to the facade library,
like timeouts, Appium's driver, etc.

# The chicken and egg problem

The first problem we will face is the implementation of the pages. 
Given the recursive nature of the system to resolve the navigation at run time,
pages need references to other pages. This is done by passing the Nodes to pages constructor,
via Map. But the Map requires the pages references to provides the Nodes.

Basically, Map needs to reference the Pages, and the Pages need to reference Map.

The solution implemented in this example is to delayed the instantiation of Nodes and Graph
when instantiating Map using System.Lazy.
