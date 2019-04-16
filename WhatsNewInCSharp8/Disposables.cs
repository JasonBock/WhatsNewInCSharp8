using System;

namespace WhatsNewInCSharp8
{
	public class DisposableService
		: IDisposable
	{
		public void Dispose() => Console.WriteLine($"{nameof(DisposableService)} - I am disposed.");
	}

	public ref struct DisposableRefStruct
	{
		public void Dispose() => Console.WriteLine($"{nameof(DisposableRefStruct)} - I am disposed.");
	}
}
