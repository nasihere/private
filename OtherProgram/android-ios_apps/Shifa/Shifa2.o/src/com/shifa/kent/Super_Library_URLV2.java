package com.shifa.kent;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

public class Super_Library_URLV2 {
    public String ChatTextSend = "";
    public String Response;
    public boolean WebProcessComplete = false;
    public String CallFunctionAfterResponseComplete = "";
    String chatRefID;
    String SessionID;
    String URL;
    Activity parentActivity;
    ProgressDialog progressDialog = null;
    Super_Library_AppClass SLAc;
    List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);


    public Super_Library_URLV2(String URL, List<NameValuePair> RequestValue, Activity parentActivity, String CallFunctionAfterResponseComplete) {
        nameValuePairs = RequestValue;
        this.CallFunctionAfterResponseComplete = CallFunctionAfterResponseComplete;
        SLAc = new Super_Library_AppClass(parentActivity);
        DownloadWebPageTask task = new DownloadWebPageTask();
        task.execute(new String[]{URL});


    }

    public Super_Library_URLV2(String URL, List<NameValuePair> RequestValue, Activity parentActivity) {
        nameValuePairs = RequestValue;
        SLAc = new Super_Library_AppClass(parentActivity);
        SLAc.SavePreference("RecordAddedStatus", "");
        this.parentActivity = parentActivity;
        DownloadWebPageTask task = new DownloadWebPageTask();
        task.execute(new String[]{URL});


    }


    private class DownloadWebPageTask extends AsyncTask<String, Context, String> {
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
                    HttpPost httpPost = new HttpPost(url);
                    try {
                        httpPost.setEntity(new UrlEncodedFormEntity(nameValuePairs));
                        HttpResponse execute = client.execute(httpPost);
                        InputStream content = execute.getEntity().getContent();
                        BufferedReader buffer = new BufferedReader(new InputStreamReader(content));
                        String s = "";
                        while ((s = buffer.readLine()) != null) {
                            response += s;
                        }

                    } catch (Exception e) {
                        Log.e("Error http:", e.toString());
                        e.printStackTrace();
                        return "-999";
                    }
                    Log.e("response", response);
                    return response;
                } catch (Exception ex) {
                    //  Toast.makeText(parentActivity, "Check your internet... Problem in connecting to the server", 1000).show();

                    Log.e("Error http:", ex.toString());
                    return "-999";
                }

            }


            return "";
        }

        @Override
        protected void onPostExecute(String result) {

            Log.e("SLR", "Result " + result);
            Response = result.trim();
            WebProcessComplete = true;

            if (CallFunctionAfterResponseComplete.equalsIgnoreCase("SetUserAccountAsPaid")) {
                if (Response.equals("1")) {
                    SLAc.SavePreference("PaidUser", "true");
                    SLAc.SavePreference("PaidUserServer", "true");

                } else {
                    //   SLAc.SavePreference("PaidUser", "false");
                    //  SLAc.SavePreference("PaidUserServer", "false");

                }
            } else if (CallFunctionAfterResponseComplete.equalsIgnoreCase("GetLoginDetails")) {
                if (!Response.equals("")) {
                    SLAc.SavePreference(".XXXXXXX", SLAc.GetValueByKey(".XXXXXXX", Response));
                    SLAc.SavePreference("session_name", SLAc.GetValueByKey("fname", Response));
                    if (SLAc.GetValueByKey("paiduser", Response).equals("1")) {
                        SLAc.SavePreference("PaidUser", "true");
                        SLAc.SavePreference("PaidUserServer", "true");
                    }
                    SLAc.ShowScreen("HomeScreen");
                    try {
                        parentActivity.finish();
                    } catch (Exception e) {

                    }
                } else {
                    SLAc.ShowToast("Access Denied, Try Again.." + Response);

                }
            } else if (CallFunctionAfterResponseComplete.equalsIgnoreCase("SetPaidAccount")) {
                SLAc.SavePreference("PaidUser", "true");
                SLAc.SavePreference("PaidUserServer", "true");
            } else if (CallFunctionAfterResponseComplete.equalsIgnoreCase("WSChatPHPGetShifaMember")) {
                SLAc.SaveWSChatPHPGetUserPvtPublicChat(Response, ".XXXXXXXapp_contact");
            } else if (CallFunctionAfterResponseComplete.equalsIgnoreCase("WSChatPHPGetUserPvtChat")) {
                SLAc.SaveWSChatPHPGetUserPvtPublicChat(Response, ".XXXXXXXapp_pvtchat");
            } else if (CallFunctionAfterResponseComplete.equalsIgnoreCase("WSChatPHPGetShifaPublicChat")) {
                SLAc.SaveWSChatPHPGetUserPvtPublicChat(Response, ".XXXXXXXapp_chat");
            } else if (CallFunctionAfterResponseComplete.equalsIgnoreCase("DoResponseActionForPvtPublicChatSend")) {
                SLAc.SaveWSChatPHPGetUserPvtPublicChat(Response, ".XXXXXXXapp_chat");
            }


        }
    }
}
