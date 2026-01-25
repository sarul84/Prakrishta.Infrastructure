# Prakrishta.Infrastructure
Small generic interfaces and common utilities for building .net applications

This project has generic repositories both sync and async that help to build .net applications. There are few extension methods and helper classes commonly used by programmers across the applications.

Build Status:
[![Build Status](https://dev.azure.com/prakrishta0270/Prakrishta%20Open%20Source/_apis/build/status/sarul84.Prakrishta.Infrastructure?branchName=master)](https://dev.azure.com/prakrishta0270/Prakrishta%20Open%20Source/_build/latest?definitionId=3&branchName=master)

Interfaces:

1. IAddCollection
2. IAddItem
3. ICount
4. ICrudRepository
5. IDeleteItem
6. ISearchCollection
7. ISearchSingle
8. IUpdateItem
9. IDynamicResolver<TInterface, TArg>

Helper classes:
1. Async Helper => to run async method synchronously with out any deadlock
2. EnumHelper
3. DelegateComparer
4. PropertyComparer
5. DateFormatConverter => to convert date to specific format while deserializing with NewtonJson
6. TypedClientResolver => to resolve named typed http client

Extensions:
1. DateTime
2. DataTable => to strongly typed entity
3. Object
4. Collection
5. Enumerable
6. String
