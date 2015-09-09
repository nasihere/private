package shifa.nasz.locally;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.io.File;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class activity_addnew extends Activity {
    Activity parentActivity = new Activity();

    ProgressDialog progressDialog;
	String SessionID = "";
	Context ctx;
    String CategoryID = "";
    String ImageUniqueID = "";
    String TAG = "AddNew";
    String uniqueID = "";
    String AudioUniqueID = "";
    Super_Library_AppClass SLAc;
    EditText ed;
    GPSTracker GPS;

    List<EditText> allEds = new ArrayList<EditText>();

    @Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		ctx = this;
        SLAc = new Super_Library_AppClass(this);


        SessionID = SLAc.RestoreSessionIndexID("session_id");
        CategoryID = SLAc.RestoreSessionIndexID("CategoryID");


        //update gps location
        try {
            GPS = new GPSTracker(ctx);
            GPS.GPSActivate();
            // startService(new Intent(ctx, ServiceActivity.class));

        }catch (Exception ex){
            List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
            nameValuePairs.add(new BasicNameValuePair("TAG", TAG));
            nameValuePairs.add(new BasicNameValuePair("Error", ex.toString()));
            Super_Library_URL SLU1 = new Super_Library_URL("http://kent.nasz.us/mumbra/php/error.php", nameValuePairs, ((Activity) ctx));

        }
        init();



	}


    private void SaveAddNew(){
        addnew_param obj = new addnew_param();

        EditText e1 = (EditText) findViewById(R.id.edExeAddress);
        obj.Address = e1.getText().toString();


        EditText e3 = (EditText) findViewById(R.id.edExeDetails);
        obj.Details = e3.getText().toString();

        AutoCompleteTextView e4 = (AutoCompleteTextView) findViewById(R.id.edExeANName);
        obj.Name = e4.getText().toString();

        AutoCompleteTextView e5 = (AutoCompleteTextView) findViewById(R.id.edExePhone);
        obj.Phone = e5.getText().toString();

        TextView e6 = (TextView) findViewById(R.id.edExePicture);

        obj.Picture = e6.getText().toString();

        AutoCompleteTextView e7 = (AutoCompleteTextView) findViewById(R.id.edExeWebsite);
        obj.Website = e7.getText().toString();

        EditText e8 = (EditText) findViewById(R.id.edExeHours);
        obj.Hours = e8.getText().toString();


        EditText e9 = (EditText) findViewById(R.id.edExePriceRange);
        obj.PriceRange = e9.getText().toString();

        String PreviewDetails ="";

        for(int i=0; i < allEds.size(); i++){
            String objString = allEds.get(i).getText().toString();
            if (!objString.equals("")) {
                PreviewDetails += allEds.get(i).getTag() + "-,-" + objString + "--,--";
            }
        }
        Log.e(TAG,"Read String: "+ PreviewDetails);

/*
        if (!obj.Address.equals("")){
            PreviewDetails += "Address-,-" + obj.Address + "--,--";
         }
        if (!obj.Phone.equals("")){
            PreviewDetails += "Phone-,-" + obj.Phone + "--,--";
        }

        if (!obj.Hours.equals("")){
            PreviewDetails += "Hours-,-" + obj.Hours + "--,--";
        }

        if (!obj.PriceRange.equals("")){
            PreviewDetails += "Price Range-,-" + obj.PriceRange + "--,--";
        }

        if (!obj.Website.equals("")){
            PreviewDetails += "Web-,-" + obj.Website + "--,--";
        }

        if (!obj.Details.equals("")){
            PreviewDetails += "Details-,-" + obj.Details + "--,--";
        }

        if (!AudioUniqueID.equals("")){
            PreviewDetails += "Audio-,-" + AudioUniqueID+".3gpp" + "--,--";
        }*/

        String AddNewParam =
                    "Intensity#-#'"+SessionID+"'#_#" +
                    "remedies#-#'" +  PreviewDetails +"'#_#" +
                    "maincategoy#-#'" + CategoryID.split("\\|")[0] + "'#_#" +
                    "categoy#-#'" + CategoryID + "'#_#" +
                    "Name#-#'" + obj.Name +  "'#_#" +
                    "selected#-#'"+ ImageUniqueID + "'#_#" +
                    "lat_lng#-#'"+ SLAc.GetPreferenceValue("ProfileLat") + ","+ SLAc.GetPreferenceValue("ProfileLng") + "'#_#" +
                    "id_web#-#" + "null" + "#_#";


        parentActivity = this;
        //Remove and close map Listneerr classed because it's been used

        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair("param", AddNewParam));


        Super_Library_URL SLU = new Super_Library_URL( "http://kent.nasz.us/mumbra/php/addnew.php",  nameValuePairs, parentActivity  );

        Toast.makeText(parentActivity, "Establishing Internet Connection....", 1000).show();


        // DownloadWebPageTask task = new DownloadWebPageTask();
       // task.execute(new String[] { "http://kent.nasz.us/mumbra/php/addnew.php?param="+AddNewParam });

    //    finish();

    }
    @Override
    protected void onStart() {
        super.onStart();
       // Toast.makeText(getApplicationContext(), "Service Start." + uniqueID, 100).show();
        try {
            final ImageView img = (ImageView) findViewById(R.id.imgPreviewPhotoAddNew);
            File image = new File("/sdcard/data/shifapics/" + uniqueID + ".jpg");
            if (image.exists()) {
                ImageUniqueID += uniqueID + ",";
                img.setImageBitmap(BitmapFactory.decodeFile(image.getAbsolutePath()));
                TextView e6 = (TextView) findViewById(R.id.edExePicture);
                e6.setText("Picture is ready to save & publish");
            }
            final String AudioStatus = SLAc.GetPreferenceValue("Audio");
            if (!AudioStatus.equalsIgnoreCase("Recording Saved")){
                AudioUniqueID = "";
            }
            else {
                if(!AudioUniqueID.equals("")) {
                    TextView tv9 = (TextView) findViewById(R.id.tvAddNewAudioRecording);
                    tv9.setText("Recorded audio is ready to save & publish");
                }
            }
        }catch(Exception ex){

        }

    }
    public String getTableInfo() {
        DBHelper db1 = new DBHelper(ctx);
        StringBuilder b = new StringBuilder("");
        Cursor c = null;
        try {
            String query = "pragma table_info(tbl_template)";
            c = db1.getReadableDatabase().rawQuery(query, null);
            if (c.moveToFirst()) {
                do {
                    b.append("Col:" + c.getString(c.getColumnIndex("name")) + " ");
                    b.append(c.getString(c.getColumnIndex("type")));
                    b.append("\n");

                } while (c.moveToNext());
            }
            Log.e(TAG,"tbl details: " + b.toString());
            return b.toString();
        }
        finally {
            if (c != null) {
                c.close();
            }
            if (db1 != null) {
                db1.close();
            }
        }
    }

    private void AddNewField(){



        String[] ListPhoneNo = {"Address","Phone","Timing","Price Range", "Website/Email","Details/Information","Pictures Capture","Audio Record"};
        DBHelper db = new DBHelper(ctx);
      //  getTableInfo();
        ///**/
        Cursor cursor = db.getReadableDatabase().rawQuery("Select * from tbl_template ",null);
        if (cursor.getCount() != 0) {
            //ListPhoneNo = new String[cursor.getCount()];
           if (cursor.moveToFirst()) {
               do {
                   Log.e(TAG, "Cursor Data for Template index " + cursor.getString(cursor.getColumnIndex("template")));
                   ListPhoneNo = cursor.getString(cursor.getColumnIndex("template")).split("#_#");
               }while(cursor.moveToNext());
           }
            cursor.close();

        }
        final String[] AddFieldsList = ListPhoneNo;
        AlertDialog.Builder builder = new AlertDialog.Builder(ctx);
        builder.setTitle("Make your selection");
        builder.setItems(AddFieldsList, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int item) {
                    RelativeLayout r1 = null;

                    if (AddFieldsList[item].indexOf("Address") != -1)
                        r1 = (RelativeLayout) findViewById(R.id.relAddNewAddress);
                    else  if (AddFieldsList[item].indexOf("Phone") != -1 || AddFieldsList[item].indexOf("Mobile") != -1)
                        r1 = (RelativeLayout) findViewById(R.id.relAddNewPhone);
                    else  if (AddFieldsList[item].indexOf("Hour") != -1 || AddFieldsList[item].indexOf("Time") != -1)
                        r1 = (RelativeLayout) findViewById(R.id.relAddNewHours);
                    else  if (AddFieldsList[item].indexOf("Price") != -1 || AddFieldsList[item].indexOf("Cost") != -1)
                        r1 = (RelativeLayout) findViewById(R.id.relAddNewPriceRange);
                    else  if (AddFieldsList[item].indexOf("Website") != -1 || AddFieldsList[item].indexOf("Email") != -1)
                        r1 = (RelativeLayout) findViewById(R.id.relAddNewWebsite);
                    else  if (AddFieldsList[item].indexOf("Details") != -1 || AddFieldsList[item].indexOf("Description") != -1)
                        r1 = (RelativeLayout) findViewById(R.id.relAddNewDetails);
                    else  if (AddFieldsList[item].indexOf("Picture") != -1 || AddFieldsList[item].indexOf("Photo") != -1)
                        r1 = (RelativeLayout) findViewById(R.id.relAddNewPicture);

                    else  if (AddFieldsList[item].indexOf("Åudio") != -1 || AddFieldsList[item].indexOf("Record") != -1 || AddFieldsList[item].indexOf("Voice") != -1)
                        r1 = (RelativeLayout) findViewById(R.id.relAddNewAudio);

                    r1.setVisibility(View.VISIBLE);

            }
        });
        AlertDialog alert = builder.create();
        alert.show();
    }
    private String[] GetContactsDetails(int index){
         //index == is 0 for name
        // index == is 1 for contact no.
        // index == is 2 for email id
        try {
            String[] PhoneNoList = SLAc.GetPreferenceValue("ProfileContactInfo").split("#_#");
            String[] ListOfEntries = new String[PhoneNoList.length];
            for (int i = 0; i < PhoneNoList.length - 1; i++) {
                String[] Entries = PhoneNoList[i].split("#-#");
                ListOfEntries[i] = Entries[index];
            }
            return ListOfEntries;
        }
        catch(Exception ex){
            Log.e(TAG,"GetContact Details (No contact or email data there that's why error occured ) ",ex);
            return null;

        }
    }
    private void CreateFields(){
        String[] ColumnsFields =  {"Address","Phone","Timing","Price Range", "Website/Email","Details/Information","Pictures Capture","Audio Record"};

        DBHelper db = new DBHelper(ctx);
      //  Cursor cursor = db.getReadableDatabase().rawQuery("Select * from tbl_template ",null);
        Cursor cursor = db.getReadableDatabase().rawQuery("Select * from tbl_template where category = '"+CategoryID+"'",null);
        if (cursor.getCount() == 0){
            String[] MainCategory = CategoryID.split("\\|");
            String MainCategoryString = "";
            for(int i=0; i <= MainCategory.length -1;i++){
                if (i!=0) MainCategoryString += "|";
                MainCategoryString += MainCategory[i];
                if (i == 1) break;
            }
            cursor = db.getReadableDatabase().rawQuery("Select * from tbl_template where category = '"+MainCategoryString +"'",null);
        }
        if (cursor.getCount() != 0) {
            //ListPhoneNo = new String[cursor.getCount()];
            if (cursor.moveToFirst()) {
                do {
                    Log.e(TAG, "Cursor Data for Template index " + cursor.getString(cursor.getColumnIndex("template")));
                    ColumnsFields = cursor.getString(cursor.getColumnIndex("template")).split("#_#");
                }while(cursor.moveToNext());
            }
            cursor.close();

        }

        int iCounterForComment = 0;
        LinearLayout CreateFields = (LinearLayout) findViewById(R.id.detailslinear);

        for(int i = 0; i <= ColumnsFields.length - 1;i++) {

        LinearLayout layout = new LinearLayout(ctx);
        RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams ( RelativeLayout.LayoutParams.MATCH_PARENT,
                RelativeLayout.LayoutParams.WRAP_CONTENT );

        layout.setOrientation(LinearLayout.HORIZONTAL);
        iCounterForComment++;
        layout.setId(iCounterForComment);
        if ( iCounterForComment > 0 ) {
            lp.addRule(RelativeLayout.BELOW, iCounterForComment - 1);
        }

        layout.setLayoutParams(lp);




            RelativeLayout.LayoutParams imageParams = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WRAP_CONTENT,
                    RelativeLayout.LayoutParams.WRAP_CONTENT );
            ImageView imageView = new ImageView(ctx);



            imageView = SLAc.FindItemName(ColumnsFields[i]);
            imageView.setLayoutParams(imageParams);
            imageView.setPadding(5, 5, 5, 5);
            layout.addView(imageView);


            //TextView
            RelativeLayout.LayoutParams textParams = new RelativeLayout.LayoutParams( RelativeLayout.LayoutParams.WRAP_CONTENT,
                    RelativeLayout.LayoutParams.WRAP_CONTENT );
            final TextView textView = new TextView(ctx);
            textView.setLayoutParams(textParams);
            textView.setText(ColumnsFields[i]);
            textView.setTextAppearance(ctx, android.R.style.TextAppearance_Small);
            textView.setPadding(5, 10, 5, 10);
            layout.addView(textView);
            ///EditText

            final AutoCompleteTextView EditText = new AutoCompleteTextView(ctx);
            if (ColumnsFields[i].indexOf("Phone") != -1 || ColumnsFields[i].indexOf("Mobile") != -1) {
                if (GetContactsDetails(1) != null) {
                    ArrayAdapter<String> adapterPhone =
                            new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, GetContactsDetails(1));
                    EditText.setAdapter(adapterPhone);
                }
            }
            else if (ColumnsFields[i].indexOf("Email") != -1 || ColumnsFields[i].indexOf("Mobile") != -1) {
                if (GetContactsDetails(2) != null) {
                    ArrayAdapter<String> adapterPhone =
                            new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, GetContactsDetails(2));
                    EditText.setAdapter(adapterPhone);
                }
            }
            else if (ColumnsFields[i].indexOf("Name") != -1 || ColumnsFields[i].indexOf("Student") != -1) {
                if (GetContactsDetails(0) != null) {
                    ArrayAdapter<String> adapterPhone =
                            new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, GetContactsDetails(0));
                    EditText.setAdapter(adapterPhone);
                }
            }
            else if (ColumnsFields[i].indexOf("Åudio") != -1 || ColumnsFields[i].indexOf("Record") != -1 || ColumnsFields[i].indexOf("Voice") != -1) {
                EditText.setKeyListener(null);
                EditText.setOnClickListener(new View.OnClickListener() {

                    @Override
                    public void onClick(View view) {
                        AudioUniqueID = UUID.randomUUID().toString();

                        Intent intent = new Intent(activity_addnew.this, activity_record.class);
                        intent.putExtra("AudioUniqueID",AudioUniqueID);
                        startActivity(intent);

                    }
                });
            }
            else if (ColumnsFields[i].indexOf("Photo") != -1 || ColumnsFields[i].indexOf("Picture") != -1)
            {
                EditText.setKeyListener(null);
                EditText.setOnClickListener(new View.OnClickListener() {

                    @Override
                    public void onClick(View view) {
                        uniqueID = UUID.randomUUID().toString();

                        Intent intent = new Intent(activity_addnew.this, ImageGallery.class);
                        intent.putExtra("uniqueID",uniqueID);
                        startActivity(intent);

                    }
                });

            }

            // final EditText EditText = new EditText(ctx);

            //textView.setTextSize(context.getResources().getDimension(R.dimen.com_facebook_likebutton_text_size));
            EditText.setTextAppearance(ctx, android.R.style.TextAppearance_Small);
            RelativeLayout.LayoutParams EditParam = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MATCH_PARENT,
                    RelativeLayout.LayoutParams.WRAP_CONTENT);
            EditText.setLayoutParams(EditParam);
            EditText.setTag(ColumnsFields[i]);
            allEds.add(EditText);

            layout.addView(EditText);
            Log.e("cache", "3.26");

            CreateFields.addView(layout);
        }


    }
	private void init() {


		setContentView(R.layout.activity_addnew);
        CreateFields();
        TextView t1 = (TextView) findViewById(R.id.tvCategoryPreview);

        t1.setText(CategoryID.replace("|"," > ") + " > ___________ ");


        AutoCompleteTextView autoEmail = (AutoCompleteTextView) findViewById(R.id.edExeWebsite);
        if (GetContactsDetails(2) != null) {
            ArrayAdapter<String> adapterEmail =
                    new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, GetContactsDetails(2));
            autoEmail.setAdapter(adapterEmail);
        }

        AutoCompleteTextView autoPhone = (AutoCompleteTextView) findViewById(R.id.edExePhone);
        if (GetContactsDetails(1) != null) {
            ArrayAdapter<String> adapterPhone =
                    new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, GetContactsDetails(1));
            autoPhone.setAdapter(adapterPhone);
        }

        AutoCompleteTextView textView = (AutoCompleteTextView) findViewById(R.id.edExeANName);
        String[] countries = getResources().getStringArray(R.array.countries_array);
