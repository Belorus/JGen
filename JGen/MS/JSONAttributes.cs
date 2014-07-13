using System;

namespace JGen.MS
{
	public enum JSONType
	{
		Unknown,
		Primitive,
		Array,
		Object,
		Enum,
	}

	public class JSONValueAttribute : Attribute
	{
		private string[] path = null;
        private string name = null;

		public JSONValueAttribute ()
		{
		}

		public string[] Path
		{
			get { return path; }
		}

        public string Name
        {
            set
            {
                name = value;
                path = name.Split(':');
            }
            get
            {
                return name;
            }
        }

		public bool IsRequired 
        { get; set; }

        public JSONType Type 
        { get; set; }
	}

    public class JSONKeyAttribute : Attribute
	{
		public JSONKeyAttribute ()
		{
		}
	}
}
