using System;
using System.Collections.Generic;
using IIM.App_Start;
using IIM.Models.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Type = IIM.Models.Domain.Type;

namespace IIM.Tests
{
    [TestClass]
    public class AppSettingsTest
    {
        private DateTimeRestriction _restriction;
        private TypeSetting _typeSetting;
        private SettingsMirror _settingsMirror;
        private string _settingMirrorSerialized;

        [TestInitialize()]
        public void Setup()
        {
            _restriction = new DateTimeRestriction()
            {
                DaysOfWeek = new List<DayOfWeek>() { DayOfWeek.Monday },
                TimeStart = new DateTime(2016, 4, 19, 8, 0, 0),
                TimeEnd = new DateTime(2016, 4, 19, 10, 0, 0),
                Type = DateTimeRestriction.RestrictionType.Deny
            };
            _typeSetting = new TypeSetting()
            {
                ReservationEndTimeRestrictions = new RangeRestriction()
                {
                    Restrictions = new List<DateTimeRestriction>()
                    {
                        _restriction
                    }
                }
            };
            _settingsMirror = new SettingsMirror()
            {
                MirroredImageStorageUrl = "Test",
                TypeSettings = new Dictionary<Type, TypeSetting>()
                {
                    { Type.Student, _typeSetting}
                }
            };
            _settingMirrorSerialized = JsonConvert.SerializeObject(_settingsMirror, new StringEnumConverter());
        }

        [TestMethod]
        public void TestCopyClass()
        {
            var serialized = (string)JsonConvert.SerializeObject(_restriction, new StringEnumConverter());
            var deserialized = JsonConvert.DeserializeObject<DateTimeRestriction>(serialized, new StringEnumConverter());
            Assert.AreEqual(_restriction.Type, deserialized.Type);
            Assert.AreEqual(_restriction.Dates, deserialized.Dates);
            Assert.AreEqual(_restriction.TimeEnd, deserialized.TimeEnd);
            Assert.AreEqual(_restriction.TimeStart, deserialized.TimeStart);
        }

        [TestMethod]
        public void TypeSettingTest()
        {
            var serialized = (string)JsonConvert.SerializeObject(_typeSetting, new StringEnumConverter());
            var deserialized = JsonConvert.DeserializeObject<TypeSetting>(serialized, new StringEnumConverter());
            Assert.AreEqual(_typeSetting.ReservationEndTimeRestrictions.Restrictions.Count,
                deserialized.ReservationEndTimeRestrictions.Restrictions.Count);
            Assert.AreEqual(_typeSetting.ReservationStartTimeRestrictions?.Restrictions.Count,
                deserialized.ReservationStartTimeRestrictions?.Restrictions.Count);
            Assert.AreEqual(_typeSetting.ReservationEndTimeRestrictions.Restrictions[0].DaysOfWeek[0],
                deserialized.ReservationEndTimeRestrictions.Restrictions[0].DaysOfWeek[0]);
        }

        [TestMethod]
        public void SettingMirrorTest()
        {
            var serialized = (string)JsonConvert.SerializeObject(_settingsMirror, new StringEnumConverter());
            Console.WriteLine(serialized);
            var deserialized = JsonConvert.DeserializeObject<SettingsMirror>(serialized, new StringEnumConverter());
            Assert.AreEqual(_settingsMirror.MirroredImageStorageUrl, deserialized.MirroredImageStorageUrl);
            Assert.AreEqual(
                _settingsMirror.TypeSettings[Type.Student].ReservationEndTimeRestrictions.Restrictions[0].DaysOfWeek[0],
                deserialized.TypeSettings[Type.Student].ReservationEndTimeRestrictions.Restrictions[0].DaysOfWeek[0]);
            Assert.AreEqual(
                _settingsMirror.TypeSettings[Type.Student].ReservationEndTimeRestrictions.Restrictions[0].TimeStart,
                deserialized.TypeSettings[Type.Student].ReservationEndTimeRestrictions.Restrictions[0].TimeStart);
        }

        [TestMethod]
        public void AppSettingsSerializeTest()
        {
            Assert.AreEqual(_settingMirrorSerialized, AppSettings.SerializeObject(_settingsMirror));
        }

        [TestMethod]
        public void AppSettingsDeserializeTest()
        {
            var deserialized = AppSettings.DeserializeObject<SettingsMirror>(_settingMirrorSerialized);
            Assert.AreEqual(_settingsMirror.MirroredImageStorageUrl, deserialized.MirroredImageStorageUrl);
            Assert.AreEqual(_settingsMirror.TypeSettings[Type.Student].ReservationEndTimeRestrictions.Restrictions.Count,
                deserialized.TypeSettings[Type.Student].ReservationEndTimeRestrictions.Restrictions.Count);
        }
    }
}
