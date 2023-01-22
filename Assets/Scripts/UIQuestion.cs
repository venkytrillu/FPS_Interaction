using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questionName;
    [SerializeField] private Image _questionImage;
    [SerializeField] private UIToggle _togglePrefab;
    [SerializeField] private RectTransform _toggleContainer;
    [SerializeField] private List<UIToggle> _uiTogglesOptions = new List<UIToggle>();
    public event Action OnQuestionsAttempted = null;

    public bool IisAttempted => _uiTogglesOptions.Any(x=>x.UiToggle.isOn);
    private Question _question;
    private string _selectedOption;
    
    public void Setup(Question question)
    {
        transform.gameObject.SetActive(true);
        _question = question;
        _questionName.text = question.QuestionName;
        _uiTogglesOptions.Clear();
        foreach (var option in question.AnswerOptions)
        {
            var toggleOptions = Instantiate(_togglePrefab, _toggleContainer);
            toggleOptions.Setup(option);
            _uiTogglesOptions.Add(toggleOptions);
            toggleOptions.UiToggle.onValueChanged.AddListener((isOn) =>
            {
                handleToggleChanged(toggleOptions);
            });
        }
       
        // setting image from question
        _questionImage.gameObject.SetActive(question.HasImage);
        _questionImage.sprite = question.Image;
        _questionImage.color = question.CorrectColor;
    }

    private void handleToggleChanged(UIToggle uiToggle)
    {
        if (uiToggle.UiToggle.isOn)
        {
            _selectedOption = uiToggle.Label;
        }
        OnQuestionsAttempted?.Invoke();
    }

    // check for correct answer
    public bool IsCorrectAnswer()
    {
        return _question.IsCorrectAnswer(_selectedOption);
    }
    
    public void Clear()
    {
        Destroy(gameObject);
    }
    
}
