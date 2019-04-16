using System;

namespace WhatsNewInCSharp8
{
   class Program
   {
		static void Main() =>
			//Program.DemonstrateNullableReferenceTypes();
			Program.DemonstrateVerbatimInterpolatedStrings();

		private static void DemonstrateNullableReferenceTypes() { }

		private static void DemonstrateRecursivePatterns() { }

		private static void DemonstrateAsyncStreams() { }

		private static void DemonstrateEnhancedUsing() { }

		private static void DemonstrateRanges() { }

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
