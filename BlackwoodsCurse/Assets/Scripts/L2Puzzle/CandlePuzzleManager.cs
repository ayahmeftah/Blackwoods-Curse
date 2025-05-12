using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandlePuzzleManager : MonoBehaviour
{
    public List<BigCandle> candles;
    public List<int> correctOrder = new List<int> { 0, 1, 2 }; // correct candle lighting order
    private List<int> currentOrder = new List<int>();
    public ResetDiningState roomRestorer;
    public Text txt;

    public void CandleLit(int index)
    {
        currentOrder.Add(index);
        Debug.Log($"Candle {index} lit. Current Order: {string.Join(", ", currentOrder)}");

        if (currentOrder.Count == correctOrder.Count)
        {
            // Check full sequence
            for (int i = 0; i < correctOrder.Count; i++)
            {
                if (currentOrder[i] != correctOrder[i])
                {
                    Debug.Log("Wrong order. Resetting.");

                    if (txt != null)
                        StartCoroutine(ShowMessage("Wrong order, please try again.", 2f));

                    ResetPuzzle();
                    return;
                }
            }

            // If correct
            Debug.Log("Puzzle solved! Unlock door or trigger next event here.");

            if (txt != null)
                txt.text = "Puzzle solved, you can escape to the next level.";

            roomRestorer.RestoreRoom();
        }
    }

    void ResetPuzzle()
    {
        currentOrder.Clear();
        foreach (var candle in candles)
        {
            candle.ResetCandle();
        }
    }

    private IEnumerator ShowMessage(string message, float duration)
    {
        txt.text = message;
        yield return new WaitForSeconds(duration);
        txt.text = "";
    }
}
