package com.shifa.employee.logger;


import android.app.AlertDialog;
import android.app.ListActivity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemLongClickListener;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;

import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;

public class activity_member_listactivity extends ListActivity {
	Handler mHandler;
	Context ctx;
	String CurrentPing = "";
	public String SessionID = "";

	public class MyCustomAdapter extends ArrayAdapter<String> {

		public MyCustomAdapter(Context context, int textViewResourceId,
				String[] objects) {
			super(context, textViewResourceId, objects);
			// TODO Auto-generated constructor stub
		}

		@Override
		public View getView(int position, View convertView, ViewGroup parent) {
			// TODO Auto-generated method stub
			//return super.getView(position, convertView, parent);

			View row = convertView;

			if(row==null){
				LayoutInflater inflater=getLayoutInflater();
				row=inflater.inflate(R.layout.activity_lst_members, parent, false);
			}





			String[] data = month[position].split("@#@");
			Log.e("data232","1");
			ImageView imageView = (ImageView) row.findViewById(R.id.act_lst_mem_status_img);
			Log.e("data65","1");
			imageView.setImageResource(data[3].equals("0") ?  R.drawable.ic_home_icon_online: R.drawable.ic_offline );
			Log.e("data86","1");

			new DownloadImageTask((ImageView) (ImageView) row.findViewById(R.id.act_lst_mem_img))
			.execute(data[5]);
			Log.e("data451","1");

			TextView TextViewName = (TextView) row.findViewById(R.id.act_lst_mem_name);
			TextViewName.setText(data[4]);
			Log.e("data451","1");
			TextView TextViewStatus = (TextView) row.findViewById(R.id.act_lst_mem_status);
			TextViewStatus.setText(data[3].equals("0") ? "has signed in" : "has signed out");
			Log.e("data453","1");




			return row;
		}
	}
	private class DownloadImageTask extends AsyncTask<String, Void, Bitmap> {
		ImageView bmImage;

		public DownloadImageTask(ImageView bmImage) {
			this.bmImage = bmImage;
		}

		protected Bitmap doInBackground(String... urls) {
			String urldisplay = urls[0];
			Bitmap mIcon11 = null;
			try {
				InputStream in = new java.net.URL(urldisplay).openStream();
				mIcon11 = BitmapFactory.decodeStream(in);
			} catch (Exception e) {
				Log.e("Error", e.getMessage());
				e.printStackTrace();
			}
			return mIcon11;
		}

		protected void onPostExecute(Bitmap result) {
			bmImage.setImageBitmap(result);
		}
	}
	String[] month = {
			"January", "February", "March", "April",
			"May", "June", "July", "August",
			"September", "October", "November", "December"
	};
	class MemberDetail
	{
		String action,keycode,Name,ImageURL;
		
		public MemberDetail(String memdet)
		{
			String[] data = memdet.split("@#@");
			action = data[3];
			keycode = data[2];
			Name = data[4];
			ImageURL = data[5];
			
		}
	}
	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		ctx = this;
		SessionID = LoggedIn();
		
		//setContentView(R.layout.main);
		/*setListAdapter(new ArrayAdapter<String>(this,
       R.layout.row, R.id.weekofday, DayOfWeek));*/
		DownloadData();

