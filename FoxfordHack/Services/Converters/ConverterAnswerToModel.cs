using System;
using System.Collections.Generic;
using System.Text.Json;
using FoxfordHack.Models.ModelParsingToJson.Answers;
using FoxfordHack.Models.ModelParsingToJson.Answers.CheckboxType;
using FoxfordHack.Models.ModelParsingToJson.Answers.Radio;
using FoxfordHack.Models.ModelParsingToJson.Answers.TextGap;

namespace FoxfordHack.Services.Converters
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
    }
}
