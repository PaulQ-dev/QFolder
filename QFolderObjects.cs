namespace QFolder
{
	internal readonly struct QItemD
	{
		public readonly string name;
		public readonly int parentD; // -1 is source folder

		public QItemD(string name, int parentD)
		{
			this.name = name;
			this.parentD = parentD;
		}
	}
	internal readonly struct QItemF
	{
		public readonly string name;
		public readonly byte[] data;
		public readonly int parentD; // -1 is source folder
		public QItemF(string name, int parentD, byte[] data)
		{
			this.name = name;
			this.parentD = parentD;
			this.data = data;
		}
	}
}