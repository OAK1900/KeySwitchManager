using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages.Aggregate;

namespace KeySwitchManager.UseCases.KeySwitches.Adding
{
    public class KeySwitchAddingRequest
    {
        public string Author { get; }
        public string Description { get; }
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }
        public string ArticulationName { get; }
        public ArticulationType ArticulationType { get; }
        public int ArticulationGroup { get; }
        public int ArticulationColor { get; }
        public IEnumerable<NoteOn> MidiNoteOns { get; }
        public IEnumerable<ControlChange> MidiControlChanges { get; }
        public IEnumerable<ProgramChange> MidiProgramChanges { get; }

        public KeySwitchAddingRequest(
            string author,
            string description,
            string developerName,
            string productName,
            string instrumentName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor,
            IEnumerable<NoteOn> midiNoteOns,
            IEnumerable<ControlChange> midiControlChanges,
            IEnumerable<ProgramChange> midiProgramChanges )
        {
            Author             = author;
            Description        = description;
            DeveloperName      = developerName;
            ProductName        = productName;
            InstrumentName     = instrumentName;
            ArticulationName   = articulationName;
            ArticulationType   = articulationType;
            ArticulationGroup  = articulationGroup;
            ArticulationColor  = articulationColor;
            MidiNoteOns        = midiNoteOns;
            MidiControlChanges = midiControlChanges;
            MidiProgramChanges = midiProgramChanges;
        }
    }
}