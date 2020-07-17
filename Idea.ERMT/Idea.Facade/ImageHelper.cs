using System.Drawing;
using System.IO;
using Idea.Entities;
using Idea.Utils;

namespace Idea.Facade
{
    public static class ImageHelper
    {
        /// <summary>
        /// Resizes an image.
        /// </summary>
        /// <param name="imageToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image ResizeImage(Image imageToResize, Size size)
        {
            return (new Bitmap(imageToResize, size));
        }

        /// <summary>
        /// Returns the image for the Marker Type.
        /// </summary>
        /// <param name="markerType"></param>
        /// <returns></returns>
        public static Image GetMarkerImage(MarkerType markerType)
        {
            string fileName = DirectoryAndFileHelper.GetMarkerTypeImagePath(markerType.Symbol);
            if (File.Exists(fileName))
            {
                Image image = Image.FromFile(fileName);
                Size imageSize = new Size();
                switch (markerType.Size)
                {
                    case "Small":
                        {
                            imageSize.Height = 15;
                            imageSize.Width = 15;
                            break;
                        }
                    case "Medium":
                        {
                            imageSize.Height = 30;
                            imageSize.Width = 30;
                            break;
                        }

                    case "Large":
                        {
                            imageSize.Height = 60;
                            imageSize.Width = 60;
                            break;
                        }
                    default:
                        {
                            imageSize.Height = 30;
                            imageSize.Width = 30;
                            break;
                        }
                }

                return ResizeImage(image, imageSize);
            }
            return null;
        }

        /// <summary>
        /// Returns the image for the Marker Type.
        /// </summary>
        /// <param name="markerType"></param>
        /// <returns></returns>
        public static Image GetMarkerImage(MarkerType markerType, Size imageSize)
        {
            if (File.Exists(DirectoryAndFileHelper.ClientIconsFolder + "\\" + markerType.Symbol))
            {
                Image image = Image.FromFile(DirectoryAndFileHelper.ClientIconsFolder + "\\" + markerType.Symbol);

                return ResizeImage(image, imageSize);
            }
            return null;
        }
    }
}
