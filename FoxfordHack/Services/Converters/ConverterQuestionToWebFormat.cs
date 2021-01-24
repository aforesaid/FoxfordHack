﻿using FoxfordHack.Models.ModelParsingToJson.Answers.CheckboxType;
using FoxfordHack.Models.ModelParsingToJson.Answers.Radio;
using FoxfordHack.Models.ModelParsingToJson.Answers.TextGap;
using FoxfordHack.Models.ModelParsingToJson.Answers.Links;
using FoxfordHack.Models.ModelParsingToJson.Answers.MatchGroup;
using FoxfordHack.Models.ModelParsingToJson.Answers.TextSelection;
using FoxfordHack.Models.ModelParsingToJson.Answers.TextCompose;
using System.Linq;
using FoxfordHack.Models.ModelParsingToJson.Question;
using System;
using System.Collections.Generic;
namespace FoxfordHack.Services.Converters
{
    class ConverterQuestionToWebFormat
    {
        private object ObjectContent;
        private int QuestionId;
        public List<KeyValuePair<string, string>> GetContentByQuestion(BaseQuestion question)
        {
            ObjectContent = question.ObjectAnswers is null? question.ObjectAnswersByText : question.ObjectAnswers;
            QuestionId = question.Id;
            return  question.Type switch
            {
                "checkbox" =>  GetContentForCheckbox(),
                "links"   =>  GetContentForLinks(),
                "radio"    =>  GetContentForRadio(),
                "text"     =>  GetContentForText(),
                "text_gap" =>  GetContentForTextGap(),
                "match_group" => GetContentForMatchGroupAnswer(),
                "text_selection" => GetContentForTextSelection(),
                "text_compose" => GetContentForTextCompose(),
                _ => throw new ArgumentException($"Invalid input type {question.Type}")
            };

        }
        private List<KeyValuePair<string, string>> GetContentForCheckbox()
        {
            var modelList = new ConverterAnswerToModel<CheckboxAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new List<KeyValuePair<string, string>>();
            foreach (var item in modelList)
                if (item.IsCorrect)
                    result.Add(new KeyValuePair<string, string>($"questions[{QuestionId}][]", $"{item.Id}"));
            return result;
        }
        private List<KeyValuePair<string, string>> GetContentForLinks()
        {
            var modelList = new ConverterAnswerToModel<LinksAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new List<KeyValuePair<string, string>>();
            foreach (var item in modelList)
            result.Add(new KeyValuePair<string, string>($"questions[{QuestionId}][{item.Id}]", $"{item?.CorrectAnswersId?[0]}"));
            return result;
        }
        private List<KeyValuePair<string, string>> GetContentForRadio()
        {
            var modelList = new ConverterAnswerToModel<RadioAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new List<KeyValuePair<string, string>>();
            foreach (var item in modelList)
                if (item.IsCorrect)
                    result.Add(new KeyValuePair<string, string>($"questions[{QuestionId}]", $"{item.Id}"));
            return result;
        }
        private List<KeyValuePair<string, string>> GetContentForText()
        {
            var modelList = new ConverterAnswerToModel<string>().ConvertObjectJsonToModel(ObjectContent);
            var result = new List<KeyValuePair<string, string>>();
            result.Add(new KeyValuePair<string, string>($"questions[{QuestionId}][]", $"{modelList[0]}"));
            return result;
        }
        private List<KeyValuePair<string, string>> GetContentForTextGap()
        {
            var modelList = new ConverterAnswerToModel<TextGapAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new List<KeyValuePair<string, string>>();
            foreach (var item in modelList)
            result.Add(new KeyValuePair<string, string>($"questions[{QuestionId}][{item.Id}]", $"{item.Content}"));
            return result;
        }
        private List<KeyValuePair<string, string>> GetContentForMatchGroupAnswer()
        {
            var modelList = new ConverterAnswerToModel<MatchGroupAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new List<KeyValuePair<string, string>>();
            foreach (var item in modelList)
            {
                var value = "";
                foreach (var answer in item.CorrectAnswer)
                    value += value == "" ? $"{answer}" : $",{answer}";
                result.Add(new KeyValuePair<string, string>($"questions[{QuestionId}][{item.Id}]", $"{value}"));
            }
            return result;
        }
        private List<KeyValuePair<string, string>> GetContentForTextSelection()
        {
            var modelList = new ConverterAnswerToModel<TextSelectionAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new List<KeyValuePair<string, string>>();
            foreach (var item in modelList)
            {
                result.Add(new KeyValuePair<string, string>($"questions[{QuestionId}][][position]", $"{item.Position}"));
                result.Add(new KeyValuePair<string, string>($"questions[{QuestionId}][][selection]", $"{item.Selection}"));
            }
            return result;
        }
        private List<KeyValuePair<string, string>> GetContentForTextCompose()
        {
            var modelList = new ConverterAnswerToModel<TextComposeAnswer>().ConvertObjectJsonToModel(ObjectContent);
            var result = new List<KeyValuePair<string, string>>();
            foreach (var item in modelList)
                result.Add(new KeyValuePair<string, string>($"questions[{QuestionId}]", $"{item.Answers[0]}"));
            return result;
        }
    }
}
