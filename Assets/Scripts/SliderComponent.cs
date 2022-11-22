using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderComponent : MonoBehaviour
{
    #region SerializeFields
    [Header("Objects")]
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI currentLevelTXT;
    [SerializeField] private TextMeshProUGUI nextLevelTXT;
    #endregion
    private void Start()
    {
        int currentLevel = LevelManager.instance.Levels[0].id;
        int nextLevel = LevelManager.instance.Levels[0].id + 1;

        currentLevelTXT.text = currentLevel.ToString();
        nextLevelTXT.text = nextLevel.ToString(); 

        _slider.maxValue = LevelManager.instance.Levels[0].xp;
    }
    public void ResetLevelBar()
    {
        _slider.value = 0;

        Debug.Log("RESETLEVELBAR");
        int currentLevel = LevelManager.instance.Levels[0].id;
        int nextLevel = LevelManager.instance.Levels[0].id + 1;

        _slider.maxValue = LevelManager.instance.Levels[0].xp;

        currentLevelTXT.text = currentLevel.ToString();
        nextLevelTXT.text = nextLevel.ToString();
    }
    public void LevelBarUpdate(int xp)
    {
       _slider.value = _slider.value + xp;
        if (_slider.value >= _slider.maxValue)
        {
            GameManager.instance.LevelUp();
        }
    }
}
