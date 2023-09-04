using UI.Screens;
using UI.ScreenService;
using UnityEngine;

namespace UI.StaticData
{
    [CreateAssetMenu(fileName = "Screens", menuName = "StaticData/Screens")]
    public class ScreenStaticData : ScriptableObject
    {
        public SerializableDictionary<ScreenId, BaseScreen> Screens;
    }
}