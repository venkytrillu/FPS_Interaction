using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;


public class UIController : Singleton<UIController>, IUIManager
{
    [SerializeField] private ThirdPersonController _thirdPersonController; 
    [SerializeField] private StarterAssetsInputs _starterAssetsInputs; 
    [SerializeField] private CinemachineVirtualCamera _playerCameera; 
    [SerializeField] public List<DialogController> _dialogControllers = new List<DialogController>();
    [SerializeField] private UIResultPopUp _uiResultPopUp;
    [SerializeField] private GameObject _questionPanel;
    [SerializeField] private UIQuestion _questionPrefab;
    [SerializeField] private RectTransform _questionsContainer;
    [SerializeField] private Button _closeBtn;
    [SerializeField] private Button _revelButton;
    [SerializeField] private GameObject _confettiObject; 
    
    private List<UIQuestion> _uiQuestions = new List<UIQuestion>();
    private Cinemachine3rdPersonFollow thirdPersonFollow;
    public GameState GameState = GameState.Idle;
    
    private void Start()
    {
        thirdPersonFollow = _playerCameera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        thirdPersonFollow.CameraDistance = -0.5f;
        GameState = GameState.Play;
        setup();
    }

    private void setup()
    {
        bind();
        setupQuestions();
    }

    private void bind()
    {
        unbind();
        _closeBtn.onClick.AddListener(handleCloseBtn);
        _revelButton.onClick.AddListener(handleRevelBtn);
    }

    private void unbind()
    {
        _closeBtn.onClick.RemoveAllListeners();
        _revelButton.onClick.RemoveAllListeners();
    }

    private void handleCloseBtn()
    {
        closePopUp();
    }
    
    private void handleRevelBtn()
    {
        var isCorrectAnswer = _uiQuestions.All(x => x.IsCorrectAnswer());
        
        _uiResultPopUp.Setup( isCorrectAnswer);
    }
    
    // setup questions using question bank
    private void setupQuestions()
    {
        foreach (var dialogController in _dialogControllers)
        {
            dialogController.Setup();
            dialogController.OnQuestionsView -= showQuestionsInPopup;
            dialogController.OnQuestionsView += showQuestionsInPopup;
        }
    }

    // open Pop up with questions
    private void showQuestionsInPopup(QuestionsToAsk questionBank)
    {
        closePopUp();
        _thirdPersonController.SetIdle();
        _starterAssetsInputs.move = Vector2.zero;
        _starterAssetsInputs.SetCursorState(false);
        GameState = GameState.Idle;
        thirdPersonFollow.CameraDistance = 4f;
        _starterAssetsInputs.cursorInputForLook = _starterAssetsInputs.cursorLocked = false;
        _questionPanel.SetActive(true);
        foreach (var question in questionBank.Questions)
        {
            var uiQuestionObj = Instantiate(_questionPrefab, _questionsContainer);
            uiQuestionObj.Setup(question);
            uiQuestionObj.OnQuestionsAttempted += handleQuestionsAttempted;
            _uiQuestions.Add(uiQuestionObj);
        }
    }

    public void SetConfettiObject(bool state)
    {
        _confettiObject.SetActive(state);
    }

    private void handleQuestionsAttempted()
    {
        var isInteractable =_uiQuestions.All(x => x.IisAttempted);
        _revelButton.interactable = isInteractable;
    }

    // clear pop up data
    private void closePopUp()
    {
        foreach (var uiQuestion in _uiQuestions)
        {
            uiQuestion.OnQuestionsAttempted -= handleQuestionsAttempted;
            uiQuestion.Clear();
        }
        _uiQuestions.Clear();
        _questionPanel.SetActive(false);
        thirdPersonFollow.CameraDistance = -0.5f;
        _starterAssetsInputs.cursorLocked = true;
        _starterAssetsInputs.cursorInputForLook = true;
        _starterAssetsInputs.SetCursorState(true);
        _uiResultPopUp.SetActive(false);
        SetConfettiObject(false);
        GameState = GameState.Play;
    }
}