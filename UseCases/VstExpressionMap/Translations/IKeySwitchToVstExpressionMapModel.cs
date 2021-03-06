using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.UseCases.VstExpressionMap.Translations
{
    public interface IKeySwitchToVstExpressionMapModel
        : IDataTranslator<KeySwitch, IText>
    {}
}