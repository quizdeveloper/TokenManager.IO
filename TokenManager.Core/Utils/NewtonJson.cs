using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Core.Logger;

namespace TokenManager.Core.Utils
{
    public class NewtonJson
    {
        private static readonly JsonSerializerSettings MicrosoftDateFormatSettings;

        static NewtonJson()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            MicrosoftDateFormatSettings = settings;
        }

        public static T Deserialize<T>(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString, MicrosoftDateFormatSettings);
            }
            catch (Exception exception)
            {
                Logger.Logger.WriteLog(Logger.Logger.LogType.Error, $"Deserialize: {jsonString} ==>  {exception.Message}");
                return default(T);
            }
        }
        public static object DeserializeObject(string jsonString, Type type)
        {
            try
            {
                return JsonConvert.DeserializeObject(jsonString, type);
            }
            catch (Exception exception)
            {
                Logger.Logger.WriteLog(Logger.Logger.LogType.Error, $"DeserializeObject: {jsonString} ==>  {exception.Message}");
                return new object();
            }
        }


        public static T Deserialize<T>(string jsonString, string dateTimeFormat)
        {
            try
            {
                JsonConverter[] converters = new JsonConverter[1];
                IsoDateTimeConverter converter = new IsoDateTimeConverter
                {
                    DateTimeFormat = dateTimeFormat
                };
                converters[0] = converter;
                return JsonConvert.DeserializeObject<T>(jsonString, converters);
            }
            catch (Exception exception)
            {
                Logger.Logger.WriteLog(Logger.Logger.LogType.Error, $"Deserialize<T>: {jsonString} ==>  {exception.Message}");
                return default(T);
            }
        }

        public static string Serialize(object @object)
        {
            try
            {
                return JsonConvert.SerializeObject(@object, MicrosoftDateFormatSettings);
            }
            catch (Exception exception)
            {
                Logger.Logger.WriteLog(Logger.Logger.LogType.Error, exception.Message);
                return string.Empty;
            }
        }

        public static string Serialize(object @object, string dateTimeFormat)
        {
            try
            {
                JsonConverter[] converters = new JsonConverter[1];
                IsoDateTimeConverter converter = new IsoDateTimeConverter
                {
                    DateTimeFormat = dateTimeFormat
                };
                converters[0] = converter;
                return JsonConvert.SerializeObject(@object, converters);
            }
            catch (Exception exception)
            {
                Logger.Logger.WriteLog(Logger.Logger.LogType.Error, exception.Message);
                return string.Empty;
            }
        }
    }
}
