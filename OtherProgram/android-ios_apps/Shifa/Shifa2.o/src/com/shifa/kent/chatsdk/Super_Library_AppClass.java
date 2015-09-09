package com.shifa.kent.chatsdk;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.text.Html;
import android.util.Log;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.GridLayout;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.utils.URLEncodedUtils;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

public class Super_Library_AppClass {
    protected static String CONST_USER_CITY = "session_city";
    protected static String CONST_USER_COUNTRY = "session_country";
    protected static String CONST_USER_EMAIL = "session_email";
    protected static String CONST_USER_INFO = "session_info";
    protected static String CONST_USER_OCCUPATION = "session_occupation";
    protected static String CONST_USER_PAID_FRIEND = "session_PAID_FRIEND";
    protected static String CONST_USER_NAME_FRIEND = "session_name_FRIEND";
    protected static String CONST_USER_CITY_FRIEND = "session_city_FRIEND";
    protected static String CONST_USER_COUNTRY_FRIEND = "session_country_FRIEND";
    protected static String CONST_USER_EMAIL_FRIEND = "session_email_FRIEND";
    protected static String CONST_USER_INFO_FRIEND = "session_info_FRIEND";
    protected static String CONST_USER_OCCUPATION_FRIEND = "session_occupation_FRIEND";
    protected static String CONST_USER_PUSH_NOTIFICATION = "session_push_notification";
    protected static String CONST_USER_EMAIL_NOTIFICATION = "session_email_notification";
    static String CONST_USER_NAME = "session_name";
    public JSONArray jsonObject = null;
    public String JsonString = "";
    Context ctx;
    String TAG = "Super_Library_AppClass";

    public Super_Library_AppClass(Context ctx) {
        this.ctx = ctx;

    }

    public String RestoreSessionIndexID(String SessionKey) {

        SharedPreferences prefs = ctx.getSharedPreferences("AppNameSettings", 0);
        String restoredText = prefs.getString(SessionKey, null);
        if (restoredText != null) {
            return restoredText;
        }
        return "1";

    }

    public void ShowToast(String Message) {
        Toast.makeText(ctx.getApplicationContext(), Message, Toast.LENGTH_SHORT).show();
    }

    protected void SaveWSChatPHPGetUserPvtPublicChat(String ResponseDataReceived, String TableName, String CacheName) {
        if (GetPreferenceValue(CacheName).equals(ResponseDataReceived)) {
            //  Log.e(TAG,"GetPreferenceValue(CacheName):" +GetPreferenceValue(CacheName));
            Log.e(TAG, "---------------------------------Same Data Cache will use..---------------------------------------------------" + CacheName);

            return;


        }

        DBHelper db1 = new DBHelper(ctx);
        //   try {
        Log.e("New Data found from server ... CacheName: ", CacheName);

        SavePreference(CacheName, ResponseDataReceived);
        String FirstTopIdWeb = "";
        Log.e(TAG, "response: " + ResponseDataReceived);
        // Log.e(TAG,"SaveWSChatPHPGetUserPvtPublicChat: "+ "Response Start");

        String[] SplitByRows = ResponseDataReceived.split("##-##");
        String QueryLogs = "";
        SavePreference("ShowLoading", "true");

        HashMap<String, String> map = new HashMap<String, String>();
        for (int i = 0; i <= SplitByRows.length - 1; i++) {
            String[] SingleRow = SplitByRows[i].split("#-#");
            // Log.e(TAG,"SaveWSChatPHPGetUserPvtPublicChat: SplitByRows "+ SplitByRows[i]);
            String key = "";
            String val = "";
            String ORREPLACE = " OR REPLACE  ";

            for (int j = 0; j <= SingleRow.length - 1; j++) {
                //   Log.e(TAG,"SaveWSChatPHPGetUserPvtPublicChat: SingleRow "+ SingleRow[j]);
                String[] KeyValue = SingleRow[j].split("#=#");
                //  Log.e(TAG,"SaveWSChatPHPGetUserPvtPublicChat: KeyValue0 "+ KeyValue[0]);
                if (KeyValue.length == 2) {
                    //  Log.e(TAG,"SaveWSChatPHPGetUserPvtPublicChat: KeyValue1 "+ KeyValue[1]);

                    if (TableName.equals(".XXXXXXXapp_contact")) { // for conatcs always fo insert or replace
                        //ORREPLACE = " OR REPLACE ";
                        key += KeyValue[0] + ", ";
                        val += "'" + KeyValue[1] + "', ";
                    } else if (KeyValue[0].equals("id_app") && KeyValue[1].equals("0")) {
                        //ORREPLACE ="";
                    } else {
                        key += KeyValue[0] + ", ";
                        val += "'" + KeyValue[1] + "', ";
                    }
                    if (FirstTopIdWeb.equals("")) {

                        if (KeyValue[0].equals("id_web") && TableName.equals(".XXXXXXXapp_contact")) {
                            FirstTopIdWeb = KeyValue[1];
                            SavePreference("contact_id_web", FirstTopIdWeb);
                        } else if (KeyValue[0].equals("id_web") && TableName.equals(".XXXXXXXapp_chat")) {
                            FirstTopIdWeb = KeyValue[1];
                        }
                    }
                }
            }
            if (!key.equals("")) {
                String ins = "INSERT " + ORREPLACE + " INTO  " + TableName;
                if (ORREPLACE.equals("")) {
                    key += "id_app, ";
                    val += "null,";
                }
                String InsertUpdate = ins + " (" + key + " id) values (" + val + " null)";
                Log.e(TAG, "SQL Query " + InsertUpdate);
                QueryLogs += InsertUpdate + "; ";
                db1.getWritableDatabase().execSQL(InsertUpdate);

            }

        }
        //   Log.e(TAG, QueryLogs);

        //This property update UI to update the list becasue new data come's in -- Important

        //  }catch(Exception ex){
        //      Log.e(TAG, "Error In Saving Database: " + ex.toString());
        //  }
        if (db1 != null) {
            db1.close();
        }
        Log.e("CHATUI", "BroadCast On");
        SavePreference("BroadCast_PvtPublic_Chat", "RefreshYourSelf");
        SavePreference("BroadCast_LoadMsg_Hide", "DOAction");
        SavePreference("ShowLoading", "");


    }

