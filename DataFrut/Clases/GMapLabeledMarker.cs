using DataFrut.Properties;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataFrut.Clases
{
    class GMapLabeledMarker : GMarkerGoogle
    {
        private Font font;
        private GMarkerGoogle innerMarker;

        public string Caption;

        Bitmap BitmapShadow;

        static Bitmap arrowshadow;
        static Bitmap msmarker_shadow;
        static Bitmap shadow_small;
        static Bitmap pushpin_shadow;
        public GMapLabeledMarker(PointLatLng p, string caption, GMarkerGoogleType type) : base(p, type)
        { 
            font = new Font("Arial", 8);
            innerMarker = new GMarkerGoogle(p, type);

            Caption = caption;
        }

        public override void OnRender(Graphics g)
        {
            
            if (innerMarker != null)
            {
                lock (Bitmap)
                {
                    if (BitmapShadow != null)
                    {
                        g.DrawImage(BitmapShadow, LocalPosition.X, LocalPosition.Y, BitmapShadow.Width, BitmapShadow.Height);
                    }
                    g.DrawImage(Bitmap, LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);
                    g.DrawString(Caption, font, Brushes.White, new PointF(LocalPosition.X+1, LocalPosition.Y));
                }
            }

        }

        public override void Dispose()
        {
            if (innerMarker != null)
            {
                innerMarker.Dispose();
                innerMarker = null;
            }

            base.Dispose();
        }

        protected GMapLabeledMarker(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }


        void LoadBitmap()
        {
            Bitmap = GetIcon(Type.ToString());
            Size = new System.Drawing.Size(Bitmap.Width, Bitmap.Height);

            switch (Type)
            {
                case GMarkerGoogleType.arrow:
                    {
                        Offset = new Point(-11, -Size.Height);

                        if (arrowshadow == null)
                        {
                            arrowshadow = Resources.arrowshadow;
                        }
                        BitmapShadow = arrowshadow;
                    }
                    break;

                case GMarkerGoogleType.blue:
                case GMarkerGoogleType.blue_dot:
                case GMarkerGoogleType.green:
                case GMarkerGoogleType.green_dot:
                case GMarkerGoogleType.yellow:
                case GMarkerGoogleType.yellow_dot:
                case GMarkerGoogleType.lightblue:
                case GMarkerGoogleType.lightblue_dot:
                case GMarkerGoogleType.orange:
                case GMarkerGoogleType.orange_dot:
                case GMarkerGoogleType.pink:
                case GMarkerGoogleType.pink_dot:
                case GMarkerGoogleType.purple:
                case GMarkerGoogleType.purple_dot:
                case GMarkerGoogleType.red:
                case GMarkerGoogleType.red_dot:
                    {
                        Offset = new Point(-Size.Width / 2 + 1, -Size.Height + 1);

                        if (msmarker_shadow == null)
                        {
                            msmarker_shadow = Resources.msmarker_shadow;
                        }
                        BitmapShadow = msmarker_shadow;
                    }
                    break;

                case GMarkerGoogleType.black_small:
                case GMarkerGoogleType.blue_small:
                case GMarkerGoogleType.brown_small:
                case GMarkerGoogleType.gray_small:
                case GMarkerGoogleType.green_small:
                case GMarkerGoogleType.yellow_small:
                case GMarkerGoogleType.orange_small:
                case GMarkerGoogleType.purple_small:
                case GMarkerGoogleType.red_small:
                case GMarkerGoogleType.white_small:
                    {
                        Offset = new Point(-Size.Width / 2, -Size.Height + 1);

                        if (shadow_small == null)
                        {
                            shadow_small = Resources.shadow_small;
                        }
                        BitmapShadow = shadow_small;
                    }
                    break;

                case GMarkerGoogleType.green_big_go:
                case GMarkerGoogleType.yellow_big_pause:
                case GMarkerGoogleType.red_big_stop:
                    {
                        Offset = new Point(-Size.Width / 2, -Size.Height + 1);
                        if (msmarker_shadow == null)
                        {
                            msmarker_shadow = Resources.msmarker_shadow;
                        }
                        BitmapShadow = msmarker_shadow;
                    }
                    break;

                case GMarkerGoogleType.blue_pushpin:
                case GMarkerGoogleType.green_pushpin:
                case GMarkerGoogleType.yellow_pushpin:
                case GMarkerGoogleType.lightblue_pushpin:
                case GMarkerGoogleType.pink_pushpin:
                case GMarkerGoogleType.purple_pushpin:
                case GMarkerGoogleType.red_pushpin:
                    {
                        Offset = new Point(-9, -Size.Height + 1);

                        if (pushpin_shadow == null)
                        {
                            pushpin_shadow = Resources.pushpin_shadow;
                        }
                        BitmapShadow = pushpin_shadow;
                    }
                    break;
            }
        }

        static readonly Dictionary<string, Bitmap> iconCache = new Dictionary<string, Bitmap>();

        internal static Bitmap GetIcon(string name)
        {
            Bitmap ret;
            if (!iconCache.TryGetValue(name, out ret))
            {
                ret = Resources.ResourceManager.GetObject(name, Resources.Culture) as Bitmap;
                iconCache.Add(name, ret);
            }
            return ret;
        }
    }
}
