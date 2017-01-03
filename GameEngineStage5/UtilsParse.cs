using System.Collections.Generic;

/// <summary>
/// Класс сериализации/десериализации в текстовую строку
/// </summary>
public class UtilsParse
{
    public delegate T ParseFunc<T>(string str);
    public delegate string ToStringFunc();

    public UtilsParse()
    {
    }

    public static string[] Split(string str, char separator)
    {
        string[] res;
        int pos = str.IndexOf(separator);
        if (pos == -1) { res = new string[1]; res[0] = str; }
        else
        {
            res = new string[2];
            res[0] = str.Substring(0, pos);
            res[1] = str.Substring(pos + 1);
        }
        return res;
    }

    public static List<T> ParseList<T>(ParseFunc<T> parse, string value)
    {
        List<T> res = new List<T>();
        List<string> values = ParseType(value);      //value.Split(";");
        for (int i = 0; i < values.Count; i++) res.Add(parse(values[i]));
        return res;
    }

    public static string ToStringList<T>(List<T> list)
    {
        string res = "";
        for (int i = 0; i < list.Count; i++) res += "<" + list[i].ToString() + ">";
        return res;
    }

    public static List<string> ParseType(string value)
    {
        List<string> res = new List<string>();
        int key = 0;
        string lex = "";
        for (int i = 0; i < value.Length; i++)
        {
            switch (value[i])
            {
                case '<':
                    if (key > 0) lex += value[i];
                    key++;
                    break;
                case '>':
                    key--;
                    if (key > 0) lex += value[i];
                    else if (key == 0)
                    {
                        res.Add(lex);
                        lex = "";
                    }
                    else throw new System.ArgumentException("Error parse value ", value);
                    break;
                default:
                    lex += value[i];
                    break;
            }
        }
        return res;
    }

}
