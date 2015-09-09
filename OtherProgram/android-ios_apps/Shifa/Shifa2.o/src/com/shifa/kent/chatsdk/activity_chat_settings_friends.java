package com.shifa.kent.chatsdk;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.shifa.kent.R;


public class activity_chat_settings_friends extends Activity {
    public ImageManager imageManager;
    Super_Library_AppClass SLAc;
    ShifaDepartment ShifaDepart;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.chat_profile_friend);
        imageManager = new ImageManager(this);
        SLAc = new Super_Library_AppClass(this);
        ShifaDepart = new ShifaDepartment(this);

        ImageView imgProfile = (ImageView) findViewById(R.id.img_setting_profile);

        final TextView txtName = (TextView) findViewById(R.id.tx_setting_name);
        TextView edtCity = (TextView) findViewById(R.id.edt_setting_city);
        TextView edtCountry = (TextView) findViewById(R.id.edt_setting_country);
        TextView edtEmail = (TextView) findViewById(R.id.edt_settings_email);
        TextView edtSpeciality = (TextView) findViewById(R.id.edt_settings_speciality);

        txtName.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_NAME_FRIEND));
        edtCity.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_CITY_FRIEND));
        edtCountry.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_COUNTRY_FRIEND));
        edtEmail.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_EMAIL_FRIEND));
        edtCity.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_CITY_FRIEND));
        edtSpeciality.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_INFO_FRIEND));

        Thread thread = new Thread() {
            @Override
            public void run() {
                try {
                    while (true) {
                        sleep(1000);
                        try {
                            if (SLAc.GetPreferenceValue("FriendProfileLoaded").equals("true")) {
                                RelativeLayout rl = (RelativeLayout) findViewById(R.id.linearchatsetting);
                                rl.setVisibility(View.GONE);

                                SLAc.SavePreference("FriendProfileLoaded", "");

                                Intent intent = getIntent();
                                intent.putExtra("Restart", "yes");
                                finish();
                                startActivity(intent);
                            }
                        } catch (Exception ex) {
                            Log.e("ERROR: setting firne", ex.toString());
                        }
                    }
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        };


        Bundle extras = getIntent().getExtras();
        if (extras != null) {
            if (extras.getString("Restart") == null) {
                RelativeLayout rl = (RelativeLayout) findViewById(R.id.linearchatsetting);
                rl.setVisibility(View.GONE);
                try {
                    setTitle("Downloading Profile..");
                } catch (Exception ex) {

                }
                ShifaDepart.GetFriendContactInfo(extras.getString(".XXXXXXX_to"));
                thread.start();
            }
            imageManager.displayImage(extras.getString(".XXXXXXX_to"), imgProfile, R.drawable.ic_launcher);
            final String PictureURI = extras.getString(".XXXXXXX_to");
            imgProfile.setOnClickListener(new View.OnClickListener() {

                @Override
                public void onClick(View v) {

                    Intent intent = new Intent(activity_chat_settings_friends.this, ImageViewFullScreen.class);
                    intent.putExtra("imageUrl", PictureURI);
                    startActivity(intent);


                }
            });
        } else {
            finish();
        }

    }

    @Override
    protected void onStop() {
        super.onStop();
        finish();


    }

    private String filterString(String Value) {
        if (Value == null) return "";
        return Value;
    }


}
