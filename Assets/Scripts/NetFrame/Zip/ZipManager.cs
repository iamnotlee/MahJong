using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.GZip;

/*
 * 
 * 作者：
 * 作用：压缩/解压缩管理类
 * 
 * */

namespace DataFrame.NetInfo {
    public class ZipManager
    {
        private static ZipManager _instance;
        public static ZipManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ZipManager();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="zipData"></param>
        /// <returns></returns>
        public byte[] Compress(byte[] zipData)
        {
            try
            {
                MemoryStream memory_stream = new MemoryStream();
                GZipOutputStream gzip_ostream = new GZipOutputStream(memory_stream);
                gzip_ostream.Write(zipData, 0, zipData.Length);
                gzip_ostream.Flush();
                gzip_ostream.Finish();
                byte[] result_bytes = memory_stream.ToArray();
                gzip_ostream.Close();

#if __Debug_Zip
                string temp = "";
                for (int i = 0; i < result_bytes.Length; i++)
                {
                    temp += result_bytes[i] + " ";
                }
                Debug.Log("压缩后数据：" + temp);
#endif

                return result_bytes;

            }
            catch (Exception ex)
            {
                Debug.LogError("Compress " + ex);
            }
            return null;
        }

        /// <summary>
        /// 解压缩字节数组
        /// </summary>
        /// <param name="zipData"></param>
        /// <returns></returns>
        public byte[] Decompress(byte[] zipData)
        {
            try
            {
                GZipInputStream gzip_istream = new GZipInputStream(new MemoryStream(zipData));
                MemoryStream memory_stream = new MemoryStream();
                int count = 0;
                byte[] data = new byte[32];
                while ((count = gzip_istream.Read(data, 0, data.Length)) != 0)
                {
                    memory_stream.Write(data, 0, count);
                }
                gzip_istream.Close();
                memory_stream.Flush();
                byte[] result_bytes = memory_stream.ToArray();
                memory_stream.Close();

#if __Debug_Zip

                string temp = "";
                for (int i = 0; i < result_bytes.Length; i++)
                {
                    temp += result_bytes[i] + " ";
                }
                Debug.Log("解压缩后数据：" + temp);
#endif

                return result_bytes;
            }
            catch (Exception ex)
            {
                Debug.LogError("Decompress " + ex);
            }
            return null;
        }

    }

}

