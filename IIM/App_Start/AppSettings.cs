using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Hosting;
using IIM.Models.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebGrease.Css.Extensions;
using Type = IIM.Models.Domain.Type;

namespace IIM.App_Start
{
    public static class AppSettings
    {
        private static readonly JsonConverter[] JsonConverters;
        private static string _filePath;
        private static bool _isLoaded;
        private static string _imageStorageUrl;
        private static Dictionary<Type, TypeSetting> _reservationRestrictions;

        public static string ImageStorageUrl
        {
            get { InitialLoad(); return _imageStorageUrl; }
            private set { _imageStorageUrl = value; }
        }

        public static Dictionary<Type, TypeSetting> ReservationRestrictions
        {
            get { InitialLoad(); return _reservationRestrictions; }
            private set { _reservationRestrictions = value; }
        }

        public static RangeRestriction GetEndDateRangeRestriction(Type type)
        {
            return ReservationRestrictions[type]?.ReservationEndTimeRestrictions;
        }

        public static RangeRestriction GetStartDateRangeRestriction(Type type)
        {
            return ReservationRestrictions[type]?.ReservationStartTimeRestrictions;
        }

        static AppSettings()
        {
            Initialize();
            JsonConverters = new JsonConverter[] { new StringEnumConverter() };
        }

        private static void Initialize()
        {
            ReservationRestrictions = new Dictionary<Type, TypeSetting>();
        }

        public static T DeserializeObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, JsonConverters);
        }

        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonConverters);
        }

        private static void LoadPath()
        {
            if (_filePath == null)
            {
                _filePath = HostingEnvironment.MapPath("~/App_Data/settings.json");
                if (_filePath == null) throw new ArgumentNullException(nameof(_filePath), "MapPath failed.");
            }
        }

        private static void WriteToFile(string data)
        {
            LoadPath();
            using (TextWriter writer = File.CreateText(_filePath))
            {
                writer.Write(data);
            }
        }

        private static string ReadFile()
        {
            LoadPath();
            try
            {
                using (TextReader writer = File.OpenText(_filePath))
                {
                    return writer.ReadToEnd();
                }
            }
            catch
            {
                return "";
            }
        }

        private static void InitialLoad()
        {
            if (!_isLoaded)
            {
                _isLoaded = true;
                Load();
            }
        }

        private static void Load()
        {
            Initialize();
            var mirror = DeserializeObject<SettingsMirror>(ReadFile());
            if (mirror == null) return;
            ImageStorageUrl = mirror.MirroredImageStorageUrl;
            ReservationRestrictions = mirror.TypeSettings;
        }

        private static void Save()
        {
            WriteToFile(SerializeObject(GetSettingsMirror()));
        }

        private static SettingsMirror GetSettingsMirror()
        {
            return new SettingsMirror()
            {
                MirroredImageStorageUrl = ImageStorageUrl,
                TypeSettings = ReservationRestrictions
            };
        }
    }
    public class SettingsMirror
    {
        [JsonProperty("ImageStorageUrl")]
        public string MirroredImageStorageUrl { get; set; }

        [JsonProperty("UserTypes")]
        public Dictionary<Type, TypeSetting> TypeSettings { get; set; }

        public SettingsMirror()
        {
            TypeSettings = new Dictionary<Type, TypeSetting>();
        }
    }

    public class TypeSetting
    {
        [JsonProperty("ReservationStartRestrictions")]
        public RangeRestriction ReservationStartTimeRestrictions { get; set; }

        [JsonProperty("ReservationEndRestrictions")]
        public RangeRestriction ReservationEndTimeRestrictions { get; set; }
    }

    public class RangeRestriction
    {
        private List<DateTimeRestriction> _restrictions;
        public List<DateTimeRestriction> Restrictions
        {
            get { return _restrictions ?? (_restrictions = new List<DateTimeRestriction>()); }
            set { _restrictions = value; }
        }
        public DateTimeRestriction.RestrictionType DefaultRestrictionType { get; set; }
    }

    public class MailSettings
    {
        [JsonProperty("Originaddress")]
        public string OriginAddress { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("Port")]
        public int Port { get; set; }
        [JsonProperty("Host")]
        public string Host { get; set; }

    }
}