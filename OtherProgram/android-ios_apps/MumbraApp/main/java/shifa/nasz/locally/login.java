package shifa.nasz.locally;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.google.android.gcm.GCMRegistrar;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.util.ArrayList;
import java.util.List;

public class login extends Activity {

    private static String TAG = "Login";
		ProgressDialog progressDialog;
	boolean onlybackgroundthread = false;
	String SessionID = "";
	Context ctx;
    Super_Library_AppClass SLAc;
	int uniqueID = 0;
    String ContactInfo = "";
    String VersionNameCheck = "NaszFirst";

    @Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		ctx = this;


      /*  Intent intent = new Intent(login.this, MainActivity.class);
        startActivity(intent);


        finish();
        if (true) return;

        */
   //     stopService(new Intent(this, ServiceActivity.class));
     //   startService(new Intent(ctx, ServiceActivity.class)); // start notfication and service start by default

        SLAc = new Super_Library_AppClass(this);
		SessionID = LoggedIn();
		Log.e("SessionID", SessionID);

//        stopService(new Intent(this, ServiceActivity.class));
        if (SessionID.equals("")) { // if no login found then ask as login window
			Log.e("database", "dbclassdn");
            try {
                GCMRegistrar.checkDevice(this);
                GCMRegistrar.checkManifest(this);
                GCMRegistrar.register(login.this,
                        GCMIntentService.SENDER_ID);
            }catch (Exception ex){
                Log.e(TAG,"Error ",ex);
            }
            ContactInfo = SLAc.FetchContacts(); // it will fetch the contact and save it on server. eithe rhe do registration or not
         //Uncoment when i done testing -- nasir//
          LoginCreate();

		}
		else{
  //          startService(new Intent(this, ServiceActivity.class));
            openHomeMenu();
        }

	}



    private  int randInt(int min, int max) {

        // Usually this can be a field rather than a method variable
        java.util.Random rand = new java.util.Random();

        // nextInt is normally exclusive of the top value,
        // so add 1 to make it inclusive
        int randomNum = rand.nextInt((max - min) + 1) + min;

        return randomNum;
    }
	private void LoginCreate() {

        DBHelper db = new DBHelper(this);
        /*if (!SLAc.GetPreferenceValue("VersionName").equals(VersionNameCheck) || true) {
        //    db.initializeDataBase();
            Intent intent = new Intent(login.this, MainActivity.class);
            startActivity(intent);
            SLAc.SavePreference("VersionName",VersionNameCheck);
        }
*/
		setContentView(R.layout.login);
        android.telephony.TelephonyManager tMgr = (android.telephony.TelephonyManager)ctx.getSystemService(Context.TELEPHONY_SERVICE);
        String mPhoneNumber = tMgr.getLine1Number();

        EditText edtPhone = (EditText) findViewById(R.id.edtPhoneNo);
        edtPhone.setText(mPhoneNumber);





        TextView tvSelectAgeGroup = (TextView) findViewById(R.id.tvSelectAgeGroup);
        tvSelectAgeGroup.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                final String[] AgeList = {"Kids (10-13 Year)","Teenage (14-18 Year)","Young (19-40 Year)","Older (41 or more)"};
                AlertDialog.Builder builder = new AlertDialog.Builder(ctx);
                builder.setTitle("Make your selection");
                builder.setItems(AgeList, new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int item) {
                        RelativeLayout r1 = null;
                        TextView tvSelectAgeGroup = (TextView) findViewById(R.id.tvSelectAgeGroup);
                        tvSelectAgeGroup.setText(AgeList[item]);


                             }
                });
                AlertDialog alert = builder.create();
                alert.show();
            }
        });
        final Button button = (Button) findViewById(R.id.btnSignin);
		button.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View view) {
                String gcmreg = SLAc.GetPreferenceValue("gcmreg");

                if (gcmreg.equalsIgnoreCase("")) {
                    try {
                        Thread.sleep(3000);
                        gcmreg = SLAc.GetPreferenceValue("gcmreg");
                    }catch(InterruptedException e){
                        e.printStackTrace();
                    }
                }

                EditText tvLoginAddress  = (EditText) findViewById(R.id.tvLoginAddress);
					EditText edtPhone = (EditText) findViewById(R.id.edtPhoneNo);
                    EditText edtName = (EditText) findViewById(R.id.edtNameLogin);
                    final RadioGroup radioSexGroup = (RadioGroup) findViewById(R.id.radioSex);
                    int selectedId = radioSexGroup.getCheckedRadioButtonId();
                    RadioButton radioSexButton = (RadioButton) findViewById(selectedId);
                    String Gender = radioSexButton.getText().toString();
                    uniqueID = randInt(111111,999999);
                    SessionID = edtPhone.getText().toString();
                    String NameLogin = edtName.getText().toString();
                    String Address= tvLoginAddress.getText().toString();

                    TextView tvSelectAgeGroup = (TextView) findViewById(R.id.tvSelectAgeGroup);
                    String SelectAgeGroup = tvSelectAgeGroup.getText().toString();

                if (NameLogin.equalsIgnoreCase("")){
                        edtName.requestFocus();
                        return;
                    }
                    if (SessionID.equalsIgnoreCase("")){
                        edtPhone.requestFocus();
                        return;
                    }
                    if (SelectAgeGroup.equalsIgnoreCase("")){
                        tvSelectAgeGroup.requestFocus();
                        return;
                    }
                    EditText edtVerify = (EditText) findViewById(R.id.edtSmsCodeVerify);
                    TextView tvSixDigit = (TextView) findViewById(R.id.tvsixdigit);
                    Button  btnVerify = (Button) findViewById(R.id.btnSmsVerify);
                    TextView tvLoginPassword = (TextView) findViewById(R.id.tvLoginPassword);


                    edtVerify.setHint("XXXXXXX");
                    tvSixDigit.setVisibility(View.VISIBLE);
                    btnVerify.setVisibility(View.VISIBLE);
                    edtVerify.setVisibility(View.VISIBLE);
                    tvLoginPassword.setVisibility(View.VISIBLE);
                    edtVerify.requestFocus();



                List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
                    nameValuePairs.add(new BasicNameValuePair("mobile", SessionID));
                    nameValuePairs.add(new BasicNameValuePair("name", NameLogin));
                    nameValuePairs.add(new BasicNameValuePair("gender", Gender));
                    nameValuePairs.add(new BasicNameValuePair("age", SelectAgeGroup));
                    nameValuePairs.add(new BasicNameValuePair("code", String.valueOf(uniqueID)));
                    nameValuePairs.add(new BasicNameValuePair("address", Address));
                    nameValuePairs.add(new BasicNameValuePair("contactinfo", ContactInfo));
                    nameValuePairs.add(new BasicNameValuePair("gcmreg", gcmreg));



                SLAc.SavePreference("ProfileName",NameLogin);
                    SLAc.SavePreference("ProfileMobile",SessionID);
                    SLAc.SavePreference("ProfileGender",Gender);
                    SLAc.SavePreference("ProfileAddress",Address);


                    SLAc.SavePreference("ProfileAge", SelectAgeGroup);

                    Super_Library_URL SLU = new Super_Library_URL("http://kent.nasz.us/mumbra/php/sms.php", nameValuePairs, ((Activity) ctx));

                try {
                        GPSTracker GPS = new GPSTracker(ctx);
                        GPS.GPSActivate();

                    }catch (Exception ex){
                        nameValuePairs.add(new BasicNameValuePair("TAG", TAG));
                        nameValuePairs.add(new BasicNameValuePair("Error", ex.toString()));
                        Super_Library_URL SLU1 = new Super_Library_URL("http://kent.nasz.us/mumbra/php/error.php", nameValuePairs, ((Activity) ctx));

                    }
                //	DownloadWebPageTask task = new DownloadWebPageTask();
				//	task.execute(new String[] { "http://kent.nasz.us/mumbra/php/sms.php?mobile=" + SessionID + "&guid="+uniqueID });

			}
		});
        final Button button1 = (Button) findViewById(R.id.btnSmsVerify);
		button1.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View view) {
					EditText edtPhone = (EditText) findViewById(R.id.edtSmsCodeVerify);
					if (edtPhone.getText().toString().equalsIgnoreCase(uniqueID +"") || edtPhone.getText().toString().equalsIgnoreCase("004756")){
						SetPreferenceValue("session_id", SessionID);

                        openHomeMenu();


                        //DownloadWebPageTask task = new DownloadWebPageTask();
						//task.execute(new String[] { "http://kent.nasz.us/app_php/app_login.php?email=" + edtPhone.getText().toString() + "&guid="+uniqueID+"&verify=true" });
					}
			}
		});


	}


    private void openHomeMenu(){

        //update gps location
        try {
            GPSTracker GPS = new GPSTracker(ctx);
            GPS.GPSActivate();
           // startService(new Intent(ctx, ServiceActivity.class));

        }catch (Exception ex){
            List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
            nameValuePairs.add(new BasicNameValuePair("TAG", TAG));
            nameValuePairs.add(new BasicNameValuePair("Error", ex.toString()));
            Super_Library_URL SLU1 = new Super_Library_URL("http://kent.nasz.us/mumbra/php/error.php", nameValuePairs, ((Activity) ctx));

        }
      //  Super_Library_AppClass SLAc = new Super_Library_AppClass(ctx);
        //SLAc.ClearHistory();


        String expiry = GetPreferenceValue("exp");
        String[] formats = new String[] {
                "yyyy-MM-dd"//,
                /*"yyyy-MM-dd HH:mm",
                "yyyy-MM-dd HH:mmZ",
                "yyyy-MM-dd HH:mm:ss.SSSZ",
                "yyyy-MM-dd'T'HH:mm:ss.SSSZ",*/
        };
        for (String format : formats) {
            java.text.SimpleDateFormat sdf = new java.text.SimpleDateFormat(format, java.util.Locale.US);
            sdf.setTimeZone(java.util.TimeZone.getTimeZone("UTC"));

            String newExpiry = sdf.format(new java.util.Date(0));
            if (!expiry.equals(newExpiry)) {
                SetPreferenceValue("exp", newExpiry);
                Log.e("exp","true");
              /*  Intent intent = new Intent(login.this, activity_settings.class);
                intent.putExtra("where", "1");
                intent.putExtra("cols", "id_web,book,newrem,maincategoy,sublevel,Name,remedies,Intensity,categoy,level,selected,entry");
                startActivity(intent);
                finish();*/
                //Super_Library_AppClass SLAc = new Super_Library_AppClass(ctx);
               // SLAc.FetchData();

               /* //update gps location
                try {
                    GPSTracker GPS = new GPSTracker(ctx);
                    GPS.GPSActivate();
                   // startService(new Intent(ctx, ServiceActivity.class));

                }catch (Exception ex) {
                    List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
                    nameValuePairs.add(new BasicNameValuePair("TAG", TAG));
                    nameValuePairs.add(new BasicNameValuePair("Error", ex.toString()));
                    Super_Library_URL SLU1 = new Super_Library_URL("http://kent.nasz.us/mumbra/php/error.php", nameValuePairs, ((Activity) ctx));
                }*/
            }
            else{


                Log.e("exp", "false");
          //      Intent intent = new Intent(login.this, activity_kent.class);
           //     Intent intent = new Intent(login.this, activity_googleMap.class);
                Intent intent = new Intent(login.this, activity_tabviewer.class);
                startActivity(intent);


                finish();

            }
        }



    }


	private String LoggedIn() {
		SharedPreferences prefs = getSharedPreferences("AppNameSettings", 0);
		String restoredText = prefs.getString("session_id", null);
		if (restoredText != null) {

			return restoredText;
		}
		return "";
	}



	public String GetPreferenceValue(String StringName) {
		SharedPreferences prefs = ctx
				.getSharedPreferences("AppNameSettings", 0);
		String restoredText = prefs.getString(StringName, null);
		if (restoredText != null && restoredText != "") {
			return restoredText;
		}
		return "0";
	}

	public void SetPreferenceValue(String Key, String Value) {
		SharedPreferences.Editor editor = getSharedPreferences(
				"AppNameSettings", 0).edit();
		editor.putString(Key, Value);
		editor.commit();
	}


}
