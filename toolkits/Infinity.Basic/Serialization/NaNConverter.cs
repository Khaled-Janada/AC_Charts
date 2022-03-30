namespace Infinity.Serialization {

    public class NaNConverter : JsonConverter<double> {

        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            return reader.TokenType == JsonTokenType.Null ? double.NaN : reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options) {
            if (double.IsNaN(value))
                writer.WriteNullValue();
            else
                writer.WriteNumberValue(value);
        }

    }

}
