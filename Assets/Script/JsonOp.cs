using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;




public class JsonOp
{
	private RootObject rb;

	private void getJson(string responseData)
    {
		//responseData.Replace("[", "").Replace("]", "");
		
		//Cannot deserialize the current JSON array (e.g. [1,2,3]) into type 'RootObject' because the type requires a JSON object (e.g. {"name":"value"}) to deserialize correctly.
		// 上面错误的出现是因为data两端有“[]”但类中没有写（也写不了这两个）
		rb = JsonConvert.DeserializeObject<RootObject>(responseData.Substring(1, responseData.Length - 2));
	}

	public float getTemp(string responseData)
	{
		getJson(responseData);
		// 成功输出
		// Debug.Log(rb.newest_events.te.val);
		float temperature = (float)Convert.ToDouble(rb.newest_events.te.val);
		return temperature;
	}

	public int getIlluminate(string responseData)
    {
		getJson(responseData);
		// 成功输出
		// Debug.Log(rb.newest_events.te.val);
		int illuminate = Convert.ToInt32(rb.newest_events.il.val);
		return illuminate;
	}

	public int getHumidity(string responseData)
    {
		getJson(responseData);
		// 成功输出
		// Debug.Log(rb.newest_events.te.val);
		int humidity = Convert.ToInt32(rb.newest_events.hu.val);
		return humidity;
	}
	// Start is called before the first frame update

}

public class Users
{
	public string id { get; set; }
	public string nickname { get; set; }
	public string superuser { get; set; }
}

public class Hu
{
	public string val { get; set; }
	public string created_at { get; set; }
}

public class Il
{
	public string val { get; set; }
	public string created_at { get; set; }
}

public class Mo
{
	public string val { get; set; }
	public string created_at { get; set; }
}

public class Te
{
	public string val { get; set; }
	public string created_at { get; set; }
}

public class Newest_events
{
	public Hu hu { get; set; }
	public Il il { get; set; }
	public Mo mo { get; set; }
	public Te te { get; set; }
}

public class RootObject
{
	public string name { get; set; }
	public string id { get; set; }
	public string created_at { get; set; }
	public string updated_at { get; set; }
	public string mac_address { get; set; }
	public string bt_mac_address { get; set; }
	public string serial_number { get; set; }
	public string firmware_version { get; set; }
	public string temperature_offset { get; set; }
	public string humidity_offset { get; set; }
	public List<Users> users { get; set; }
	public Newest_events newest_events { get; set; }
}

