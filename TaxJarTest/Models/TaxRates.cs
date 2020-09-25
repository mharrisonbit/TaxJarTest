﻿using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using TaxJarTest.Models;
//
//    var taxRate = TaxRate.FromJson(jsonString);

namespace TaxJarTest.Models
{

    public partial class TaxRate
    {
        [JsonProperty("rate")]
        public Rate Rate { get; set; }
    }

    public partial class Rate
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("city_rate")]
        public string CityRate { get; set; }

        [JsonProperty("combined_district_rate")]
        public string CombinedDistrictRate { get; set; }

        [JsonProperty("combined_rate")]
        public string CombinedRate { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_rate")]
        public string CountryRate { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("county_rate")]
        public string CountyRate { get; set; }

        [JsonProperty("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("state_rate")]
        public string StateRate { get; set; }

        [JsonProperty("zip")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Zip { get; set; }
    }

    public partial class TaxRate
    {
        public static TaxRate FromJson(string json) => JsonConvert.DeserializeObject<TaxRate>(json, TaxRateConverter.Settings);
    }

    public static class TaxRateSerialize
    {
        public static string ToJson(this TaxRate self) => JsonConvert.SerializeObject(self, TaxRateConverter.Settings);
    }

    internal static class TaxRateConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TaxRateParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
