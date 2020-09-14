using System.Collections.Generic;

using ArticulationManager.Databases.LiteDB.Articulations.Model;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;
using ArticulationManager.Domain.Translations;

namespace ArticulationManager.Databases.LiteDB.Translations
{
    public class EntityToDbModel : IDataTranslator<KeySwitch, KeySwitchModel>
    {
        public KeySwitchModel Translate( KeySwitch source )
        {
            var articulationModels = new List<ArticulationModel>();

            foreach( var i in source.Articulations )
            {
                var noteOn = new List<MidiMessageModel>();
                var controlChange = new List<MidiMessageModel>();
                var programChange = new List<MidiMessageModel>();

                ConvertMessageList( i.MidiNoteOns,        noteOn );
                ConvertMessageList( i.MidiControlChanges, controlChange );
                ConvertMessageList( i.MidiProgramChanges, programChange );

                var articulation = new ArticulationModel(
                    i.ArticulationName.Value,
                    i.ArticulationGroup.Value,
                    i.ArticulationColor.Value,
                    noteOn,
                    controlChange,
                    programChange
                );

                articulationModels.Add( articulation );
            }

            return new KeySwitchModel(
                EntityDateTimeService.ToDateTime( source.Created ),
                EntityDateTimeService.ToDateTime( source.LastUpdated ),
                source.DeveloperName.Value,
                source.ProductName.Value,
                source.InstrumentName.Value,
                articulationModels
            );
        }

        private static void ConvertMessageList( IEnumerable<IMessage> src, List<MidiMessageModel> dest )
        {
            foreach( var i in src )
            {
                dest.Add(
                    new MidiMessageModel(
                        i.Status.Value,
                        i.DataByte1.Value,
                        i.DataByte2.Value
                    )
                );
            }
        }
    }
}