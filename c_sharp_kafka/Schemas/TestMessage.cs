// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.7.7.5
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace c_sharp_kafka.Schemas
{
    using Avro.Specific;
    using global::Avro;
	
	public partial class TestMessage : ISpecificRecord
	{
		public static Schema _SCHEMA = Schema.Parse("{\"type\":\"record\",\"name\":\"TestMessage\",\"namespace\":\"c_sharp_kafka.Schemas\",\"fields" +
				"\":[{\"name\":\"body\",\"type\":\"string\"}]}");
		private string _body;
		public virtual Schema Schema
		{
			get
			{
				return TestMessage._SCHEMA;
			}
		}
		public string body
		{
			get
			{
				return this._body;
			}
			set
			{
				this._body = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.body;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.body = (System.String)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
