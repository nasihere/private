package com.shifa.kent;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.util.Log;
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

    protected void SaveWSChatPHPGetUserPvtPublicChat(String ResponseDataReceived, String TableName) {
        try {
            if (GetPreferenceValue("WSChatPHPGetShifaPublicChat").equals(ResponseDataReceived)) {
                Log.e(TAG, "Chat Will not update becasue it's in cache...");
                return;
            }
            SavePreference("WSChatPHPGetShifaPublicChat", ResponseDataReceived);
            DBHelper db1 = new DBHelper(ctx);
            String FirstTopIdWeb = "";
            // Log.e(TAG,"SaveWSChatPHPGetUserPvtPublicChat: "+ "Response Start");
            String[] SplitByRows = ResponseDataReceived.split("##-##");
            String QueryLogs = "";
            HashMap<String, String> map = new HashMap<String, String>();
            for (int i = 0; i <= SplitByRows.length - 1; i++) {
                String[] SingleRow = SplitByRows[i].split("#-#");
                // Log.e(TAG,"SaveWSChatPHPGetUserPvtPublicChat: SplitByRows "+ SplitByRows[i]);
                String key = "";
                String val = "";
                String ORREPLACE = "";

                for (int j = 0; j <= SingleRow.length - 1; j++) {
                    //   Log.e(TAG,"SaveWSChatPHPGetUserPvtPublicChat: SingleRow "+ SingleRow[j]);
                    String[] KeyValue = SingleRow[j].split("#=#");
                    //  Log.e(TAG,"SaveWSChatPHPGetUserPvtPublicChat: KeyValue0 "+ KeyValue[0]);
                    if (KeyValue.length == 2) {
                        //  Log.e(TAG,"SaveWSChatPHPGetUserPvtPublicChat: KeyValue1 "+ KeyValue[1]);

                        if (KeyValue[0].equals("id_app") && !KeyValue[1].equals("0")) {
                            ORREPLACE = " OR REPLACE ";
                            key += KeyValue[0] + ", ";
                            val += "'" + KeyValue[1] + "', ";
                        } else if (KeyValue[0].equals("id_app") && KeyValue[1].equals("0")) {
                            ORREPLACE = "";
                        } else {
                            key += KeyValue[0] + ", ";
                            val += "'" + KeyValue[1] + "', ";
                        }
                        if (FirstTopIdWeb.equals("")) {
                            if (KeyValue[0].equals("id_web") && TableName.equals(".XXXXXXXapp_pvtchat")) {
                                FirstTopIdWeb = KeyValue[1];
                                SavePreference("pvt_id_web", FirstTopIdWeb);
                            } else if (KeyValue[0].equals("id_web") && TableName.equals(".XXXXXXXapp_contact")) {
                                FirstTopIdWeb = KeyValue[1];
                                SavePreference("contact_id_web", FirstTopIdWeb);
                            } else if (KeyValue[0].equals("id_web") && TableName.equals(".XXXXXXXapp_chat")) {
                                FirstTopIdWeb = KeyValue[1];
                                SavePreference("public_id_web", FirstTopIdWeb);
                            }
                        }
                    }
                }
                if (!key.equals("")) {
                    String ins = "INSERT " + ORREPLACE + " INTO  " + TableName;
                    String InsertUpdate = ins + " (" + key + " id) values (" + val + " null)";
                    Log.e(TAG, "SQL Query " + InsertUpdate);
                    QueryLogs += InsertUpdate + "; ";
                    db1.getWritableDatabase().execSQL(InsertUpdate);
                }

            }
            Log.e(TAG, QueryLogs);

            //This property update UI to update the list becasue new data come's in -- Important
            SavePreference("BroadCast_PvtPublic_Chat", "RefreshYourSelf");
            if (db1 != null) {
                db1.close();
            }
        } catch (Exception ex) {
            Log.e(TAG, "SaveWSChatPHPGetUserPvtPublicChat: " + ex.toString());
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

    public void ShowScreen(String ScreenName) {
        Intent intent = null;//
        if (ScreenName.equalsIgnoreCase("Login")) {
            intent = new Intent(ctx, login.class);
        } else if (ScreenName.equalsIgnoreCase("HomeScreen")) {
            intent = new Intent(ctx, home_menu.class);
        }
        if (intent == null) return;
        ctx.startActivity(intent);

    }

    public boolean NewDay() {
          /* Get Last Update Time from Preferences */
//        SharedPreferences prefs = ctx.getSharedPreferences("AppNameSettings", 0);
        String GetLastDay = GetPreferenceValue("lastUpdateTimeNewDay");
        if (GetLastDay.equals("0") || GetLastDay.equals("")) return true;
        long lastUpdateTime = Long.valueOf(GetLastDay);
		/* Should Activity Check for Updates Now? */
        if ((lastUpdateTime + (24 * 60 * 60 * 1000)) < System.currentTimeMillis()) {

		    /* Save current timestamp for next Check*/
            SavePreference("lastUpdateTimeNewDay", System.currentTimeMillis() + "");

            return true;


        } else {

            return true;
        }
    }

    public String UserSessionId() {
        SharedPreferences prefs = ctx.getSharedPreferences("AppNameSettings", 0);
        String restoredText = prefs.getString(".XXXXXXX", null);
        if (restoredText != null) {
            Log.e("UserSessionID", restoredText);
            return restoredText;
        }
        Log.e("UserSessionID", "No SessionId Found");
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
