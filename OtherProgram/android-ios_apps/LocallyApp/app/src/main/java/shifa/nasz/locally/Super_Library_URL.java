package shifa.nasz.locally;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;


import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.util.Log;
import android.widget.Toast;

public class Super_Library_URL {
	 public String ChatTextSend = "";
	   
	   String chatRefID;
	   String SessionID;
	   String URL;
	   Activity parentActivity;
       ProgressDialog progressDialog = null;
            Super_Library_AppClass SLAc;
	   List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
       
		public Super_Library_URL(String URL,       List<NameValuePair> RequestValue , Activity parentActivity)
		{
			nameValuePairs  = RequestValue;
            SLAc = new Super_Library_AppClass(parentActivity);
            SLAc.SavePreference("RecordAddedStatus","");
            this.parentActivity = parentActivity;
            DownloadWebPageTask task = new DownloadWebPageTask();
            task.execute(new String[] { URL});


		
		}

    public Super_Library_URL(String ErrorMsg, Activity parentActivity)
    {
        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
        nameValuePairs.add(new BasicNameValuePair("Error", ErrorMsg));

        SLAc = new Super_Library_AppClass(parentActivity);
        this.parentActivity = parentActivity;
        DownloadWebPageTask task = new DownloadWebPageTask();
        task.execute(new String[] { "http://kent.nasz.us/mumbra/php/error.php"});



    }

	private class DownloadWebPageTask extends AsyncTask<String, Context, String> {
		protected Context ctx;
		@Override
	    protected String doInBackground(String... urls) {
			
		  Log.e("doInBackground","enter");
	      String response = "";
	      String uri = "";
	      for (String url : urls) {
	    	  uri = url;
	    	  Log.e("uri",uri);
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
                    Log.e("response",response);
			        return response;
			  }
	    	  catch(Exception ex)
	    	  {
                      Toast.makeText(parentActivity, "Check your internet... Problem in connecting to the server", 1000).show();

                  Log.e("Error http:", ex.toString());
	    		  return "-999";
	    	  }

	      }
	   
	 
	    return "";
	    }

	    @Override
	    protected void onPostExecute(String result) {

            Log.e("SLR","Result " + result);
            try{
                String query = result.replace("insert into mumbra", "insert into tbl_shifa");

            query = query.replace("update mumbra set", "update tbl_shifa set");

             if (result.indexOf("-Details-:") != -1){
                 Toast.makeText(parentActivity, result.substring(10,result.length()), 1000).show();
             }
             else if (result.toLowerCase().indexOf("insert into") != -1 || result.toLowerCase().indexOf("update tbl_shifa") != -1 || result.toLowerCase().indexOf("delete table") != -1) {
                DBclass db1 = new DBclass(parentActivity);
                 try{
                        if (result.indexOf("#_#") != -1){
                            db1.KentShifaInData(query);
                        }
                        else{
                            db1.DbQry(query);
                        }
                     db1.db.close();
                 }
                 catch(Exception ex){
                         Toast.makeText(parentActivity, "Connection problem.. ", 1000).show();

                 }


            }
            if (progressDialog != null){
                progressDialog.hide();
                progressDialog = null;
            }
            Log.e("SLR","query " + query);
          //  Toast.makeText(parentActivity, query, 1000).show();
            if (query.indexOf("insert")!=-1){

                SLAc.SavePreference("RecordAddedStatus","Added Successfully");

                parentActivity.finish();

            }
            }
            catch (Exception ex){
                Log.e("error in SuperLibrary URL ", ex.toString());
            }

	    }
	  }
}
