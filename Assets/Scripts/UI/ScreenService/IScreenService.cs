using UI.Screens;

namespace UI.ScreenService
{
    public interface IScreenService
    {
        BaseScreen Open(ScreenId screenId);
        void Close(ScreenId screenId);
        BaseScreen Get(ScreenId screenId);
    }
}