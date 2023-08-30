using Base.Services;

namespace UserInput
{
    public interface IInputService : IService
    {
        bool IsPressed { get; }
    }
}