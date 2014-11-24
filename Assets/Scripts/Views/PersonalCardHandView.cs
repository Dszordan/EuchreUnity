using System.Diagnostics;
using Assets.Scripts.Signals;
using Euchre.BusinessLogic;
using strange.extensions.mediation.impl;
using Euchre.Models;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Euchre.Views
{
    public class PersonalCardHandView : View
    {
        public Player CurrentPlayer { get; set; }
        public Signal RequestDealCardSignal = new Signal();
        public GameObject BlankCard;

        public PersonalCardHandView(){}
        public void DisplayHand(Hand handToDisplay)
        {
            //Display the cards to the user = create a bunch of gameobject(VIEWS) cards and display them
            GameObject playerHand = GameObject.Find("User Interface/PlayerUI/PlayerHand/playerhandContainer");
            GameObject cardAnchor = GameObject.Find("User Interface/PlayerUI/PlayerHand/CardAnchor");
            Vector3 spawnLocationVector3 = new Vector3(playerHand.transform.position.x, playerHand.transform.position.y, 1);

            foreach (Transform child in playerHand.transform)
            {
                Destroy(child.gameObject);
            }
            var xincrement = 0f;
            foreach (var cardToDisplay in handToDisplay.CardList)
            {
                GameObject newCard = (GameObject)Instantiate(BlankCard, spawnLocationVector3, new Quaternion());
                newCard.transform.parent = playerHand.transform;
                newCard.transform.position = new Vector3(cardAnchor.transform.position.x + 0.5f + xincrement, cardAnchor.transform.position.y, 1);
                TextMesh cardText = newCard.GetComponentInChildren<TextMesh>();
                cardText.text = cardToDisplay.CardNumber.ToString();
                cardText.renderer.sortingLayerID = newCard.renderer.sortingLayerID;
                newCard.GetComponent<CardView>().DisplayCard = cardToDisplay;
                newCard.name = "ShittyCard";
                xincrement += 2f;
            }
            //GameObject newCard = (GameObject)Instantiate(BlankCard, spawnLocationVector3, new Quaternion());
            //newCard.transform.parent = playerHand.transform;
            //newCard.transform.position = new Vector3(cardAnchor.transform.position.x + 0.5f, cardAnchor.transform.position.y, 1);
            //TextMesh cardText = newCard.GetComponentInChildren<TextMesh>();
            //cardText.text = 9.ToString();
            //cardText.renderer.sortingLayerID = newCard.renderer.sortingLayerID;
            //newCard.GetComponent<CardView>().DisplayCard = new Card(9, Suit.Heart);

            //spawnLocationVector3 = new Vector3(playerHand.transform.position.x + 3, playerHand.transform.position.y, 1);
            //newCard = (GameObject)Instantiate(BlankCard, spawnLocationVector3, new Quaternion());
            //newCard.transform.parent = playerHand.transform;
            //newCard.transform.position = new Vector3(cardAnchor.transform.position.x + 2.5f, cardAnchor.transform.position.y, 1);
            //cardText = newCard.GetComponentInChildren<TextMesh>();
            //cardText.text = 10.ToString();
            //cardText.renderer.sortingLayerID = newCard.renderer.sortingLayerID;
            //newCard.GetComponent<CardView>().DisplayCard = new Card(10, Suit.Heart);

            //spawnLocationVector3 = new Vector3(playerHand.transform.position.x + 6, playerHand.transform.position.y, 1);
            //newCard = (GameObject)Instantiate(BlankCard, spawnLocationVector3, new Quaternion());
            //newCard.transform.parent = playerHand.transform;
            //newCard.transform.position = new Vector3(cardAnchor.transform.position.x + 4.5f, cardAnchor.transform.position.y, 1);
            //cardText = newCard.GetComponentInChildren<TextMesh>();
            //cardText.text = "Q";
            //cardText.renderer.sortingLayerID = newCard.renderer.sortingLayerID;
            //newCard.GetComponent<CardView>().DisplayCard = new Card(12, Suit.Heart);

            //spawnLocationVector3 = new Vector3(playerHand.transform.position.x + 9, playerHand.transform.position.y, 1);
            //newCard = (GameObject)Instantiate(BlankCard, spawnLocationVector3, new Quaternion());
            //newCard.transform.parent = playerHand.transform;
            //newCard.transform.position = new Vector3(cardAnchor.transform.position.x + 6.5f, cardAnchor.transform.position.y, 1);
            //cardText = newCard.GetComponentInChildren<TextMesh>();
            //cardText.text = "J";
            //cardText.renderer.sortingLayerID = newCard.renderer.sortingLayerID;
            //newCard.GetComponent<CardView>().DisplayCard = new Card(11, Suit.Heart);

            //spawnLocationVector3 = new Vector3(playerHand.transform.position.x + 12, playerHand.transform.position.y, 1);
            //newCard = (GameObject)Instantiate(BlankCard, spawnLocationVector3, new Quaternion());
            //newCard.transform.parent = playerHand.transform;
            //newCard.transform.position = new Vector3(cardAnchor.transform.position.x + 8.5f, cardAnchor.transform.position.y, 1);
            //cardText = newCard.GetComponentInChildren<TextMesh>();
            //cardText.text = "K";
            //cardText.renderer.sortingLayerID = newCard.renderer.sortingLayerID;
            //newCard.GetComponent<CardView>().DisplayCard = new Card(13, Suit.Heart);
        }
        public void RequestDealCards()
        {
            RequestDealCardSignal.Dispatch();
        }
    }
    public class PersonalCardHandMediator : Mediator
    {
        [Inject]
        public PersonalCardHandView HandView { get; set; }
        [Inject]
        public DealCardsSignal DealCardsSignal { get; set; }
        [Inject]
        public CardsDealtSignal CardsDealtSignal { get; set; }
        [Inject]
        public IEuchrePlays EuchreEngine { get; set; }

        public override void OnRegister()
        {
            UnityEngine.Debug.Log("Registering (Also I'm creating teams here. This isn't the spot for it, remember to take this out.");
            EuchreEngine.CreateTeams("Jordan", "Graham", "Mark", "Dan");
            var currentEuchreGame = EuchreEngine.GetCurrentEuchreGame();
            HandView.CurrentPlayer = currentEuchreGame.Team1.Player1;
            CardsDealtSignal.AddListener(CardsDealt);
            HandView.RequestDealCardSignal.AddListener(DealCards);
        }
        private void DealCards()
        {
            DealCardsSignal.Dispatch();
        }
        public override void OnRemove()
        {
            CardsDealtSignal.RemoveListener(CardsDealt);
        }
        private void CardsDealt(Player player)
        {
            if (player.Equals(HandView.CurrentPlayer))
            {
                HandView.DisplayHand(player.CurrentHand);
            }
        }
    }
}