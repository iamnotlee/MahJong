using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

/*
 * 
 * 作者：
 * 作用：加密管理类
 * 
 * */

namespace DataFrame.NetInfo {
    public class EncryptManager
    {
        /// <summary>
        /// AES加密解密钥匙串
        /// </summary>
        private static string AES_Default_Key = "abcde234123@#ew9182h4r1238132&AUIFQEIfaidsfnqeiqewrndsifqnwqiwernqewiqeiqe";
        /// <summary>
        /// AES加密解密向量
        /// </summary>
        private static string AES_Default_IV = "s9afn1@#$!#asdfiqwnerasidfnqwiesndaf912#$!@341";

        private static byte[] Default_Key_Bytes = UTF8Encoding.UTF8.GetBytes(AES_Default_Key.Substring(0, 32));// key必须是32位
        private static byte[] Default_IV_Bytes = UTF8Encoding.UTF8.GetBytes(AES_Default_IV.Substring(0, 16));//iv必须是16位

        private static EncryptManager _instance;
        public static EncryptManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EncryptManager();
                }
                return _instance;
            }
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <returns></returns>
        private byte[] AESEncrypt(byte[] inputByte)
        {
            //  Debug.Log(DES_Key + "      " + DES_Key.Substring(0, 32));

            RijndaelManaged rDel = new RijndaelManaged();

            rDel.Key = Default_Key_Bytes;
            rDel.Mode = CipherMode.CBC;
            rDel.IV = Default_IV_Bytes;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputByte, 0, inputByte.Length);

            string s = "";
            for (int i = 0; i < resultArray.Length; i++)
            {
                s += resultArray[i] + " ";
            }
       //     Debug.Log("加密后：" + s);

            return resultArray;

            //DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //Debug.Log("tempB.Length = " + tempB.Length + "      " + des.BlockSize);
            //des.Mode = CipherMode.CBC;
            //des.Key = tempB;
            //des.IV = tempB;
            //MemoryStream ms = new MemoryStream();
            //CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //cs.Write(inputByte, 0, inputByte.Length);
            //cs.FlushFinalBlock();
            //ms.Position = 0;
            //byte[] outputByte = ms.ToArray();
            //ms.Close();
            //return outputByte;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <returns></returns>
        private byte[] AESDecrypt(byte[] inputByte)
        {
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = Default_Key_Bytes;
            rDel.Mode = CipherMode.CBC;
            rDel.IV = Default_IV_Bytes;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputByte, 0, inputByte.Length);
            string s = "";
            for (int i = 0; i < resultArray.Length; i++)
            {
                s += resultArray[i] + " ";
            }
         //   Debug.Log("解密后：" + s);
            return resultArray;

            //DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //des.Key = tempB;
            //des.IV = tempB;
            //MemoryStream ms = new MemoryStream();
            //CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //cs.Write(inputByte, 0, inputByte.Length);
            //cs.FlushFinalBlock();
            //ms.Position = 0;
            //byte[] outputByte = ms.ToArray();
            //ms.Close();
            //return outputByte;
        }

        /// <summary>
        /// md5加密字节数组
        /// </summary>
        /// <param name="inputByte"></param>
        /// <returns></returns>
        private byte[] MD5EncryptByte(byte[] inputByte)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(inputByte);
        }

        /// <summary>
        /// md5加密字符串
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        private string MD5EncryptString(string inputStr)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(inputStr));
            return System.Text.Encoding.Default.GetString(result);
        }

        /// <summary>
        /// md5  32位 加密字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string MD5EncryptBy32Bit(byte[] inputByte)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            inputByte = md5.ComputeHash(inputByte);
            md5.Clear();

            string ret = "";
            for (int i = 0; i < inputByte.Length; i++)
            {
                ret += Convert.ToString(inputByte[i], 16).PadLeft(2, '0');
            }

            return ret.PadLeft(32, '0');
        }

        /// <summary>
        /// MD5加密Sn
        /// </summary>
        /// <param name="inputByte"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string MD5Encrypt(byte[] inputByte, string key = "test")
        {

            byte[] tempStr = System.Text.Encoding.Default.GetBytes(key);

            byte[] tempTotal = new byte[tempStr.Length + inputByte.Length];

            Array.Copy(inputByte, 0, tempTotal, 0, inputByte.Length);
            Array.Copy(tempStr, 0, tempTotal, inputByte.Length, tempStr.Length);

            string temp = MD5EncryptBy32Bit(tempTotal);
            return temp;
        }

        /// <summary>
        /// 校验Sn
        /// </summary>
        /// <param name="serverSn"></param>
        /// <param name="inputByte"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CompareServerSnWithLocalSn(string serverSn, byte[] inputByte, string key = "test")
        {

           
            
            string temp = MD5Encrypt(inputByte, key);


            if (serverSn != null && serverSn.Equals(temp))
            {
            //    Logger.LogError("校验Sn: 通过");
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        private byte[] encrption(byte[] inputData, string key, string iv)
        {
            MemoryStream msEncrypt = null;
            RijndaelManaged aesAlg = null;
            try
            {
                byte[] keys = System.Text.Encoding.UTF8.GetBytes(key);
                byte[] ivs = System.Text.Encoding.UTF8.GetBytes(iv);
                aesAlg = new RijndaelManaged();

                aesAlg.Key = keys;
                aesAlg.IV = ivs;

                ICryptoTransform ict = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                msEncrypt = new MemoryStream();

                using (CryptoStream cts = new CryptoStream(msEncrypt, ict, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cts))
                    {
                        sw.Write(inputData);
                    }
                }

            }
            finally
            {
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }
            byte[] content = null;
            if (msEncrypt != null)
            {
                content = msEncrypt.ToArray();
            }
            return content;
        }
    }

}

