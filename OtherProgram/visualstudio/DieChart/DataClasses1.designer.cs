﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DieChart
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="DB_9B1091_general")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["DB_9B1091_generalConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Die> Dies
		{
			get
			{
				return this.GetTable<Die>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Die")]
	public partial class Die
	{
		
		private string _Code;
		
		private string _Description;
		
		private string _Id1;
		
		private string _Id2;
		
		private string _Length;
		
		private System.Nullable<int> _Price_val;
		
		private string _Glass_Size;
		
		private string _ID1_ID2;
		
		public Die()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", DbType="NVarChar(115)")]
		public string Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this._Code = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(MAX)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this._Description = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id1", DbType="NVarChar(110)")]
		public string Id1
		{
			get
			{
				return this._Id1;
			}
			set
			{
				if ((this._Id1 != value))
				{
					this._Id1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id2", DbType="NVarChar(110)")]
		public string Id2
		{
			get
			{
				return this._Id2;
			}
			set
			{
				if ((this._Id2 != value))
				{
					this._Id2 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Length", DbType="NVarChar(110)")]
		public string Length
		{
			get
			{
				return this._Length;
			}
			set
			{
				if ((this._Length != value))
				{
					this._Length = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Price_val", DbType="Int")]
		public System.Nullable<int> Price_val
		{
			get
			{
				return this._Price_val;
			}
			set
			{
				if ((this._Price_val != value))
				{
					this._Price_val = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Glass_Size", DbType="NVarChar(125)")]
		public string Glass_Size
		{
			get
			{
				return this._Glass_Size;
			}
			set
			{
				if ((this._Glass_Size != value))
				{
					this._Glass_Size = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID1_ID2", DbType="NVarChar(50)")]
		public string ID1_ID2
		{
			get
			{
				return this._ID1_ID2;
			}
			set
			{
				if ((this._ID1_ID2 != value))
				{
					this._ID1_ID2 = value;
				}
			}
		}
	}
}
#pragma warning restore 1591