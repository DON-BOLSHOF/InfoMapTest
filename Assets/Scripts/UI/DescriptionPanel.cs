using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DescriptionPanel:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;

        [SerializeField] private Image _view;

        [SerializeField] private PictureListing _pictureListing;

        private InfoPoint _currentPoint;

        public void Show(InfoPoint infoPoint)
        {
            _name.text = infoPoint.Name;
            _description.text = infoPoint.Description;

            _view.sprite = infoPoint.MainView;

            _currentPoint = infoPoint;
        }

        public void OpenGallery()
        {
            _pictureListing.OpenListing(_currentPoint.PointPictures);
            
        }

        public void Close()
        {
            _pictureListing.ClosePull();
        }
    }
}