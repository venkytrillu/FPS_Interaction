using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour, IDialogController
{
    public event Action<QuestionsToAsk> OnQuestionsView = null;
    
    [SerializeField] private QuestionsToAsk _questionBank;
    [SerializeField] private GameObject _palyer;
    private Vector3 distance;
    
    public void Setup()
    {
        _palyer = GameObject.FindGameObjectWithTag(Helper.Player);
    }

    private void Update()
    {
        distance = transform.position - _palyer.transform.position;
        if (distance.magnitude <= Helper.DistanceOffset && Input.GetKeyDown(KeyCode.E))
        {
            OnQuestionsView?.Invoke(_questionBank);
        }
    }
}
