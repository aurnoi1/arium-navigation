# Get started

We will use the `FacadeExample` project as support to understand how you could implement `Arium`.
This example was created as UI automation support but the logic could apply to other type of network requiring navigation.

# Definitions
Some definitions of the `FacadeExample` to put you in context.

## AUT
The `AUT` is a representation of the application under test.

## Page/Navigable
The word `Page` references to a representation of a page of your AUT. It could implements a [Page Model Object Pattern](https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/). In this example, the terms `Page` and `Navigable` are interchangeable, `Navigable` being the generic term of the network's objects. 

## Map
The word `Map` references to an object containing the declaration of your AUT's pages.

## Observers
The `Observers` are the objects that watch any change of a page's state.

## Log
A `Log` contains the historic of the navigation. It also notify the page's observers of a change of state.

## Navigator
The `Navigator` is the object that will resolved the navigation at run time.

# The chicken and egg problem
The first problem we will face is the implementation of the pages. The navigation needs page to reference instances of other pages