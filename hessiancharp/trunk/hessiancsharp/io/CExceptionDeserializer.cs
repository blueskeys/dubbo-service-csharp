using System;
using System.Collections; using System.Collections.Generic;
using System.Reflection;

namespace hessiancsharp.io
{
	/// <summary>
	/// Summary description for CExceptionDeserializer.
	/// </summary>
	public class CExceptionDeserializer : CObjectDeserializer
	{
		private IDictionary m_deserFields = new Dictionary<Object, Object>();
		private Type m_type = null;
		public CExceptionDeserializer(Type type):base(type)
		{
			List<Object> fieldList = CExceptionSerializer.GetSerializableFields();
			foreach (FieldInfo fieldInfo in fieldList)
			{
				m_deserFields[fieldInfo.Name] = fieldInfo;
			}
			m_type = type;
		}

		public override IDictionary GetDeserializableFields()
		{
			return m_deserFields;
		}

		public override object ReadMap(AbstractHessianInput abstractHessianInput)
		{
			Dictionary<Object, Object> fieldValueMap = new Dictionary<Object, Object>();
			string _message = null;
			Exception _innerException = null;
			while (! abstractHessianInput.IsEnd()) 
			{
				object objKey = abstractHessianInput.ReadObject();
				if(objKey != null)
				{
					IDictionary deserFields = GetDeserializableFields();
                    FieldInfo field = (FieldInfo) deserFields[objKey];
                    // try to convert a Java Exception in a .NET exception
					if(objKey.ToString() == "_message" || objKey.ToString() == "detailMessage")
					{
                        if (field != null)
						    _message = abstractHessianInput.ReadObject(field.FieldType) as string;
                        else
                            _message = abstractHessianInput.ReadObject().ToString();
					}
					else if(objKey.ToString() == "_innerException" || objKey.ToString() == "cause")
					{
                        try
                        {
                            if (field != null)
                                _innerException = abstractHessianInput.ReadObject(field.FieldType) as Exception;
                            else
                                _innerException = abstractHessianInput.ReadObject(typeof(Exception)) as Exception;
                        }
                        catch (Exception)
                        {
                            // als Cause ist bei Java gerne mal eine zirkul�re Referenz auf die Exception selbst
                            // angegeben. Das klappt nicht, weil die Referenz noch nicht registriert ist,
                            // weil der Typ noch nicht klar ist (s.u.)
                        }
                    }
					else
					{
                        if (field != null)
                        {
                            object objFieldValue = abstractHessianInput.ReadObject(field.FieldType);
                            fieldValueMap.Add(field, objFieldValue);
                        } else
                            // ignore (z. B. Exception Stacktrace "stackTrace" von Java)
                            abstractHessianInput.ReadObject();
						//field.SetValue(result, objFieldValue);
					}
				}
				
			}
			abstractHessianInput.ReadEnd();

			object result =  null;
			try
			{
#if COMPACT_FRAMEWORK
            	//CF TODO: tbd
#else
				try
				{
					result = Activator.CreateInstance(this.m_type, new object[2]{_message, _innerException});	
				}
				catch(Exception)
				{
					try
					{
						result = Activator.CreateInstance(this.m_type, new object[1]{_innerException});		
					}
					catch(Exception)
					{
						try
						{
							result = Activator.CreateInstance(this.m_type, new object[1]{_message});			
						}
						catch(Exception)
						{
							result = Activator.CreateInstance(this.m_type);			
						}
					}
                }
#endif

            }
			catch(Exception)
			{
				result = new Exception(_message, _innerException);
			}
			foreach (KeyValuePair<object, object> entry in fieldValueMap)
			{
				FieldInfo fieldInfo = (FieldInfo) entry.Key;
				object value = entry.Value;
				try {fieldInfo.SetValue(result, value);} catch(Exception){}
			}

            // besser sp�t als gar nicht.
            int refer = abstractHessianInput.AddRef(result);


			return result;
		}
	}
}
