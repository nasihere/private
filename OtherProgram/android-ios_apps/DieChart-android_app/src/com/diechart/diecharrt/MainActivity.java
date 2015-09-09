package com.diechart.diecharrt;

import java.io.ByteArrayOutputStream;
import java.io.IOException;

import org.apache.http.HttpResponse;
import org.apache.http.HttpStatus;
import org.apache.http.StatusLine;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;

import com.diechart.database.DatabaseHandler;

import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.os.SystemClock;
import android.app.ActionBar;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.AvoidXfermode;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.Toast;

public class MainActivity extends BaseActivity implements OnClickListener {

	private ProgressDialog progress;
	final int totalProgressTime = 100;

	@Override
	protected void onDestroy() {
		// TODO Auto-generated method stub
		super.onDestroy();
	}

	@Override
	protected void onPause() {
		// TODO Auto-generated method stub
		super.onPause();
	}

	@Override
	protected void onResume() {
		// TODO Auto-generated method stub
		super.onResume();
	}

	@Override
	protected void onStart() {
		// TODO Auto-generated method stub
		super.onStart();
	}

	@Override
	protected void onStop() {
		// TODO Auto-generated method stub
		super.onStop();
	}

	private Button pull_but;
	private Button show_records;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		// setContentView(R.layout.activity_main);

		pull_but = (Button) findViewById(R.id.button1);
		show_records = (Button) findViewById(R.id.button2);
		show_records.setOnClickListener(this);

		pull_but.setOnClickListener(this);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return false;
	}

	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		switch (v.getId()) {
		case R.id.button1:
			if (isNetworkAvailable(this)) {
				progress = new ProgressDialog(this);
				progress.setMessage("Downloading Records  ");
				progress.setProgressStyle(ProgressDialog.STYLE_HORIZONTAL);
				progress.setIndeterminate(false);
				progress.setCanceledOnTouchOutside(false);
				

				new pullJson()
						.execute("http://XXXXXXX/api/Server");

			} else {
				Toast.makeText(getApplicationContext(),
						"Network not available to perform this task!",
						Toast.LENGTH_LONG).show();
				return;
			}

			break;
		case R.id.button2:
			showRecord();
			break;

		default:
			break;
		}

	}

	private void showRecord() {
		// TODO Auto-generated method stub

		Intent intent = new Intent(MainActivity.this, ShowResultActivity.class);
		intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);

		intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
		startActivity(intent);
		// finish();
		overridePendingTransition(android.R.anim.fade_in,
				android.R.anim.fade_out);

	}

	class pullJson extends AsyncTask<String, Integer, String> {

		private int myProgressCount;

		@Override
		protected void onPreExecute() {
			// TODO Auto-generated method stub
			// super.onPreExecute();
			progress.setProgress(0);
			myProgressCount = 0;
			progress.show();

		}

		@Override
		protected void onProgressUpdate(final Integer... values) {
			// TODO Auto-generated method stub
			if (progress != null) {
				new Handler().post(new Runnable() {

					@Override
					public void run() {
						// TODO Auto-generated method stub
						progress.setProgress(values[0]);

					}
				});
				// progress.setProgress(values[0]);
			}
		}

		@Override
		protected String doInBackground(String... uri) {

			if (progress != null) {
				while (myProgressCount < 100) {
					myProgressCount++;

					publishProgress(myProgressCount);
					SystemClock.sleep(30);
				}
			}
			HttpClient httpclient = new DefaultHttpClient();
			HttpResponse response;
			String responseString = null;
			try {
				response = httpclient.execute(new HttpGet(uri[0]));
				StatusLine statusLine = response.getStatusLine();
				if (statusLine.getStatusCode() == HttpStatus.SC_OK) {
					ByteArrayOutputStream out = new ByteArrayOutputStream();
					response.getEntity().writeTo(out);
					out.close();
					responseString = out.toString();
				} else {
					// Closes the connection.
					response.getEntity().getContent().close();
					throw new IOException(statusLine.getReasonPhrase());
				}
			} catch (ClientProtocolException e) {
				// TODO Handle problems..
			} catch (IOException e) {
				// TODO Handle problems..
			}
			return responseString;
		}

		@Override
		protected void onPostExecute(String result) {
			// super.onPostExecute(result);
			// Do anything with response..
			// System.out.println(result);
			if (progress != null) {
				progress.dismiss();
				storeData(result);
			}

		}
	}

	public void storeData(String result) {
		// TODO Auto-generated method stub
		DatabaseHandler db = new DatabaseHandler(getApplicationContext());
		if (db.insertRecords(result)) {
			Toast.makeText(getApplicationContext(), "Update success!",
					Toast.LENGTH_LONG).show();
		} else {
			Toast.makeText(getApplicationContext(), "Update failed!",
					Toast.LENGTH_LONG).show();
		}

	}

	public static boolean isNetworkAvailable(Context context) {
		ConnectivityManager connectivity = (ConnectivityManager) context
				.getSystemService(Context.CONNECTIVITY_SERVICE);

		if (connectivity != null) {
			NetworkInfo[] info = connectivity.getAllNetworkInfo();

			if (info != null) {
				for (int i = 0; i < info.length; i++) {
					Log.i("Class", info[i].getState().toString());
					if (info[i].getState() == NetworkInfo.State.CONNECTED) {
						return true;
					}
				}
			}
		}
		return false;
	}

}
