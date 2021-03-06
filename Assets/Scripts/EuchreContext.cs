﻿using Assets.Scripts.Signals;
using Euchre.BusinessLogic;
using Euchre.Controllers;
using Euchre.Infrastructure;
using Euchre.Models;
using Euchre.Signals;
using Euchre.Views;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Euchre
{
    public class EuchreContext : MVCSContext
    {
        public EuchreContext (MonoBehaviour view) : base(view)
		{
		}

        public EuchreContext(MonoBehaviour view, ContextStartupFlags flags)
            : base(view, flags)
		{
		}

        // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        // Override Start so that we can fire the StartSignal 
        override public IContext Start()
        {
            base.Start();
            return this;
        }

        protected override void mapBindings()
        {
            UnityEngine.Debug.Log("Binding.");
            injectionBinder.Bind<IEuchrePlays>().To<EuchreEngineBusinessLogicLocal>();
            injectionBinder.Bind<IGameplayRepository>().To<GameplayRepository>();
            //injectionBinder.Bind<IExampleModel>().To<ExampleModel>().ToSingleton();
            //injectionBinder.Bind<IExampleService>().To<ExampleService>().ToSingleton();

            mediationBinder.Bind<CardView>().To<CardMediator>();
            mediationBinder.Bind<PersonalCardHandView>().To<PersonalCardHandMediator>();
            //commandBinder.Bind<CallWebServiceSignal>().To<CallWebServiceCommand>();

            //StartSignal is now fired instead of the START event.
            //Note how we've bound it "Once". This means that the mapping goes away as soon as the command fires.
            //commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
            commandBinder.Bind<DealCardsSignal>().To<DealRandomCardsCommand>();

            //In MyFirstProject, there's are SCORE_CHANGE and FULFILL_SERVICE_REQUEST Events.
            //Here we change that to a Signal. The Signal isn't bound to any Command,
            //so we map it as an injection so a Command can fire it, and a Mediator can receive it
            injectionBinder.Bind<CardSelectedSignal>().ToSingleton();
            injectionBinder.Bind<CardsDealtSignal>().ToSingleton();
            //injectionBinder.Bind<FulfillWebServiceRequestSignal>().ToSingleton();

        }
    }
}
