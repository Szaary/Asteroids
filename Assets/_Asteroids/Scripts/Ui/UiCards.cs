using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UiCards : Singleton<UiCards>
{
    [SerializeField] private float uiShowTime = 0.3f;
    [SerializeField] private float uiHideTime = 0.3f;

    public static event Action<bool> CardsEnabled;
    [SerializeField] private TextMeshProUGUI titleReference;
    [SerializeField] private List<Card> cards;

    private readonly List<Action> _uiCardCalls = new();
    private Action _previous;


    public void ShowButtons(string title,
        List<CardData> cardsData,
        Action onShow = null,
        Action onHide = null)
    {
        if (cardsData.Count < 1)
        {
            Debug.LogError("Send empty list of cards to UiCard");
            return;
        }

        _uiCardCalls.Add(() =>
        {
            onShow?.Invoke();

            titleReference.enabled = true;
            titleReference.text = title;

            for (var i = 0; i < cardsData.Count; i++)
            {
                if (i >= cards.Count)
                {
                    break;
                }

                if (!cardsData[i].CanUse(cardsData[i].Target)) return;
                ShowButton(cards[i], cardsData[i], onHide);
            }

            CardsEnabled?.Invoke(true);
        });


        if (_uiCardCalls.Count == 1)
            _uiCardCalls[0].Invoke();
    }


    private void ShowButton(Card card, CardData data, Action onHide)
    {
        card.button.onClick.AddListener(delegate
        {
            data.Apply.Invoke();
            HideButtons(onHide, data.LoopInMenu);
        });
        card.nameKey.text = data.Name;
        card.description.text = data.Description;

        if (data.Cost.Item2 > 0)
        {
            card.resource.text = data.Cost.Item1.resourceName;
            card.cost.text = data.Cost.Item2.ToString();
            card.SetActiveCost(true);
        }
        else
        {
           card.SetActiveCost(false);
        }

        card.button.gameObject.SetActive(true);
        card.transform.localScale = Vector3.zero;
        card.transform.DOScale(Vector3.one, uiShowTime).SetUpdate(true).onComplete =
            () => card.button.enabled = true;
    }

    private void HideButtons(Action onHide, bool dataLoopInMenu)
    {
        foreach (var card in cards)
        {
            card.button.onClick.RemoveAllListeners();
        }

        foreach (var card in cards)
        {
            HideButton(card);
        }

        StartCoroutine(CallOnHide(onHide, dataLoopInMenu));
    }

    private void HideButton(Card card)
    {
        card.button.enabled = false;
        card.transform.DOScale(Vector3.zero, uiHideTime).SetUpdate(true).onComplete =
            () => card.button.gameObject.SetActive(false);
    }

    private IEnumerator CallOnHide(Action onHide, bool dataLoopInMenu)
    {
        titleReference.enabled = false;
        yield return new WaitForSecondsRealtime(uiHideTime);
        CardsEnabled?.Invoke(false);
        onHide?.Invoke();

        if (!dataLoopInMenu)
        {
            _previous = _uiCardCalls[0];
            _uiCardCalls.RemoveAt(0);
        }

        if (_uiCardCalls.Count > 0)
        {
            _uiCardCalls[0].Invoke();
        }
    }

    public static void GenerateCard(string title, MenuData menusData, GameObject gameObject)
    {
        var cardsData = new List<CardData> {GenerateCardData(menusData, gameObject)};
        Instance.ShowButtons(title, cardsData);
    }

    public static void GenerateCards(string title, List<MenuData> menusData, GameObject gameObject)
    {
        var cardsData = new List<CardData>();
        foreach (var menuData in menusData)
        {
            cardsData.Add(GenerateCardData(menuData, gameObject));
        }

        Instance.ShowButtons(title, cardsData);
    }

    private static CardData GenerateCardData(MenuData data, GameObject gameObject)
    {
        var cardData = new CardData(
            data.GetTitle(),
            data.GetDescription(),
            data.GetCost(),
            gameObject,
            _ => data.CanShow(gameObject),
            () => { data.Apply(gameObject); },
            data.LoopInMenu()
        );
        return cardData;
    }

    public class CardData
    {
        public readonly (Resource, int) Cost;
        public readonly string Name;
        public readonly string Description;
        public readonly GameObject Target;
        public readonly Action Apply;
        public readonly Func<GameObject, bool> CanUse;
        public readonly bool LoopInMenu;

        public CardData(string name, string description,  (Resource, int) cost,GameObject target,
            Func<GameObject, bool> canUse, Action apply, bool loopInMenu)
        {
            Name = name;
            Description = description;
            Cost = cost;
            CanUse = canUse;
            Apply = apply;
            LoopInMenu = loopInMenu;
            Target = target;
        }
    }

    public void AddPrevious()
    {
        _uiCardCalls.Add(_previous);
    }
}