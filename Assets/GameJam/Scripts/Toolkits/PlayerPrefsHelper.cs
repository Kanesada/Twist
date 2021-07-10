using System;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public static class PlayerPrefsHelper
{

	public static void SaveJsonData<T>(string key, T obj)
	{
		if (obj == null || string.IsNullOrEmpty(key))
			return;
		var json = JsonConvert.SerializeObject(obj);
		SaveStoredData<string>(key, json);
	}

	public static T LoadJsonData<T>(string key)
	{
		if (string.IsNullOrEmpty(key))
			return default(T);

		string json = GetStoredData<string>(key);
		T value = JsonConvert.DeserializeObject<T>(json);
		return value;
	}


	/// <summary>
	/// 保存值到PlayerPrefs
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value">传null表示删除key</param>
	/// <returns></returns>
	public static void SaveStoredData<T>(string key, object value)
	{
		if (string.IsNullOrEmpty(key))
			return;
		if (value == null)
		{
			PlayerPrefs.DeleteKey(key);
			return;
		}
		Type t = typeof(T);
		if (t == typeof(string))
		{
			string str = value.ToString();
			//用base64转码一下 以免中文出错
			str = ToBase64(str);
			PlayerPrefs.SetString(key, str);
		}
		else if (t == typeof(int) || t == typeof(Int16) || t == typeof(long) || t == typeof(Int64) || t == typeof(byte) || t == typeof(char))
		{
			PlayerPrefs.SetInt(key, (int)value);
		}
		else if (t == typeof(float) || t == typeof(decimal) || t == typeof(double))
		{
			PlayerPrefs.SetFloat(key, (float)value);
		}
		else
		{
			throw new Exception("T 暂时只支持 数字和字符串类型的数据");
		}

		PlayerPrefs.Save();
	}
	/// <summary>
	/// 获取PlayerPrefs存储的数据
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="key"></param>
	/// <returns></returns>
	public static T GetStoredData<T>(string key)
	{
		if (string.IsNullOrEmpty(key))
			throw new Exception("key is not existed.");
		Type t = typeof(T);
		if (t == typeof(int) || t == typeof(Int16) || t == typeof(long) || t == typeof(Int64) || t == typeof(byte) || t == typeof(char))
		{
			return (T)(object)PlayerPrefs.GetInt(key);
		}
		if (t == typeof(float) || t == typeof(decimal) || t == typeof(double))
		{
			return (T)(object)PlayerPrefs.GetFloat(key);
		}
		if (t == typeof(string))
		{
			//用base64转码一下 以免中文出错
			string str = PlayerPrefs.GetString(key);
			str = FromBase64(str);
			return (T)(object)str;
		}
		throw new Exception("T 暂时只支持 数字和字符串类型的数据");

	}
	/// <summary>
	/// PlayerPrefs是否包含有存储指定的key 的数据
	/// </summary>
	/// <param name="key"></param>
	/// <returns>PlayerPrefs.HasKey(key)</returns>
	public static bool HasStoredData(string key)
	{
		if (string.IsNullOrEmpty(key))
			return false;
		return PlayerPrefs.HasKey(key);
	}
	/// <summary>
	///  PlayerPrefs.DeleteKey(key)
	/// </summary>
	/// <param name="key"></param>
	public static void DeleteStoredData(string key)
	{
		if (string.IsNullOrEmpty(key))
			return;
		PlayerPrefs.DeleteKey(key);
		PlayerPrefs.Save();
	}
	/// <summary>
	///  PlayerPrefs.DeleteAll
	/// </summary>
	public static void ClearStoredData()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}

	/// <summary>
	/// 转成base64编码
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	private static string ToBase64(string str)
	{
		byte[] bta = Encoding.Default.GetBytes(str);
		return Convert.ToBase64String(bta);
	}
	/// <summary>
	/// 从base64字符串解码转成字符串
	/// </summary>
	/// <param name="base64"></param>
	/// <returns></returns>
	private static string FromBase64(string base64)
	{
		byte[] bta = Convert.FromBase64String(base64);
		return Encoding.Default.GetString(bta);
	}
}