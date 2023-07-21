using UI;
using UniRx;
using UnityEngine;

namespace System
{
    public class EntryPoint : MonoBehaviour//Чисто для подписок
    {
        [SerializeField] private Map _map;
        [SerializeField] private Describer _describer;

        private void Start()
        {
            var temp = _map.InfoPoints.ConvertAll(infoPoint => infoPoint.OnClicked.AsObservable());

            _describer.Init(temp);
        }
    }
}