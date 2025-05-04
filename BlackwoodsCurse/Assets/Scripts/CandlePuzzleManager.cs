using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePuzzleManager : MonoBehaviour
{
    public List<BigCandle> candles; // Assign in the inspector
    public List<int> correctOrder = new List<int> { 0, 1, 2 }; // Expected lighting order

    private List<int> currentOrder = new List<int>();
    public ResetDiningState roomRestorer;


    public void CandleLit(int index)
    {
        currentOrder.Add(index);
        Debug.Log($"Candle {index} lit. Current Order: {string.Join(", ", currentOrder)}");

        if (currentOrder.Count > correctOrder.Count)
        {
            Debug.Log("Too many candles lit. Resetting.");
            ResetPuzzle();
            return;
        }

        for (int i = 0; i < currentOrder.Count; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                Debug.Log("Wrong order. Resetting.");
                ResetPuzzle();
                return;
            }
        }

        if (currentOrder.Count == correctOrder.Count)
        {
            Debug.Log("Puzzle solved! Unlock door or trigger next event here.");
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
}