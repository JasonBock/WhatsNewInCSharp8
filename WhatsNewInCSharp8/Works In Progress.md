# Works in Progress
These are features that have been proposed for C#8, but they aren't available just yet. Some of this may be incorrect, or may change in the future.
## Default Interface Members
https://github.com/dotnet/csharplang/blob/master/proposals/default-interface-methods.md
```
public interface ICalculation
{
	int Calculate() => 0;
}

public sealed class Calculation
	: ICalculation
{
	// This is OK, even though we don't implement Calculate().
}
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
## Target-types `new` Expresssion
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