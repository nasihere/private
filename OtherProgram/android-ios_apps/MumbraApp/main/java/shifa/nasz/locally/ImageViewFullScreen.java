package shifa.nasz.locally;




import android.app.Activity;
import android.os.Bundle;
import android.widget.ImageView;

public class ImageViewFullScreen extends Activity {


    public static final String URL =
            "http://kent.nasz.us/app_php/shifaappsettings/mumbra/.jpg";
    ImageView imageView;
    ImageManager imageManager;
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.imagefullscreen);
        imageManager = new ImageManager(this);
        imageView = (ImageView) findViewById(R.id.imgFullScreenPreview);

        Bundle extras = getIntent().getExtras();
        String imageURL = "";
        if (extras != null) {
            imageURL = extras.getString("imageUrl");
        }
        imageManager.displayImage(imageURL+"_thumb", imageView, R.drawable.ic_screen_shot_2015_01_21_at_5);

        imageManager.displayImage(imageURL, imageView, R.drawable.ic_screen_shot_2015_01_21_at_5);



    }

}