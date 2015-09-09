package com.shifa.kent.chatsdk;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Switch;
import android.widget.TextView;

import com.shifa.kent.R;


public class activity_chat_settings extends Activity {
    public ImageManager imageManager;
    Super_Library_AppClass SLAc;
    ShifaDepartment ShifaDepart;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.chat_settings);
        imageManager = new ImageManager(this);
        SLAc = new Super_Library_AppClass(this);
        ShifaDepart = new ShifaDepartment(this);

        ImageView imgProfile = (ImageView) findViewById(R.id.img_setting_profile);
        final TextView txtName = (TextView) findViewById(R.id.tx_setting_name);
        TextView txttaptoedit = (TextView) findViewById(R.id.tv_setting_taptoedit);
        final EditText edtName = (EditText) findViewById(R.id.edt_setting_name);
        EditText edtCity = (EditText) findViewById(R.id.edt_setting_city);
        EditText edtCountry = (EditText) findViewById(R.id.edt_setting_country);
        TextView edtEmail = (TextView) findViewById(R.id.edt_settings_email);
        EditText edtSpeciality = (EditText) findViewById(R.id.edt_settings_speciality);
        TextView tvSaveLink = (TextView) findViewById(R.id.tvSaveLink);

        Switch myPushNotification;

        Switch myEmailNotification;

        myEmailNotification = (Switch) findViewById(R.id.swt_settings_emailnotfication);
        myPushNotification = (Switch) findViewById(R.id.swt_settings_pushnotification);


        txtName.setText(SLAc.UserName());
        edtName.setText(SLAc.UserName());
        edtCity.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_CITY));
        edtCountry.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_COUNTRY));
        edtEmail.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_EMAIL));
        edtCity.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_CITY));
        edtSpeciality.setText(SLAc.GetPreferenceValue(SLAc.CONST_USER_INFO));

        if (SLAc.GetPreferenceValue(SLAc.CONST_USER_EMAIL_NOTIFICATION).equals("1")) {
            myEmailNotification.setChecked(true);
        }
        if (SLAc.GetPreferenceValue(SLAc.CONST_USER_PUSH_NOTIFICATION).equals("1")) {
            myPushNotification.setChecked(true);
        }

        txttaptoedit.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                edtName.setVisibility(View.VISIBLE);
                txtName.setVisibility(View.GONE);
                edtName.requestFocus();
            }
        });


        imageManager.displayImage(SLAc.UserSessionId(), imgProfile, R.drawable.ic_launcher);

        final String PictureURI = SLAc.UserSessionId();
        imgProfile.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {

                Intent intent = new Intent(activity_chat_settings.this, ImageViewFullScreen.class);
                intent.putExtra("imageUrl", PictureURI);
                startActivity(intent);


            }
        });
        tvSaveLink.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {

                finish();
            }
        });
    }

    @Override
    protected void onStop() {
        super.onStop();
        final TextView txtName = (TextView) findViewById(R.id.tx_setting_name);
        TextView txttaptoedit = (TextView) findViewById(R.id.tv_setting_taptoedit);
        final EditText edtName = (EditText) findViewById(R.id.edt_setting_name);
        EditText edtCity = (EditText) findViewById(R.id.edt_setting_city);
        EditText edtCountry = (EditText) findViewById(R.id.edt_setting_country);
        TextView edtEmail = (TextView) findViewById(R.id.edt_settings_email);
        EditText edtSpeciality = (EditText) findViewById(R.id.edt_settings_speciality);
        RadioGroup radioSexGroup;
        radioSexGroup = (RadioGroup) findViewById(R.id.radioOccupation);
        int selectedId = radioSexGroup.getCheckedRadioButtonId();
        RadioButton radioSexButton;
        radioSexButton = (RadioButton) findViewById(selectedId);
        Switch myPushNotification;

        Switch myEmailNotification;

        myEmailNotification = (Switch) findViewById(R.id.swt_settings_emailnotfication);
        myPushNotification = (Switch) findViewById(R.id.swt_settings_pushnotification);


//s(String Name, String Email, String City, String Country, String Occupation, String Push_notification, String Email_Notification){
        ShifaDepart.SetSettings(
                filterString(edtName.getText().toString()),
                filterString(edtEmail.getText().toString()),
                filterString(edtCity.getText().toString()),
                filterString(edtCountry.getText().toString()),
                filterString(edtSpeciality.getText().toString()),

                filterString(radioSexButton.getText().toString()),
                myPushNotification.isChecked(),
                myEmailNotification.isChecked());
        Log.e("Chat Activity", "user not longer in the application");

    }

    private String filterString(String Value) {
        if (Value == null) return "";
        return Value;
    }


}
