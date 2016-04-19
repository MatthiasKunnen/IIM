using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
        private static Dictionary<Type, List<DateTimeRestriction>> _reservationStartTimeRanges;
        private static Dictionary<Type, List<DateTimeRestriction>> _reservationEndTimeRanges;

        public static string ImageStorageUrl
        {
            get { InitialLoad(); return _imageStorageUrl; }
            private set { _imageStorageUrl = value; }
        }

        public static Dictionary<Type, List<DateTimeRestriction>> ReservationStartTimeRanges
        {
            get { InitialLoad(); return _reservationStartTimeRanges; }
            private set { _reservationStartTimeRanges = value; }
        }

        public static Dictionary<Type, List<DateTimeRestriction>> ReservationEndTimeRanges
        {
            get { InitialLoad(); return _reservationEndTimeRanges; }
            private set { _reservationEndTimeRanges = value; }
        }

        static AppSettings()
        {
            Initialize();
            JsonConverters = new JsonConverter[] { new StringEnumConverter() };
        }

        private static void Initialize()
        {
            ReservationStartTimeRanges = new Dictionary<Type, List<DateTimeRestriction>>();
            ReservationEndTimeRanges = new Dictionary<Type, List<DateTimeRestriction>>();
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
            GetTypes().ForEach(t =>
            {
                if (!mirror.TypeSettings.ContainsKey(t)) return;
                if (mirror.TypeSettings[t]?.ReservationEndTimeRestrictions.Any() == true) ReservationEndTimeRanges[t] = mirror.TypeSettings[t].ReservationEndTimeRestrictions;
                if (mirror.TypeSettings[t]?.ReservationStartTimeRestrictions.Any() == true) ReservationStartTimeRanges[t] = mirror.TypeSettings[t].ReservationStartTimeRestrictions;
            });
        }

        private static void Save()
        {
            WriteToFile(SerializeObject(GetSettingsMirror()));
        }

        private static SettingsMirror GetSettingsMirror()
        {
            var mirror = new SettingsMirror() { MirroredImageStorageUrl = ImageStorageUrl };

            ReservationStartTimeRanges.ForEach(rsdr =>
            {
                if (!mirror.TypeSettings.ContainsKey(rsdr.Key))
                    mirror.TypeSettings.Add(rsdr.Key, new TypeSetting());
                mirror.TypeSettings[rsdr.Key].ReservationStartTimeRestrictions.AddRange(rsdr.Value);
            });
            ReservationEndTimeRanges.ForEach(rsdr =>
            {
                if (mirror.TypeSettings.ContainsKey(rsdr.Key))
                    mirror.TypeSettings.Add(rsdr.Key, new TypeSetting());
                mirror.TypeSettings[rsdr.Key].ReservationEndTimeRestrictions.AddRange(rsdr.Value);
            });
            return mirror;
        }
        private static IEnumerable<Type> GetTypes()
        {
            return ((Type[])Enum.GetValues(typeof(Type))).AsQueryable();
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
        private List<DateTimeRestriction> _reservationStartTimeRestrictions;
        private List<DateTimeRestriction> _reservationEndTimeRestrictions;

        [JsonProperty("ReservationStartRestrictions")]
        public List<DateTimeRestriction> ReservationStartTimeRestrictions
        {
            get { return _reservationStartTimeRestrictions ?? (_reservationStartTimeRestrictions = new List<DateTimeRestriction>()); }
            set { _reservationStartTimeRestrictions = value; }
        }

        [JsonProperty("ReservationEndRestrictions")]
        public List<DateTimeRestriction> ReservationEndTimeRestrictions
        {
            get { return _reservationEndTimeRestrictions ?? (_reservationEndTimeRestrictions = new List<DateTimeRestriction>()); }
            set { _reservationEndTimeRestrictions = value; }
        }

        public TypeSetting()
        {
            ReservationStartTimeRestrictions = new List<DateTimeRestriction>();
            ReservationEndTimeRestrictions = new List<DateTimeRestriction>();
        }
    }
}