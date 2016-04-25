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
    public class AppSettings
    {
        private readonly JsonConverter[] _jsonConverters;
        private bool _isLoaded;
        private string _imageStorageUrl;
        private readonly ISettingsFile _settingsFile;
        private Dictionary<Type, TypeSetting> _reservationRestrictions;
        private static AppSettings _this;

        [JsonProperty("ImageStorageUrl")]
        public string ImageStorageUrl
        {
            get { InitialLoad(); return _imageStorageUrl; }
            private set { _imageStorageUrl = value; }
        }

        [JsonProperty("UserTypes")]
        public Dictionary<Type, TypeSetting> ReservationRestrictions
        {
            get { InitialLoad(); return _reservationRestrictions; }
            private set { _reservationRestrictions = value; }
        }

        public RangeRestriction GetEndDateRangeRestriction(Type type)
        {
            return ReservationRestrictions[type]?.ReservationEndTimeRestrictions;
        }

        public RangeRestriction GetStartDateRangeRestriction(Type type)
        {
            return ReservationRestrictions[type]?.ReservationStartTimeRestrictions;
        }

        private AppSettings(ISettingsFile settingsFile)
        {
            _settingsFile = settingsFile;
            Initialize();
            _jsonConverters = new JsonConverter[] { new StringEnumConverter() };
        }

        public static AppSettings GetInstance(ISettingsFile iSettingsFile)
        {
            return _this ?? (_this = new AppSettings(iSettingsFile));
        }

        public static AppSettings GetInstance()
        {
            return GetInstance(new DefaultSettingsFile());
        }

        private void Initialize()
        {
            ReservationRestrictions = new Dictionary<Type, TypeSetting>();
        }

        public T DeserializeObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, _jsonConverters);
        }

        public string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, _jsonConverters);
        }

        private void InitialLoad()
        {
            if (!_isLoaded)
            {
                _isLoaded = true;
                Load();
            }
        }

        private void Load()
        {
            Initialize();
            var mirror = DeserializeObject<SettingsMirror>(_settingsFile.ReadFile());
            if (mirror == null) return;
            ImageStorageUrl = mirror.MirroredImageStorageUrl;
            ReservationRestrictions = mirror.TypeSettings;
        }

        private void Save()
        {
            _settingsFile.WriteToFile(SerializeObject(this));
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

    public class DefaultSettingsFile : ISettingsFile
    {
        private string _filePath;
        public void WriteToFile(string data)
        {
            LoadPath();
            using (TextWriter writer = File.CreateText(_filePath))
            {
                writer.Write(data);
            }
        }

        public string ReadFile()
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

        private void LoadPath()
        {
            if (_filePath == null)
            {
                _filePath = HostingEnvironment.MapPath("~/App_Data/settings.json");
                if (_filePath == null) throw new ArgumentNullException(nameof(_filePath), "MapPath failed.");
            }
        }
    }
}