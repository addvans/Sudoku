using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject failMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject emptyFailText;
    [SerializeField] GameObject duplesFailText;
    [SerializeField] Button LoadButton;
    [SerializeField] TMP_Text pencilText;
    [SerializeField] TMP_InputField difficultyLevel;
    TMP_Text chosenText;
    TMP_Text[] texts;
    EventSystem eventSystem;
    bool isPencilMode = false;
    const string pencilTag = "<i><color=#914400>";

    void Start()
    {
        texts = GetComponentsInChildren<TMP_Text>();
        eventSystem = FindObjectOfType<EventSystem>();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = string.Empty;
        }
    }

    public void TogglePencil()
    {
        isPencilMode = !isPencilMode;
        pencilText.text = isPencilMode ? "Выключить пометки" : "Включить пометки";
    }

    public void StartGenerator()
    {
        int difficulty;
        if (int.TryParse(difficultyLevel.text, out difficulty))
        {
            if (difficulty > 81 | difficulty < 1)
                return;
        }
        else
            return;
        SudokuGenerator.ShuffleDesk(texts, difficulty);
    }

    public void SaveDesk()
    {
        SaveManager.SaveDesk(texts);
        CheckSaveFile();
    }

    public void CheckSaveFile()
    {
        LoadButton.interactable = SaveManager.IsSaveFileExist();
    }

    public void LoadDesk()
    {
        if(SaveManager.IsSaveFileExist())
        {
            SudokuDesk sd = SaveManager.LoadDesk();
            for(int i = 0; i < sd.numbers.Length; i++)
                texts[i].text = sd.numbers[i];
            UpdateColors();
        }
    }
    /// <summary>
    /// Возвращает true, если в строке, столбце и блоке 3х3 число под индексом numberIndex НЕ повторяется. Иначе возвращает false 
    /// </summary>
    bool IsNumberCorrect(int numberIndex)
    {
        for (int j = numberIndex % 9; j < texts.Length; j += 9) // проверяем столбец сверху вниз
        {
            if (j == numberIndex)
                continue;

            if (texts[numberIndex].text == texts[j].text)
                return false;
            
        }
        for (int j = numberIndex - (numberIndex % 9); j < numberIndex + (9 - numberIndex % 9); j++) // проверяем строку от номера слева направо
        {
            if (j == numberIndex)
                continue;
            if (texts[numberIndex].text == texts[j].text) 
                return false;
            
        }
        int leftUpCornerRow = numberIndex / 9;
        int leftUpCornerRowCol = numberIndex % 9;
        leftUpCornerRow -= leftUpCornerRow % 3;
        leftUpCornerRowCol -= leftUpCornerRowCol % 3;
        for (int r = leftUpCornerRow; r < leftUpCornerRow + 3; r++) // проверяем квадрат 3х3
            for (int c = leftUpCornerRowCol; c < leftUpCornerRowCol + 3; c++)
            {
                int blockIndex = r * 9 + c;
                if (blockIndex == numberIndex)
                    continue;
                if (texts[blockIndex].text == texts[numberIndex].text)
                    return false;
            }
        return true;
    }
    /// <summary>
    /// Возвращает множество индексов полей, в которых значения повторяются в столбцах, рядах или блоках 3х3
    /// </summary>
    HashSet<int> GetDuplicates(out int emptyCount)
    {
        emptyCount = 0;
        HashSet<int> wrongNumbers = new HashSet<int>(); // делаем множество индексов - избежим повторов, в отличие от привычного List
        for (int i = 0; i < texts.Length - 1; i++)
            if (texts[i].text.Length == 0 | texts[i].text.Contains(pencilTag))
                emptyCount++;
            else if (!IsNumberCorrect(i))
                wrongNumbers.Add(i);
        return wrongNumbers;
    }

    public void CheckDesk()
    {
        int emptyCount;
        HashSet<int> wrongNumbers = GetDuplicates(out emptyCount);
        failMenu.SetActive(wrongNumbers.Count > 0 | emptyCount > 0);
        emptyFailText.SetActive(emptyCount > 0);
        duplesFailText.SetActive(wrongNumbers.Count > 0);
        if (wrongNumbers.Count > 0)
            DeskPainter.RecolorDuplicates(texts, wrongNumbers, Color.red);
        else
            DeskPainter.RecolorNumbers(texts, Color.black);
        winMenu.SetActive(emptyCount == 0 & wrongNumbers.Count == 0);
        
    }
    
    public void ChangeActiveButton()
    {
        GameObject selectedObj = eventSystem.currentSelectedGameObject;
        if(selectedObj.name.Contains("Slot"))
        {
            if (chosenText != null)
            {
                int oldIndex = System.Array.IndexOf(texts, chosenText);
                DeskPainter.HighlightNumber(texts, oldIndex, Color.white * 2);
                if (chosenText.transform.parent.gameObject == selectedObj)
                {
                    chosenText = null;
                    return;
                }
            }
            chosenText = selectedObj.GetComponentInChildren<TMP_Text>();
            int newIndex = System.Array.IndexOf(texts, chosenText);
            DeskPainter.HighlightNumber(texts, newIndex, Color.green);
        }
    }

    public void SetSlotValue(int value)
    {
        if (chosenText == null)
            return;
        if(isPencilMode)
        {
            string temp = chosenText.text.Replace(pencilTag, string.Empty);
            if(!temp.Contains(value.ToString()))
                if(chosenText.text.Contains(pencilTag))
                    chosenText.text += " " + value.ToString();
                else 
                    chosenText.text = pencilTag + value.ToString();
        }
        else
            chosenText.text = value.ToString();
        UpdateColors();
    }

    void UpdateColors()
    {
        DeskPainter.RecolorNumbers(texts, Color.black);
        HashSet<int> duples = GetDuplicates(out _);
        if (duples.Count > 0)
            DeskPainter.RecolorDuplicates(texts, duples, Color.red);
    }
}
