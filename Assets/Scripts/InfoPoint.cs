using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InfoPoint: MonoBehaviour
{
    [field: SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string Description{ get; private set; }

    [field:SerializeField] public Sprite MainView{ get; private set; }
    [field:SerializeField] public List<Sprite> PointPictures{ get; private set; }

    public readonly ReactiveCommand<InfoPoint> OnClicked = new();

    private void Start()
    {
        var button = GetComponent<Button>();

        button.OnClickAsObservable().Subscribe(_ => OnClicked?.Execute(this)).AddTo(this);
    }
}