// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.7.7.5
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace c_sharp_kafka.Schemas
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	public partial class weather_record : ISpecificRecord
	{
		public static Schema _SCHEMA = Schema.Parse("{\"type\":\"record\",\"name\":\"weather_record\",\"namespace\":\"c_sharp_kafka.Schemas\",\"fie" +
				"lds\":[{\"name\":\"id\",\"type\":\"int\"},{\"name\":\"main\",\"type\":\"string\"},{\"name\":\"descri" +
				"ption\",\"type\":\"string\"},{\"name\":\"icon\",\"type\":\"string\"}]}");
		private int _id;
		private string _main;
		private string _description;
		private string _icon;
		public virtual Schema Schema
		{
			get
			{
				return weather_record._SCHEMA;
			}
		}
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}
		public string main
		{
			get
			{
				return this._main;
			}
			set
			{
				this._main = value;
			}
		}
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}
		public string icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.id;
			case 1: return this.main;
			case 2: return this.description;
			case 3: return this.icon;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.id = (System.Int32)fieldValue; break;
			case 1: this.main = (System.String)fieldValue; break;
			case 2: this.description = (System.String)fieldValue; break;
			case 3: this.icon = (System.String)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