		getListView().setOnItemLongClickListener(new OnItemLongClickListener(){
		
		@Override
		public boolean onItemLongClick(AdapterView<?> arg0, View arg1,
				int arg2, long arg3) {
			
			 return true;
			 
			 
		}
		}
		);
	}
	private void openURL(String URL)
	{
		
		  Intent intent = new Intent(activity_member_listactivity.this, activity_events.class);
		  intent.putExtra("url", URL);
		  startActivity(intent);
		  
	}
	private String LoggedIn()
	{

		SharedPreferences prefs = getSharedPreferences("AppNameSettings",0); 
		String restoredText = prefs.getString("session_id", null);
		if (restoredText != null) 
		{
			return restoredText;
		}
		return "";

	}
	private void DownloadData()
	{



		mHandler = new Handler();
		new Thread(new Runnable() {
			@Override
			public void run() {
				// TODO Auto-generated method stub

				try {
					mHandler.post(new Runnable() {

						@Override
						public void run() {

							DownloadWebPageTask task = new DownloadWebPageTask();
							task.execute(new String[] { "http://kent.nasz.us/elog_php/manage.php?session_id=" + SessionID});
							return;
						}
					});
					Thread.sleep(3000);

				} catch (Exception e) {
					//     	Toast.makeText(getApplicationContext(), e.toString(), 1000).show();
					// TODO: handle exception
				}


			}
		}).start();


	}

	private class DownloadWebPageTask extends AsyncTask<String, Context, String> {
		protected Context ctx;
		@Override
		protected String doInBackground(String... urls) {
			String response = "";
			String uri = "";
			for (String url : urls) {
				uri = url;
				try {
					Log.e("URL",url);
					DefaultHttpClient client = new DefaultHttpClient();
					HttpGet httpGet = new HttpGet(url);
					try {
						HttpResponse execute = client.execute(httpGet);
						InputStream content = execute.getEntity().getContent();

						BufferedReader buffer = new BufferedReader(new InputStreamReader(content));
						String s = "";
						while ((s = buffer.readLine()) != null) {
							response += s;
						}

					} catch (Exception e) {
						e.printStackTrace();
						return "-999";
					}
				}
				catch(Exception ex)
				{
					return "-999";
				}
			}
			Log.e("Response data background",response);

			return response;
		}

		@Override
		protected void onPostExecute(String result) {
			//progressDialog.dismiss();
			if (result.equals("-999")) return;
			if (result.trim().equals("9001"))
			{
				RestartWindow();
				return;
			}
			else
			{
				try
				{
					int len = result.split("-#@#@#@-").length - 1;
					int i = 0;
					String[] s = new String[len];
					for(String s1 : result.split("-#@#@#@-"))
					{
						s[i] = s1;
						i++;
						if (i == len) break;
					}
					month = s;
					//Response = result;
					setListAdapter(new MyCustomAdapter(activity_member_listactivity.this, R.layout.activity_lst_members, month));
	
				}
				catch(Exception ex)
				{
					Log.e("Ex-",ex.toString());
				}
			}
		}
	}
	public void RestartWindow()
	{
		finish();
		Intent intent = new Intent(activity_member_listactivity.this, activity_member_listactivity.class);
		startActivity(intent);
	}
	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		// TODO Auto-generated method stub
		//super.onListItemClick(l, v, position, id);
		String selection = l.getItemAtPosition(position).toString();
		//Toast.makeText(this, selection, Toast.LENGTH_LONG).show();
		//String selection = arg0.getItemAtPosition(arg2).toString();
		   
		final MemberDetail md = new MemberDetail(selection);
		CharSequence SearchArray[] = new CharSequence[] {"View Log", "Delete Member", "Custom Report"};

		AlertDialog.Builder builder = new AlertDialog.Builder(ctx);
		builder.setTitle("Activity");
		builder.setItems(SearchArray, new DialogInterface.OnClickListener() {
		    @Override
		    public void onClick(DialogInterface dialog, int which) {
		        // the user clicked on colors[which]
		    	if (which == 0)
		    	{
		    		openURL("http://kent.nasz.us/elog_php/managesessiondatewisereport.php?session_id="+SessionID + "&keycode="+md.keycode);
	        		
		    	}
		    	else if (which == 1)
		    	{
		    		
		    		new AlertDialog.Builder(ctx)
                	 .setTitle("Delete entry")
                    .setMessage("Are you sure you want to delete " + md.Name +" logs?")
                    .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int which) { 
                            // continue with delete
                        	DownloadWebPageTask task = new DownloadWebPageTask();
                            task.execute(new String[] { "http://kent.nasz.us/elog_php/managesessionkeycodedelete.php?session_id="+SessionID + "&keycode="+md.keycode});
	        				  
                        }
                     })
                    .setNegativeButton(android.R.string.no, new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int which) { 
                            // do nothing
                        	
                        }
                     })
                    .setIcon(R.drawable.ic_man)
                     .show();
		    		
		    		
		    	}
		    	else if (which == 2)
		    	{
		    		Intent intent = new Intent(activity_member_listactivity.this, custom_report.class);
		    		startActivity(intent);
		      		finish();	            
		    	}	
			            
		    }
		});
		builder.show();
	}
	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		   if (keyCode == KeyEvent.KEYCODE_BACK) {
			   finish();
			   
		        return true;
		    }
	    return super.onKeyDown(keyCode, event);
	}	
	

	
}