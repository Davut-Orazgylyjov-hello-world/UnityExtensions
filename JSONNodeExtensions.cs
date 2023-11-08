using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace Extensions
{
    public static class JSONNodeExtensions
    {
        public static JSONNode GetJsonNodeByFullKeyPath(string fullKey, JSONNode jsonData)
           {
               List<string> curKey = new List<string>();
               curKey.AddRange(fullKey.Split("/"));
               return GetJsonNodeByFullKeyPath(fullKey, jsonData, curKey);
           }
           
           private static JSONNode GetJsonNodeByFullKeyPath(string fullKey, JSONNode jsonData, List<string> curKey)
           {
               try
               {
                   if (jsonData == null || jsonData.IsNull) return jsonData;
                   if (fullKey == "" || curKey.Count == 0) return jsonData;
                   string parentKey = curKey[0];
                   if (curKey.Count > 1)
                   {
                       if (jsonData.HasKey(parentKey))
                       {
                           curKey.Remove(parentKey);
                         return GetJsonNodeByFullKeyPath(fullKey, jsonData[parentKey], curKey);
                       }
                       if (jsonData.IsArray)
                       {
                           curKey.Remove(parentKey);
                          return GetJsonNodeByFullKeyPath(fullKey,jsonData[int.Parse(parentKey)], curKey);
                       }
                       
                       Debug.LogWarning($"Can't find value for this key {parentKey}");
                       return jsonData;
                   }
       
                   curKey.Clear();
                   curKey.AddRange(fullKey.Split("/"));
                   if (jsonData.IsString)
                   {
                       return jsonData.Value;
                   }
                   if (jsonData.HasKey(parentKey)) 
                       return jsonData[parentKey];
                   
                   if (jsonData.IsArray)
                       return jsonData[int.Parse(parentKey)];
                   
                   Debug.LogWarning($"Can't find value for this key {parentKey}");
               }
               catch (Exception e)
               {
                   Debug.LogWarning($"Failed Get Json Node by full key path");
               }
       
               Debug.LogWarning($"Failed Get Json Node by full key path");
               return jsonData;
           }
    }
}