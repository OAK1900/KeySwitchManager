using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Removing;

namespace KeySwitchManager.Interactors.KeySwitches.Removing
{
    public class RemovingInteractor : IRemovingUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IRemovingPresenter Presenter { get; }

        public RemovingInteractor(
            IKeySwitchRepository repository,
            IRemovingPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public void Execute( InputData inputData )
        {
            var developerName = new DeveloperName( inputData.DeveloperName );
            var productName = new ProductName( inputData.ProductName );

            Repository.Delete( developerName, productName );

            Presenter.Output( new OutputData( true ) );
        }
    }
}