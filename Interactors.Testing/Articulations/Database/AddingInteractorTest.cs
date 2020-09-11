using System.Collections.Generic;
using System.IO;

using ArticulationManager.Databases.LiteDB.Articulations;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Interactors.Articulations.Database;
using ArticulationManager.Presenters.Articulations.Database;
using ArticulationManager.UseCases.Articulations.Database.Adding;

using NUnit.Framework;

namespace ArticulationManager.Interactors.Testing.Articulations.Database
{
    [TestFixture]
    public class AddingInteractorTest
    {
        [Test]
        public void AddingTest()
        {
            var inputData = new InputData(
                "Developer",
                "Product",
                "Power Chord",
                ArticulationType.Direction,
                0,
                0,
                new List<NoteOn>(),
                new List<ControlChange>(),
                new List<ProgramChange>()
            );

            var presenter = new IAddingArticulationPresenter.Null();
            var repository = new LiteDbArticulationRepository( new MemoryStream() );
            var interactor = new AddingArticulationInteractor( repository, presenter );

            interactor.Execute( inputData );

            Assert.AreEqual( 1, repository.Count() );
        }
    }
}