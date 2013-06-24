using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Converters;

namespace MilkShakeFramework.Tools.Physics.Rube
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RubeFile
    {
        [JsonProperty]
        public bool AllowSleep { get; set; }

        [JsonProperty]
        public bool AutoClearForces { get; set; }

        [JsonProperty]
        public bool ContinuousPhysics { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(VectorConverter))]
        public Vector2 Gravity { get; set; }

        [JsonProperty]
        public int PositionIterations { get; set; }

        [JsonProperty]
        public List<RubeBody> Body { get; set; }

        [JsonProperty]
        public float StepsPerSecond { get; set; }

        [JsonProperty]
        public bool SubStepping { get; set; }

        [JsonProperty]
        public int VelocityIterations { get; set; }

        [JsonProperty]
        public bool WarmStarting { get; set; }
        
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class RubeBody
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public int Type { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(VectorConverter))]
        public Vector2 Position { get; set; }

        [JsonProperty]
        public float Angle { get; set; }

        [JsonProperty]
        public float AngularVelocity { get; set; }

        [JsonProperty]
        public bool Awake { get; set; }

        [JsonProperty]
        public float LinearVelocity { get; set; }

        [JsonProperty]
        public List<RubeFixture> Fixture { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class RubeFixture
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public float Density { get; set; }

        [JsonProperty]
        public float Friction { get; set; }

        [JsonProperty]
        public RubeCircle Circle { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class RubeCircle
    {
        [JsonProperty]
        public float Center { get; set; }

        [JsonProperty]
        public float Radius { get; set; }
    }

    public class RubeImporter
    {
        public RubeImporter(string url)
        {
            RubeFile rubeObject = JsonConvert.DeserializeObject <RubeFile>(File.ReadAllText(url));
        }
    }
        
    public class VectorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Vector2).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Vector2 vec = Vector2.Zero;

            reader.Read(); // x
            reader.Read(); // Value
            vec.X = Convert.ToSingle(reader.Value);

            reader.Read(); // y
            reader.Read(); // Value
            vec.Y = Convert.ToSingle(reader.Value);

            reader.Read(); // ?

            return vec;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
    