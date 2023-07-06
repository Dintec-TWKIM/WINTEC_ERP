using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Data;

namespace DX
{
	public class JSON
	{
		//public static Dictionary<string, string> Deserialize(string jsonString)
		//{
		//	Dictionary<string, string> step1 = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

		//	//if (step1)
		//	if (step1.ElementAt(0).Key == "d")
		//		return JsonConvert.DeserializeObject<Dictionary<string, string>>(step1["d"]);
		//	else 
		//		return step1;
		//}

		//public static DataTable Deserialize(string jsonString)
		//{
		//	return JsonConvert.DeserializeObject<DataTable>(jsonString);
		//}

		public static T Deserialize<T>(string jsonString)
		{
			return JsonConvert.DeserializeObject<T>(jsonString);
		}

		public static string Serialize(object value)
		{ 
			return JsonConvert.SerializeObject(value);
		}

		





		public static string 직렬화(object value)
		{
			return JsonConvert.SerializeObject(value);
		}

		public static T 역직렬화<T>(string jsonString)
		{
			T json = JsonConvert.DeserializeObject<T>(jsonString);

			//if (step1)

			if (typeof(T) == typeof(Dictionary<string, string>) && (json as Dictionary<string, string>).ElementAt(0).Key == "d")
			{
				
					return JsonConvert.DeserializeObject<T>((json as Dictionary<string, string>)["d"]);
				

				//if (((Dictionary<string, string>)Convert.ChangeType(json, typeof(Dictionary<string, string>))).Keys.ElementAt(0).Key == "d")
				//{

				//}

				//if (((Dictionary<string, string>)json).key.ElementAt(0).Key == "d")
				//{

				//}
				//if (json.ElementAt(0).Key == "d")
				//	return JsonConvert.DeserializeObject<T>(json["d"]);
			}
			else
				return json;


			//return JsonConvert.DeserializeObject<T>(jsonString);
		}
	}
}
