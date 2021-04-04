using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Server.Spells;

namespace Server.Json
{
  public class SpellCircleConverterFactory : JsonConverterFactory
  {
      private readonly IEnumerable<SpellCircle> m_Circles;

      public SpellCircleConverterFactory(IEnumerable<SpellCircle> circles)
      {
          m_Circles = circles;
      }

      public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(SpellCircle);

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
      new SpellCircleConverter(m_Circles);
  }
}
