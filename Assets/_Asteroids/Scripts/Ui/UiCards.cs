using System;
using System.Collections.Generic;
using UnityEngine;

public class UiCards : MonoBehaviour
{
    public static event Action<bool> CardsEnabled;

    public List<Card> cards;

    public void ShowButtons(List<CardData> cardsData)
    {
        if (cardsData.Count < 1) return;

        for (var i = 0; i < cardsData.Count; i++)
        {
            if (i >= cards.Count)
            {
                break;
            }
            ShowButton(cards[i], cardsData[i]);
        }
        CardsEnabled?.Invoke(true);
    }

    private void RemoveListenersFromButtons()
    {
        foreach (var card in cards)
        {
            card.button.onClick.RemoveAllListeners();
        }
        HideButtons();
    }

    private void HideButtons()
    {
        foreach (var card in cards)
        {
            HideButton(card);
        }
        CardsEnabled?.Invoke(false);
    }

    private void ShowButton(Card card, CardData data)
    {
        card.button.onClick.AddListener(delegate
        {
            Debug.Log("Invoking");
            data.Action.Invoke();
            RemoveListenersFromButtons();
        });

        card.nameKey.text = data.Name;
        card.description.text = data.Description;
        card.button.gameObject.SetActive(true);
    }

    private void HideButton(Card card)
    {
        card.button.gameObject.SetActive(false);
    }


    public class CardData
    {
        public readonly string Name;
        public readonly string Description;
        public readonly Action Action;

        public CardData(string name, string description, Action action)
        {
            Name = name;
            Description = description;
            Action = action;
        }
    }
}