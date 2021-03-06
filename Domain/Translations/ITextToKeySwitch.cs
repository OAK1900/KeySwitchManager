using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;

namespace KeySwitchManager.Domain.Translations
{
    public interface ITextToKeySwitch : IDataTranslator<IText, KeySwitch>
    {}
}