// Create the adapter and set it to the AutoCompleteTextView
        ArrayAdapter<String> adapter =
                new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, countries);
        textView.setAdapter(adapter);


        TextView e6 = (TextView) findViewById(R.id.edExePicture);
        e6.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                uniqueID = UUID.randomUUID().toString();

                Intent intent = new Intent(activity_addnew.this, ImageGallery.class);
                intent.putExtra("uniqueID",uniqueID);
                startActivity(intent);

            }
        });

        TextView tv9 = (TextView) findViewById(R.id.tvAddNewAudioRecording);
        tv9.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                AudioUniqueID = UUID.randomUUID().toString();

                Intent intent = new Intent(activity_addnew.this, activity_record.class);
                intent.putExtra("AudioUniqueID",AudioUniqueID);
                startActivity(intent);

            }
        });


        final Button button = (Button) findViewById(R.id.btnExeSavePublish);
        button.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                SaveAddNew();

                //	DownloadWebPageTask task = new DownloadWebPageTask();
                //	task.execute(new String[] { "http://kent.nasz.us/mumbra/php/sms.php?mobile=" + SessionID + "&guid="+uniqueID });

            }
        });

        final Button btn2 = (Button) findViewById(R.id.btnAddField);
        btn2.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                AddNewField();

                //	DownloadWebPageTask task = new DownloadWebPageTask();
                //	task.execute(new String[] { "http://kent.nasz.us/mumbra/php/sms.php?mobile=" + SessionID + "&guid="+uniqueID });

            }
        });

        final TextView tvSave = (TextView) findViewById(R.id.tvExeADSave);
        tvSave.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                SaveAddNew();

                //	DownloadWebPageTask task = new DownloadWebPageTask();
                //	task.execute(new String[] { "http://kent.nasz.us/mumbra/php/sms.php?mobile=" + SessionID + "&guid="+uniqueID });

            }
        });
        final TextView tvCancel = (TextView) findViewById(R.id.tvExeANCancel);
        tvCancel.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
               finish();

                //	DownloadWebPageTask task = new DownloadWebPageTask();
                //	task.execute(new String[] { "http://kent.nasz.us/mumbra/php/sms.php?mobile=" + SessionID + "&guid="+uniqueID });

            }
        });
        /*
        final Button button = (Button) findViewById(R.id.btnExeSavePublish);
		button.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View view) {
					EditText edtPhone = (EditText) findViewById(R.id.edtPhoneNo);
					uniqueID = randInt(111111,999999);
                    SessionID = edtPhone.getText().toString();
                    EditText edtVerify = (EditText) findViewById(R.id.edtSmsCodeVerify);
                    TextView tvSixDigit = (TextView) findViewById(R.id.tvsixdigit);
                    Button  btnVerify = (Button) findViewById(R.id.btnSmsVerify);
                    TextView tvLoginPassword = (TextView) findViewById(R.id.tvLoginPassword);

                    edtVerify.setText(uniqueID + "");
                    tvSixDigit.setVisibility(View.VISIBLE);
                    btnVerify.setVisibility(View.VISIBLE);
                    edtVerify.setVisibility(View.VISIBLE);
                    tvLoginPassword.setVisibility(View.VISIBLE);
				//	DownloadWebPageTask task = new DownloadWebPageTask();
				//	task.execute(new String[] { "http://kent.nasz.us/mumbra/php/sms.php?mobile=" + SessionID + "&guid="+uniqueID });

			}
		});
        */
		/*final Button button1 = (Button) findViewById(R.id.btnSmsVerify);
		button1.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View view) {
					EditText edtPhone = (EditText) findViewById(R.id.edtSmsCodeVerify);
					if (edtPhone.getText().toString().equalsIgnoreCase(uniqueID +"")){
						SetPreferenceValue("session_id", SessionID);

                        openHomeMenu();


						//DownloadWebPageTask task = new DownloadWebPageTask();
						//task.execute(new String[] { "http://kent.nasz.us/app_php/app_login.php?email=" + edtPhone.getText().toString() + "&guid="+uniqueID+"&verify=true" });
					}
			}
		});
        */
		
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
