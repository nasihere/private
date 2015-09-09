package com.shifa.kent.chatsdk;


import android.app.Activity;
import android.os.Bundle;
import android.widget.ImageView;

import com.shifa.kent.R;

public class ImageViewFullScreen extends Activity {


    ImageView imageView;
    ImageManager imageManager;

    /**
     * Called when the activity is first created.
     */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.imagefullscreen);
        imageManager = new ImageManager(this);
        imageManager.FacebookProfileSize("normal");
        imageView = (ImageView) findViewById(R.id.imgFullScreenPreview);

        Bundle extras = getIntent().getExtras();
        String imageURL = "";
        if (extras != null) {
            imageURL = extras.getString("imageUrl");
        }
        imageManager.displayImage(imageURL, imageView, R.drawable.ic_editor_insert_emoticon_right);

        imageURL = imageURL.replace("thumb", "normal");


        imageManager.displayImage(imageURL, imageView, R.drawable.ic_editor_insert_emoticon_right);


    }

}