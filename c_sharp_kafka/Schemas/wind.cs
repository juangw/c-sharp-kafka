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
	
	public partial class wind : ISpecificRecord
	{
		public static Schema _SCHEMA = Schema.Parse("{\"type\":\"record\",\"name\":\"wind\",\"namespace\":\"c_sharp_kafka.Schemas\",\"fields\":[{\"na" +
				"me\":\"speed\",\"type\":\"float\"},{\"name\":\"deg\",\"type\":\"int\"},{\"name\":\"gust\",\"type\":\"f" +
				"loat\"}]}");
		private float _speed;
		private int _deg;
		private float _gust;
		public virtual Schema Schema
		{
			get
			{
				return wind._SCHEMA;
			}
		}
		public float speed
		{
			get
			{
				return this._speed;
			}
			set
			{
				this._speed = value;
			}
		}
		public int deg
		{
			get
			{
				return this._deg;
			}
			set
			{
				this._deg = value;
			}
		}
		public float gust
		{
			get
			{
				return this._gust;
			}
			set
			{
				this._gust = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.speed;
			case 1: return this.deg;
			case 2: return this.gust;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.speed = (System.Single)fieldValue; break;
			case 1: this.deg = (System.Int32)fieldValue; break;
			case 2: this.gust = (System.Single)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
