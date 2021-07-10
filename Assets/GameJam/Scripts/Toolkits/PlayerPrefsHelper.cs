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
	/// ����ֵ��PlayerPrefs
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value">��null��ʾɾ��key</param>
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
			//��base64ת��һ�� �������ĳ���
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
			throw new Exception("T ��ʱֻ֧�� ���ֺ��ַ������͵�����");
		}

		PlayerPrefs.Save();
	}
	/// <summary>
	/// ��ȡPlayerPrefs�洢������
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
			//��base64ת��һ�� �������ĳ���
			string str = PlayerPrefs.GetString(key);
			str = FromBase64(str);
			return (T)(object)str;
		}
		throw new Exception("T ��ʱֻ֧�� ���ֺ��ַ������͵�����");

	}
	/// <summary>
	/// PlayerPrefs�Ƿ�����д洢ָ����key ������
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
	/// ת��base64����
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	private static string ToBase64(string str)
	{
		byte[] bta = Encoding.Default.GetBytes(str);
		return Convert.ToBase64String(bta);
	}
	/// <summary>
	/// ��base64�ַ�������ת���ַ���
	/// </summary>
	/// <param name="base64"></param>
	/// <returns></returns>
	private static string FromBase64(string base64)
	{
		byte[] bta = Convert.FromBase64String(base64);
		return Encoding.Default.GetString(bta);
	}
}