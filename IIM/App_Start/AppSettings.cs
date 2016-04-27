using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using IIM.Models.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        private static Dictionary<string, SmtpCredential> _smtpCredentials;
        private static string _defaultEmailOrigin;

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

        public static SmtpCredential GetSmtpCredential(string email)
        {
            InitialLoad();
            return _smtpCredentials[email];
        }

        public static string DefaultEmailOrigin
        {
            get { InitialLoad(); return _defaultEmailOrigin; }
            private set { _defaultEmailOrigin = value; }
        }

        static AppSettings()
        {
            Initialize();
            JsonConverters = new JsonConverter[] { new StringEnumConverter() };
        }

        private static void Initialize()
        {
            ReservationRestrictions = new Dictionary<Type, TypeSetting>();
            _smtpCredentials = new Dictionary<string, SmtpCredential>();
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
            _smtpCredentials = mirror.SmtpCredentials;
            DefaultEmailOrigin = mirror.DefaultEmailOrigin;
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
                TypeSettings = ReservationRestrictions,
                SmtpCredentials = _smtpCredentials,
                DefaultEmailOrigin = DefaultEmailOrigin
            };
        }
    }
    public class SettingsMirror
    {
        [JsonProperty("ImageStorageUrl")]
        public string MirroredImageStorageUrl { get; set; }

        [JsonProperty("UserTypes")]
        public Dictionary<Type, TypeSetting> TypeSettings { get; set; }

        [JsonProperty("Emails")]
        public Dictionary<string, SmtpCredential> SmtpCredentials { get; set; }

        public string DefaultEmailOrigin { get; set; }

        public SettingsMirror()
        {
            TypeSettings = new Dictionary<Type, TypeSetting>();
            SmtpCredentials = new Dictionary<string, SmtpCredential>();
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

    public class SmtpCredential
    {
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("Port")]
        public int Port { get; set; }
        [JsonProperty("Host")]
        public string Host { get; set; }
        [JsonProperty("EnableSSL")]
        public bool EnableSSL { get; set; }
    }
}