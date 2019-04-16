# Default Interface Members

# Generic Attributes


# Default in Deconstruction
```
private static void DemonstrateDefaultInDeconstruction()
{
	//(int id, string? name) = (default, default);
	(int id, string name) = default;

	Console.Out.WriteLine($"{nameof(id)} = {id}, {nameof(name)} = {name}");
}
```
# Caller Expression Attribute