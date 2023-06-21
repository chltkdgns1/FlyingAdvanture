using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Pair<T, U>
{
    public T first;
    public U second;

    public Pair(T first, U second)
    {
        this.first = first;
        this.second = second;
    }
}

public class UtilManager
{
    static StringBuilder tempA = new StringBuilder();
    static StringBuilder tempB = new StringBuilder();
    static public long GetLongValueDic(Dictionary<string, object> dic, string key)
    {
        if (dic.ContainsKey(key))
        {
            return long.Parse(dic[key].ToString());
        }
        return 0;
    }

    static public string GetStrValueDic(Dictionary<string, object> dic, string key)
    {
        if (dic.ContainsKey(key))
        {
            return dic[key].ToString();
        }
        return "null";
    }

    static public bool GetBoolValueDic(Dictionary<string, object> dic, string key)
    {
        if (dic.ContainsKey(key))
        {
            return bool.Parse(dic[key].ToString());
        }
        return false;
    }

    static public object GetObjectValueDic(Dictionary<string, object> dic, string key)
    {
        if (dic.ContainsKey(key))
        {
            return dic[key];
        }
        return null;
    }

    static public int GetIntValueDic(Dictionary<string, object> dic, string key)
    {
        if (dic.ContainsKey(key))
        {
            return int.Parse(dic[key].ToString());
        }
        return 0;
    }

    static public List<T> ToList<T>(T[] arr)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < arr.Length; i++)
        {
            list.Add(arr[i]);
        }
        return list;
    }
    static public void Split(List<string> splitList, string str, char ch)
    {
        tempA.Clear();
        splitList.Clear();

        int len = str.Length;

        for (int i = 0; i < len; i++)
        {
            if (str[i] == ch)
            {
                splitList.Add(tempA.ToString());
                tempA.Clear();
                continue;
            }

            tempA.Append(str[i]);
        }

        if (tempA.Length > 0)
        {
            splitList.Add(tempA.ToString());
        }
    }

    static public string GetStrCoin(long coin)
    {
        tempA.Clear();
        tempB.Clear();

        int cnt = 0;
        while (coin != 0)
        {
            if (cnt == 3)
            {
                tempA.Append(',');
                cnt = 0;
            }
            tempA.Append((coin % 10).ToString());
            coin /= 10;
            cnt++;
        }

        if (tempA.Length == 0) tempA.Append("0");

        int sIndex = tempA.Length - 1;
        for (int i = sIndex; i >= 0; i--)
        {
            tempB.Append(tempA[i]);
        }

        return tempB.ToString();
    }

    static public void OpenURL(string url)
    {

    }

    static public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    static public Vector3 GetReverseAngle(Vector3 angle)
    {
        float x = angle.x > 0 ? -angle.x : angle.x;
        float y = angle.y > 0 ? -angle.y : angle.y;
        float z = angle.z > 0 ? -angle.z : angle.z;
        return new Vector3(x, y, z);
    }

    static public Vector3 GetMinAngleAbs(Vector3 angles)
    {
        float x = GetMaxAbs(angles.x, GetPosAngle(angles.x));
        float y = GetMaxAbs(angles.y, GetPosAngle(angles.y));
        float z = GetMaxAbs(angles.z, GetPosAngle(angles.z));
        return new Vector3(x, y, z);
    }

    static float GetPosAngle(float x)
    {
        return 360 - x;
    }

    static float GetMaxAbs(float x, float y)
    {
        float absX = Mathf.Abs(x);
        float absY = Mathf.Abs(y);
        if (absX > absY)
            return x;
        return -x;
    }
}
