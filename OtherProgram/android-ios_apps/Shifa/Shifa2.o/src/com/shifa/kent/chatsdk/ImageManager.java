package com.shifa.kent.chatsdk;

import android.app.Activity;
import android.content.Context;
import android.content.res.Configuration;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.widget.ImageView;

import com.shifa.kent.R;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.lang.ref.SoftReference;
import java.net.URL;
import java.net.URLConnection;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.HashMap;
import java.util.Stack;

public class ImageManager {

    final BitmapFactory.Options options = new BitmapFactory.Options();
    public Context ctx;
    private long cacheDuration;
    private DateFormat mDateFormatter;
    private HashMap<String, SoftReference<Bitmap>> imageMap = new HashMap<String, SoftReference<Bitmap>>();
    private String FBSize = "small";
    private File cacheDir;
    private int HeightDim, WidthDim = 50;
    private boolean FirstTimeSetting = false;
    private ImageQueue imageQueue = new ImageQueue();
    private Thread imageLoaderThread = new Thread(new ImageQueueManager());

    public ImageManager(Context context, long _cacheDuration) {

        ctx = context;

        cacheDuration = _cacheDuration;
        //TODO Make this match the date format of your server
        mDateFormatter = new SimpleDateFormat("EEE',' dd MMM yyyy HH:mm:ss zzz");

        // Make background thread low priority, to avoid affecting UI performance
        imageLoaderThread.setPriority(Thread.NORM_PRIORITY - 1);

        // Find the dir to save cached images
        String sdState = android.os.Environment.getExternalStorageState();
        if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
            File sdDir = android.os.Environment.getExternalStorageDirectory();
            cacheDir = new File(sdDir, "data/shifapics");

        } else {
            cacheDir = context.getCacheDir();
        }

        if (!cacheDir.exists()) {
            cacheDir.mkdirs();

            try {
                File output = new File(cacheDir, ".nomedia");
                boolean filecreated = output.createNewFile();
            } catch (IOException e) {
                // TODO Auto-generated catch block
                // e.printStackTrace();
            }
        }


