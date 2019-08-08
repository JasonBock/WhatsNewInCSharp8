# Works in Progress
These are features that have been proposed for "C# Next". It's unclear if that means a point release for C#8, or a future version of C#.
## Caller Expression Attribute
https://github.com/dotnet/csharplang/blob/master/proposals/caller-argument-expression.md
```
T Single<T>(this T[] array)
{
	Debug.Assert(array != null);
	Debug.Assert(array.Length == 1);

	return array[0];
}

// Compiles to...

T Single<T>(this T[] array)
{
	Debug.Assert(array != null, "array != null");
	Debug.Assert(array.Length == 1, "array.Length == 1");

	return array[0];
}
```
## Target-types new
https://github.com/dotnet/csharplang/issues/100
```
public sealed class Customer
{
	public Customer(string name) => this.Name = name;
	public string Name { get; }
}

var Customer = new Customer[]
{
	new("Jason"), new("Joan")
};
```
## Generic Attributes
https://github.com/dotnet/csharplang/issues/124

https://github.com/JasonBock/Injectors
```
[AttributeUsage(AttributeTargets.Class)]
public abstract class InjectorAttribute<T>
	: Attribute
{
	protected abstract void OnInject(T target);
}

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
[Serializable]
public sealed class NotNullAttribute : InjectorAttribute<ParameterDefinition>
{
	protected override void OnInject(ParameterDefinition target) { // ... }
}
```
## Default in Deconstruction
https://github.com/dotnet/roslyn/pull/25562
```
private static void DemonstrateDefaultInDeconstruction()
{
	// This currently works:
	//(int id, string? name) = (default, default);

	// This is the proposal:
	(int id, string name) = default;

	Console.Out.WriteLine($"{nameof(id)} = {id}, {nameof(name)} = {name}");
}
```
## Relax ordering of `ref` and `partial` modifiers
https://github.com/dotnet/csharplang/issues/946
```
// Both of these would be allowed.
// Right now, only the first is allowed.
ref partial struct AnotherStruct { }

partial ref struct SomeStruct { }
```
## Parameter null-checking
https://github.com/dotnet/csharplang/issues/2145

Changes this:
```
void Insert(string s) 
{
	if (s is null) 
	{
		throw new ArgumentNullException(nameof(s));
	}
	// ...
}
```
To this:
```
void Insert(string s!) 
{
	//  ...
}
```