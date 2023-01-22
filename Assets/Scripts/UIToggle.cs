using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIToggle : MonoBehaviour
{
   [SerializeField] private Text _questionName;

   public Toggle UiToggle => GetComponent<Toggle>();
   
   public string Label => _questionName.text;
   
   public void Setup(string labelName)
   {
      transform.gameObject.SetActive(true);
      _questionName.text = labelName;
   }

   public void DestroyToggle()
   {
      Destroy(gameObject);
   }
}