    public LinearLayout ChatToTemplate(String str) {
        try {
            EditText et;
            TextView tv;
            CheckBox ck;
            LinearLayout ll = new LinearLayout(ctx);
            ll.setOrientation(LinearLayout.VERTICAL);
            final float scale = ctx.getResources().getDisplayMetrics().density;
            int padding_5dp = (int) (5 * scale + 0.5f);
            int padding_20dp = (int) (20 * scale + 0.5f);
            int padding_50dp = (int) (50 * scale + 0.5f);
            str = str.replace(":", "");
            String[] LineQues = str.split("#-#");
            int Id_Set = 0;


            GridLayout hh = new GridLayout(ctx);
            for (int i = 0; i <= LineQues.length - 1; i++) {
                LineQues[i] = LineQues[i].trim();
                String StrLine = LineQues[i];

                if (LineQues[i].indexOf("#@#") != -1) {
                    if (StrLine.split(" ").length > 2) {
                        String[] SplitWord = StrLine.split(" ");
                        StrLine = "";
                        for (int k = 0; k <= SplitWord.length - 1; k++) {
                            if (k == 2 || k == 4 || k == 6) {
                                StrLine += "<br/>";
                            }
                            StrLine += SplitWord[k] + " ";

                        }
                    }

                    LineQues[i] = StrLine;

                    if (hh == null) {
                        hh = new GridLayout(ctx);
                        hh.setOrientation(GridLayout.HORIZONTAL);
                        int ColumnCount = 2;//ctx.getResources().getInteger(R.integer.abc_max_action_buttons);
                        hh.setColumnCount(ColumnCount - 1);
                    }
                    ck = new CheckBox(ctx);
                    ck.setText(Html.fromHtml(LineQues[i].substring(3)), TextView.BufferType.SPANNABLE);
                    ck.setId(Id_Set++);
                    hh.addView(ck);

                } else if (LineQues[i].indexOf("#=#") != -1) {
                    if (hh != null) {
                        ll.addView(hh);
                        hh = null;
                    }
                    tv = new TextView(ctx);

                    tv.setPadding(padding_5dp, padding_5dp, padding_5dp, padding_5dp);
                    tv.setText(Html.fromHtml(LineQues[i].substring(3)), TextView.BufferType.SPANNABLE);
                    tv.setId(Id_Set++);

                    ll.addView(tv);
                } else {
                    if (hh != null) {
                        ll.addView(hh);
                        hh = null;
                    }
                    et = new EditText(ctx);
                    et.setHint(LineQues[i]);
                    et.setId(Id_Set++);

                    ll.addView(et);

                }
            }
            if (hh != null) {
                ll.addView(hh);
                hh = null;
            }
            return ll;
        } catch (Exception ex) {
            return null;
        }
    }

    public HashMap<String, String> SetValueByKey(String ResponseValue) {
        String[] SplitByRows = ResponseValue.split("##-##");
        HashMap<String, String> map = new HashMap<String, String>();
        for (int i = 0; i <= SplitByRows.length - 1; i++) {
            String[] SingleRow = SplitByRows[i].split("#-#");

            for (int j = 0; j <= SingleRow.length - 1; j++) {
                String[] KeyValue = SingleRow[j].split("#=#");
                map.put(KeyValue[0], KeyValue[1]);

            }

        }
        return map;
    }

    public String GetValueByKey(String Key, String InternetResponseData) {
        HashMap<String, String> data = SetValueByKey(InternetResponseData);
        Iterator it = data.entrySet().iterator();
        while (it.hasNext()) {
            Map.Entry pairs = (Map.Entry) it.next();
            //      System.out.println(pairs.getKey() + " = " + pairs.getValue());
            if (pairs.getKey().equals(Key)) {
                return pairs.getValue().toString();
            }
        }
        return "";
    }

