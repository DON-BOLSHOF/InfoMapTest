using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PictureListing : MonoBehaviour
    {
        [SerializeField] private List<Image> _pulledImages;

        [SerializeField] private Image _imageSample;

        [SerializeField] private Transform _positionToSpawn;

        public void OpenListing(List<Sprite> currentPointPictures)
        {
            for (int i = 0; i < _pulledImages.Count && i<currentPointPictures.Count ; i++)
            {
                _pulledImages[i].sprite = currentPointPictures[i];
                _pulledImages[i].gameObject.SetActive(true);
            }

            for (int i = _pulledImages.Count; i < currentPointPictures.Count; i++)
            {
                var image =Instantiate(_imageSample, _positionToSpawn);
                image.sprite = currentPointPictures[i];
                
                _pulledImages.Add(image);
            }
        }

        public void ClosePull()
        {
            foreach (var pulledImage in _pulledImages)
            {
                pulledImage.gameObject.SetActive(false);
            }
        }
    }
}