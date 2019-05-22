# Works in Progress
These are features that have been proposed for C#8, but they aren't available just yet. Some of this may be incorrect, or may change in the future.
## stackalloc in nested contexts
https://github.com/dotnet/csharplang/issues/1412

https://github.com/dotnet/csharplang/issues/1757

Honestly, I wish I could figure out what this is :). The most interesting part of this feature is this:
> We could permit params Span<T> to implement params parameter-passing without any heap allocation. This could make the use of params methods much more efficient.