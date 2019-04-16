using System;
using System.Threading.Tasks;

namespace WhatsNewInCSharp8
{
   class Program
   {
		static void Main() =>
			//Program.DemonstrateNullableReferenceTypes();
			//Program.DemonstrateEnhancedUsing();
			Program.DemonstrateRanges();
			//Program.DemonstrateVerbatimInterpolatedStrings();

		/*
		static async Task Main() =>
			await Program.DemonstrateAsyncStreams();
		*/

		private static void DemonstrateNullableReferenceTypes() { }

		private static void DemonstrateRecursivePatterns() { }

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
		private static void DemonstrateRanges()
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
			Console.Out.WriteLine($"From the 2nd to the 5th (exclusive) with a range: {string.Join(", ", values[range])}");
		}

		private static void DemonstrateDefaultInDeconstruction() { }

		private static void DemonstrateNullCoalescingAssigments() { }

		// https://github.com/dotnet/csharplang/issues/1630
		private static void DemonstrateVerbatimInterpolatedStrings()
		{
			var value = new Random().Next();
			var currentWay = $@"Here is the current way: {value}";
			var newWay = @$"Here is the new way: {value}";

			Console.WriteLine(currentWay);
			Console.WriteLine(newWay);
		}

		private static void DemonstrateStaticLocalFunctions() { }
	}
}
