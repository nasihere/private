package shifa.nasz.locally;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

public class activity_addnew_category extends Activity {
    Activity parentActivity = new Activity();

    ProgressDialog progressDialog;
	String SessionID = "";
	String CategoryID = "";
    Context ctx;
	int uniqueID = 0;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		ctx = this;
        Super_Library_AppClass SLAc;
        SLAc = new Super_Library_AppClass(this);
        SessionID = SLAc.RestoreSessionIndexID("session_id");
        CategoryID = SLAc.RestoreSessionIndexID("CategoryID");

        init();

		

	}


    private void SaveAddNew(){
        addnew_param obj = new addnew_param();

        AutoCompleteTextView e1 = (AutoCompleteTextView) findViewById(R.id.edExeANCategory);
        obj.Name = e1.getText().toString();




        String AddNewParam =
                    "Intensity#-#'"+SessionID+"'#_#" +
                    "maincategoy#-#'" + obj.Name + "'#_#" +
                    "categoy#-#'" + CategoryID + "'#_#" +
                    "Name#-#'" + obj.Name +  "'#_#" +
                    "level#-#'" + "" +  "'#_#" +
                    "remedies#-#'" + "" +  "'#_#" +
                    "sublevel#-#'" + "2" +  "'#_#" +
                    "id_web#-#" + "null" + "#_#";


        parentActivity = this;
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair("param", AddNewParam));

        Super_Library_URL SLU = new Super_Library_URL( "http://kent.nasz.us/mumbra/php/addnew.php",  nameValuePairs, parentActivity  );
        Toast.makeText(parentActivity, "Establishing Internet Connection....", 1000).show();



       // DownloadWebPageTask task = new DownloadWebPageTask();
       // task.execute(new String[] { "http://kent.nasz.us/mumbra/php/addnew.php?param="+AddNewParam });

    //    finish();

    }
	private void init() {
		 DBHelper db = new DBHelper(this);

		setContentView(R.layout.activity_addnew_category);
        AutoCompleteTextView textView = (AutoCompleteTextView) findViewById(R.id.edExeANCategory);
        String[] countries = getResources().getStringArray(R.array.countries_array);
// Create the adapter and set it to the AutoCompleteTextView
        ArrayAdapter<String> adapter =
                new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, countries);
        textView.setAdapter(adapter);


        final Button button = (Button) findViewById(R.id.btnADCategoryExeSavePublish);
        button.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                SaveAddNew();

                //	DownloadWebPageTask task = new DownloadWebPageTask();
                //	task.execute(new String[] { "http://kent.nasz.us/mumbra/php/sms.php?mobile=" + SessionID + "&guid="+uniqueID });

            }
        });

        final TextView tvSave = (TextView) findViewById(R.id.tvExeADCategorySave);
        tvSave.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                SaveAddNew();

                //	DownloadWebPageTask task = new DownloadWebPageTask();
                //	task.execute(new String[] { "http://kent.nasz.us/mumbra/php/sms.php?mobile=" + SessionID + "&guid="+uniqueID });

            }
        });
        final TextView tvCancel = (TextView) findViewById(R.id.tvExeANCategoryCancel);
        tvCancel.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
              //  SaveAddNew();
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

	private class DownloadWebPageTask extends
	AsyncTask<String, Context, String> {
		protected Context ctx;

		@Override
		protected String doInBackground(String... urls) {
			
			Log.e("doInBackground", "enter");
			String response = "";
			String uri = "";
			for (String url : urls) {
				uri = url;
				Log.e("uri", uri);
				try {
					DefaultHttpClient client = new DefaultHttpClient();
					HttpGet httpGet = new HttpGet(url);
					try {
						HttpResponse execute = client.execute(httpGet);
						InputStream content = execute.getEntity().getContent();

						BufferedReader buffer = new BufferedReader(
								new InputStreamReader(content));
						String s = "";
						while ((s = buffer.readLine()) != null) {
							response += s;
						}

					} catch (Exception e) {
						Log.e("Error http:", e.toString());

						e.printStackTrace();
						return "-999";
					}
				} catch (Exception ex) {
					Log.e("Error http:", ex.toString());
					return "-999";
				}
			}
			Log.e("Response data background", response);

			return response;
		}

		@Override
		protected void onPostExecute(String result) {
			if (progressDialog != null) {
                progressDialog.dismiss();
            }
			if (result.equals("-999"))
				return;


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
