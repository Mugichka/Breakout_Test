using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundPlayer : MonoBehaviour
{
    [SerializeField] private string clickSound;
    void Awake()
    {
        foreach(Button button in FindObjectsOfType<Button>())
        {
            button.onClick.AddListener(()=>AudioManager.Instance.PlaySFX(clickSound));
        }
    }
}
