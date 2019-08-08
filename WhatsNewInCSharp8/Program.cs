using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WhatsNewInCSharp8
{
	class Program
   {
		static void Main() =>
			//Program.DemonstrateRecursivePatterns();
			//Program.DemonstrateNullableReferenceTypes();
			//Program.DemonstrateNullablesAndReflection();
			//Program.DemonstrateEnhancedUsing();
			// Don't forget about Program.DemonstrateAsyncDisposable()
			// and Program.DemonstrateAsyncStreams()...
			//Program.DemonstrateRangesAndIndexes();
			//Program.DemonstrateDefaultInterfaceMembers();
			//Program.DemonstrateNullCoalescingAssigments();
			//Program.DemonstrateStaticLocalFunctions();
			//Program.DemonstrateVerbatimInterpolatedStrings();


		//static async Task Main() =>
			//await Program.DemonstrateAsynchronousDisposable();
			//await Program.DemonstrateAsynchronousStreams();


		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/patterns.md
		private static void DemonstrateRecursivePatterns()
		{
			var person = new Person { Id = Guid.NewGuid(), Name = "Jason" };

			var value = person switch
			{
				{ Name: var name } => name switch
				{
					{ Length: var length } => length,
					null => person.Id.ToString().Length,
				},
			};

			Console.Out.WriteLine($"{nameof(value)} is {value}");
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/nullable-reference-types.md
		private static void DemonstrateNullableReferenceTypes()
		{
			var person = new Person { Id = Guid.NewGuid() , Name = "Jason" };

			Console.Out.WriteLine($"{person.Name!.Length}, {person.Id}");
		}

		public static void ComplexParameter(
			Dictionary<List<string>, KeyValuePair<Guid, byte[]?>> value) { }

		private static void DemonstrateNullablesAndReflection()
		{
			Console.Out.WriteLine($"typeof(string).FullName - {typeof(string).FullName}");
			//Console.Out.WriteLine($"typeof(string?).FullName - {typeof(string?).FullName}");

			var parameter = typeof(Program).GetMethod(nameof(Program.ComplexParameter))!.GetParameters()[0];

			Console.Out.WriteLine(parameter.ParameterType.FullName);

			var nullableAttributeFlags =
				(from attribute in parameter.GetCustomAttributesData()
				 let attributeType = attribute.AttributeType
				 where attributeType.Name == "NullableAttribute" &&
				 attributeType.Namespace == "System.Runtime.CompilerServices" &&
				 attribute.ConstructorArguments.Count > 0
				 let nullableCtor = attribute.ConstructorArguments[0]
				 select nullableCtor.ArgumentType.IsArray switch
				 {
					 true => ((IList<CustomAttributeTypedArgument>)nullableCtor.Value!).Select(_ => (byte)_.Value!).ToArray(),
					 _ => new byte[] { (byte)nullableCtor.Value! }
				 }).First();

			Console.Out.WriteLine($"{nameof(nullableAttributeFlags)} - {string.Join(", ", nullableAttributeFlags)}");
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/using.md
		private static void DemonstrateEnhancedUsing()
		{
			using var _ = new DisposableService();
			Console.Out.WriteLine($"I am within the disposable scope of {nameof(DisposableService)}");
			Console.Out.WriteLine($"I am still within the disposable scope of {nameof(DisposableService)}");

			using (var refStruct = new DisposableRefStruct())
			{
				Console.Out.WriteLine($"I am within the disposable scope of {nameof(DisposableRefStruct)}");
				Console.Out.WriteLine($"I am still within the disposable scope of {nameof(DisposableRefStruct)}");
			}
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/async-streams.md
		private static async Task DemonstrateAsynchronousDisposable()
		{
			await using (var _ = new AsyncDisposableService())
			await Console.Out.WriteLineAsync(
				$"I am within the disposable scope of {nameof(AsyncDisposableService)}");
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/async-streams.md
		private static async Task DemonstrateAsynchronousStreams()
		{
			await foreach(var value in new AsynchronousRandom(10))
			{
				await Console.Out.WriteLineAsync(value.ToString());
			}
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/ranges.md
		private static void DemonstrateRangesAndIndexes()
		{
			var values = new int[] { 3, 1, 4, 1, 5, 9, 2, 6, 5 };

			Console.Out.WriteLine($"Everything: {string.Join(", ", values[..])}");
			Console.Out.WriteLine($"From the 2nd to the 5th (exclusive): {string.Join(", ", values[2..5])}");
			Console.Out.WriteLine($"Everything after the 3rd (including the 3rd): {string.Join(", ", values[3..])}");
			Console.Out.WriteLine($"Everything except the last: {string.Join(", ", values[..^1])}");
			Console.Out.WriteLine($"Just the last: {string.Join(", ", values[^1])}");
			Console.Out.WriteLine($"Just the second to the last: {string.Join(", ", values[^2])}");
			Console.Out.WriteLine($"Just the third to the last: {string.Join(", ", values[^3])}");

			var range = new Range(2, 5);
			var index = new Index(3, true);
			Console.Out.WriteLine($"From the 2nd to the 5th (exclusive) with a range: {string.Join(", ", values[range])}");
			Console.Out.WriteLine($"Just the third to the last with an index: {string.Join(", ", values[index])}");

			var rangeCopy = values[0..3];
			rangeCopy[0] = 22;
			Console.Out.WriteLine($"Changing rangeCopy[0]: {values[0]}");

			var rangeSlice = values.AsSpan()[0..3];
			rangeSlice[0] = 22;
			Console.Out.WriteLine($"Changing rangeSlice[0]: {values[0]}");
		}

		public interface IService
		{
			private static void WriteNewThing() => Console.Out.WriteLine("I did a new thing.");
			void DoSomething();
			void DoNewThing() => IService.WriteNewThing();
		}

		public sealed class Service 
			: IService
		{
			public void DoSomething() => Console.Out.WriteLine("I did something.");
		}

		public sealed class ServiceImplementingEverything
			: IService
		{
			public void DoSomething() => Console.Out.WriteLine("I did something.");
			public void DoNewThing() => Console.Out.WriteLine("I did my own thing");
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/default-interface-methods.md
		private static void DemonstrateDefaultInterfaceMembers()
		{
			var service = new Service();
			service.DoSomething();

			IService iService = new Service();
			iService.DoSomething();
			iService.DoNewThing();

			var everythingService = new ServiceImplementingEverything();
			everythingService.DoSomething();

			IService iEverythingService = new ServiceImplementingEverything();
			iEverythingService.DoSomething();
			iEverythingService.DoNewThing();
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/null-coalescing-assignment.md
		private static void DemonstrateNullCoalescingAssigments()
		{
			string? GetName() => "Jason";

			string? GetNullName() => null;

			var name = GetNullName();

			// Old way:
			/*
			if (name == null)
			{
				name = GetName();
			}
			*/

			// New way:
			name ??= GetName();

			Console.Out.WriteLine(name);
		}

		// https://github.com/dotnet/csharplang/issues/1630
		private static void DemonstrateVerbatimInterpolatedStrings()
		{
			var value = new Random().Next();
			var currentWay = $@"Here is the current way: {value}";
			var newWay = @$"Here is the new way: {value}";

			Console.Out.WriteLine(currentWay);
			Console.Out.WriteLine(newWay);
		}

		// https://github.com/dotnet/csharplang/issues/1565
		private static void DemonstrateStaticLocalFunctions()
		{
			var value = new Random().Next();

			int CanCaptureLocal() => value;

			static int CannotCaptureLocal() => new Random().Next();

			Console.Out.WriteLine($"{nameof(value)} - {CanCaptureLocal()}");
			Console.Out.WriteLine($"{nameof(CanCaptureLocal)} - {CanCaptureLocal()}");
			Console.Out.WriteLine($"{nameof(CannotCaptureLocal)} - {CannotCaptureLocal()}");
		}
	}
}