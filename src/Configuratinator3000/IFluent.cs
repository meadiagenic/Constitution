using System;
using System.ComponentModel;

namespace Configuratinator3000
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface IFluent
	{
		/// <summary/>
		[EditorBrowsable(EditorBrowsableState.Never)]
		Type GetType();

		/// <summary/>
		[EditorBrowsable(EditorBrowsableState.Never)]
		int GetHashCode();

		/// <summary/>
		[EditorBrowsable(EditorBrowsableState.Never)]
		string ToString();

		/// <summary/>
		[EditorBrowsable(EditorBrowsableState.Never)]
		bool Equals(object obj);
	}
}