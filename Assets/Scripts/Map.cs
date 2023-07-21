using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<InfoPoint> _infoPoints;

    public List<InfoPoint> InfoPoints => _infoPoints;
}