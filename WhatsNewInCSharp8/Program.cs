using System;
using System.Threading.Tasks;

namespace WhatsNewInCSharp8
{
   class Program
   {
		static void Main() =>
			//Program.DemonstrateNullableReferenceTypes();
			Program.DemonstrateRecursivePatterns();
			//Program.DemonstrateEnhancedUsing();
			//Program.DemonstrateRangesAndIndexes();
			//Program.DemonstrateNullCoalescingAssigments();
			//Program.DemonstrateVerbatimInterpolatedStrings();
			//Program.DemonstrateStaticLocalFunctions();

		/*
		static async Task Main() =>
			await Program.DemonstrateAsyncStreams();
		*/

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/nullable-reference-types.md
		private static void DemonstrateNullableReferenceTypes()
		{
			//var person = new Person { Id = Guid.NewGuid(), Name = "Jason" };
			var person = new Person();
			Console.Out.WriteLine($"{person.Name}, {person.Id}");
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/patterns.md
		private static void DemonstrateRecursivePatterns()
		{
			var person = new Person { Id = Guid.NewGuid()/*, Name = "Jason"*/ };

			var value = person switch
			{
				{ Name: var name } => name switch
					{
						null => person.Id.ToString().Length,
						{ Length: var length } => length,
					}
			};

			Console.Out.WriteLine($"{nameof(value)} is {value}");
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/async-streams.md
		private static async Task DemonstrateAsyncStreams()
		{
			await foreach(var value in new AsynchronousRandom(10))
			{
				await Console.Out.WriteLineAsync(value.ToString());
			}
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/using.md
		private static void DemonstrateEnhancedUsing()
		{
			using var _ = new DisposableService();
			Console.WriteLine($"I am within the disposable scope of {nameof(DisposableService)}");
			Console.WriteLine($"I am still within the disposable scope of {nameof(DisposableService)}");

			using (var refStruct = new DisposableRefStruct())
			{
				Console.WriteLine($"I am within the disposable scope of {nameof(DisposableRefStruct)}");
				Console.WriteLine($"I am still within the disposable scope of {nameof(DisposableRefStruct)}");
			}
		}

		// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/ranges.md
		private static void DemonstrateRangesAndIndexes()
		{
			var values = new int[] { 3, 1, 4, 1, 5, 9, 2, 6, 5 };

			Console.Out.WriteLine($"Everything: {string.Join(", ", values[..])}");
			Console.Out.WriteLine($"From the 2nd to the 5th (exclusive): {string.Join(", ", values[2..5])}");
			Console.Out.WriteLine($"Everything after the 3rd: {string.Join(", ", values[3..])}");
			Console.Out.WriteLine($"Everything except the last: {string.Join(", ", values[..^1])}");
			Console.Out.WriteLine($"Just the last: {string.Join(", ", values[^1])}");
			Console.Out.WriteLine($"Just the second to the last: {string.Join(", ", values[^2])}");
			Console.Out.WriteLine($"Just the third to the last: {string.Join(", ", values[^3])}");

			var range = new Range(2, 5);
			var index = new Index(3, true);
			Console.Out.WriteLine($"From the 2nd to the 5th (exclusive) with a range: {string.Join(", ", values[range])}");
			Console.Out.WriteLine($"Just the third to the last with an index: {string.Join(", ", values[index])}");
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

			Console.WriteLine(currentWay);
			Console.WriteLine(newWay);
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