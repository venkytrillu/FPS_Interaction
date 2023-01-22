using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionBank")]
public class QuestionsToAsk : ScriptableObject
{
    public List<Question> Questions = new List<Question>();
}

[Serializable]
public class Question
{
    public string QuestionName;
    public string Answer;
    public List<string> AnswerOptions = new List<string>();
    public Sprite Image;
    public bool HasImage = false;
    public Color CorrectColor;

    public Question()
    {
    }

    public Question(Question question)
    {
        QuestionName = question.QuestionName;
        Answer = question.Answer;
        AnswerOptions = question.AnswerOptions;
    }

    public bool IsCorrectAnswer(string optionChosen)
    {
        return Answer == optionChosen;
    }
}