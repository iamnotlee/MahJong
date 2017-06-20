using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class CSVReader
{
    static CSVReader csvreader;

    public static Dictionary<string, List<string>> SplitCsvGrid(string csvText)
    {
        return getDirectory(readcsv(csvText));
    }

    static string makefield(ref List<byte> bl, Encoding encoding)
    {
        if (bl.Count > 1 && bl[0] == '"' && bl[bl.Count - 1] == '"')
        {
            bl.RemoveAt(0);
            bl.RemoveAt(bl.Count - 1);
        }
        int n = 0;
        while (true)
        {
            if (n >= bl.Count)
                break;
            if (bl[n] == '"')
            {
                if (n < bl.Count - 1 && bl[n + 1] == '"')
                {
                    bl.RemoveAt(n + 1);
                    n++;
                }
                else
                    bl.RemoveAt(n);
            }
            else
                n++;
        }

        return encoding.GetString(bl.ToArray());
    }

    List<List<string>> endreadcsv(Encoding encoding)
    {
        string field = makefield(ref bl, encoding);
        if (!string.IsNullOrEmpty(field)) csvline.Add(field);
        if (csvline.Count > 0) csvdata.Add(csvline);
        csvline = new List<string>();
        bl = new List<byte>();

        return csvdata;
    }

    public void readcsv(byte[] data, int startwith, int datalen, Encoding encoding)
    {
        int dataoffset = startwith;
        if (readedbytes == 0)
        {
            if (datalen >= 3 &&
                data[0] == 0xEF &&
                data[1] == 0xBB &&
                data[2] == 0xBF)
            {
                dataoffset += 3;
                datalen -= 3;
            }
        }

        for (int i = dataoffset; i < dataoffset + datalen; i++)
        {
            if (IsInConvert)
            {
                IsInConvert = false;
                switch (data[i])
                {
                    case (byte)'n':
                        bl.Add((byte)'\n');
                        break;
                    case (byte)'t':
                        bl.Add((byte)'\t');
                        break;
                    case (byte)'r':
                        break;
                    default:
                        bl.Add((byte)'\\');
                        bl.Add(data[i]);
                        break;
                }
                continue;
            }

            switch (data[i])
            {
                case (byte)'\r':
                    if (IsInQuote)
                        bl.Add((byte)'\r');
                    break;
                case (byte)'\n':
                    if (IsInQuote)
                        bl.Add((byte)'\n');
                    else
                    {
                        string field = makefield(ref bl, encoding);
                        if (!string.IsNullOrEmpty(field)) csvline.Add(field);
                        csvdata.Add(csvline);
                        csvline = new List<string>();
                        bl = new List<byte>();
                    }
                    break;
                case (byte)',':
                    if (IsInQuote)
                        bl.Add(data[i]);
                    else
                    {
                        csvline.Add(makefield(ref bl, encoding));
                        bl = new List<byte>();
                    }
                    break;
                case (byte)'"':
                    IsInQuote = !IsInQuote;
                    bl.Add((byte)'"');
                    break;
                case (byte)'\\':
                    IsInConvert = true;
                    break;
                default:
                    bl.Add(data[i]);
                    break;
            }

        }

        readedbytes += (ulong)datalen;
    }

    public static List<List<string>> readcsv(string strin, Encoding encoding)
    {
        //if (csvreader == null)
        csvreader = new CSVReader();

        byte[] byt = encoding.GetBytes(strin);
        int n = byt.Length;

        int readed = 0;
        while (n > readed)
        {
            int nread = Mathf.Min(n - readed, 4096);
            csvreader.readcsv(byt, readed, nread, encoding);
            readed += nread;
        }
        return csvreader.endreadcsv(encoding);
    }

    public static List<List<string>> readcsv(string strin)
    {
        return readcsv(strin, Encoding.UTF8);
    }

    public static Dictionary<string, List<string>> getDirectory(List<List<string>> listin)
    {
        Dictionary<string, List<string>> dir = new Dictionary<string, List<string>>();
        for (int i = 0; i < listin.Count; i++)
        {
            if (string.IsNullOrEmpty(listin[i][0]))
                continue;
            dir[listin[i][0]] = listin[i];
        }
        return dir;
    }

    public static List<Dictionary<string, string>> getPCIK(List<List<string>> _Data)
    {
        List<Dictionary<string, string>> lst = new List<Dictionary<string, string>>();
        Dictionary<string, int> tableIndexs = new Dictionary<string, int>();
        for (int i = 0; i < _Data.Count; i++)
        {
            if (_Data[i][0].StartsWith("#%"))
            {
                for (int j = 0; j < _Data[i].Count; j++)
                {
                    if (string.IsNullOrEmpty(_Data[i][j]))
                        continue;

                    //_Data[i][j] = _Data[i][j].Replace(" ", "");

                    if (tableIndexs.ContainsKey(_Data[i][j]))
                    {
                        Debug.LogError("重复的#%" + _Data[i][j]);
                        continue;
                    }

                    if (j == 0)
                        tableIndexs.Add(_Data[i][j].Substring(2), j);
                    else
                        tableIndexs.Add(_Data[i][j], j);
                }
                continue;
            }
            if (_Data[i][0].StartsWith("#") || string.IsNullOrEmpty(_Data[i][0]))
                continue;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            lst.Add(dic);

            foreach (var item in tableIndexs)
                if (_Data[i].Count > item.Value)
                    dic.Add(item.Key, _Data[i][item.Value]);
        }
        return lst;
    }

    public static int CsvConfigFieldCheck(List<List<string>> data)
    {
        if (data.Count < 2)
            return -1;

        for (int l = 0; l < data.Count; l++)
        {
            if (data[l].Count < 1 || !data[l][0].StartsWith("#"))
                continue;
            for (int i = 0; i < data[l].Count; i++)
            {
                if (data[l][i] == "[END]")
                {
                    if (data[l + 1].Count > i && int.Parse(data[l + 1][i]) == i + 1)
                        return i;
                    return -1;
                }
            }
        }
        return -1;
    }

    public void clear()
    {
        bl = new List<byte>();
        csvline = new List<string>();
        csvdata = new List<List<string>>();
        IsInConvert = IsInQuote = false;
        readedbytes = 0;
    }

    bool IsInQuote = false;
    bool IsInConvert = false;

    List<byte> bl = new List<byte>();
    List<string> csvline = new List<string>();
    List<List<string>> csvdata = new List<List<string>>();

    ulong readedbytes = 0;
}
