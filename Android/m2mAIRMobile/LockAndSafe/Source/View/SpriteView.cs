using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Content.Res;
using LockAndSafe;



namespace com.telit.lock_and_safe
{
    public class SpriteView : View
    {
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private Bitmap mBitmap;
        private int mAtlasWidth = -1;
        private int mAtlasHeight = -1;
        private int mMiliscPerFrame = 20;
        private int mFrameColomn = 0;
        private int mFrameRow = 0;
        private int mCurrentFrame = 0;
        private long mTimer = 0;
        private int mSpriteWidth;
        private int mSpriteHeight;
        private Rect mDrawRect = new Rect();

        public Func<Canvas,int> destinationHeight{ set; get; }

        public Func<Canvas,int> destinationWidth { set; get; }

        public SpriteView(Context context, Bitmap bmp, int aw, int ah, int framePerSec = 60)
            : base(context)
        {
            mBitmap = bmp;
            mAtlasWidth = aw;
            mAtlasHeight = ah;
            mMiliscPerFrame = 1000 / framePerSec; 
            Initialize();
        }

        public SpriteView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            parseAttributes(context, attrs);
            Initialize();
        }

        public SpriteView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            parseAttributes(context, attrs);
            Initialize();
        }

        void Initialize()
        {
            destinationHeight = c =>
            {
                return c.Height;
            };
            destinationWidth = c =>
            {
                return c.Width;
            };

            if (mAtlasWidth < 1 || mAtlasHeight < 1 || mBitmap == null || mBitmap.IsRecycled)
            {
                Console.Error.WriteLine(Class.Name + ".Initialize(): wrong values");
                return;
            }
            mSpriteWidth = (int)((float)mBitmap.Width / mAtlasWidth + 0.5f);
            mSpriteHeight = (int)((float)mBitmap.Height / mAtlasHeight + 0.5f);
        }


        private void parseAttributes(Context context, IAttributeSet attrs)
        {
            TypedArray a = context.Theme.ObtainStyledAttributes(
                               attrs,
                               Resource.Styleable.com_telit_lock_and_safe_SpriteView,
                               0, 0);

            try
            {
                int img = a.GetResourceId(Resource.Styleable.com_telit_lock_and_safe_SpriteView_atlas_image, 0);
                if (img != 0)
                {
                    mBitmap = BitmapFactory.DecodeResource(Context.Resources, img);
                }
                mAtlasWidth = a.GetInt(Resource.Styleable.com_telit_lock_and_safe_SpriteView_atlas_width, -1);
                mAtlasHeight = a.GetInt(Resource.Styleable.com_telit_lock_and_safe_SpriteView_atlas_height, -1);

            }
            finally
            {
                a.Recycle();
            }
        }


        protected override void OnDraw(Canvas canvas)
        {
            if (Visibility == ViewStates.Visible)
            {
                render(canvas, (long)(DateTime.UtcNow - Jan1St1970).TotalMilliseconds);
                Invalidate();
            }
        }

        private Rect dest;

        private void render(Canvas canvas, long currentTime)
        {   
            if (mBitmap == null || mBitmap.IsRecycled)
            {
                Console.Error.WriteLine(Class.Name + ".render(): called to render while bitmap is null");
                return;
            }

            if (currentTime >= mTimer + mMiliscPerFrame)
            {
                mTimer = currentTime;
                calculateFrame();

                mDrawRect.Left = (int)((float)mSpriteWidth * mFrameColomn + 0.5f);
                mDrawRect.Right = (int)((float)mSpriteWidth * (mFrameColomn + 1) + 0.5f);
                mDrawRect.Top = (int)((float)mSpriteHeight * mFrameRow + 0.5f);
                mDrawRect.Bottom = (int)((float)mSpriteHeight * (mFrameRow + 1) + 0.5f);
                ++mCurrentFrame;
            }
            if (dest == null)
                dest = new Rect(0, 0, destinationWidth(canvas), destinationHeight(canvas));
            else
            {
                dest.Right = destinationWidth(canvas);
                dest.Bottom = destinationHeight(canvas);
            }
            canvas.DrawBitmap(mBitmap, mDrawRect, dest, null);

        }

        private void calculateFrame()
        {
            mCurrentFrame %= (mAtlasHeight * mAtlasWidth);
            mFrameColomn = mCurrentFrame % mAtlasWidth;
            mFrameRow = mCurrentFrame / mAtlasWidth;
        }

    }
    // class
}
// namespace