    public String GetPreferenceValue(String StringName) {
        SharedPreferences prefs = ctx.getSharedPreferences("AppNameSettings", 0);
        String restoredText = prefs.getString(StringName, null);

        if (restoredText != null) {
            //	Log.e("value pre " + StringName, restoredText);
            return restoredText;
        }
        return "0";
    }

    /* public void ShowScreen(String ScreenName){
         Intent intent = null;//
         if (ScreenName.equalsIgnoreCase("Login")){
              intent = new Intent(ctx, login.class);
         }
         else if (ScreenName.equalsIgnoreCase("HomeScreen")){
             intent = new Intent(ctx, home_menu.class);
         }
         if (intent == null) return;
         ctx.startActivity(intent);

     }*/
    public boolean NewDay() {
          /* Get Last Update Time from Preferences */
//        SharedPreferences prefs = ctx.getSharedPreferences("AppNameSettings", 0);
        String GetLastDay = GetPreferenceValue("lastUpdateTime");
        long lastUpdateTime = Long.valueOf(GetLastDay);
		/* Should Activity Check for Updates Now? */
        if ((lastUpdateTime + (24 * 60 * 60 * 1000)) < System.currentTimeMillis()) {

		    /* Save current timestamp for next Check*/
            SavePreference("lastUpdateTime", System.currentTimeMillis() + "");

            return true;


        } else {

            return true;
        }
    }

    public String UserSessionId() {
        SharedPreferences prefs = ctx.getSharedPreferences("AppNameSettings", 0);
        String restoredText = prefs.getString(".XXXXXXX", null);
        if (restoredText != null) {
            //  Log.e("UserSessionID", restoredText);
            return restoredText;
        }
        // Log.e("UserSessionID", "No SessionId Found");
        return "";
    }

    public String UserName() {
        SharedPreferences prefs = ctx.getSharedPreferences("AppNameSettings", 0);
        String restoredText = prefs.getString("session_name", null);
        if (restoredText != null) {
            Log.e("session_name", restoredText);
            return restoredText;
        }
        Log.e("session_name", "No SessionId Found");
        return "";
    }

    public void SavePreference(String StringName, String StringValue) {

        SharedPreferences.Editor editor = ctx.getSharedPreferences("AppNameSettings", 0).edit();
        editor.putString(StringName, StringValue);
        // Log.e("SetPreferenceValue " + StringName, StringValue);
        editor.commit();
    }

    public boolean isPaidMember() {
        SharedPreferences prefs = ctx.getSharedPreferences("AppNameSettings", 0);
        String restoredText = prefs.getString("RemoveAds", null);
        if (restoredText != null) // Register user
        {
            return true;
        } else {
            return false;

        }
    }

    public void PostWebApi(String[] values) {
        new MyAsyncTask().execute(values);
    }

    public HttpClient MyAsyncTask() {
        // TODO Auto-generated method stub
        return null;
    }

    public class MyAsyncTask extends AsyncTask<String, Integer, Double> {

        @Override
        protected Double doInBackground(String... params) {
            // TODO Auto-generated method stub

            if (params.length == 2)
                postData(params[0], params[1], false);
            else
                postData(params[0], params[1], true);
            return null;
        }

        protected void onPostExecute(Double result) {
            //			pb.setVisibility(View.GONE);

        }

        protected void onProgressUpdate(Integer... progress) {
//				pb.setProgress(progress[0]);
        }

        public void postData(String valueIWantToSend, String URL, Boolean flagresponse) {
            // Create a new HttpClient and Post Header
            StringBuilder builder = new StringBuilder();
            if (!URL.endsWith("?"))
                URL += "?";
            InputStream inputStream = null;
            String result = "";
            try {

                // create HttpClient
                HttpClient httpclient = new DefaultHttpClient();
                List<BasicNameValuePair> params = new LinkedList<BasicNameValuePair>();
                params.add(new BasicNameValuePair("value", valueIWantToSend));
                String paramString = URLEncodedUtils.format(params, "utf-8");
                URL += paramString;
                Log.e("url", URL);
                Log.e("url param", valueIWantToSend);
                // make GET request to the given URL
                HttpResponse httpResponse = httpclient.execute(new HttpGet(URL));
                Log.e("url response flag", flagresponse.toString());
                if (flagresponse == true) {

                    HttpEntity entity = httpResponse.getEntity();
                    InputStream content = entity.getContent();
                    BufferedReader reader = new BufferedReader(new InputStreamReader(content));
                    String line;
                    while ((line = reader.readLine()) != null) {
                        Log.e("jsonObject", line);
                        builder.append(line);
                    }
                    Log.e("jsonObject", "insert jsonObject");
                    JsonString = builder.toString();
                    jsonObject = new JSONArray(builder.toString());
                    Log.e("jsonObject", "received jsonObject");
                }

            } catch (Exception e) {
                JsonString = "";
                jsonObject = null;
                Log.e("url error", e.getLocalizedMessage());
                Log.d("InputStream", e.getLocalizedMessage());


            }

        }

    }
}
