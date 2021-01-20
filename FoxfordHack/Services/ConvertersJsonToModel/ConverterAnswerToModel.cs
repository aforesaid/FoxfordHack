using System;
using System.Collections.Generic;
using System.Text.Json;
using FoxfordHack.Models.ModelParsingToJson.Answers;
namespace FoxfordHack.Services.ConvertersJsonToModel
{
    class ConverterAnswerToModel<T>
    {
        public List<T> ConvertObjectJsonToModel(object Info)
        {
            try
            {
                var jsonInfo = JsonSerializer.Serialize(Info);
                var list = JsonSerializer.Deserialize<List<T>>(jsonInfo);
                return list;
            }
            catch (Exception ex)
            {
                new Logging().FixTheError(ex);
                return null;
            }
        }
        public Type GetTypeByEnum(ANSWER_TYPES type)
            => type switch
            {

                _ => throw new ArgumentException("Bad type")
            };
    }
}
