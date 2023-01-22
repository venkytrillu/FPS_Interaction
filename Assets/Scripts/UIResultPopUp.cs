using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIResultPopUp : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _feedbackTexte;
   
   public void Setup(bool isAllCorrectAnswers)
   {
      SetActive(true);
      _feedbackTexte.text = isAllCorrectAnswers ? Helper.KUDOS : Helper.BADLUCK;
      UIController.Instance.SetConfettiObject(isAllCorrectAnswers);
   }

   public void SetActive(bool isActive)
   {
      transform.gameObject.SetActive(isActive);
   }
}
