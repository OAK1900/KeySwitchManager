using System;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Domain.Articulations;
using ArticulationManager.Gateways.Articulations;
using ArticulationManager.Presenters.Articulations.Database;
using ArticulationManager.UseCases.Articulations.Database.Adding;

namespace ArticulationManager.Interactors.Articulations.Database
{
    public class AddingArticulationInteractor : IAddingArticulationUseCase
    {
        private IArticulationRepository Repository { get; }
        private IArticulationFactory ArticulationFactory { get; }
        private IAddingArticulationPresenter Presenter { get; }

        public AddingArticulationInteractor(
            IArticulationRepository repository,
            IAddingArticulationPresenter presenter )
            : this(
                repository,
                new IArticulationFactory.DefaultFactory(),
                presenter )
        {}

        public AddingArticulationInteractor(
            IArticulationRepository repository,
            IArticulationFactory articulationFactory,
            IAddingArticulationPresenter presenter )
        {
            Repository          = repository;
            ArticulationFactory = articulationFactory;
            Presenter           = presenter;
        }

        public void Execute( InputData inputData )
        {
            var created = DateTimeHelper.NowUtc();
            var entity = ArticulationFactory.Create(
                Guid.NewGuid(),
                created,
                created,
                inputData.DeveloperName,
                inputData.ProductName,
                inputData.ArticulationName,
                inputData.ArticulationType,
                inputData.ArticulationGroup,
                inputData.ArticulationColor,
                inputData.MidiNoteOns,
                inputData.MidiControlChanges,
                inputData.MidiProgramChanges
            );

            Repository.Save( entity );

            //TODO
            Presenter.Output( new OutputData() );
        }
    }
}