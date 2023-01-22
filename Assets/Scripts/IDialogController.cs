using System;

public interface IDialogController
{
    /// <summary>
    /// on questions to view in popup menu
    /// </summary>
    public event Action<QuestionsToAsk> OnQuestionsView;
    
    /// <summary>
    /// Setup
    /// </summary>
    void Setup();
}