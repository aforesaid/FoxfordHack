using System;
using System.Collections.Generic;
using System.Text.Json;
namespace FoxfordHack.Services.Converters
{
    class ConverterAnswerToModel<T>
    {
        public List<T> ConvertObjectJsonToModel(object Info)
        {
            try
            {
                var jsonInfo = JsonSerializer.Serialize(Info);
                var list = JsonSerializer.Deserialize<List<T>>(jsonInfo,);
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
