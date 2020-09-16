using System;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Adding;

namespace KeySwitchManager.Interactors.KeySwitches.Adding
{
    public class AddingInteractor : IAddingUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchFactory KeySwitchFactory { get; }
        private IArticulationFactory ArticulationFactory { get; }
        private IAddingPresenter Presenter { get; }

        public AddingInteractor(
            IKeySwitchRepository repository,
            IAddingPresenter presenter )
            : this(
                repository,
                new IKeySwitchFactory.Default(),
                new IArticulationFactory.Default(),
                presenter )
        {}

        public AddingInteractor(
            IKeySwitchRepository repository,
            IKeySwitchFactory keySwitchFactory,
            IArticulationFactory articulationFactory,
            IAddingPresenter presenter )
        {
            Repository          = repository;
            KeySwitchFactory    = keySwitchFactory;
            ArticulationFactory = articulationFactory;
            Presenter           = presenter;
        }

        public void Execute( InputData inputData )
        {
            var created = DateTimeHelper.NowUtc();
            var articulation = ArticulationFactory.Create(
                inputData.ArticulationName,
                inputData.ArticulationType,
                inputData.ArticulationGroup,
                inputData.ArticulationColor,
                inputData.MidiNoteOns,
                inputData.MidiControlChanges,
                inputData.MidiProgramChanges
            );
            var keySwitch = KeySwitchFactory.Create(
                Guid.NewGuid(),
                inputData.Author,
                inputData.Description,
                created,
                created,
                inputData.DeveloperName,
                inputData.ProductName,
                inputData.InstrumentName,
                new []{ articulation }
            );

            Repository.Save( keySwitch );

            Presenter.Output( new OutputData( true ) );
        }
    }
}