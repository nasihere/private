package shifa.nasz.locally;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.KeyEvent;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

public class activity_settings extends Activity {
	private ProgressDialog progress;
	
	ProgressDialog pd;
	String source = "";
	int iServerCounter = 0;
	boolean DownloadingData = false;
	Context ctx;
    String whereClause = "";
    String ColsSQL = "";
    private static String TAG = "Settings";
    String[][] sHistory = new String[100][3];
	Super_Library_AppClass SLAc;
	public String SessionID = "";
    List<NameValuePair> nameValuePairs;

	@Override
	protected void onCreate(Bundle savedInstanceState) {

		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_settings);
		ctx = this;
		SLAc = new Super_Library_AppClass(this);
		SessionID = SLAc.RestoreSessionIndexID("session_id");



        Bundle extras = getIntent().getExtras();

        if (extras != null) {
            whereClause = extras.getString("where");
            ColsSQL = extras.getString("cols");
        }

        final Handler myHandler = new Handler();
        progress = new ProgressDialog(ctx);
        progress.setMessage("Downloading Data ");
        progress.setIndeterminate(false);
        progress.setCanceledOnTouchOutside(false);
        progress.show();
        (new Thread(new Runnable() {

            @Override
            public void run() {
                try {
                    Thread.sleep(10);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }

                nameValuePairs = new ArrayList<NameValuePair>(2);
                nameValuePairs.add(new BasicNameValuePair("where", whereClause));
                nameValuePairs.add(new BasicNameValuePair("cols", ColsSQL));

                new pullJson()
                        .execute("http://kent.nasz.us/mumbra/php/fetch.php");

            }
        })).start();






	}


	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			switch (keyCode) {
			case KeyEvent.KEYCODE_BACK:

				Intent intent = new Intent(activity_settings.this,
						activity_tabviewer.class);
				startActivity(intent);
				finish();

				return true;
			}

		}
		return super.onKeyDown(keyCode, event);

	}


    private void openMumbra(){
        if (progress != null) {
            progress.dismiss();
            progress = null;

        }
        Intent intent = new Intent(activity_settings.this, activity_tabviewer.class);
        startActivity(intent);
        finish();
    }



















	////////////////////////////////////////////////////////////////////Class Start ///////////////////////////////////////////////////////////
	class pullJson extends AsyncTask<String, Integer, String> {

		private int myProgressCount;

		@Override
		protected void onPreExecute() {
			// TODO Auto-generated method stub
			// super.onPreExecute();
			DownloadingData = true;

		}

		@Override
		protected void onProgressUpdate(final Integer... values) {
			// TODO Auto-generated method stub

		}

		@Override
		protected String doInBackground(String... uri) {

            DBclass db1 = new DBclass(ctx);


            String responseString = "";
            Log.e("URL ",uri[0]);
            HttpClient httpclient = new DefaultHttpClient();
			HttpResponse response= null;
			try {
                DefaultHttpClient client = new DefaultHttpClient();
                HttpPost httpPost = new HttpPost(uri[0]);
                try {
                    httpPost.setEntity(new UrlEncodedFormEntity(nameValuePairs));
                    HttpResponse execute = client.execute(httpPost);
                    InputStream content = execute.getEntity().getContent();
                    BufferedReader buffer = new BufferedReader(new InputStreamReader(content));
                    String s = "";
                    String lastItemQuery ="";
                    while ((s = buffer.readLine()) != null) {
                        s = lastItemQuery + s;
                        //Example: S string has "insert .... #_#Insert .... #_#"
                        // iEndHash will return assume 11 and length is 10
                        int iEndHash = s.lastIndexOf("#_#") + 3;
                    //    System.out.println("iEndHasH " + iEndHash);
                        if (iEndHash < s.length()) {
                            lastItemQuery = s.substring(iEndHash,s.length());
                        }
                        else{
                            lastItemQuery = "";
                        }
                        s = s.substring(0,iEndHash);
                        db1.KentShifaInData(s);

                        //responseString += s;

                    }
                    if (db1 != null) {
                        db1.db.close();
                    }

                } catch (Exception e) {
                    Log.e("Error http:", e.toString());
                    e.printStackTrace();
                    openMumbra();
                    return "-999";
                }
                Log.e("Setting","Level 3 " + responseString );

                return responseString;
            }
            catch(Exception ex)
            {
                Log.e("Error http:", ex.toString());
                openMumbra();
                return "-999";
            }


		}

		@Override
		protected void onPostExecute(String result) {

			// super.onPostExecute(result);
			// Do anything with response..
			// System.out.println(result);

		/*	Log.e("Setting","Level 4 " );
			if (result.indexOf("insert")==-1)
			{
			        finish();
            	return;
				
			}
			//if (progress != null) {
				//	Log.e("Setting","Level 5 " );
			
					//Log.e("Setting","Level 6 " );
					
				storeData(result);
				//Log.e("Setting","Level 7 " );
				DownloadingData = false;	
			//}

		}
		////////////////////////////////////////////////Class End
		
		
		
///////////////////////////////////////////////////////// Call when api server respond data		
		public void storeData(String result) {

          //  DBclass db1 = new DBclass(ctx);
         //   db1.KentShifaInData(result);

*/
            openMumbra();


			 

		}		
		////////////////////////////////////////////////////////////////
		
		
		
		
		
		
	}
	

	

	

	
	
	
	
	
	
	
	
	
	
	
}
