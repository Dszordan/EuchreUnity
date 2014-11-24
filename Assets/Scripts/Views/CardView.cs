using Euchre.Models;
using Euchre.Signals;
using strange.extensions.mediation.impl;

namespace Euchre.Views
{
    public class CardView:View
    {
        public Card DisplayCard { get; set; }
        public CardSelectedSignal CardSelectedSignal = new CardSelectedSignal();

        public void SelectCard()
        {
            //Raise Select Card Signal   
            CardSelectedSignal.Dispatch(DisplayCard);
        }
        void OnMouseDown()
        {
            SelectCard();    
        }
    }

    public class CardMediator : Mediator
    {
        [Inject]
        public CardView CardView { get; set; }

        public override void OnRegister()
        {
            CardView.CardSelectedSignal.AddListener(CardSelected);
        }
        public override void OnRemove()
        {
            CardView.CardSelectedSignal.RemoveListener(CardSelected);
        }
        private void CardSelected(Card selectedCard)
        {
            UnityEngine.Debug.Log("Mediator found a card has been selected: card number " + selectedCard.CardNumber);
        }
    }
}
