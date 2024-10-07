using System;
using System.Collections.Generic;

namespace Socks
{
	public class Disposable : IDisposable
	{
		private bool _disposedValue;
		private List<IDisposable> _disposables = [];

		public bool IsDisposed => _disposedValue;

		public Disposable()
		{
		}

		protected T Add<T>(T disposable) where T : IDisposable
		{
			_disposables.Add(disposable);
			return disposable;
		}

		protected void ThrowIfDisposed()
		{
			ObjectDisposedException.ThrowIf(_disposedValue, this);
		}

		protected void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					_disposables.ForEach(disposable => disposable.Dispose());

					Disposing();
				}

				_disposedValue = true;
			}
		}

		protected virtual void Disposing()
		{
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}