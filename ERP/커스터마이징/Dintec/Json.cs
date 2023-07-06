using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace Dintec
{
	public class Json
	{
		public static Dictionary<string, string> Deserialize(string jsonString)
		{
			Dictionary<string, string> step1 = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

			//if (step1)
			if (step1.ElementAt(0).Key == "d")
				return JsonConvert.DeserializeObject<Dictionary<string, string>>(step1["d"]);
			else 
				return step1;
		}

		public static string Serialize(object value)
		{ 
			return JsonConvert.SerializeObject(value);
		}
	}
}
