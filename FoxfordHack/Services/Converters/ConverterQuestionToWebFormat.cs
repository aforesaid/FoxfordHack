using FoxfordHack.Models.ModelParsingToJson.Answers;
using FoxfordHack.Models.ModelParsingToJson.Answers.CheckboxType;
using FoxfordHack.Models.ModelParsingToJson.Answers.Radio;
using FoxfordHack.Models.ModelParsingToJson.Answers.TextGap;
using FoxfordHack.Models.ModelParsingToJson.Answers.Links;
using FoxfordHack.Models.ModelParsingToJson.Answers.Text;

using FoxfordHack.Models.ModelParsingToJson.Question;
using System;
using System.Collections.Generic;
namespace FoxfordHack.Services.Converters
{
    class ConverterQuestionToWebFormat
    {
        private object ObjectContent;
        private int QuestionId;
        public Dictionary<string, string> GetContentByQuestion(BaseQuestion question)
        {
            ObjectContent = question.ObjectAnswers;
            QuestionId = question.Id;
            return  question.Type switch
            {
                "checkbox" =>  GetContentForCheckbox(),
                "links"   =>  GetContentForLinks(),
                "radio"    =>  GetContentForRadio(),
                "text"     =>  GetContentForText(),
                "text_gap" =>  GetContentForTextGap(),
                _ => throw new ArgumentException($"Invalid input type {question.Type}")
            };

        }
        private Dictionary<string, string> GetContentForCheckbox()
        {
            var modelList = new ConverterAnswerToModel<CheckboxAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new Dictionary<string, string>();
            foreach (var item in modelList)
                if (item.IsCorrect)
                    result.Add($"questions[{QuestionId}][]", $"{item.Id}");
            return result;
        }
        private Dictionary<string, string> GetContentForLinks()
        {
            var modelList = new ConverterAnswerToModel<LinksAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new Dictionary<string, string>();
            foreach (var item in modelList)
            result.Add($"questions[{QuestionId}][{item.Id}]", $"{item?.CorrectAnswersId?[0]}");
            return result;
        }
        private Dictionary<string, string> GetContentForRadio()
        {
            var modelList = new ConverterAnswerToModel<RadioAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new Dictionary<string, string>();
            foreach (var item in modelList)
                if (item.IsCorrect)
                    result.Add($"questions[{QuestionId}]", $"{item.Id}");
            return result;
        }
        private Dictionary<string, string> GetContentForText()
        {
            var modelList = new ConverterAnswerToModel<TextAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new Dictionary<string, string>();
            foreach (var item in modelList)
            result.Add($"questions[{QuestionId}][]", $"{item.CorrectAnswers[0]}");
            return result;
        }
        private Dictionary<string, string> GetContentForTextGap()
        {
            var modelList = new ConverterAnswerToModel<TextGapAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new Dictionary<string, string>();
            foreach (var item in modelList)
            result.Add($"questions[{QuestionId}][{item.Id}]", $"{item.Content}");
            return result;
        }
    }
}