        if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
            File sdDir = android.os.Environment.getExternalStorageDirectory();
            cacheDir = new File(sdDir, "data/shifapics/chat_img");

        } else {
            cacheDir = context.getCacheDir();
        }

        if (!cacheDir.exists()) {
            cacheDir.mkdirs();

            try {
                File output = new File(cacheDir, ".nomedia");
                boolean filecreated = output.createNewFile();
            } catch (IOException e) {
                // TODO Auto-generated catch block
                // e.printStackTrace();
            }
        }

        if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
            File sdDir = android.os.Environment.getExternalStorageDirectory();
            cacheDir = new File(sdDir, "data/shifapics/chat_img/thumb");

        } else {
            cacheDir = context.getCacheDir();
        }

        if (!cacheDir.exists()) {
            cacheDir.mkdirs();

            try {
                File output = new File(cacheDir, ".nomedia");
                boolean filecreated = output.createNewFile();
            } catch (IOException e) {
                // TODO Auto-generated catch block
                //e.printStackTrace();
            }
        }
        if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
            File sdDir = android.os.Environment.getExternalStorageDirectory();
            cacheDir = new File(sdDir, "data/shifapics/chat_img/normal");

        } else {
            cacheDir = context.getCacheDir();
        }

        if (!cacheDir.exists()) {
            cacheDir.mkdirs();

            try {
                File output = new File(cacheDir, ".nomedia");
                boolean filecreated = output.createNewFile();
            } catch (IOException e) {
                // TODO Auto-generated catch block
                // e.printStackTrace();
            }
        }
    }

    public ImageManager(Context context) {

        ctx = context;
        cacheDuration = 1200000;
        //TODO Make this match the date format of your server
        mDateFormatter = new SimpleDateFormat("EEE',' dd MMM yyyy HH:mm:ss zzz");

        // Make background thread low priority, to avoid affecting UI performance
        imageLoaderThread.setPriority(Thread.NORM_PRIORITY - 1);

        // Find the dir to save cached images
        String sdState = android.os.Environment.getExternalStorageState();
        if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
            File sdDir = android.os.Environment.getExternalStorageDirectory();
            cacheDir = new File(sdDir, "data/shifapics");

        } else {
            cacheDir = context.getCacheDir();
        }

        if (!cacheDir.exists()) {
            cacheDir.mkdirs();
        }

        if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
            File sdDir = android.os.Environment.getExternalStorageDirectory();
            cacheDir = new File(sdDir, "data/shifapics/chat_img");

        } else {
            cacheDir = context.getCacheDir();
        }

        if (!cacheDir.exists()) {
            cacheDir.mkdirs();
        }

        if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
            File sdDir = android.os.Environment.getExternalStorageDirectory();
            cacheDir = new File(sdDir, "data/shifapics/chat_img/normal");

        } else {
            cacheDir = context.getCacheDir();
        }

        if (!cacheDir.exists()) {
            cacheDir.mkdirs();
        }


        if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
            File sdDir = android.os.Environment.getExternalStorageDirectory();
            cacheDir = new File(sdDir, "data/shifapics/chat_img/thumb");

        } else {
            cacheDir = context.getCacheDir();
        }

        if (!cacheDir.exists()) {
            cacheDir.mkdirs();
        }

        if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
            File sdDir = android.os.Environment.getExternalStorageDirectory();
            cacheDir = new File(sdDir, "data/shifapics");

        } else {
            cacheDir = context.getCacheDir();
        }

        if (!cacheDir.exists()) {
            cacheDir.mkdirs();
        }

    }

    public static boolean isNumeric(String str) {
        try {
            double d = Double.parseDouble(str);
        } catch (NumberFormatException nfe) {
            return false;
        }
        return true;
    }

    public void FacebookProfileSize(String FBSize) {
        this.FBSize = FBSize;
    }

    private void SetImageHeightWidth() {
        if (FirstTimeSetting == false) {
            FirstTimeSetting = true;
            BitmapDrawable bd = (BitmapDrawable) ctx.getResources().getDrawable(R.drawable.ic_launcher);
            HeightDim = bd.getBitmap().getHeight();
            WidthDim = bd.getBitmap().getWidth();
        }
    }

    private void removeFBSmallpic(String url) {

    }

    public void displayImage(String url, ImageView imageView, int defaultDrawableId) {

        try {
            if (url == null) return;
            // //Log.e("DisplayImage", "URL: " + url);
            if (isNumeric(url)) {
                if (FBSize.equals("normal")) {
                    url = url + "254678619890736373363827362834";
                }
            }
            SetImageHeightWidth();
            if (url.equals("123456789")) { // demo skip it
                imageView.setImageResource(defaultDrawableId);

            } else {
                imageView.setImageResource(defaultDrawableId);
                queueImage(url, imageView, defaultDrawableId);
            }
         /*  if(imageMap.containsKey(url)) {
                //Log.e("DisplayImage","IF");
                if (imageMap.get(url).get() == null) return;
                imageView.setImageBitmap(imageMap.get(url).get());

            }
            else if(isNumeric(url) || url.indexOf("-") != -1){
                //Log.e("DisplayImage"," Facebook or image id");

                ////Log.e("DisplayImage","imageview "  + defaultDrawableId);
                //imageView.setImageResource(defaultDrawableId);
            }
            else {
                ////Log.e("DisplayImage","else");
                //queueImage(url, imageView, defaultDrawableId);
               // //Log.e("DisplayImage","shifa id "  + defaultDrawableId);
                imageView.setImageResource(defaultDrawableId);

            }*/


        } catch (Exception ex) {
        }
    }

    private void queueImage(String url, ImageView imageView, int defaultDrawableId) {
        // This ImageView might have been used for other images, so we clear
        // the queue of old tasks before starting.
        imageQueue.Clean(imageView);
        ImageRef p = new ImageRef(url, imageView, defaultDrawableId);
        synchronized (imageQueue.imageRefs) {
            imageQueue.imageRefs.push(p);
            imageQueue.imageRefs.notifyAll();
        }
        // Start thread if it's not started yet
        if (imageLoaderThread.getState() == Thread.State.NEW) {
            imageLoaderThread.start();
        }
    }

    private boolean isTablet(Context context) {

        return (context.getResources().getConfiguration().screenLayout
                & Configuration.SCREENLAYOUT_SIZE_MASK)
                >= Configuration.SCREENLAYOUT_SIZE_LARGE;
    }

    public Bitmap decodeSampledBitmapFromPath(String path, int reqWidth,
                                              int reqHeight) {

        final BitmapFactory.Options options = new BitmapFactory.Options();
        options.inJustDecodeBounds = true;
        BitmapFactory.decodeFile(path, options);

        options.inSampleSize = calculateInSampleSize(options, reqWidth,
                reqHeight);

        ;
        //Log.e("getBitmap", "inSampleSize " + options.inSampleSize);
        // Decode bitmap with inSampleSize set

        options.inJustDecodeBounds = false;
        Bitmap bmp = BitmapFactory.decodeFile(path, options);
        return bmp;
    }

    public int calculateInSampleSize(BitmapFactory.Options options,
                                     int reqWidth, int reqHeight) {

        final int height = options.outHeight;
        final int width = options.outWidth;
        int inSampleSize = 1;

        if (height > reqHeight || width > reqWidth) {
            if (width > height) {
                inSampleSize = Math.round((float) height / (float) reqHeight);
            } else {
                inSampleSize = Math.round((float) width / (float) reqWidth);
            }
        }
        return inSampleSize;
    }

    private Bitmap getBitmap(String url) {


        try {

            //Log.i("getBitmap", url);
            String filename = url + ".jpg";
            //Log.i("getBitmap", "1");

            File bitmapFile = new File(cacheDir, filename);

            //Log.i("getBitmap", bitmapFile.getPath());
            options.inSampleSize = 0;

            Bitmap bitmap = decodeSampledBitmapFromPath(bitmapFile.getPath(), 300, 300);//BitmapFactory.decodeFile(bitmapFile.getPath(),options);
            // Is the bitmap in our cache?
            if (bitmap != null) {
                //Log.i("getBitmap", "Bitmap cache");
                return bitmap;
            }

            URLConnection openConnection = null;
            //Log.i("getBitmap", "url " + url);

            if (url.indexOf("-") != -1) {
                //Log.i("getBitmap", "inside http://kent..XXXXXXX/app_php/shifaappsettings/" + url + ".jpg");
                openConnection = new URL("http://kent..XXXXXXX/app_php/shifaappsettings/" + url + ".jpg").openConnection();
                //   return null;
            } else if (isNumeric(url)) {
                // Feed Images

                if (FBSize.equals("normal")) {
                    openConnection = new URL("https://graph.facebook.com/" + url.replace("254678619890736373363827362834", "") + "/picture?type=large").openConnection();
                } else {
                    openConnection = new URL("https://graph.facebook.com/" + url + "/picture?width=" + WidthDim + "&height=" + HeightDim).openConnection();
                }
            } else {

                return null;
            }


            //Log.e("URL Image", openConnection.getURL().toString());
            // Nope, have to download it
            if (openConnection.getInputStream() == null) {
                //Log.e("Response","Image not found "+openConnection.getURL().toString());
                return null;
            }
            //Log.e("Response","Image found Success");

            try {


                byte[] buffer = new byte[4096];
                int n = -1;
                //Log.e("bitmap path:" ,bitmapFile.getPath().toString());
                java.io.OutputStream output = new FileOutputStream(bitmapFile.getPath());
                while ((n = openConnection.getInputStream().read(buffer)) != -1) {
                    if (n > 0) {
                        output.write(buffer, 0, n);
                    }
                }
                output.close();

                bitmap = decodeSampledBitmapFromPath(bitmapFile.getPath(), 300, 300);
                if (bitmap == null) return null;
                //options.inSampleSize = 3;
                // bitmap = BitmapFactory.decodeStream(openConnection.getInputStream(), null, options);
                //Log.i("getBitmap", "bitmapFile saving..  " + bitmapFile);
                writeFile(bitmap, bitmapFile);

            } catch (Exception ex) {
                ////Log.e("Error 8098 ", ex.toString());
                return null;

            }
            // save bitmap to cache for later
            //Log.i("getBitmap", "7");

            //Log.i("getBitmap", "8");


            return bitmap;

        } catch (Exception ex) {
            //Log.i("getBitmap", "8 " + ex.toString());
            //ex.printStackTrace();

            return null;
        }
    }

    private void writeFile(Bitmap bmp, File f) {
        FileOutputStream out = null;

        try {
            out = new FileOutputStream(f);
            bmp.compress(Bitmap.CompressFormat.PNG, 80, out);
        } catch (Exception e) {
            //e.printStackTrace();
        } finally {
            try {
                if (out != null) out.close();
            } catch (Exception ex) {
            }
        }
    }

    /**
     * Classes *
     */

    private class ImageRef {
        public String url;
        public ImageView imageView;
        public int defDrawableId;

        public ImageRef(String u, ImageView i, int defaultDrawableId) {
            url = u;
            imageView = i;
            defDrawableId = defaultDrawableId;
        }
    }

    //stores list of images to download
    private class ImageQueue {
        private Stack<ImageRef> imageRefs =
                new Stack<ImageRef>();

        //removes all instances of this ImageView
        public void Clean(ImageView view) {

            for (int i = 0; i < imageRefs.size(); ) {
                if (imageRefs.get(i).imageView == view)
                    imageRefs.remove(i);
                else ++i;
            }
        }
    }

    private class ImageQueueManager implements Runnable {
        @Override
        public void run() {
            try {
                while (true) {
                    // Thread waits until there are images in the
                    // queue to be retrieved
                    //Log.i("ImageQueueManager", "1");
                    if (imageQueue.imageRefs.size() == 0) {
                        //Log.i("ImageQueueManager", "2");

                        synchronized (imageQueue.imageRefs) {
                            //Log.i("ImageQueueManager", "3");

                            imageQueue.imageRefs.wait();
                        }
                    }
                    //Log.i("ImageQueueManager", "4");

                    // When we have images to be loaded
                    if (imageQueue.imageRefs.size() != 0) {
                        //Log.i("ImageQueueManager", "5");

                        ImageRef imageToLoad;

                        synchronized (imageQueue.imageRefs) {
                            //Log.i("ImageQueueManager", "6");

                            imageToLoad = imageQueue.imageRefs.pop();
                        }
                        try {
                            Bitmap bmp = null;
                            if (imageToLoad.url.startsWith("-")) {
                                break;
                            } else {
                                bmp = getBitmap(imageToLoad.url);
                            }
                            if (bmp != null) {

                                //Log.i("ImageQueueManager", "7 " + imageToLoad.url);
                                String session = imageToLoad.url;//.substring(imageToLoad.url.indexOf("shifaappsettings/")+17,imageToLoad.url.indexOf(".jpg"));
                                //Log.i("ImageQueueManager", "session " + session);
                                // imageMap.put(session, new SoftReference<Bitmap>(bmp));

                                // Object tag = imageToLoad.imageView.getTag();
                                //   //Log.i("ImageQueueManager","8" );

                                // Make sure we have the right view - thread safety defender
                                //   if(tag != null && ((String)tag).equals(imageToLoad.url)) {
                                //     //Log.i("ImageQueueManager","9" );
                                BitmapDisplayer bmpDisplayer =
                                        new BitmapDisplayer(bmp, imageToLoad.imageView, R.drawable.ic_editor_insert_emoticon);


                                ////Log.i("ImageQueueManager", "10");
                                Activity a =
                                        (Activity) imageToLoad.imageView.getContext();
                                ////Log.i("ImageQueueManager", "11");
                                a.runOnUiThread(bmpDisplayer);
                                ////Log.i("ImageQueueManager", "12");
                                // }
                            } else {
                                BitmapDisplayer bmpDisplayer =
                                        new BitmapDisplayer(null, imageToLoad.imageView, R.drawable.ic_editor_insert_emoticon);
                            }
                        } catch (Exception ex) {
                        }
                    }

                    if (Thread.interrupted())
                        break;
                    //Log.i("ImageQueueManager", "13");
                }
            } catch (InterruptedException e) {
                //    Log.i("ImageQueueManager err", "14 " + e.toString());
            }
        }
    }

    //Used to display bitmap in the UI thread
    private class BitmapDisplayer implements Runnable {
        Bitmap bitmap;
        ImageView imageView;
        int defDrawableId;

        public BitmapDisplayer(Bitmap b, ImageView i, int defaultDrawableId) {
            bitmap = b;
            imageView = i;
            defDrawableId = defaultDrawableId;
        }

        public void run() {
            if (bitmap != null)
                imageView.setImageBitmap(bitmap);

            else
                imageView.setImageResource(R.drawable.ic_editor_insert_emoticon);

        }
    }
}
